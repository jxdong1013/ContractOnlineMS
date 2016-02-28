package com.jxd.contractonlinems.activity;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.List;
import java.util.regex.Pattern;

import com.jxd.contractonlinems.R;
import com.jxd.contractonlinems.adapter.MenuListAdapter;
import com.jxd.contractonlinems.bean.Contract;
import com.jxd.contractonlinems.bean.JsonResult;
//import com.jxd.contractonlinems.bean.ContractStateEnum;
import com.jxd.contractonlinems.bean.Menu;
import com.jxd.contractonlinems.common.IShowData;
import com.jxd.contractonlinems.handler.RegisterHttpResponseHandler;
import com.jxd.contractonlinems.receiver.FunKeyReceiver;
//import com.jxd.contractonlinems.thread.SaveContractThread;
import com.jxd.contractonlinems.thread.ScanAndCheckThread;
import com.jxd.contractonlinems.util.AsyncHttpUtil;
import com.jxd.contractonlinems.util.ToastUtil;
import com.jxd.contractonlinems.app.ContractApplication;
import com.loopj.android.http.RequestParams;

import android.app.ProgressDialog;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.KeyEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.Button;
import android.widget.EditText;
//import android.widget.RadioButton;
import android.widget.TextView;

/*
 * 登记
 */
public class ContractRegisterActivity extends BaseActivity implements OnClickListener {
	Contract mContract=null;
	EditText mETseq = null;
	EditText mETcontractNum = null;
	EditText mETprojectNum=null;
	EditText mETprojectName = null;
	EditText mETprojectManager=null;
	EditText mETtel =null;
	EditText mETdepart=null;
	EditText mETlinker= null;
	EditText mETpaymethod=null;
	EditText mETmoney =null;
	EditText mETbcompany=null;
	EditText mETsigningdate=null;
	EditText mETcontractPlace = null;
	EditText mETcontractRFID = null;
	//RadioButton rbInstore = null;
	//RadioButton rbOutstore=null;
	TextView mTvTitle=null;
	//CheckBox chkUnShelve =null;
	Button mbtnScanrfid = null;
	Button mbtnSaveNext = null;
	Button mbtnCancel = null;
	int msgfalse = 0;
	int msgSaveQuit = 1;
	int msgSaveNext = 2;
	Button mbtnMenu=null;
	TextView mTvUser=null;
	static ProgressDialog mProgressDlg = null;
	MyHandler mHandler = null;
	FunKeyReceiver mKeyReceiver = null;
	String mSqlOtherCondition =null;
	RegisterShowData mRegisterShowData=null;
	RegisterHttpResponseHandler mRegisterHttpHandler=null;


	@Override
	protected void onCreate(Bundle savedInstanceState) {



		//requestWindowFeature(Window.FEATURE_NO_TITLE);

		setContentView(R.layout.activity_contractregister);

		Init();

		super.onCreate(savedInstanceState);
	}

	@Override
	protected void SetMenuData() {
		// TODO Auto-generated method stub
		List<Menu> menus = new ArrayList<Menu>();
		Menu m = new Menu(R.string.menuRegister, this.getString(R.string.menuRegister), R.drawable.navigation_contract, true);
		menus.add(m);
		//m = new Menu(R.string.menushelve, this.getString(R.string.menushelve), R.drawable.navigation_shelve, false);
		//menus.add(m);
		m = new Menu(R.string.menuContractQuery, this.getString(R.string.menuContractQuery), R.drawable.navigation_search, false);
		menus.add(m);

		mLeftMenuAdapter = new MenuListAdapter( menus , this);
		mLvLeftMenu.setAdapter(mLeftMenuAdapter);
		mLeftMenuAdapter.notifyDataSetChanged();
	}

	@Override
	protected void onPause() {
		if (mKeyReceiver != null) {
			unregisterReceiver(mKeyReceiver);
		}

		ToastUtil.Cancel();

		super.onPause();
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

		if (getIntent().hasExtra("contract") == false)	return;
		Contract contract = (Contract) getIntent().getSerializableExtra("contract");
		SetContract(contract);

		if( getIntent().hasExtra("otherCondition") ){
			mSqlOtherCondition = getIntent().getStringExtra("otherCondition");
		}else{
			mSqlOtherCondition= null;
		}
	}

	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {
		if (keyCode == KeyEvent.KEYCODE_BACK
				&& event.getAction() == KeyEvent.ACTION_DOWN) {
			QuitUI();
		}
		return true;
	}


