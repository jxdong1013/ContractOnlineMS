package com.jxd.contractonlinems.thread;

import com.jxd.contractonlinems.util.NFCUtil;

import android.content.Context;
import android.os.Handler;
import android.os.Message;

public class ScanRFIDThread extends Thread {
	private Handler _handler =null;
	private Context _context =null;
	public static int MsgScanOK = 14;
	public static int MsgScanFalse=15;

	public ScanRFIDThread( Handler handler , Context context ){
		_handler = handler;
		_context = context;
		}
		@Override
		public void run() {
			if( NFCUtil.Single() == null ){
				Message msg = _handler.obtainMessage(MsgScanFalse);
				msg.obj="初始化失败无法扫描标签";
				 _handler.sendMessage(msg);
				 return;
			}
			String uid = NFCUtil.GetUID2();
			if (uid.isEmpty() == false) {
				Message msg = _handler.obtainMessage(MsgScanOK);
				msg.obj = uid;
				_handler.sendMessage(msg);
			}else {
				Message msg = _handler.obtainMessage(MsgScanFalse);
				msg.obj = "没有扫描到标签";
				_handler.sendMessage(msg);
			}
		}
	}

