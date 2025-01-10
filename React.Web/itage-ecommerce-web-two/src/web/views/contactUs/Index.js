import React, { useEffect, useState } from "react";
import { MakeApiCallAsync } from "../../../helpers/ApiHelpers";
import { useSelector, useDispatch } from "react-redux";
import Config from "../../../helpers/Config";
import {
	showErrorMsg,
	showSuccessMsg,
	validateAnyFormField,
} from "../../../helpers/ValidationHelper";
import SiteBreadcrumb from "../../components/layout/SiteBreadcrumb";
import BestFacilities from "../../components/shared/BestFacilities";
import rootAction from "../../../stateManagment/actions/rootAction";
import { LOADER_DURATION } from "../../../helpers/Constants";
import { Helmet } from "react-helmet";
import {
	GetLocalizationControlsJsonDataForScreen,
	replaceLoclizationLabel,
} from "../../../helpers/CommonHelper";
import GlobalEnums from "../../../helpers/GlobalEnums";
import { Input, Row, Col, Form, FormGroup, Label } from "reactstrap";
import { useTranslation } from "react-i18next";

const ContactUs = () => {
	const { t } = useTranslation();

	const dispatch = useDispatch();
	const [siteTitle, setSiteTitle] = useState(Config["SITE_TTILE"]);
	const [FullName, setFullName] = useState("");
	const [Email, setEmail] = useState("");
	const [PhoneNumber, setPhoneNumber] = useState("");
	const [Subject, setSubject] = useState("");
	const [Message, setMessage] = useState("");
	const [LocalizationLabelsArray, setLocalizationLabelsArray] = useState([]);

	const handleContactUsForm = async (event) => {
		event.preventDefault();

		try {
			//--start loader
			dispatch(rootAction.commonAction.setLoading(true));

			let isValid = false;
			let validationArray = [];

			isValid = validateAnyFormField(
				"Name",
				FullName,
				"text",
				null,
				200,
				true
			);
			if (isValid == false) {
				validationArray.push({
					isValid: isValid,
				});
			}

			isValid = validateAnyFormField(
				"Email",
				Email,
				"email",
				null,
				100,
				true
			);
			if (isValid == false) {
				validationArray.push({
					isValid: isValid,
				});
			}

			// isValid = validateAnyFormField('Phone Number', PhoneNumber, 'text', null, 20, true);
			// if (isValid == false) {
			//   validationArray.push({
			//     isValid: isValid
			//   });
			// }

			isValid = validateAnyFormField(
				"Subject",
				Subject,
				"text",
				null,
				150,
				true
			);
			if (isValid == false) {
				validationArray.push({
					isValid: isValid,
				});
			}

			isValid = validateAnyFormField(
				"Message",
				Message,
				"text",
				null,
				2000,
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
						FullName: FullName,
						Email: Email,
						PhoneNumber: PhoneNumber,
						Subject: Subject,
						Message: Message,
					},
				};

				//--make api call for data operation
				const response = await MakeApiCallAsync(
					Config.END_POINT_NAMES["CONTACT_US"],
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
						userData[0].ResponseMsg == "Saved Successfully"
					) {
						showSuccessMsg("Message sent successfully!");

						//--Empty form fields
						setFullName("");
						setEmail("");
						setPhoneNumber("");
						setSubject("");
						setMessage("");
					} else {
						showErrorMsg("An error occured. Please try again!");
						return false;
					}
				}
			}
		} catch (err) {
			console.log(err);
			showErrorMsg("An error occured. Please try again!");

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
		const dataOperationInUseEffect = async () => {
			//-- Get website localization data
			let arryRespLocalization =
				await GetLocalizationControlsJsonDataForScreen(
					GlobalEnums.Entities["Index_Contact_Us"],
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
		//--start loader
		dispatch(rootAction.commonAction.setLoading(true));

		// call the function
		dataOperationInUseEffect().catch(console.error);

		//--stop loader
		setTimeout(() => {
			dispatch(rootAction.commonAction.setLoading(false));
		}, LOADER_DURATION);
	}, []);

	return (
		<>
			<Helmet>
				<title>
					{siteTitle} - {t("contact_us")}
				</title>
				<meta
					name="description"
					content={siteTitle + " - Contact Us"}
				/>
				<meta name="keywords" content="Contact Us"></meta>
			</Helmet>
			<SiteBreadcrumb title="Contact Us" parent="Home" />

			<section className="contact-page section-big-py-space bg-light">
				<div className="custom-container">
					<h3 className="text-center mb-3">{t("get_in_touch")}</h3>
					<Row className="section-big-pb-space g-4">
						<Col xl="6">
							<Form
								className="theme-form"
								onSubmit={handleContactUsForm}
							>
								<div className="form-row row">
									<Col md="6">
										<FormGroup>
											<Label
												htmlFor="FullName"
												id="lbl_cont_form_name"
											>
												{t("name")}
												<span className="required-field">
													*
												</span>
											</Label>
											<Input
												type="text"
												name="FullName"
												id="FullName"
												className="form-control"
												required={true}
												data-error={t(
													"enter_your_name_msg"
												)}
												placeholder={t(
													"enter_your_name"
												)}
												value={FullName}
												onChange={(e) =>
													setFullName(e.target.value)
												}
											/>
										</FormGroup>
									</Col>
									<Col md="6">
										<FormGroup>
											<Label
												htmlFor="Email"
												id="lbl_cont_form_email"
											>
												{t("email")}
												<span className="required-field">
													*
												</span>
											</Label>
											<Input
												type="email"
												name="Email"
												id="Email"
												className="form-control"
												required={true}
												data-error={t(
													"enter_your_email_msg"
												)}
												placeholder={t(
													"enter_your_email"
												)}
												value={Email}
												onChange={(e) =>
													setEmail(e.target.value)
												}
											/>
										</FormGroup>
									</Col>
									<Col md="6">
										<FormGroup>
											<Label
												htmlFor="PhoneNumber"
												id="lbl_cont_form_phon"
											>
												{t("phone_number")}
											</Label>
											<Input
												type="text"
												name="PhoneNumber"
												id="PhoneNumber"
												className="form-control"
												required={false}
												data-error={t(
													"enter_your_phone_msg"
												)}
												placeholder={t(
													"enter_your_phone_number"
												)}
												value={PhoneNumber}
												onChange={(e) =>
													setPhoneNumber(
														e.target.value
													)
												}
											/>
										</FormGroup>
									</Col>
									<Col md="6">
										<FormGroup>
											<Label
												htmlFor="Subject"
												id="lbl_cont_form_subj"
											>
												{t("subject")}
												<span className="required-field">
													*
												</span>
											</Label>
											<Input
												type="text"
												name="Subject"
												id="Subject"
												className="form-control"
												required={true}
												data-error={t(
													"enter_subject_msg"
												)}
												placeholder={t(
													"enter_subject_here"
												)}
												value={Subject}
												onChange={(e) =>
													setSubject(e.target.value)
												}
											/>
										</FormGroup>
									</Col>
									<Col md="12">
										<div>
											<Label
												htmlFor="message"
												id="lbl_cont_form_msg"
											>
												{t("your_message")}
												<span className="required-field">
													*
												</span>
											</Label>
											<textarea
												name="Message"
												id="message"
												cols="30"
												rows="8"
												required={true}
												data-error={t(
													"enter_your_message_msg"
												)}
												className="form-control"
												placeholder={t(
													"enter_your_message"
												)}
												value={Message}
												onChange={(e) =>
													setMessage(e.target.value)
												}
											/>
										</div>
									</Col>
									<Col md="12">
										<button
											className="btn btn-normal"
											type="submit"
										>
											{t("send_your_message")}
										</button>
									</Col>
								</div>
							</Form>
						</Col>
						<Col xl="6" className="map">
							<div className="theme-card">
								<iframe
									src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3261.6449684495624!2d136.90080398790641!3d35.165474087904826!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x600370d187729707%3A0x817ed32202a24f6f!2z5qCq5byP5Lya56S-77yp77y077yh77yn77yl77yn77ys77yv77yi77yh77ys!5e0!3m2!1sen!2sjp!4v1731336613991!5m2!1sen!2sjp"
									width="800"
									height="600"
									allowfullscreen=""
									loading="lazy"
									referrerpolicy="no-referrer-when-downgrade"
								></iframe>
							</div>
						</Col>
					</Row>
					<Row></Row>
				</div>
			</section>

			<BestFacilities />
		</>
	);
};

export default ContactUs;
