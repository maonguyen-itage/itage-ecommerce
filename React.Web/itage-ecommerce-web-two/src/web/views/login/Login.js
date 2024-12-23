import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useSelector, useDispatch } from "react-redux";
import SiteBreadcrumb from "../../components/layout/SiteBreadcrumb";
import BestFacilities from "../../components/shared/BestFacilities";
import rootAction from "../../../stateManagment/actions/rootAction";
import Config from "../../../helpers/Config";
import {
	MakeApiCallSynchronous,
	MakeApiCallAsync,
} from "../../../helpers/ApiHelpers";
import {
	showErrorMsg,
	showSuccessMsg,
	validateAnyFormField,
} from "../../../helpers/ValidationHelper";
import { LOADER_DURATION } from "../../../helpers/Constants";
import { Helmet } from "react-helmet";
import {
	getLanguageCodeFromSession,
	GetLocalizationControlsJsonDataForScreen,
	replaceLoclizationLabel,
} from "../../../helpers/CommonHelper";
import GlobalEnums from "../../../helpers/GlobalEnums";
import { Row, Col, Input, Label } from "reactstrap";
import { useTranslation } from "react-i18next";
import InputAdornment from "@material-ui/core/InputAdornment";
import IconButton from "@material-ui/core/IconButton";
import Visibility from "@material-ui/icons/VisibilityOff";
import VisibilityOff from "@material-ui/icons/VisibilityOff";

const Login = () => {
	const { t } = useTranslation();

	const dispatch = useDispatch();
	const navigate = useNavigate();
	const [siteTitle, setSiteTitle] = useState(Config["SITE_TTILE"]);
	const [LocalizationLabelsArray, setLocalizationLabelsArray] = useState([]);
	const [Email, setEmail] = useState("yamamoto@itageshop.com");
	const [Password, setPassword] = useState("123456");

	const [values, setValues] = useState({
		password: "",
		showPassword: false,
	});

	const [showPassword, setShowPassword] = useState(false);
	const [showConfirmPassword, setShowConfirmPassword] = useState(false);

	const togglePasswordVisibility = () => {
		setShowPassword(!showPassword);
	};

	const toggleConfirmPasswordVisibility = () => {
		setShowConfirmPassword(!showConfirmPassword);
	};

	const submitLoginForm = async (event) => {
		//--start loader
		dispatch(rootAction.commonAction.setLoading(true));
		try {
			event.preventDefault();
			let isValid = false;
			let validationArray = [];

			//--validation for email
			isValid = validateAnyFormField(
				"Email",
				Email,
				"email",
				null,
				200,
				true
			);
			if (isValid == false) {
				validationArray.push({
					isValid: isValid,
				});
			}

			//--validation password
			isValid = validateAnyFormField(
				"Password",
				Password,
				"password",
				null,
				30,
				true
			);
			if (isValid == false) {
				validationArray.push({
					isValid: isValid,
				});
			}

			//--check if any field is not valid
			if (validationArray != null && validationArray.length > 0) {
				isValid = false;
				return false;
			} else {
				isValid = true;
			}

			if (isValid) {
				const headers = {
					Accept: "application/json",
					"Content-Type": "application/json",
				};
				const param = {
					requestParameters: {
						Email: Email,
						Password: Password,
					},
				};

				//--make api call for data operation
				const response = await MakeApiCallAsync(
					Config.END_POINT_NAMES["GET_USER_LOGIN"],
					null,
					param,
					headers,
					"POST",
					true
				);
				if (response != null && response.data != null) {
					let userData = JSON.parse(response.data.data);
					if (
						userData.length > 0 &&
						userData[0].ResponseMsg != undefined &&
						userData[0].ResponseMsg === "Login Successfully"
					) {
						showSuccessMsg(t("login_successfully_msg"));

						//--save user data in session
						localStorage.setItem(
							"user",
							JSON.stringify(userData[0])
						);
						dispatch(
							rootAction.userAction.setUser(
								JSON.stringify(userData[0])
							)
						);

						//--set Token in local storage
						localStorage.setItem(
							"Token",
							response.data.token ?? ""
						);

						navigate("/" + getLanguageCodeFromSession() + "/");
					} else {
						showErrorMsg(t("password_incorrect_msg"));
						return false;
					}
				}
			}
		} catch (err) {
			console.log(err);
			showErrorMsg(t("error_occurred_msg"));
			//--empty existing user data if set at some level in above line of code
			localStorage.setItem("user", "{}");
			dispatch(rootAction.userAction.setUser("{}"));
			return false;
		} finally {
			//--stop loader
			setTimeout(() => {
				dispatch(rootAction.commonAction.setLoading(false));
			}, LOADER_DURATION);
		}
	};

	useEffect(() => {
		// declare the data fetching function
		const dataOperationFunc = async () => {
			//-- Get website localization data
			let arryRespLocalization =
				await GetLocalizationControlsJsonDataForScreen(
					GlobalEnums.Entities["Login"],
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
		<>
			<Helmet>
				<title>
					{siteTitle} - {t("login")}
				</title>
				<meta
					name="description"
					content={siteTitle + " - " + t("login")}
				/>
				<meta name="keywords" content={t("login")}></meta>
			</Helmet>

			<SiteBreadcrumb title={t("login")} parent={t("home")} />
			<section className="login-page section-big-py-space bg-light">
				<div className="custom-container">
					<Row className="row">
						<Col
							xl="4"
							lg="6"
							md="8"
							className="offset-xl-4 offset-lg-3 offset-md-2"
						>
							<div className="theme-card">
								<h3 className="text-center">{t("login")}</h3>
								<form
									className="theme-form"
									onSubmit={submitLoginForm}
								>
									<div className="form-group">
										<Label htmlFor="name">
											{t("email")}
										</Label>
										<Input
											type="email"
											className="form-control"
											placeholder={t("enter_your_email")}
											id="name"
											name="name"
											required={true}
											value={Email}
											onChange={(e) =>
												setEmail(e.target.value)
											}
										/>
									</div>
									<div className="">
										<Label htmlFor="password">
											{t("password")}
										</Label>
										<span className="input-group border">
											<Input
												type={
													showPassword
														? "text"
														: "password"
												}
												className="form-control"
												placeholder={t(
													"enter_your_password"
												)}
												id="password"
												name="password"
												required={true}
												value={Password}
												onChange={(e) =>
													setPassword(e.target.value)
												}
											/>

											<button
												type="button"
												style={{
													padding: "14px 10px",
													backgroundColor: "#ffffff",
												}}
												className="btn btn-light input-group-text border"
												onClick={
													togglePasswordVisibility
												}
											>
												<i
													className={
														showPassword
															? "fa fa-eye"
															: "fa fa-eye-slash"
													}
												></i>
											</button>
										</span>
									</div>

									<Link
										to={`/${getLanguageCodeFromSession()}/reset-password`}
										className="float-end txt-default mt-2 form-group"
									>
										{t("forgot_your_password")}
									</Link>
									<button
										type="submit"
										className="btn btn-normal w-100"
										id="lbl_login_loginbtn"
									>
										{t("login")}
									</button>
								</form>

								<p id="lbl_login_inst_singup" className="mt-3">
									{t("do_not_account")}
								</p>

								<Link
									to={`/${getLanguageCodeFromSession()}/signup`}
									className="txt-default pt-3 d-block"
									id="lbl_login_createacnt_2"
								>
									{t("create_an_account")}
								</Link>
							</div>
						</Col>
					</Row>
				</div>
			</section>
		</>
	);
};

export default Login;
