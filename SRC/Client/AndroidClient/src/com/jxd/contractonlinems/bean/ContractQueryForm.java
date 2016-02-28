package com.jxd.contractonlinems.bean;

import java.io.Serializable;

public class ContractQueryForm implements Serializable {
	/**
	 *
	 */
	private static final long serialVersionUID = 1L;
	private String seq;
	private String projectName;
	private String contractNum;
	private String projectNo;
	private String contractName;
	private String contractState;
	private String contractPlace;
	private String contractRFID;
	private String bCompany;
	private String signingDate;
	private String money;
	private String otherCondition;
	private int pageidx;
	private boolean isrefresh;
	private int pageSize = 20;

	public String getContractNum() {
		return contractNum;
	}
	public void setContractNum(String contractNum) {
		this.contractNum = contractNum;
	}
	public String getContractName() {
		return contractName;
	}
	public void setContractName(String contractName) {
		this.contractName = contractName;
	}
	public String getContractState() {
		return contractState;
	}
	public void setContractState(String contractState) {
		this.contractState = contractState;
	}
	public String getContractPlace() {
		return contractPlace;
	}
	public void setContractPlace(String contractPlace) {
		this.contractPlace = contractPlace;
	}
	public String getContractRFID() {
		return contractRFID;
	}
	public void setContractRFID(String contractRFID) {
		this.contractRFID = contractRFID;
	}
	public String getbCompany() {
		return bCompany;
	}
	public void setbCompany(String bCompany) {
		this.bCompany = bCompany;
	}
	public String getSigningDate() {
		return signingDate;
	}
	public void setSigningDate(String signingDate) {
		this.signingDate = signingDate;
	}
	public int getPageidx() {
		return pageidx;
	}
	public void setPageidx(int pageidx) {
		this.pageidx = pageidx;
	}
	public boolean isIsrefresh() {
		return isrefresh;
	}
	public void setIsrefresh(boolean isrefresh) {
		this.isrefresh = isrefresh;
	}
	public int getPageSize() {
		return pageSize;
	}
	public void setPageSize(int pageSize) {
		this.pageSize = pageSize;
	}
	public String getSeq() {
		return seq;
	}
	public void setSeq(String seq) {
		this.seq = seq;
	}
	public String getProjectName() {
		return projectName;
	}
	public void setProjectName(String projectName) {
		this.projectName = projectName;
	}
	public String getMoney() {
		return money;
	}
	public void setMoney(String money) {
		this.money = money;
	}
	public String getOtherCondition() {
		return otherCondition;
	}
	public void setOtherCondition(String otherCondition) {
		this.otherCondition = otherCondition;
	}
	public String getProjectNo() {
		return projectNo;
	}
	public void setProjectNo(String projectNo) {
		this.projectNo = projectNo;
	}
}
