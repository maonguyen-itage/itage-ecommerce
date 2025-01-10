import React, { useEffect, useState } from "react";
import { Row, Col } from "reactstrap";
import { useTranslation } from "react-i18next";
import GlobalEnums from "../../../../helpers/GlobalEnums";
import {
	getLanguageCodeFromSession,
	GetLocalizationControlsJsonDataForScreen,
	setLanguageCodeInSession,
} from "../../../../helpers/CommonHelper";
import { Link } from "react-router-dom";
import SearchHeader from "./SearchHeader";
import { LANGUAGES } from "../../../../constants";

const TopHeader = () => {
	const { i18n, t } = useTranslation();

	// const [openLang, setOpenLang] = useState(false);
	const [url, setUrl] = useState("");
	// const toggleLang = () => {
	// 	setOpenLang(!openLang);
	// };

	const onChangeLang = (e) => {
		const lang_code = e.target.value;
		i18n.changeLanguage(lang_code);
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
					<Col xl="2" md="2" sm="2">
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
					<Col xl="4" md="4" sm="4">
						<div className="">
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
								<div
									className="sub-footer-contain"
									style={{ float: "right" }}
								>
									<div className="language-block">
										<div className="language-dropdown">
											<select
												class="form-select"
												defaultValue={i18n.language}
												onChange={onChangeLang}
											>
												{LANGUAGES.map(
													({ code, label }) => (
														<option
															key={code}
															value={code}
														>
															{label}
														</option>
													)
												)}
											</select>
										</div>
									</div>
								</div>
							</span>
						</div>
					</Col>
				</Row>
			</div>
		</div>
	);
};

export default TopHeader;
