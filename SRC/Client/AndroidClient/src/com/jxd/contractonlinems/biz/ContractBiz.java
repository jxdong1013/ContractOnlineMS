package com.jxd.contractonlinems.biz;


import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import android.app.ProgressDialog;
import android.content.Context;

import com.jxd.contractonlinems.R;
import com.jxd.contractonlinems.activity.LoginActivity;
import com.jxd.contractonlinems.bean.Contract;
import com.jxd.contractonlinems.bean.ContractQueryForm;
import com.jxd.contractonlinems.util.AsyncHttpUtil;
//import com.jxd.contractonlinems.util.ExportTxtUtil;
//import com.jxd.contractonlinems.util.SQLiteUitl;
import com.loopj.android.http.RequestParams;

public class ContractBiz {
	public final static int Failure = 0;
	public final static int UpdateSuccess = 1;
	public final static int AddSuccess = 2;
	//private SQLiteUitl mDao = null;
	private Context mContext = null;
	private String _host = "192.168.1.6";
	private String _port = "80";


	public ContractBiz(Context context) {
		mContext = context;
		//mDao = new SQLiteUitl(mContext);
	}

//	public int ContractProcess(Contract contract) throws Exception {
//		if (contract == null) {
//			throw new Exception("²ÎÊý¿ÕÒì³£");
//		}
//		boolean isExist=false;
//
//		isExist = mDao.ExistContractByContractNum( contract.getContractNo());
//
//		if (isExist) {
//			return mDao.UpdateContract(contract) > 0 ? UpdateSuccess : Failure;
//		} else {
//			List<Contract> list = new ArrayList<Contract>();
//			list.add(contract);
//			return mDao.InsertContracts(list) > 0 ? AddSuccess : Failure;
//		}
//	}

//	public boolean ExistContractByContractNum(String contractNum){
//		return mDao.ExistContractByContractNum(contractNum);
//	}

	public boolean ExistContractByContractNumNotContrainContractId(String contractNum , Integer contractId ){
		return false;// mDao.ExistContractByContractNumNotContractId(contractNum, contractId);
	}
//
	public boolean ExistContractByProjectNumAndProjectNameNotContrainSelf( String projectNum,
			String projectName , Integer contractId ){
		return false;//mDao.ExistContractByProjectNumAndProjectNameNotContrainSelf( projectNum,projectName , contractId);
	}
//
	public boolean ExistContractByProjectNumAndProjectName(String projectNum , String projectName){
		return false;// mDao.ExistContractByProjectNumAndProjectName(projectNum, projectName);
	}

	public int Insert(Contract contract) {
		List<Contract> list = new ArrayList<Contract>();
		list.add(contract);
		return -1;// mDao.InsertContracts(list)>0? AddSuccess : Failure;
	}
//
	public int Update( Contract contract){
		return -1;// mDao.UpdateContract(contract)>0 ? UpdateSuccess : Failure;
	}
//
	public int UpdateByProjectNumAndProjectName( Contract contract){
		return  -1; // mDao.UpdateContractByProjectNumAndProjectName(contract)>0? UpdateSuccess : Failure;
	}

//	public List<Contract> Query(String operator, int pageid, int pagesize , String contractNum ,String contractName , String uid) {
//		String start ="";
//		return mDao.QueryContractByPage(operator, pageid, pagesize, contractNum , contractName ,uid ,start );
//	}

//	public int QueryCount( String operator,String contractNum ,String contractName , String uid){
//		String start ="";
//		return mDao.QueryCount( operator , contractNum , contractName , uid , start);
//	}

	public int QueryCount( ContractQueryForm queryForm){
		//return mDao.QueryCount(queryForm);
		return -1;

	}

//	public List<Contract> Query( ContractQueryForm queryForm ){
//		//return mDao.QueryContractByPage(queryForm);
//	}

	public boolean ExistUID(String uid) {
		return false;//mDao.ExistByUID(uid , "");
	}
//
	public int DeleteAllData() {
		return -1;//mDao.DeleteAllContracts();
	}
//
	public int DeleteByIds( List<Integer> contractIds){
		return -1;//mDao.DeleteContractByContractIds(contractIds);
	}
//
//	public int DeleteById( Integer contractId ){
//		return mDao.DeleteContractByContractId(contractId);
//	}
//
//	public String getTxtPath(String filename){
//		return new ExportTxtUtil(mContext).getFileFullPath()+"/"+filename;
//	}

//	public boolean ExportTxt(String fileName) throws IOException {
//		String start ="";
//		String username ="";
//		List<Contract> list = mDao.QueryContractsByOperatorAndOperateTime(username, start);
//		if (list != null && list.size() > 0) {
//			ExportTxtUtil util = new ExportTxtUtil(mContext);
//
//			util.createSDFile( fileName);
//
//			util.writeFile(list, fileName);
//			return true;
//		}
//		return false;
//	}

//	public Contract GetContractByRFID(String rfid){
//		return mDao.GetContractByRFID(rfid);
//	}
//
//	public Contract GetNextContract(String contractId , String modifyTime , String otherCondition ){
//		return mDao.GetNextContract( contractId , modifyTime , otherCondition );
//	}
}
