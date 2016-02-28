package com.jxd.contractonlinems.thread;

import java.util.Date;

import android.content.Context;
import android.os.Handler;
import android.os.Message;
import com.jxd.contractonlinems.biz.ContractBiz;
import com.jxd.contractonlinems.bean.Contract;

public class SaveContractThread extends Thread {
	private Contract mContract = null;
	private int mMsg = 0;
	protected Handler mHandler = null;
	protected Context mContext=null;
	public static int msgfalse = 0;
	public static int msgSaveQuit = 1;
	public static int msgSaveNext = 2;
	private String mOtherCondition = null;

	public SaveContractThread( Context context , Contract contract, int msg , Handler handler , String otherCondition ) {
		mContract = contract;
		mContext = context;
		mMsg = msg;
		mHandler = handler;
		mOtherCondition = otherCondition;
	}

	protected void Add(){
		ContractBiz biz = new ContractBiz(mContext);
		boolean isExist = biz.ExistContractByProjectNumAndProjectName( mContract.getProjectnum() , mContract.getProjectname() );
		if( isExist ){
			Message msg = mHandler.obtainMessage(msgfalse);
			msg.obj = "项目编号和项目名称已经存在，操作失败。";
			mHandler.sendMessage(msg);
			return ;
		}

		int status = biz.Insert(mContract);
		if (status == ContractBiz.Failure) {
			Message msg = mHandler.obtainMessage(msgfalse);
			msg.obj = "新增操作失败";
			mHandler.sendMessage(msg);
		} else if (status == ContractBiz.AddSuccess) {
			Message msg = mHandler.obtainMessage(mMsg);
			msg.obj = "新增操作成功";
			mHandler.sendMessage(msg);
		}
	}

	protected Contract GetNextContract( String contractId , Date modifyTime , String otherCondition ){
		ContractBiz biz= new ContractBiz(mContext);
		Contract contract = null;// biz.GetNextContract( contractId , modifyTime , otherCondition );
		return contract;
	}

	protected void Update(){
		ContractBiz biz = new ContractBiz(mContext);
		boolean isExist = biz.ExistContractByProjectNumAndProjectNameNotContrainSelf(
				mContract.getProjectnum(), mContract.getProjectname() , mContract.getContractid());
		if( isExist ){
			Message msg = mHandler.obtainMessage(msgfalse);
			msg.obj = "项目编号和项目名称已经存在，操作失败。";
			mHandler.sendMessage(msg);
			return ;
		}
		int status = biz.Update(mContract);
		if (status == ContractBiz.Failure) {
			Message msg = mHandler.obtainMessage(msgfalse);
			msg.obj = "修改操作失败";
			mHandler.sendMessage(msg);
		} else if (status == ContractBiz.UpdateSuccess) {
			Message msg = mHandler.obtainMessage(mMsg);
			//msg.obj = "修改操作成功";

			String contractId =String.valueOf( mContract.getContractid() );
			Date modifyTime = new Date(); //mContract.getModifytime();
			Contract nextContract  = GetNextContract(contractId, modifyTime, mOtherCondition );
			msg.obj = nextContract;
			mHandler.sendMessage(msg);
		}
	}

	@Override
	public void run() {
		try {
			if( mContract ==null ){
				throw new Exception("参数空异常");
			}

			if( mContract.getContractid() !=null ){
				Update();
			}else
			{
				Add();
			}

		} catch (Exception e) {
			Message msg = mHandler.obtainMessage(msgfalse);
			msg.obj = e.getMessage();
			mHandler.sendMessage(msg);
			e.printStackTrace();
		}
	}
}