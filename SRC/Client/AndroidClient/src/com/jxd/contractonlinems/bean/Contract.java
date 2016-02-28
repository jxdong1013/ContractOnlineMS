package com.jxd.contractonlinems.bean;

import java.io.Serializable;

public class Contract implements Serializable{
	  /**
	 *
	 */
	private static final long serialVersionUID = -8413901495254179274L;
	private Integer contractid;
      private String seq;
      private String contractnum;
      private String contractname;
      private String contractstate;
      private String unshelve;
      private String projectnum;
      private String projectname;
      private String projectmanager;
      private String tel;
      private String depart;
      private String linker;
      private String paymethod;
      private String money;
      private String contractplace;
      private String contractrfid;
      private String bcompany;
      private String signingdate;
      private String packageName;
      private String packageBudget;
      private String tendarNum;
      private String tendarCompany;
      private String tendarStartTime;
      private String phone;
      private String deliveryTime;
      private String inspection;
      private String progress;
      private String  isPayAll ;
      private String  isArmoured;
      private String  isRefund;
      private String remark;
      private String operatorId;
      private String operatorName;
      private Long  createtimelong;
      private Long  modifytimelong;


  	private boolean isSelected;


	public Integer getContractid() {
		return contractid;
	}
	public void setContractid(Integer contractid) {
		this.contractid = contractid;
	}
	public String getSeq() {
		return seq;
	}
	public void setSeq(String seq) {
		this.seq = seq;
	}
	public String getContractnum() {
		return contractnum;
	}
	public void setContractnum(String contractnum) {
		this.contractnum = contractnum;
	}
	public String getContractstate() {
		return contractstate;
	}
	public void setContractstate(String contractstate) {
		this.contractstate = contractstate;
	}
	public String getProjectnum() {
		return projectnum;
	}
	public void setProjectnum(String projectnum) {
		this.projectnum = projectnum;
	}
	public String getUnshelve() {
		return unshelve;
	}
	public void setUnshelve(String unshelve) {
		this.unshelve = unshelve;
	}
	public String getContractplace() {
		return contractplace;
	}
	public void setContractplace(String contractplace) {
		this.contractplace = contractplace;
	}
	public String getProjectmanager() {
		return projectmanager;
	}
	public void setProjectmanager(String projectmanager) {
		this.projectmanager = projectmanager;
	}
	public String getProjectname() {
		return projectname;
	}
	public void setProjectname(String projectname) {
		this.projectname = projectname;
	}
	public String getPaymethod() {
		return paymethod;
	}
	public void setPaymethod(String paymethod) {
		this.paymethod = paymethod;
	}
	public String getLinker() {
		return linker;
	}
	public void setLinker(String linker) {
		this.linker = linker;
	}
	public String getDepart() {
		return depart;
	}
	public void setDepart(String depart) {
		this.depart = depart;
	}
	public String getTel() {
		return tel;
	}
	public void setTel(String tel) {
		this.tel = tel;
	}
	public String getContractrfid() {
		return contractrfid;
	}
	public void setContractrfid(String contractrfid) {
		this.contractrfid = contractrfid;
	}
	public String getTendarnum() {
		return tendarNum;
	}
	public void setTendarnum(String tendarnum) {
		this.tendarNum = tendarnum;
	}
	public String getPackagename() {
		return packageName;
	}
	public void setPackagename(String packagename) {
		this.packageName = packagename;
	}
	public String getInspection() {
		return inspection;
	}
	public void setInspection(String inspection) {
		this.inspection = inspection;
	}
	public String getIspayall() {
		return isPayAll;
	}
	public void setIspayall(String ispayall) {
		this.isPayAll = ispayall;
	}
	public String getTendarcompany() {
		return tendarCompany;
	}
	public void setTendarcompany(String tendarcompany) {
		this.tendarCompany = tendarcompany;
	}
	public String getPackagebudget() {
		return packageBudget;
	}
	public void setPackagebudget(String packagebudget) {
		this.packageBudget = packagebudget;
	}
	public String getSigningdate() {
		return signingdate;
	}
	public void setSigningdate(String signingdate) {
		this.signingdate = signingdate;
	}
	public String getBcompany() {
		return bcompany;
	}
	public void setBcompany(String bcompany) {
		this.bcompany = bcompany;
	}
	public String getDeliverytime() {
		return deliveryTime;
	}
	public void setDeliverytime(String deliverytime) {
		this.deliveryTime = deliverytime;
	}
	public String getTendarstarttime() {
		return tendarStartTime;
	}
	public void setTendarstarttime(String tendarstarttime) {
		this.tendarStartTime = tendarstarttime;
	}
	public String getPhone() {
		return phone;
	}
	public void setPhone(String phone) {
		this.phone = phone;
	}
	public String getProgress() {
		return progress;
	}
	public void setProgress(String progress) {
		this.progress = progress;
	}
	public String getIsarmoured() {
		return isArmoured;
	}
	public void setIsarmoured(String isarmoured) {
		this.isArmoured = isarmoured;
	}
	public String getIsrefund() {
		return isRefund;
	}
	public void setIsrefund(String isrefund) {
		this.isRefund = isrefund;
	}
	public String getMoney() {
		return money;
	}
	public void setMoney(String money) {
		this.money = money;
	}
	public String getContractname() {
		return contractname;
	}
	public void setContractname(String contractname) {
		this.contractname = contractname;
	}
	public Long getModifytimelong() {
		return modifytimelong;
	}
	public void setModifytimelong(Long modifytimelong) {
		this.modifytimelong = modifytimelong;
	}
	public Long getCreatetimelong() {
		return createtimelong;
	}
	public void setCreatetimelong(Long createtimelong) {
		this.createtimelong = createtimelong;
	}
	public String getOperatorNam() {
		return operatorName;
	}
	public void setOperatorname(String operatorname) {
		this.operatorName = operatorname;
	}
	public String getOperatorId() {
		return operatorId;
	}
	public void setOperatorId(String operatorid) {
		this.operatorId = operatorid;
	}
	public String getRemark() {
		return remark;
	}
	public void setRemark(String remark) {
		this.remark = remark;
	}
	public boolean getSelected() {
		return isSelected;
	}
	public void setSelected(boolean isSelected) {
		this.isSelected = isSelected;
	}
}