	protected void SetContract(Contract contract ){
		mContract = contract;
		if (contract == null) return;
		  //mbtnCancel.setText( contract.getModifytime());

			mETseq.setText(contract.getSeq());
			mETcontractNum.setText(contract.getContractnum());
			mETprojectNum.setText(contract.getProjectnum());
			mETprojectName.setText(contract.getProjectname());
			mETprojectManager.setText(contract.getProjectmanager());
			mETtel.setText(contract.getTel());
			mETdepart.setText(contract.getDepart());
			mETlinker.setText(contract.getLinker());
			mETpaymethod.setText(contract.getPaymethod());
			mETmoney.setText(contract.getMoney());
			mETbcompany.setText(contract.getBcompany());
			mETsigningdate.setText(contract.getSigningdate());

			mETcontractPlace.setText(contract.getContractplace());
			mETcontractRFID.setText(contract.getContractrfid());

			//if( contract.getContractState().equals(ContractStateEnum.InStore.getCode().toString() ) ){
			//	rbInstore.setChecked(true);
			//}
			//else if( contract.getContractState().equals(ContractStateEnum.OutStore.getCode().toString())){
			//	rbOutstore.setChecked(true);
			//}


			//if(  Boolean.parseBoolean( contract.getUnshelve() )){
			//	chkUnShelve.setChecked(  true);
			//}else {
			//	chkUnShelve.setChecked(false);
			//}
	}

	private void Init() {
		mHandler = new MyHandler();

		mTvTitle = (TextView)this.findViewById(R.id.tvAppTitle);
		mTvTitle.setText( this.getString(R.string.menuRegister));
		mETseq = (EditText)this.findViewById(R.id.seq);
		mETcontractNum = (EditText) this.findViewById(R.id.contractnum);
		mETprojectNum = (EditText)this.findViewById(R.id.projectNo);
		mETprojectName = (EditText) this.findViewById(R.id.projectname);
		mETprojectManager = (EditText)this.findViewById(R.id.projectManager);
		mETtel = (EditText)this.findViewById(R.id.projectTel);
		mETdepart = (EditText)this.findViewById(R.id.projectDepart);
		mETlinker = (EditText)this.findViewById(R.id.projectLinker);
		mETpaymethod = (EditText)this.findViewById(R.id.projectPayMethod);
		mETmoney = (EditText)this.findViewById(R.id.projectMoney);
		mETbcompany = (EditText)this.findViewById(R.id.contractcompanyb);
		mETsigningdate=(EditText)this.findViewById(R.id.contractsigningdate);

		//rbInstore = (RadioButton)this.findViewById(R.id.rbInStore);
		//rbInstore.setChecked(true);
		//rbOutstore = (RadioButton)this.findViewById(R.id.rbOutStore);
		//rbOutstore.setChecked(false);

		mETcontractPlace = (EditText) this.findViewById(R.id.contractplace);
		mETcontractRFID = (EditText) this.findViewById(R.id.rfiduid);

		//mbtnScanrfid = (Button) this.findViewById(R.id.btnScanRfid);
		//mbtnScanrfid.setOnClickListener(this);
		mbtnSaveNext = (Button) this.findViewById(R.id.savenext);
		mbtnSaveNext.setOnClickListener(this);
		mbtnCancel = (Button) this.findViewById(R.id.cancelquit);
		mbtnCancel.setOnClickListener(this);

		//chkUnShelve = (CheckBox)this.findViewById(R.id.projectUnShelve);
		//chkUnShelve.setOnClickListener(this);

		CreateLeftMenu();

		mLvLeftMenu.setOnItemClickListener(new OnItemClickListener() {

			@Override
			public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,
					long arg3) {

				if( arg2 <0 || ContractApplication.mApp.LeftMenuList ==null )return;

				Menu menuL = ContractApplication.mApp.LeftMenuList.get(arg2);
				if( menuL==null) return;

				if( menuL.getMenuId() == R.string.menuRegister ){
					if(mLeftMenu.isMenuShowing()) mLeftMenu.showContent();
				}
//				else if( menuL.getMenuId() == R.string.menuBack){
//
//					ContractRegisterActivity.this.finish();
//					 Intent intent = new Intent();
//					 intent.setClass(ContractRegisterActivity.this, ContractBackActivity.class);
//					 ContractRegisterActivity.this.startActivity(intent);
//				}
				else if( menuL.getMenuId() == R.string.menuContractQuery){

					ContractRegisterActivity.this.finish();
					 Intent intent = new Intent();
					 intent.setClass(ContractRegisterActivity.this, ContractQueryActivity.class);
					 ContractRegisterActivity.this.startActivity(intent);
				}
//				else if( menuL.getMenuId() == R.string.menushelve){
//					ContractRegisterActivity.this.finish();
//					Intent intent = new Intent();
//					intent.setClass(ContractRegisterActivity.this, ContractShelveListActivity.class);
//					ContractRegisterActivity.this.startActivity(intent);
//				}
			}
		});

		mTvUser = (TextView)this.findViewById(R.id.tvUser);
		mTvUser.setText("用户:" + ContractApplication.mApp.mAccount.getUsername());

