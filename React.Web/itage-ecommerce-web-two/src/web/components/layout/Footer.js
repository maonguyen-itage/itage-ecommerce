import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Row, Col, Container, Media } from "reactstrap";
import logoImage from "../../../resources/custom/images/ITage_Shop_logo.png";
import SubscribeNewsLetter from "../shared/SubscribeNewsLetter";
import { useSelector, useDispatch } from "react-redux";
import Config from "../../../helpers/Config";
import { MakeApiCallAsync } from "../../../helpers/ApiHelpers";
import { checkIfStringIsEmtpy } from "../../../helpers/ValidationHelper";
import rootAction from "../../../stateManagment/actions/rootAction";
import { LOADER_DURATION } from "../../../helpers/Constants";
import {
	getLanguageCodeFromSession,
	GetLocalizationControlsJsonDataForScreen,
	replaceLoclizationLabel,
} from "../../../helpers/CommonHelper";
import GlobalEnums from "../../../helpers/GlobalEnums";
import { useTranslation } from "react-i18next";
import { LANGUAGES } from "../../../constants";

const Footer = ({ layoutLogo }) => {
	const { i18n, t } = useTranslation();

	const dispatch = useDispatch();
	const [paymentMethods, setPaymentMethods] = useState([]);
	const [adminPanelBaseURL, setadminPanelBaseURL] = useState(
		Config["ADMIN_BASE_URL"]
	);
	const [LocalizationLabelsArray, setLocalizationLabelsArray] = useState([]);
	const [LogoImageFromStorage, setLogoImageFromStorage] = useState(
		useSelector((state) => state.commonReducer.websiteLogoInLocalStorage)
	);

	const onChangeLang = (e) => {
		const lang_code = e.target.value;
		i18n.changeLanguage(lang_code);
	};

	useEffect(() => {
		// declare the data fetching function
		const DataOperationFunc = async () => {
			const headers = {
				Accept: "application/json",
				"Content-Type": "application/json",
			};

			const param = {
				requestParameters: {
					recordValueJson: "[]",
				},
			};

			//--Get payment methods
			const responsePaymentMethods = await MakeApiCallAsync(
				Config.END_POINT_NAMES["GET_PAYMENT_METHODS"],
				null,
				param,
				headers,
				"POST",
				true
			);
			if (
				responsePaymentMethods != null &&
				responsePaymentMethods.data != null
			) {
				await setPaymentMethods(
					JSON.parse(responsePaymentMethods.data.data)
				);
			}

			//--Get Website Logo
			if (!checkIfStringIsEmtpy(LogoImageFromStorage)) {
				let paramLogo = {
					requestParameters: {
						recordValueJson: "[]",
					},
				};

				let WebsiteLogoInLocalStorage = "";
				let logoResponse = await MakeApiCallAsync(
					Config.END_POINT_NAMES["GET_WEBSITE_LOGO"],
					null,
					paramLogo,
					headers,
					"POST",
					true
				);
				if (logoResponse != null && logoResponse.data != null) {
					console.log(logoResponse.data);

					if (logoResponse.data.data != "") {
						let logoData = JSON.parse(logoResponse.data.data);
						WebsiteLogoInLocalStorage = logoData[0].AppConfigValue;
						dispatch(
							rootAction.commonAction.setWebsiteLogo(
								WebsiteLogoInLocalStorage
							)
						);
						setLogoImageFromStorage(WebsiteLogoInLocalStorage);
					}
				}
			}
		};

		//--start loader
		dispatch(rootAction.commonAction.setLoading(true));

		// call the function
		DataOperationFunc().catch(console.error);

		//--stop loader
		setTimeout(() => {
			dispatch(rootAction.commonAction.setLoading(false));
		}, LOADER_DURATION);
	}, []);

	useEffect(() => {
		// declare the data fetching function
		const dataOperationFunc = async () => {
			//-- Get website localization data
			let arryRespLocalization =
				await GetLocalizationControlsJsonDataForScreen(
					GlobalEnums.Entities["Footer"],
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
		dataOperationFunc().catch(console.error);
	}, []);

	return (
		<footer className="footer-2 border-top mt-5">
			<Container>
				<Row className="row">
					<Col xs="12">
						<div className="footer-main-contian">
							<Row>
								<Col lg="4" md="12">
									<div className="footer-left">
										<div className="footer-logo">
											<Media
												src={logoImage}
												className="img-fluid  "
												alt="logo"
											/>
										</div>
										<div className="footer-detail">
											<p>
												ITage Shop is a powerful
												Multi-Vendor eCommerce Web
												Application that combines the
												robustness of ASP MVC .NET 8.0
												with the interactive user
												interface of React Js. The
												application offers a
												comprehensive Admin Panel
												providing advanced management
												capabilities.
											</p>
											<ul className="paymant-bottom">
												{paymentMethods?.map(
													(item, idx) => (
														<li
															key={
																item.PaymentMethodId
															}
														>
															<a href="#">
																<Media
																	src={
																		adminPanelBaseURL +
																		item.ImageUrl
																	}
																	className="img-fluid"
																	alt="pay"
																/>
															</a>
														</li>
													)
												)}
											</ul>
										</div>
									</div>
								</Col>
								<Col lg="8" md="12">
									<div className="footer-right">
										<Row className="row">
											<Col md="12">
												<SubscribeNewsLetter />
											</Col>
											<Col md="12">
												<div className="account-right">
													<div className="row">
														<div className="col-md-4">
															<div className="footer-box">
																<div className="footer-title">
																	<h5>
																		{/* {LocalizationLabelsArray.length >
																		0
																			? replaceLoclizationLabel(
																					LocalizationLabelsArray,
																					"my account",
																					"lbl_footr_myaccount"
																			  )
																			: "my account"} */}
																		{t(
																			"my_account"
																		)}
																	</h5>
																</div>
																<div className="footer-contant">
																	<ul>
																		<li>
																			<Link
																				to={`/${getLanguageCodeFromSession()}/login`}
																			>
																				{/* {LocalizationLabelsArray.length >
																				0
																					? replaceLoclizationLabel(
																							LocalizationLabelsArray,
																							"Login",
																							"lbl_footr_login"
																					  )
																					: "Login"} */}
																				{t(
																					"login"
																				)}
																			</Link>
																		</li>
																		<li>
																			<Link
																				to={`/${getLanguageCodeFromSession()}/about`}
																				id="lbl_footr_about"
																			>
																				{t(
																					"about_us"
																				)}
																			</Link>
																		</li>
																		<li>
																			<Link
																				to={`/${getLanguageCodeFromSession()}/contact-us`}
																				id="lbl_footr_cont"
																			>
																				{t(
																					"contact_us"
																				)}
																			</Link>
																		</li>

																		<li>
																			<Link
																				to={`/${getLanguageCodeFromSession()}/signup`}
																			>
																				{t(
																					"create_an_account"
																				)}
																			</Link>
																		</li>
																		<li>
																			<Link
																				to={`/${getLanguageCodeFromSession()}/become-seller`}
																			>
																				{t(
																					"become_vendor"
																				)}
																			</Link>
																		</li>
																	</ul>
																</div>
															</div>
														</div>
														<div className="col-md-3">
															<div className="footer-box">
																<div className="footer-title">
																	<h5>
																		{t(
																			"quick_links"
																		)}
																	</h5>
																</div>
																<div className="footer-contant">
																	<ul>
																		<li>
																			<Link
																				to={`/${getLanguageCodeFromSession()}/cart`}
																			>
																				{t(
																					"cart"
																				)}
																			</Link>
																		</li>
																		<li>
																			<Link
																				to={`/${getLanguageCodeFromSession()}/faq`}
																				id="lbl_footr_faq"
																			>
																				{LocalizationLabelsArray.length >
																				0
																					? replaceLoclizationLabel(
																							LocalizationLabelsArray,
																							" FAQ",
																							"lbl_footr_faq"
																					  )
																					: " FAQ"}
																			</Link>
																		</li>
																		<li>
																			<Link
																				to={`/${getLanguageCodeFromSession()}/`}
																				id="lbl_footr_home"
																			>
																				{t(
																					"home"
																				)}
																			</Link>
																		</li>
																		<li>
																			<Link
																				to={`/${getLanguageCodeFromSession()}/compare`}
																			>
																				{t(
																					"compare"
																				)}
																			</Link>
																		</li>
																		<li>
																			<Link
																				to={`/${getLanguageCodeFromSession()}/all-products/0/all-categories`}
																			>
																				{t(
																					"all_products"
																				)}
																			</Link>
																		</li>
																	</ul>
																</div>
															</div>
														</div>
														<div className="col-md-5">
															<div className="footer-box footer-contact-box">
																<div className="footer-title">
																	<h5>
																		{t(
																			"contact_us"
																		)}
																	</h5>
																</div>
																<div className="footer-contant">
																	<ul className="contact-list">
																		<li>
																			<i className="fa fa-map-marker"></i>
																			<span>
																				ITage
																				demo
																				store{" "}
																				<br />{" "}
																				<span>
																					{" "}
																					pak-19800
																				</span>
																			</span>
																		</li>
																		<li>
																			<i className="fa fa-phone"></i>
																			<span>
																				Call
																				us:
																				052-269-3101
																			</span>
																		</li>
																		<li>
																			<i className="fa fa-envelope-o"></i>
																			<span>
																				{t(
																					"email"
																				)}{" "}
																				:
																				nguyen.van.mao@itage.co.jp
																			</span>
																		</li>
																		<li>
																			<i className="fa fa-fax"></i>
																			<span>
																				fax
																				123456
																			</span>
																		</li>
																	</ul>
																</div>
															</div>
														</div>
													</div>
												</div>
											</Col>
										</Row>
									</div>
								</Col>
							</Row>
						</div>
					</Col>
				</Row>
			</Container>
			<div className="app-link-block  bg-transparent">
				<Container>
					<Row>
						<div className="app-link-bloc-contain app-link-bloc-contain-1">
							<div className="app-item-group ">
								<div className="social-block">
									<h6>{t("follow_us")}</h6>
									<ul className="social">
										<li>
											<Link
												to="https://www.facebook.com/mao.nguyen89"
												target="_blank"
											>
												<i className="fa fa-facebook"></i>
											</Link>
										</li>
										<li>
											<Link
												to="https://www.linkedin.com/in/marvin-nguyen%E2%9C%85-8a95a952/"
												target="_blank"
											>
												<i className="fa fa-linkedin"></i>
											</Link>
										</li>
										<li>
											<Link to="#" target="_blank">
												<i className="fa fa-youtube-play"></i>
											</Link>
										</li>
									</ul>
									<div className="footer-box">
										<div className="footer-contant">
											<ul>
												<li>リンク</li>
												<li>
													特定商取引法に基づく表記
												</li>
												<li>プライバシーポリシー</li>
												<li>利用規約</li>
												<li>
													<Link
														to={`/${getLanguageCodeFromSession()}/contact-us`}
														id="lbl_thead_contct"
														className="dark-menu-item border-end has-submenu"
													>
														{t("contact")}
													</Link>
												</li>
											</ul>
										</div>
									</div>
									<div className="footer-box footer-contact-box">
										<div className="footer-contant">
											<ul className="contact-list">
												<li>
													<Link
														to={`/${getLanguageCodeFromSession()}/contact-us`}
														id="lbl_thead_contct"
														className="dark-menu-item border-end has-submenu"
													>
														{t("contact")}
													</Link>
												</li>
												<li>
													<Link
														to={`/${getLanguageCodeFromSession()}/about`}
														id="lbl_footr_about"
													>
														{t("about_us")}
													</Link>
												</li>
											</ul>
										</div>
									</div>
								</div>
							</div>
						</div>
					</Row>
				</Container>
			</div>
			<div className="sub-footer">
				<Container>
					<Row>
						<Col xs="12">
							<div className="sub-footer-contain">
								<p>
									<span>2024 - 25 </span>
									{t("copy_right_by")}
								</p>
							</div>
						</Col>
					</Row>
				</Container>
			</div>
		</footer>
	);
};

export default Footer;
