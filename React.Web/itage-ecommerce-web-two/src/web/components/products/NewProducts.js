import React, { useEffect, useState } from "react";
import {
	TabContent,
	TabPane,
	Nav,
	NavItem,
	NavLink,
	Row,
	Col,
} from "reactstrap";
import Slider from "react-slick";
import ProductBox from "./ProductBox";
import Skeleton from "./Skeleton/Skeleton";
import { useSelector, useDispatch } from "react-redux";

import {
	getLanguageCodeFromSession,
	GetLocalizationControlsJsonDataForScreen,
} from "../../../helpers/CommonHelper";
import Config from "../../../helpers/Config";
import { MakeApiCallAsync } from "../../../helpers/ApiHelpers";
import GlobalEnums from "../../../helpers/GlobalEnums";
import rootAction from "../../../stateManagment/actions/rootAction";
import { LOADER_DURATION } from "../../../helpers/Constants";
import { useTranslation } from "react-i18next";
import NewProduct from "../../../resources/themeContent/images/icon/New_Product.png";
import Deal from "../../../resources/themeContent/images/icon/Deal.png";
import Coupon from "../../../resources/themeContent/images/icon/Coupon.png";
import BestSeller from "../../../resources/themeContent/images/icon/Best_Seller.png";

var settings = {
	arrows: true,
	dots: false,
	infinite: true,
	speed: 300,
	slidesToShow: 6,
	slidesToScroll: 1,

	responsive: [
		{
			breakpoint: 1700,
			settings: {
				slidesToShow: 5,
				slidesToScroll: 5,
				infinite: true,
			},
		},
		{
			breakpoint: 1200,
			settings: {
				slidesToShow: 4,
				slidesToScroll: 4,
				infinite: true,
			},
		},
		{
			breakpoint: 991,
			settings: {
				slidesToShow: 3,
				slidesToScroll: 3,
				infinite: true,
			},
		},
		{
			breakpoint: 576,
			settings: {
				slidesToShow: 2,
				slidesToScroll: 2,
			},
		},
	],
};

const NewProducts = ({ effect }) => {
	const { t } = useTranslation();
	const dispatch = useDispatch();
	const [activeTab, setActiveTab] = useState("new_products");
	const [adminPanelBaseURL, setBaseUrl] = useState(Config["ADMIN_BASE_URL"]);
	const [collection, setCollection] = useState([
		{
			nameKey: "new_products",
			image: NewProduct,
		},
		{
			nameKey: "on_sale",
			image: Deal,
		},
		{
			nameKey: "hot_deal",
			image: Coupon,
		},
		{
			nameKey: "best_sellers",
			image: BestSeller,
		},
	]);

	const [ProductsList, setProductsList] = useState([]);
	const [langCode, setLangCode] = useState("");
	const [LocalizationLabelsArray, setLocalizationLabelsArray] = useState([]);
	const [PaginationInfo, setPaginationInfo] = useState({
		PageNo: 1,
		PageSize: 20,
		TotalRecords: 0,
	});

	const getNewProductsList = async (TabName) => {
		await setActiveTab(TabName);
		//--empty list of product
		await setProductsList([]);

		const headers = {
			Accept: "application/json",
			"Content-Type": "application/json",
		};

		const param = {
			requestParameters: {
				PageNo: PaginationInfo.PageNo,
				PageSize: PaginationInfo.PageSize,
				TabName: TabName,
				recordValueJson: "[]",
			},
		};

		const response = await MakeApiCallAsync(
			Config.END_POINT_NAMES["GET_RECENTS_PRODUCTS_LIST"],
			null,
			param,
			headers,
			"POST",
			true
		);

		if (response != null && response.data != null) {
			let ProductData = JSON.parse(response.data.data);
			// let slidesToShow = 6;
			let slidesToShow = 0;

			if (ProductData != undefined && ProductData.length < slidesToShow) {
				ProductData = ProductData.concat(ProductData);
			}

			await setProductsList(ProductData);
		}
	};

	useEffect(() => {
		const dataOperationInUseEffect = async () => {
			let lnCode = getLanguageCodeFromSession();
			await setLangCode(lnCode);

			// await getNewProductsList("new_products");
			await getNewProductsList("new_products");
			// await getNewProductsList(t("new_products"));
			let arryRespLocalization =
				await GetLocalizationControlsJsonDataForScreen(
					GlobalEnums.Entities["NewProducts"],
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
			<section className="section-pt-space">
				<div className="tab-product-main border-top">
					<div className="tab-prodcut-contain shadow-sm bg-body rounded">
						{/* <Nav tabs>
							{collection &&
								collection?.map((c, i) => (
									<NavItem key={i}>
										<NavLink
											className={
												activeTab === c.nameEn
													? "active"
													: ""
											}
											onClick={() =>
												getNewProductsList(c.nameEn)
											}
										>
											{langCode != null &&
											langCode ==
												Config.LANG_CODES_ENUM["Arabic"]
												? c.nameAr
												: c.nameEn}
										</NavLink>
									</NavItem>
								))}
						</Nav> */}
						<Nav tabs>
							{collection &&
								collection.map((c, i) => (
									<NavItem key={i}>
										<NavLink
											className={
												activeTab === c.nameKey
													? "active"
													: ""
											}
											onClick={() =>
												getNewProductsList(c.nameKey)
											}
										>
											<img
												src={c.image}
												alt=""
												style={{
													width: "44px",
													height: "44px",
													marginRight: "5px",
												}}
											/>
											{t(c.nameKey)}
										</NavLink>
									</NavItem>
								))}
						</Nav>
					</div>
				</div>
			</section>
			<section className="section-py-space ratio_asos product">
				<div className="custom-container">
					<Row>
						<Col className="pe-0">
							<TabContent activeTab={activeTab}>
								<TabPane tabId={activeTab}>
									<div className="product product-slide-6 product-m no-arrow">
										<div>
											{!ProductsList ||
											ProductsList.length < 0 ? (
												<Skeleton />
											) : (
												<Slider {...settings}>
													{ProductsList &&
														ProductsList.map(
															(itm, i) => (
																<div key={i}>
																	<ProductBox
																		hoverEffect={
																			effect
																		}
																		item={
																			itm
																		}
																		layout=""
																		ProductDetailPageForceUpload={
																			false
																		}
																	/>
																</div>
															)
														)}
												</Slider>
											)}
										</div>
									</div>
								</TabPane>
							</TabContent>
						</Col>
					</Row>
				</div>
			</section>
		</>
	);
};

export default NewProducts;
