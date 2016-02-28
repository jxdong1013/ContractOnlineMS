package com.jxd.contractonlinems.thread;

import android.content.Context;
import android.os.Handler;
import android.os.Message;
import com.jxd.contractonlinems.biz.ContractBiz;
import com.jxd.contractonlinems.util.NFCUtil;

public class ScanAndCheckThread extends Thread {
	private Handler _handler =null;
	private Context _context =null;
	public static int MsgScanOK = 8;
	public static int MsgScanFalse = 9;

	public ScanAndCheckThread( Handler handler , Context context ){
		_handler = handler;
		_context = context;
	}

	@Override
	public void run() {
		if( NFCUtil.Single() == null ){
			Message msg = _handler.obtainMessage(MsgScanFalse);
			msg.obj="��ʼ��ʧ���޷�ɨ���ǩ";
			 _handler.sendMessage(msg);
			 return;
		}
		String uid = NFCUtil.GetUID2();
		if (uid.isEmpty() == false) {
			ContractBiz biz = new ContractBiz( _context );
			boolean isExist = biz.ExistUID(uid);
			if (isExist) {
				Message msg = _handler.obtainMessage(MsgScanFalse);
				msg.obj = "��ǩ�Ѿ���ʹ��";
				_handler.sendMessage(msg);
			} else {
				Message msg = _handler.obtainMessage(MsgScanOK);
				msg.obj = uid;
				_handler.sendMessage(msg);
			}
		} else {
			Message msg = _handler.obtainMessage(MsgScanFalse);
			msg.obj = "û��ɨ�赽��ǩ";
			_handler.sendMessage(msg);
		}
	}
}
