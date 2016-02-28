package com.jxd.contractonlinems.thread;

import java.util.List;

import com.jxd.contractonlinems.biz.ContractBiz;
import com.jxd.contractonlinems.bean.Contract;

import android.content.Context;
import android.os.Handler;
import android.os.Message;


public class ContractListThread extends Thread {

		int _pageIdx = 0;
		boolean _isRefresh = false;
		Handler _handler = null;
		int PageSize = 50;
		public static final int MSG_Refresh = 1;
		public static final int MSG_Loaded = 2;
		public static final int MSG_Error = 0;
		Context _context = null;
		String _contractNum = "";
		String _contractName = "";
		String _uid = "";

		public ContractListThread(Context context, int pageidx, Handler handler,
				boolean isrefresh, String contractNum, String contractName , String uid) {

			_pageIdx = pageidx;
			_isRefresh = isrefresh;
			_handler = handler;
			_context = context;
			_contractNum = contractNum;
			_contractName = contractName;
			_uid = uid;
		}

		@Override
		public void run() {

//			try {
//				ContractBiz biz = new ContractBiz(_context);
//				List<Contract> list = biz.Query("",
//						_pageIdx, PageSize, _contractNum, _contractName, _uid);
//				int count = biz.QueryCount("", _contractNum, _contractName, _uid);
//
//				if (_isRefresh) {
//					Message msg = _handler.obtainMessage(MSG_Refresh);
//					msg.obj = list;
//					msg.arg1=count;
//					_handler.sendMessage(msg);
//				} else {
//					Message msg = _handler.obtainMessage(MSG_Loaded);
//					msg.obj = list;
//					msg.arg1 = count;
//					_handler.sendMessage(msg);
//				}
//			} catch (Exception e) {
//				Message msg = _handler.obtainMessage(MSG_Error);
//				msg.obj = e.getMessage() == null ? "·¢ËÍ´íÎó!" : e.getMessage();
//				msg.arg1=0;
//				_handler.sendMessage(msg);
//				return;
//			}
		}

}
