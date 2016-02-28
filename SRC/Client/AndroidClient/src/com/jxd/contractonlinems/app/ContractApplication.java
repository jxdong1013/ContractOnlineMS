package com.jxd.contractonlinems.app;

import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;

import com.jxd.contractonlinems.R;
import com.jxd.contractonlinems.bean.Menu;
import com.jxd.contractonlinems.bean.Person;
import com.jxd.contractonlinems.util.NFCUtil;
import com.jxd.contractonlinems.util.PreferencesUtils;

import android.app.Application;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;

/**
 * @author 向东
 *
 */
public class ContractApplication extends Application{
	public static ContractApplication mApp;
	//public String mUserName="";
	public static int CurrentVersionCode=0;
	public static String CurrentVersioname="";
	public static String SessionId = "";
	public Person mAccount=null;
	public List<Menu> LeftMenuList=null;
	public static LinkedHashMap<Integer, String> ExcelMap=null;
	public static String CookieId="cid";
	public static String CookieValue = "";
	public static String Host="";
	//public static String Port="80";

	/**
	 *
	 */
	public String mPassword="";

	@Override
	public void onCreate() {
		// TODO Auto-generated method stub
		super.onCreate();
		mApp = this;
		GetVersionInfo();

		Host = PreferencesUtils.getString(mApp, "Url");
		if( Host ==null || Host.isEmpty() || Host =="" )
		{
			Host="http://htgl.bistu.edu.cn/contractWeb/";
		}

		//Port=PreferencesUtils.getString(mApp, "Port");

		InitMenu();

		InitExcelMaps();
	}


	@Override
	public void onTerminate() {
		// TODO Auto-generated method stub
		NFCUtil.Close();
		super.onTerminate();
	}
	/*
	 * 获得APK的版本信息
	 */
	protected void GetVersionInfo(){
		PackageManager manager = this.getPackageManager();
		try {
			PackageInfo pkgInfo = manager.getPackageInfo(this.getPackageName(), PackageManager.GET_CONFIGURATIONS);
			CurrentVersionCode = pkgInfo.versionCode;
			CurrentVersioname = pkgInfo.versionName;
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	protected void InitMenu()	{
			LeftMenuList = new ArrayList<Menu>();
			Menu m = new Menu(R.string.menuRegister, this.getString(R.string.menuRegister), R.drawable.navigation_contract, false);
			LeftMenuList.add(m);
			//m = new Menu(R.string.menuBorrow, this.getString(R.string.menuBorrow), R.drawable.navigation_borrow, false);
			//LeftMenuList.add(m);
			//m = new Menu(R.string.menushelve, this.getString(R.string.menushelve), R.drawable.navigation_shelve, false);
			//LeftMenuList.add(m);
			m = new Menu(R.string.menuContractQuery, this.getString(R.string.menuContractQuery), R.drawable.navigation_search, false);
			LeftMenuList.add(m);
			//m = new Menu(R.string.menuBackup, this.getString(R.string.menuBackup ), R.drawable.navigation_backup, false);
			//LeftMenuList.add(m);
			//m = new Menu(R.string.menuRestore, this.getString(R.string.menuRestore), R.drawable.navigation_restore, false);
			//LeftMenuList.add(m);

	}

	protected void InitExcelMaps(){
		ExcelMap = new LinkedHashMap<Integer, String>();

		ExcelMap.put(0,  "contractNo" );
		ExcelMap.put(1, "seq");
		ExcelMap.put(2,"projectNo");
		ExcelMap.put(3,"projectName");

		ExcelMap.put(4,"projectManager");
		ExcelMap.put(5, "tel");
		ExcelMap.put(6,"depart");

		ExcelMap.put(12,"paymethod");
		ExcelMap.put(13,"bCompany");
		ExcelMap.put(14,"linker");
		ExcelMap.put(16,"money");
		ExcelMap.put(17, "signingDate");
	}
}
