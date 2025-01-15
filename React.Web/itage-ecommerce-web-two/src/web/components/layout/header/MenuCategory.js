import React, {
	Fragment,
	useContext,
	useEffect,
	Component,
	useState,
} from "react";
import { Container, Row, Col, Media } from "reactstrap";
import { useSelector, useDispatch } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import Config from "../../../../helpers/Config";
import { MakeApiCallAsync } from "../../../../helpers/ApiHelpers";
import {
	getLanguageCodeFromSession,
	GetLocalizationControlsJsonDataForScreen,
	replaceLoclizationLabel,
} from "../../../../helpers/CommonHelper";
import GlobalEnums from "../../../../helpers/GlobalEnums";
import {
	makeAnyStringLengthShort,
	makeProductShortDescription,
	replaceWhiteSpacesWithDashSymbolInUrl,
} from "../../../../helpers/ConversionHelper";
import rootAction from "../../../../stateManagment/actions/rootAction";

const MenuCategory = () => {
	const dispatch = useDispatch();
	const [showState, setShowState] = useState(false);
	const [showSidebar, setShowSidebar] = useState(false);

	const { t } = useTranslation();
	let leftMenuState = useSelector(
		(state) => state.commonReducer.isLeftMenuSet
	);
	const setLeftMenuManual = (value) => {
		dispatch(rootAction.commonAction.setLeftMenu(value));
	};

	const [PopularCategoriesList, setPopularCategories] = useState([]);
	const [langCode, setLangCode] = useState("");
	const [LocalizationLabelsArray, setLocalizationLabelsArray] = useState([]);
	const [adminPanelBaseURL, setBaseUrl] = useState(Config["ADMIN_BASE_URL"]);

	const forceLoadCategory = (url) => {
		window.location.href = url;
	};

	const handleOverlayClick = (e) => {
		if (e.target.className === "sidebar-overlay active") {
			setShowSidebar(false);
		}
	};

	useEffect(() => {
		// declare the data fetching function
		const getPopularCategories = async () => {
			//--Get language code
			let lnCode = getLanguageCodeFromSession();
			await setLangCode(lnCode);

			const headers = {
				// customerid: userData?.UserID,
				// customeremail: userData.EmailAddress,
				Accept: "application/json",
				"Content-Type": "application/json",
			};

			const param = {
				requestParameters: {
					PageNo: 1,
					PageSize: 30,
					recordValueJson: "[]",
				},
			};

			const response = await MakeApiCallAsync(
				Config.END_POINT_NAMES["GET_POPULAR_CATEGORIES"],
				null,
				param,
				headers,
				"POST",
				true
			);
			if (response != null && response.data != null) {
				setPopularCategories(JSON.parse(response.data.data));
			}

			//-- Get website localization data
			let arryRespLocalization =
				await GetLocalizationControlsJsonDataForScreen(
					GlobalEnums.Entities["PopularCategories"],
					null
				);
			if (
				arryRespLocalization != null &&
				arryRespLocalization != undefined &&
				arryRespLocalization.length > 0
			) {
				await setLocalizationLabelsArray(arryRespLocalization);
			}
		};

		// call the function
		getPopularCategories().catch(console.error);
	}, []);

	return (
		<>
			<div className="nav-block" onClick={() => setShowState(!showState)}>
				<div className="nav-left">
					<nav
						className="navbar"
						data-toggle="collapse"
						data-target="#navbarToggleExternalContent"
					>
						<button
							className="navbar-toggler"
							type="button"
							onClick={() => setShowState(!showState)}
						>
							<span className="navbar-icon ">
								<i
									class="fa fa-bars sidebar-bar"
									style={{ fontSize: "1.5em" }}
									onClick={() => setShowSidebar(true)}
								></i>
							</span>
						</button>
					</nav>
					<div
						className={`collapse  nav-desk ${
							showState ? "show" : ""
						}`}
						id="navbarToggleExternalContent"
					>
						<a
							href="#"
							onClick={() => {
								setLeftMenuManual(!leftMenuState);
								document.body.style.overflow = "visible";
							}}
							className={`overlay-cat ${
								leftMenuState ? "showoverlay" : ""
							}`}
						></a>

						<div
							className={`sidebar-overlay ${
								showSidebar ? "active" : ""
							}`}
							onClick={handleOverlayClick}
						>
							<div
								className={`sidebar ${
									showSidebar ? "open" : ""
								}`}
							>
								<button
									type="button"
									className="btn-close-custom"
									aria-label="Close"
									onClick={() => setShowSidebar(false)}
								>
									&times;
								</button>
								<ul
									className={`nav-cat title-font ${
										leftMenuState ? "openmenu" : ""
									}`}
								>
									<li
										className="back-btn"
										onClick={() => {
											setLeftMenuManual(false);
											document.body.style.overflow =
												"visible";
										}}
									>
										{/* <a>
											<i className="fa fa-angle-left"></i>
											{t("back")}
										</a> */}
									</li>

									{PopularCategoriesList.map((item, i) => (
										<li className="list-group-item" key={i}>
											<a
												href={`/${getLanguageCodeFromSession()}/all-products/${
													item.CategoryID ?? 0
												}/${item.Name}`}
												onClick={() => {
													forceLoadCategory(
														`/${getLanguageCodeFromSession()}/all-products/${
															item.CategoryID ?? 0
														}/${item.Name}`
													);
												}}
											>
												<img
													src={
														adminPanelBaseURL +
														item.AttachmentURL
													}
													alt={item.Name}
													style={{
														width: "30px",
														height: "30px",
														marginRight: "10px",
													}}
												/>
												{item.Name}
											</a>
										</li>
									))}
								</ul>
							</div>
						</div>
					</div>
				</div>
			</div>

			<style>{`
                .sidebar-overlay {
									position: fixed;
									top: 0;
									left: 0;
									width: 100%;
									height: 100%;
									background: rgba(0, 0, 0, 0.6);
									z-index: 1049;
									display: none;
									transition: opacity 0.3s ease-in-out;
							}
							.sidebar-overlay.active {
									display: block;
									opacity: 1;
							}

							/* Sidebar Container */
							.sidebar {
									position: fixed;
									top: 0;
									left: -300px;
									width: 300px;
									height: 100%;
									background: #ffffff; /* Nền trắng */
									box-shadow: 3px 0 10px rgba(0, 0, 0, 0.2);
									overflow-y: auto;
									transition: left 0.3s ease-in-out;
									z-index: 1050;
									padding: 20px;
									display: flex;
									flex-direction: column;
							}
							.sidebar.open {
									left: 0;
							}

							/* Close Button */
							.btn-close-custom {
									position: absolute;
									top: 10px;
									right: 10px;
									width: 40px;
									height: 40px;
									background-color: #f0f0f0;
									color: #333;
									font-size: 1.5rem;
									display: flex;
									justify-content: center;
									align-items: center;
									border: none;
									border-radius: 50%;
									cursor: pointer;
									transition: background-color 0.3s ease;
							}
							.btn-close-custom:hover {
									background-color: #e0e0e0;
							}

							/* Menu List */
							.nav-cat {
									list-style: none;
									padding: 0;
							}
							.nav-cat li {
									margin-bottom: 15px;
							}
							.nav-cat li a {
								width: 100%;
								display: flex;
								align-items: center;
								text-decoration: none;
								color: #333;
								font-size: 1rem;
								padding: 10px 15px;
								border-radius: 5px;
								transition: background-color 0.3s ease, color 0.3s ease;
							}
							.nav-cat li a:hover {
									background-color: #f5f5f5;
									color: #007bff;
							}
							.nav-cat li a img {
									width: 30px;
									height: 30px;
									margin-right: 10px;
									border-radius: 5px;
							}

							/* Scrollable Sidebar */
							.sidebar::-webkit-scrollbar {
									width: 8px;
							}
							.sidebar::-webkit-scrollbar-thumb {
									background: #ccc;
									border-radius: 4px;
							}
							.sidebar::-webkit-scrollbar-thumb:hover {
									background: #aaa;
							}
            `}</style>
		</>
	);
};

export default MenuCategory;
