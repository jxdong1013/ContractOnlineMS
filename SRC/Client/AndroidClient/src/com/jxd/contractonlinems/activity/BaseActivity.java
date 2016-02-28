package com.jxd.contractonlinems.activity;

import com.jeremyfeinstein.slidingmenu.lib.SlidingMenu;
import com.jxd.contractonlinems.R;
import com.jxd.contractonlinems.adapter.MenuListAdapter;
import com.jxd.contractonlinems.util.CommonUtil;
import com.jxd.contractonlinems.app.ContractApplication;
import android.app.Activity;
import android.os.Bundle;
import android.widget.ListView;

public class BaseActivity extends Activity {
	protected SlidingMenu mLeftMenu=null;
	protected ListView mLvLeftMenu=null;
	protected MenuListAdapter mLeftMenuAdapter=null;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
	}

	protected void CreateLeftMenu(){
		mLeftMenu = new SlidingMenu(this);
		mLeftMenu.setMode(SlidingMenu.LEFT);
		mLeftMenu.setBehindOffsetRes(R.dimen.slidingmenu_offset);
		mLeftMenu.setFadeDegree(0.35f);
		mLeftMenu.setTouchModeAbove(SlidingMenu.TOUCHMODE_FULLSCREEN);
		mLeftMenu.attachToActivity(this, SlidingMenu.SLIDING_CONTENT);
		mLeftMenu.setMenu(R.layout.layout_leftmenu);
		mLvLeftMenu=(ListView)this.findViewById(R.id.menulist);

		CommonUtil.SetLeftMenuSelected(ContractApplication.mApp.LeftMenuList, -1 ,true);
		SetMenuData();
	}

	protected void SetMenuData(){
		mLeftMenuAdapter = new MenuListAdapter( ContractApplication.mApp.LeftMenuList , this);
		mLvLeftMenu.setAdapter(mLeftMenuAdapter);
		mLeftMenuAdapter.notifyDataSetChanged();
	}
}
