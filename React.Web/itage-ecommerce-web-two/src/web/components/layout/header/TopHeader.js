import React, { useContext, useEffect, useState, Component } from "react";
import { Row, Col, Dropdown, DropdownToggle } from "reactstrap";
import { useTranslation } from "react-i18next";
import GlobalEnums from "../../../../helpers/GlobalEnums";
import {
	getLanguageCodeFromSession,
	GetLocalizationControlsJsonDataForScreen,
	replaceLoclizationLabel,
	setLanguageCodeInSession,
} from "../../../../helpers/CommonHelper";
import { Link } from "react-router-dom";
import SearchHeader from "./SearchHeader";

const TopHeader = () => {
	const { t } = useTranslation();
	const [openLang, setOpenLang] = useState(false);
	const [url, setUrl] = useState("");
	const toggleLang = () => {
		setOpenLang(!openLang);
	};

	//  const onChangeLang = (e: React.ChangeEvent<HTMLSelectElement>) => {
	// 		const lang_code = e.target.value;
	// 		i18n.changeLanguage(lang_code);
	// 	};

	useEffect(() => {
		const path = window.location.pathname.split("/");
		const urlTemp = path[path.length - 1];
		setUrl(urlTemp);
	}, []);

	const [langCode, setLangCode] = useState("");
	const [LocalizationLabelsArray, setLocalizationLabelsArray] = useState([]);
	const handleLangCodeInSession = async (value) => {
		await setLanguageCodeInSession(value);
		await setLangCode(value);

		let homeUrl = "/" + value + "/";
		window.location.href = homeUrl;
	};

	// const signin = (event) => {
	// 	event.preventDefault();
	// 	history.push("/en/login");
	// };

	useEffect(() => {
		// declare the data fetching function
		const dataOperationFunc = async () => {
			let lnCode = getLanguageCodeFromSession();
			setLangCode(lnCode);

			//-- Get website localization data
			let arryRespLocalization =
				await GetLocalizationControlsJsonDataForScreen(
					GlobalEnums.Entities["TopHeader"],
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
		<div
			className={`top-header ${
				url === "layout6" ? "top-header-inverse" : ""
			}`}
		>
			<div className="custom-container ">
				<Row>
					<Col xl="3" md="3" sm="3">
						<div className="top-header-left">
							<div className="logo-block">
								<h4 className="text-center mt-2 ms-5">
									<Link to={`/`}>
										<span className="text-black">
											傾奇MONO
										</span>
									</Link>
								</h4>
							</div>
							{/* <div className="app-link">
								<h6>
									<Link
										to={`/${getLanguageCodeFromSession()}/become-seller`}
										id="lbl_thead_seller"
										style={{
											color: "inherit",
											textDecoration: "none",
										}}
									>
										{t("become_seller")}
									</Link>
								</h6>
							</div> */}
						</div>
					</Col>
					<Col xl="6" md="6" sm="6">
						<div className="layout-header2">
							<SearchHeader />
						</div>
					</Col>
					<Col xl="3" md="3" sm="3">
						<div className="top-header-right">
							<span>
								<Link
									to={`/signup`}
									href="#"
									className="btn btn-link text-primary"
								>
									{t("membership_registration")}
								</Link>
								<button className="btn btn-dark">
									<Link to={`/login`}>
										<span className="text-white">
											{t("login")}
										</span>
									</Link>
								</button>
							</span>
							{/* <div className="top-menu-block">
								<ul>
									<li>
										<a href="#">gift cards</a>
									</li>
									<li>
										<a href="#">Notifications</a>
									</li>
									<li>
										<a href="#">help & contact</a>
									</li>
									<li>
										<a href="#">todays deal</a>
									</li>
									<li>
										<a href="#">track order</a>
									</li>
									<li>
										<a href="#">shipping </a>
									</li>
									<li>
										<a href="#">easy returns</a>
									</li>
								</ul>
							</div> */}
						</div>
					</Col>
				</Row>
			</div>
		</div>
	);
};

export default TopHeader;
