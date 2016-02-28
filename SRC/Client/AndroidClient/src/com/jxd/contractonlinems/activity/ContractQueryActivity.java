package com.jxd.contractonlinems.activity;

import java.util.ArrayList;
import java.util.List;

import com.jxd.contractonlinems.R;
import com.jxd.contractonlinems.adapter.MenuListAdapter;
import com.jxd.contractonlinems.bean.ContractQueryForm;
//import com.jxd.contractonlinems.bean.ContractStateEnum;
import com.jxd.contractonlinems.bean.Menu;
import com.jxd.contractonlinems.receiver.FunKeyReceiver;
import com.jxd.contractonlinems.thread.ScanRFIDThread;
import com.jxd.contractonlinems.util.ToastUtil;
import com.jxd.contractonlinems.app.ContractApplication;
import com.jxd.contractonlinems.widget.RightClickEditText;
import android.app.ProgressDialog;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.View;
import android.view.View.OnClickListener;
//import android.view.Window;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.RadioButton;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemClickListener;

public class ContractQueryActivity extends BaseActivity implements OnClickListener {

	Button mbtnQuery =null;
	Button mbtnScan=null;
	EditText metSeq=null;
	EditText metcontractno=null;
	EditText metprojectno=null;
	EditText metprojectname=null;
	EditText metbcompany=null;
	RightClickEditText metcontractrfid=null;
	EditText metcontractplace=null;
	EditText metmoney =null;
	RadioButton rbIn=null;
	RadioButton rbOut=null;
	RadioButton rbAll=null;
	FunKeyReceiver mKeyReceiver=null;
	QueryHandler mHandler=null;
	ProgressDialog mProgressDlg = null;
	TextView mTvUser =null;
	TextView mTvTitle=null;
	Button mBtnMenu=null;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		//requestWindowFeature(Window.FEATURE_NO_TITLE);

		this.setContentView(R.layout.activity_contract_query);

       mHandler = new QueryHandler();
		mbtnQuery=(Button)this.findViewById(R.id.contractqueryquery);
		mbtnScan = (Button)this.findViewById(R.id.contractqueryscan);
		metSeq=(EditText)this.findViewById(R.id.contractqueryseq);
		metcontractno=(EditText)this.findViewById(R.id.contractqueryno);
		metprojectno=(EditText)this.findViewById(R.id.contractqueryprojectno);
		metprojectname=(EditText)this.findViewById(R.id.contractqueryprojectname);
		metcontractrfid=(RightClickEditText)this.findViewById(R.id.contractqueryrfid);
		metcontractplace =(EditText)this.findViewById(R.id.contractqueryplace);
		metbcompany = (EditText)this.findViewById(R.id.contractquerybcompany);
		metmoney = (EditText)this.findViewById(R.id.contractquerymoney);
		//rbIn =(RadioButton)this.findViewById(R.id.contractqueryinstore);
		//rbOut =(RadioButton)this.findViewById(R.id.contractqueryoutstore);
		//rbAll =(RadioButton)this.findViewById(R.id.contractqueryall);

		mbtnQuery.setOnClickListener(this);
		mbtnScan.setOnClickListener(this);

		mTvUser = (TextView)this.findViewById(R.id.tvUser);
		mTvUser.setText( "用户:"+ContractApplication.mApp.mAccount.getUsername() );
		mTvTitle=(TextView)this.findViewById(R.id.tvAppTitle);
		mTvTitle.setText(this.getString(R.string.menuContractQuery));

