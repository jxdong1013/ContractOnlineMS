package com.jxd.contractonlinems.util;

import com.jxd.contractonlinems.app.ContractApplication;

import android.content.Context;
import android.net.ConnectivityManager;

public class WifiUtil {
	public static Boolean CheckWifi(){
		 final ConnectivityManager connMgr = (ConnectivityManager) ContractApplication.mApp.getSystemService(Context.CONNECTIVITY_SERVICE);

	        final android.net.NetworkInfo wifi =connMgr.getNetworkInfo(ConnectivityManager.TYPE_WIFI);
	        //final android.net.NetworkInfo mobile =connMgr.getNetworkInfo(ConnectivityManager.TYPE_MOBILE);

	        if(wifi.isAvailable())
	            return true;
	        else
	            return false;
	}
}
