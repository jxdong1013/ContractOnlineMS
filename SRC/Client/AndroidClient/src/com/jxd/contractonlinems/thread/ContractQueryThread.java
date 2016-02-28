package com.jxd.contractonlinems.thread;

import java.util.List;

import com.jxd.contractonlinems.biz.ContractBiz;
import com.jxd.contractonlinems.bean.Contract;
import com.jxd.contractonlinems.bean.ContractQueryForm;
import com.loopj.android.http.AsyncHttpResponseHandler;
import com.loopj.android.http.BaseJsonHttpResponseHandler;

import android.content.Context;
import android.os.Handler;
import android.os.Message;

public class ContractQueryThread extends Thread {
	Handler mHandler=null;
	ContractQueryForm mQueryForm = null;
	Context mContext=null;
	public static final int MSG_ContractQueryRefresh = 20;
	public static final int MSG_ContractQueryLoaded = 21;
	public static final int MSG_ContractQueryError = 22;
	AsyncHttpResponseHandler mResponseHandler =null;

	public ContractQueryThread(Context context,  Handler handler, ContractQueryForm queryForm ) {
		mContext = context;
		mHandler=handler;
		mQueryForm = queryForm;

	}

	@Override
	public void run() {
		try {
			ContractBiz biz = new ContractBiz(  mContext);
			List<Contract> list = null ;//biz.Query( mQueryForm );
			int count = biz.QueryCount( mQueryForm );

			if ( mQueryForm.isIsrefresh()) {
				Message msg = mHandler.obtainMessage(MSG_ContractQueryRefresh);
				msg.obj = list;
				msg.arg1=count;
				mHandler.sendMessage(msg);
			} else {
				Message msg = mHandler.obtainMessage(MSG_ContractQueryLoaded);
				msg.obj = list;
				msg.arg1 = count;
				mHandler.sendMessage(msg);
			}
		} catch (Exception e) {
			Message msg = mHandler.obtainMessage(MSG_ContractQueryError);
			msg.obj = e.getMessage() == null ? "·¢Éú´íÎó!" : e.getMessage();
			msg.arg1=0;
			mHandler.sendMessage(msg);
			return;
		}
	}
}


