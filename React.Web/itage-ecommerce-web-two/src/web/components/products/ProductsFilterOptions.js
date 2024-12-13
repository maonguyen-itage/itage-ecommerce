import React, { useState, useEffect } from "react";
import { Col, Row } from "reactstrap";
import filterIcon2 from "../../../resources/themeContent/images/category/icon/2.png";
import filterIcon3 from "../../../resources/themeContent/images/category/icon/3.png";
import filterIcon4 from "../../../resources/themeContent/images/category/icon/4.png";
import { useTranslation } from "react-i18next";

const ProductsFilterOptions = (props) => {
	const { i18n, t } = useTranslation();

	return (
		<>
			<Row>
				<Col xs="12">
					<div className="filter-main-btn">
						<span
							className="filter-btn"
							onClick={(e) => {
								props.setLeftSidebarOpenCloseFromFilter(
									e,
									true
								);
							}}
						>
							<i className="fa fa-filter" aria-hidden="true"></i>{" "}
							{t("filter")}
						</span>
					</div>
				</Col>
				<Col xs="12">
					<div className="product-filter-content">
						<div className="collection-view">
							<ul>
								<li
									onClick={() => {
										props.setLayout("");
										props.setGrid(props.cols);
									}}
								>
									<i className="fa fa-th grid-layout-view"></i>
								</li>
								<li
									onClick={() => {
										props.setLayout("list-view");
										props.setGrid("col-lg-12");
									}}
								>
									<i className="fa fa-list-ul list-layout-view"></i>
								</li>
							</ul>
						</div>
						<div
							className="collection-grid-view"
							style={
								props.layout === "list-view"
									? { opacity: 0 }
									: { opacity: 1 }
							}
						>
							<ul>
								<li onClick={() => props.setGrid("col-lg-6")}>
									<img
										src={filterIcon2}
										alt=""
										className="product-2-layout-view"
									/>
								</li>
								<li onClick={() => props.setGrid("col-lg-4")}>
									<img
										src={filterIcon3}
										alt=""
										className="product-3-layout-view"
									/>
								</li>
								<li onClick={() => props.setGrid("col-lg-3")}>
									<img
										src={filterIcon4}
										alt=""
										className="product-4-layout-view"
									/>
								</li>
							</ul>
						</div>
						<div className="product-page-per-view">
							<select
								onChange={(e) =>
									props.setPageSizeFromProductFilter(e)
								}
							>
								<option value="10">
									10 {t("products_per_page")}
								</option>
								<option value="15">
									15 {t("products_per_page")}
								</option>
								<option value="20">
									20 {t("products_per_page")}
								</option>
								<option value="30">
									30 {t("products_per_page")}
								</option>
								<option value="40">
									40 {t("products_per_page")}
								</option>
								<option value="50">
									50 {t("products_per_page")}
								</option>
								<option value="100">
									100 {t("products_per_page")}
								</option>
							</select>
						</div>
						<div className="product-page-filter">
							<select onChange={(e) => props.setSortByFilter(e)}>
								<option value="">{t("featured")}</option>

								<option value="Price ASC">
									{t("price_ascending")}
								</option>
								<option value="Price DESC">
									{t("price_descending")}
								</option>
								<option value="Date DESC">
									{t("date_descending")}
								</option>
								<option value="Date ASC">
									{t("date_ascending")}
								</option>
							</select>
						</div>
					</div>
				</Col>
			</Row>
		</>
	);
};

export default ProductsFilterOptions;