		mbtnMenu=(Button)this.findViewById(R.id.btnMenu);
		mbtnMenu.setOnClickListener(this);

		mRegisterShowData = new RegisterShowData();
		mRegisterHttpHandler = new RegisterHttpResponseHandler(this ,  mRegisterShowData);
	}

	protected void ClearText() {
		mETseq.setText("");
		mETcontractNum.setText("");
		mETprojectNum.setText("");
		mETprojectName.setText("");
		mETprojectManager.setText("");
		mETlinker.setText("");
		mETdepart.setText("");
		mETtel.setText("");
		mETpaymethod.setText("");
		mETmoney.setText("");
		mETbcompany.setText("");
		mETsigningdate.setText("");

		mETcontractPlace.setText("");
		mETcontractRFID.setText("");

		//rbInstore.setChecked(true);
		//rbOutstore.setChecked(false);

		//chkUnShelve.setChecked(false);

		mETseq.requestFocus();
	}

	private Contract GetContractByUI() {
		if( mContract==null)
		{
			mContract= new Contract();
		}
		mContract.setSeq(mETseq.getText().toString().trim());
		mContract.setContractnum(mETcontractNum.getText().toString().trim());
		mContract.setProjectnum(mETprojectNum.getText().toString().trim());
		mContract.setProjectname(mETprojectName.getText().toString().trim());
		mContract.setProjectmanager(mETprojectManager.getText().toString().trim());
		mContract.setLinker(mETlinker.getText().toString().trim());
		mContract.setTel(mETtel.getText().toString().trim());
		mContract.setDepart(mETdepart.getText().toString().trim());
		mContract.setPaymethod(mETpaymethod.getText().toString().trim());
		mContract.setMoney(mETmoney.getText().toString().trim());
		mContract.setBcompany(mETbcompany.getText().toString().trim());
		mContract.setSigningdate(mETsigningdate.getText().toString().trim());

		mContract.setContractplace(mETcontractPlace.getText().toString().trim());
		mContract.setContractrfid(mETcontractRFID.getText().toString().trim());
		mContract.setOperatorname(ContractApplication.mApp.mAccount.getUsername());

		//if( rbInstore.isChecked()){
		//	mContract.setContractState( ContractStateEnum.InStore.getCode().toString() );
		//}else{
		//	mContract.setContractState( ContractStateEnum.OutStore.getCode().toString());
		//}

		//mContract.setUnshelve( String.valueOf( chkUnShelve.isChecked() ));
		mContract.setUnshelve("false");

		return mContract;
	}

	protected boolean CheckData() {
		Contract contract = GetContractByUI();

		if (contract.getSeq().isEmpty()) {
			ToastUtil.Show("请输入序号");
			mETseq.requestFocus();
			return false;
		}

		if (contract.getContractnum().isEmpty()) {
			ToastUtil.Show("请输入合同编号");
			mETcontractNum.requestFocus();
			return false;
		}

		if (contract.getProjectnum().isEmpty()) {
			ToastUtil.Show("请输入项目编号");
			mETprojectNum.requestFocus();
			return false;
		}

		if (contract.getContractnum().length() > 15 ) {
			ToastUtil.Show("合同编号长度不能超过15位");
			mETcontractNum.requestFocus();
			return false;
		}
		if (contract.getProjectname().isEmpty()) {
			ToastUtil.Show("请输入项目名称");
			mETprojectName.requestFocus();
			return false;
		}

		if( contract.getProjectname().length()> 30){
			ToastUtil.Show("项目名称长度不能超过30位");
			mETprojectName.requestFocus();
			return false;
		}

		if ( contract.getUnshelve().equals("false") && contract.getContractplace().isEmpty()) {
			ToastUtil.Show("请输入合同存放位置");
			mETcontractPlace.requestFocus();
			return false;
		}
		if( contract.getContractplace().length()>10){
			ToastUtil.Show("合同存放位置长度不能超过15位");
			mETcontractPlace.requestFocus();
			return false;
		}

//		if( contract.getContractrfid().isEmpty() ){
//			ToastUtil.Show("请扫描标签号");
//			return false;
//		}

		return true;
	}

	protected void ShowProgressDialog(String message) {
		if (mProgressDlg != null) {
			mProgressDlg.dismiss();
			mProgressDlg = null;
		}
		mProgressDlg = new ProgressDialog(ContractRegisterActivity.this);
		mProgressDlg.setMessage(message);
		mProgressDlg.show();
	}

	/**
	 *
	 */
	protected void SaveNext() {
		if (CheckData() == false) return;
		ShowProgressDialog(getString(R.string.savetip));

		String url = String.format("%sContractRestfull/SaveContract", ContractApplication.Host);

		RequestParams params = new RequestParams();
		Contract model = GetContractByUI();
		params.put("contractid", model.getContractid());
		params.put("contractnum", model.getContractnum());
		params.put("seq", model.getSeq());
		params.put("projectnum", model.getProjectnum());
		params.put("projectname", model.getProjectname());
		params.put("projectmanager", model.getProjectmanager());
		params.put("tel", model.getTel());
		params.put("depart", model.getDepart());
		params.put("packageName", model.getPackagename());
		params.put("packageBudget", model.getPackagebudget());
		params.put("tendarNum", model.getTendarnum());
		params.put("tendarCompany", model.getTendarcompany());
		params.put("tendarStartTime", model.getTendarstarttime());
		params.put("paymethod", model.getPaymethod());
		params.put("bcompany", model.getBcompany());
		params.put("linker", model.getLinker());
		params.put("phone", model.getPhone());
		params.put("money", model.getMoney());
		params.put("signingdate", model.getSigningdate());
		params.put("deliveryTime", model.getDeliverytime());
		params.put("inspection", model.getInspection());
		params.put("progress", model.getProgress());
		params.put("isPayAll", model.getIspayall());
		params.put("isArmoured", model.getIsarmoured());
		params.put("isRefund", model.getIsrefund());
		params.put("contractplace", model.getContractplace());
		params.put("contractrfid", model.getContractrfid());

		//params.add("model", new com.google.gson.Gson().toJson( model));
		AsyncHttpUtil.post(this , url, params, mRegisterHttpHandler);

		//new SaveContractThread( ContractRegisterActivity.this , GetContractByUI(), msgSaveNext , mHandler , mSqlOtherCondition ).start();
	}

	/**
	 * 扫描RFID标签
	 */
	protected void ScanRFID() {
		ShowProgressDialog(getString(R.string.scantip));
		new ScanAndCheckThread(mHandler, ContractRegisterActivity.this).start();
	}

	public  class MyHandler extends Handler {
		@Override
		public void handleMessage(Message msg) {
			if (mProgressDlg != null) {
				mProgressDlg.dismiss();
				mProgressDlg = null;
			}

			if (msg.what == msgfalse) {
				ToastUtil.Show(msg.obj.toString());
			}
//			else if (msg.what == msgSaveNext) {
//				ClearText();
//				if( msg.obj instanceof Contract){
//					Contract nextContract = (Contract) msg.obj;
//					SetContract(nextContract);
//				}else{
//					ToastUtil.Show(msg.obj.toString());
//				}
//			} else if (msg.what == msgSaveQuit) {
//				ContractRegisterActivity.this.finish();
//				System.exit(0);
//			}
			else if (msg.what == ScanAndCheckThread.MsgScanOK) {
				String uid = msg.obj.toString();
				mETcontractRFID.setText(uid);
			} else if (msg.what == ScanAndCheckThread.MsgScanFalse) {
				String message = msg.obj.toString();
				mETcontractRFID.setText("");
				ToastUtil.Show(message);
			} else if (msg.what == FunKeyReceiver.MsgF1) {
				ScanRFID();
			}else if( msg.what == FunKeyReceiver.MsgF2){
				SaveNext();
			}
			super.handleMessage(msg);
		}
	}


	@Override
	public void onClick(View view) {
		if ( view == mbtnScanrfid) {
			ScanRFID();
		}
		else if (view == mbtnSaveNext) {
			SaveNext();
		}
		else if (view == mbtnCancel) {
			//QuitUI();
			ContractRegisterActivity.this.finish();
		}
		//else if( view == chkUnShelve){
		//	if( chkUnShelve.isChecked()){
		//		mETcontractPlace.setText("");
		//		mETcontractPlace.setEnabled(false);
		//	}else{
		//		mETcontractPlace.setEnabled(true);
		//	}
		//}
	    else if( view == mbtnMenu){
			if(mLeftMenu.isMenuShowing()){
				mLeftMenu.showContent();
			}else{
				mLeftMenu.showMenu();
			}
		}
	}

	protected void QuitUI() {
		ContractRegisterActivity.this.setResult(RESULT_OK , getIntent());
		ContractRegisterActivity.this.finish();
	}

	public class RegisterShowData implements IShowData<JsonResult< String>>{

		@Override
		public void ShowData(JsonResult<String> data) {
			if(mProgressDlg!=null){
				mProgressDlg.dismiss();
				mProgressDlg=null;
			}

			if( data == null){
				ToastUtil.Show("操作失败");
				return;
			}
			if(data.getCode()== 0 && data.getMessage()!=null){
				ToastUtil.Show(data.getMessage());
				return;
			}

			QuitUI();
		}

		@Override
		public void Error(String msg) {
			if(mProgressDlg!=null){
				mProgressDlg.dismiss();
				mProgressDlg=null;
			}
			ToastUtil.Show(msg);
		}

	}
}
