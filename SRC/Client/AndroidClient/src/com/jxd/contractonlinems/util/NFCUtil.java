package com.jxd.contractonlinems.util;

import android.util.Log;

import com.android.hdhe.nfc.NFCcmdManager;

public class NFCUtil {
	private static int mPort = 13;
	private static int mBandRate = 115200;
	private static int mFlag = 0;
	private static NFCcmdManager mInstance = null;

	public static NFCcmdManager Single() {
		if (mInstance == null) {
			try {
				mInstance = NFCcmdManager.getNFCcmdManager(mPort, mBandRate,
						mFlag);
				if( mInstance == null) return null;
			} catch (SecurityException e) {
				e.printStackTrace();
				return null;
			}
			PowerOn();
		}
		return mInstance;
	}

	public static String GetVersion() {
		return Single().getVersion();
	}

	private static boolean PowerOn() {
		boolean ison = Single().readerPowerOn();
		Log.i("poweron:", String.valueOf(ison));
		return ison;
	}

	private static boolean PowerOff() {
		boolean isoff = Single().readerPowerOff();
		Log.i("poweroff:", String.valueOf(isoff));
		return isoff;
	}

	private static boolean CancleInit15693() {
		boolean cancel = Single().deinit15693();
		Log.i("deinit15693:", String.valueOf(cancel));
		return cancel;
	}

	private static boolean Init15693() {
		boolean init = Single().init15693();
		Log.i("init15693:", String.valueOf(init));
		return init;
	}

	private static String Inventory15693() {
		int inventoryCount = 30;
		int idx = 0;

		while (idx < inventoryCount) {
			byte[] buffer = Single().inventory15693();
			if (buffer == null || buffer.length < 1) {
				Log.i("inventory15693:", "Пе");
				idx++;
				try {
					Thread.sleep(50);
				} catch (InterruptedException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			} else {
				String uid = ByteUtil.Bytes2HexString(buffer, buffer.length);
				Log.i("inventory15693:", uid);
				return uid;
			}
		}
		return "";
	}

	public static String GetUID2() {
		if( Single() == null ) return "";
		int inventoryCount = 25;
		int idx = 0;
		while (idx < inventoryCount) {
			CancleInit15693();
			Init15693();
			String uid = Inventory15693_2();
			if( uid.isEmpty() == false && uid.length()>4 ) return uid;
            idx++;
			try {
				Thread.sleep(50);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		return "";
	}

	protected static String Inventory15693_2() {
		byte[] buffer = Single().inventory15693();
		if (buffer == null || buffer.length < 1) {
			Log.i("inventory15693:", "Пе");
			return "";
		} else {
			String uid = ByteUtil.Bytes2HexString(buffer, buffer.length);
			Log.i("inventory15693:", uid);
			return uid;
		}
	}

	public static String GetUID() {
		CancleInit15693();
		Init15693();
		return Inventory15693();

		// int inventoryCount = 50;
		// int idx = 0;
		// while (idx < inventoryCount) {
		// CancleInit15693();
		// Init15693();
		// String uid = Inventory15693();
		// if( uid == ""){
		// idx++;
		// continue;
		// }
		// else
		// {
		// return uid;
		// }
		//
		// }
		// return "";
	}

	public static void Close() {
		if (Single() != null) {
			PowerOff();
			Single().close(mPort);
			mInstance = null;
		}
	}
}
