import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Pagination from "react-js-pagination";
import { useTranslation } from "react-i18next";

const SitePagination = (props) => {
	const { i18n, t } = useTranslation();

	const [activePage, setActivePage] = useState(props.CurrentPage);
	const [TotalRecords, setTotalRecords] = useState(props.TotalRecords);
	const [PageSize, setPageSize] = useState(props.PageSize);

	const handlePageChange = (pageNumber) => {
		console.log(`active page is ${pageNumber}`);
		// setActivePage(pageNumber);
		props.setCurrentPage(pageNumber);
	};

	useEffect(() => {}, []);

	return (
		<div style={{ marginTop: "14px", float: "right" }}>
			<Pagination
				activePage={activePage}
				itemsCountPerPage={PageSize}
				totalItemsCount={TotalRecords}
				pageRangeDisplayed={5}
				onChange={handlePageChange.bind(this)}
				prevPageText={t("prev")}
				nextPageText={t("next")}
				activeClass={"page-item active"}
				activeLinkClass={"page-link"}
				itemClass={"page-item pagination-li-item-custom"}
				innerClass={"pagination"}
				linkClass={"page-link"}
			/>
		</div>
	);
};

export default SitePagination;
