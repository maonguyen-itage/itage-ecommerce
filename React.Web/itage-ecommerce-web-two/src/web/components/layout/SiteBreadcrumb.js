import React from "react";
import { Row, Col, Container } from "reactstrap";
import { useTranslation } from "react-i18next";

const SiteBreadcrumb = ({ title, parent }) => {
	const { t } = useTranslation();

	return (
		<>
			{/* <div className="breadcrumb-main ">
				<Container>
					<Row>
						<Col>
							<div className="breadcrumb-contain">
								<div>
									<h2>{t("product")}</h2>
									<ul>
										<li>
											<a href="#">{parent}</a>
										</li>
										<li>
											<i className="fa fa-angle-double-right"></i>
										</li>
										<li>
											<a href="#">{t("product")}</a>
										</li>
									</ul>
								</div>
							</div>
						</Col>
					</Row>
				</Container>
			</div> */}
		</>
	);
};

export default SiteBreadcrumb;
