package com.jxd.contractonlinems.thread;

import com.jxd.contractonlinems.biz.ContractBiz;

import android.content.Context;
import android.os.Handler;
import android.os.Message;

public class DeleteAllThread extends Thread {
	public static final int msgDeleteAllOK = 16;
	public static final int msgDeleteAllFalse = 17;
	private Context mContext = null;
	private Handler mHandler = null;
	public DeleteAllThread(Context context, Handler handler ) {
		mContext = context;
		mHandler = handler;
		}

	@Override
	public void run() {
		ContractBiz biz = new ContractBiz(mContext);
        int result = biz.DeleteAllData();
        Message msg = mHandler.obtainMessage( result > 0 ? msgDeleteAllOK: msgDeleteAllFalse);
        msg.obj = result>0 ? "É¾³ı³É¹¦":"É¾³ıÊ§°Ü";
        mHandler.sendMessage(msg);
		super.run();
	}
}
