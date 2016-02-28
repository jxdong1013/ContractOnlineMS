package com.jxd.contractonlinems.receiver;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.widget.Toast;

public class FunKeyReceiver extends BroadcastReceiver {
	public static int MsgF1 = 30;
	public static int MsgF2=31;

	private Handler _handler =null;

	public FunKeyReceiver( Handler handler ){
		_handler= handler;
	}

	@Override
	public void onReceive(Context context, Intent intent ) {
		boolean defaultdown=false;
        int keycode = intent.getIntExtra("keycode", 0);
        boolean keydown = intent.getBooleanExtra("keydown", defaultdown);
        Log.i("ServiceDemo", "receiver:keycode="+keycode+"keydown="+keydown);
        if(keycode == 133 && keydown){
        	Toast.makeText( context, "这是左侧下按键", Toast.LENGTH_SHORT).show();
        }

        if(keycode == 134 && keydown){
        	Toast.makeText( context, "这是右侧按键", Toast.LENGTH_SHORT).show();
        }

        if(keycode == 131 && keydown){
        	//Toast.makeText( context, "这是F1按键", 0).show();
        	Message msg = _handler.obtainMessage( MsgF1);
        	_handler.sendMessage(msg);
        	//new ScanThread(_handler, context).start();
        }

        if(keycode == 132 && keydown){
        	//Toast.makeText( context, "这是F2按键", 0).show();
        	Message msg = _handler.obtainMessage(MsgF2);
        	_handler.sendMessage(msg);
        }


	}

}