		mBtnMenu=(Button)this.findViewById(R.id.btnMenu);
		mBtnMenu.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				if(mLeftMenu.isMenuShowing())
				{
					mLeftMenu.showContent();

				}else{
					mLeftMenu.showMenu();
				}
			}
		});

		CreateLeftMenu();

		mLvLeftMenu=(ListView)this.findViewById(R.id.menulist);
		mLvLeftMenu.setOnItemClickListener(new OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,
					long arg3) {

				if( arg2 <0 || ContractApplication.mApp.LeftMenuList==null )return;

				Menu menuL = ContractApplication.mApp.LeftMenuList.get(arg2);
				if( menuL==null) return;

				if( menuL.getMenuId() == R.string.menuRegister ){
					ContractQueryActivity.this.finish();
				 Intent intent = new Intent();
				 intent.setClass(ContractQueryActivity.this, ContractRegisterActivity.class);
				 ContractQueryActivity.this.startActivity(intent);
				}
//				else if(menuL.getMenuId() == R.string.menuBorrow){
//					ContractQueryActivity.this.finish();
//					 Intent intent = new Intent();
//					 intent.setClass(ContractQueryActivity.this, ContractBorrowActivity.class);
//					 ContractQueryActivity.this.startActivity(intent);
//				}
//				else if( menuL.getMenuId() == R.string.menuBack){
//					ContractQueryActivity.this.finish();
//					 Intent intent = new Intent();
//					 intent.setClass(ContractQueryActivity.this, ContractBackActivity.class);
//					 ContractQueryActivity.this.startActivity(intent);
//				}
				else if( menuL.getMenuId() == R.string.menuContractQuery){
					mLeftMenu.showContent();
				}
//				else if( menuL.getMenuId() == R.string.menuInventory){
//					ContractQueryActivity.this.finish();
//					 Intent intent = new Intent();
//					 intent.setClass(ContractQueryActivity.this, ContractInventoryActivity.class);
//					 ContractQueryActivity.this.startActivity(intent);
//				}
//				else if( menuL.getMenuId() == R.string.menushelve){
//					ContractQueryActivity.this.finish();
//					Intent intent = new Intent();
//					intent.setClass(ContractQueryActivity.this, ContractShelveListActivity.class);
//					ContractQueryActivity.this.startActivity(intent);
//				}
			}
		});
	}



	@Override
	protected void SetMenuData() {
		// TODO Auto-generated method stub
		List<Menu> menus = new ArrayList<Menu>();
		Menu m = new Menu(R.string.menuRegister, this.getString(R.string.menuRegister), R.drawable.navigation_contract, false);
		menus.add(m);
		//m = new Menu(R.string.menushelve, this.getString(R.string.menushelve), R.drawable.navigation_shelve, false);
		//menus.add(m);
		m = new Menu(R.string.menuContractQuery, this.getString(R.string.menuContractQuery), R.drawable.navigation_search, true);
		menus.add(m);

		mLeftMenuAdapter = new MenuListAdapter( menus , this);
		mLvLeftMenu.setAdapter(mLeftMenuAdapter);
		mLeftMenuAdapter.notifyDataSetChanged();
	}


	@Override
	protected void onResume() {
		super.onResume();

		if (mKeyReceiver == null) {
			mKeyReceiver = new FunKeyReceiver(mHandler);
		}
		IntentFilter filter = new IntentFilter();
		filter.addAction("android.intent.action.FUN_KEY");
		registerReceiver(mKeyReceiver, filter);
	}

	@Override
	protected void onPause() {
		if (mKeyReceiver != null) {
			unregisterReceiver(mKeyReceiver);
		}
		super.onPause();
	}

	@Override
	public void onClick(View v) {
		 if(v==mbtnScan){
			 	ScanRFID();
			}else if( v== mbtnQuery){
				Query();
		 }
	}

	protected void Query(){
		Intent intent = new Intent();
		String seq = metSeq.getText().toString().trim();
		String contractno = metcontractno.getText().toString().trim();
		String projectno = metprojectno.getText().toString().trim();
		String projectname = metprojectname.getText().toString().trim();
		String contractplace = metcontractplace.getText().toString().trim();
		String contractbcompany=metbcompany.getText().toString().trim();
		String contractrfid = metcontractrfid.getText().toString().trim();
		String contractmoney =metmoney.getText().toString().trim();
		//boolean inState = rbIn.isChecked();
		//boolean outState = rbOut.isChecked();
		//boolean allState = rbAll.isChecked();
//		int contractState = ContractStateEnum.AllStore.getCode();
//		if( inState){
//			contractState = ContractStateEnum.InStore.getCode();
//		}
//		if( outState){
//			contractState= ContractStateEnum.OutStore.getCode();
//		}
//		if( allState){
//			contractState= ContractStateEnum.AllStore.getCode();
//		}

		ContractQueryForm queryForm = new ContractQueryForm();
		queryForm.setSeq( seq);
		queryForm.setbCompany(contractbcompany);
		queryForm.setProjectNo(projectno);
		queryForm.setProjectName(projectname);
		queryForm.setContractNum(contractno);
		queryForm.setContractPlace(contractplace);
		queryForm.setContractRFID(contractrfid);
		//queryForm.setContractState( String.valueOf( contractState));
		queryForm.setMoney(contractmoney);
		queryForm.setIsrefresh(true);
		queryForm.setPageidx(0);
		intent.putExtra("queryForm", queryForm);

		ContractQueryActivity.this.finish();
		intent.setClass(ContractQueryActivity.this,  ContractListActivity.class);
		ContractQueryActivity.this.startActivity(intent);
	}

	/**
	 * 扫描RFID标签
	 */
	protected void ScanRFID() {
		ShowProgressDialog(getString(R.string.scantip));
		new ScanRFIDThread(mHandler, ContractQueryActivity.this).start();
	}
	protected void ShowProgressDialog(String message) {
		if (mProgressDlg != null) {
			mProgressDlg.dismiss();
			mProgressDlg = null;
		}
		mProgressDlg = new ProgressDialog(ContractQueryActivity.this);
		mProgressDlg.setMessage(message);
		mProgressDlg.show();
	}

	protected void CloseProgressDialog(){
		if (mProgressDlg != null) {
			mProgressDlg.dismiss();
			mProgressDlg = null;
		}
	}

	class QueryHandler extends Handler{
		@Override
		public void handleMessage(Message msg) {
			try {
				if (msg.what == FunKeyReceiver.MsgF1){
					ScanRFID();
				}else if( msg.what == FunKeyReceiver.MsgF2){
					Query();
				}else if (msg.what == ScanRFIDThread.MsgScanOK) {
					CloseProgressDialog();
					String uid = msg.obj.toString();
					metcontractrfid.setText(uid);
				} else if (msg.what == ScanRFIDThread.MsgScanFalse) {
					CloseProgressDialog();
					String message = msg.obj.toString();

					Toast.makeText(ContractQueryActivity.this, message,
					Toast.LENGTH_LONG).show();
				}

	} catch (Exception e) {
		ToastUtil.Show( "handlemessage出错了!");
	}
		}
	}
}
