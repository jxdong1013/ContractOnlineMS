package com.jxd.contractonlinems.bean;

import java.util.List;

public class ContractPage {
	private int TotalPages;
	private int TotalRecords ;
	private int PageIdx ;
	private int PageSize ;

	private List<Contract> Data ;

	public int getTotalPages() {
		return TotalPages;
	}

	public void setTotalPages(int totalPages) {
		TotalPages = totalPages;
	}

	public int getTotalRecords() {
		return TotalRecords;
	}

	public void setTotalRecords(int totalRecords) {
		TotalRecords = totalRecords;
	}

	public int getPageIdx() {
		return PageIdx;
	}

	public void setPageIdx(int pageIdx) {
		PageIdx = pageIdx;
	}

	public int getPageSize() {
		return PageSize;
	}

	public void setPageSize(int pageSize) {
		PageSize = pageSize;
	}

	public List<Contract> getData() {
		return Data;
	}

	public void setData(List<Contract> data) {
		Data = data;
	}
}
