package com.jxd.contractonlinems.thread;

import java.util.List;

import com.jxd.contractonlinems.biz.ContractBiz;

import android.content.Context;
import android.os.Handler;
import android.os.Message;

public class DeleteContractThread extends Thread {
	public static final int msgDeleteOK = 10;
	public static final int msgDeleteFalse = 11;
	private Context mContext = null;
	private Handler mHandler = null;
    private List<Integer> mContractIdList = null;
	public DeleteContractThread(Context context, Handler handler , List<Integer> contractIdList ) {
		mContext = context;
		mHandler = handler;
		mContractIdList = contractIdList;
	}

	@Override
	public void run() {
		ContractBiz biz = new ContractBiz(mContext);
        int result = biz.DeleteByIds(mContractIdList);
        Message msg = mHandler.obtainMessage( result > 0 ? msgDeleteOK: msgDeleteFalse);
        msg.obj = result>0 ? "É¾³ý³É¹¦":"É¾³ýÊ§°Ü";
        mHandler.sendMessage(msg);
		super.run();
	}
}
