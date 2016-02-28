package com.jxd.contractonlinems.activity;

import java.io.UnsupportedEncodingException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;

import org.apache.http.entity.StringEntity;
import org.apache.http.message.BasicHeader;

import com.google.gson.Gson;
import com.jxd.contractonlinems.R;
import com.jxd.contractonlinems.adapter.ContractAdapter;
import com.jxd.contractonlinems.bean.Contract;
import com.jxd.contractonlinems.bean.ContractPage;
import com.jxd.contractonlinems.bean.ContractQueryForm;
import com.jxd.contractonlinems.bean.JsonResult;
import com.jxd.contractonlinems.bean.Menu;
import com.jxd.contractonlinems.common.IShowData;
import com.jxd.contractonlinems.handler.ContractHttpResponseHandler;
import com.jxd.contractonlinems.handler.DeleteContractHttpResponseHandler;
import com.jxd.contractonlinems.handler.LogoutHttpResponseHandler;
import com.jxd.contractonlinems.receiver.FunKeyReceiver;
import com.jxd.contractonlinems.thread.ContractQueryThread;
import com.jxd.contractonlinems.thread.DeleteAllThread;
import com.jxd.contractonlinems.thread.DeleteContractThread;
//import com.jxd.contractonlinems.thread.ExportTxtThread;
import com.jxd.contractonlinems.thread.ImportExcelThread;
//import com.jxd.contractonlinems.thread.ImportTxtThread;
import com.jxd.contractonlinems.thread.ScanRFIDThread;
import com.jxd.contractonlinems.util.AsyncHttpUtil;
import com.jxd.contractonlinems.util.ToastUtil;
import com.jxd.contractonlinems.app.ContractApplication;
//import com.jxd.contractonlinems.variable.Variable;
import com.jxd.contractonlinems.widget.XListView;
import com.jxd.contractonlinems.widget.XListView.IXListViewListener;
import com.loopj.android.http.RequestParams;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
//import android.preference.PreferenceActivity.Header;
//import android.text.InputType;
import android.view.ContextMenu;
import android.view.ContextMenu.ContextMenuInfo;
import android.view.KeyEvent;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnCreateContextMenuListener;
//import android.view.View.OnFocusChangeListener;
//import android.view.Window;
import android.widget.AdapterView;
import android.widget.AdapterView.AdapterContextMenuInfo;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.AutoCompleteTextView;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.CompoundButton.OnCheckedChangeListener;
//import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.Toast;
//import android.support.v7.widget.PopupMenu;
//import android.support.v7.widget.PopupMenu.OnMenuItemClickListener;
import android.widget.TextView;

public class ContractListActivity extends BaseActivity {

	XListView mlvContracts = null;
	List<Contract> mContractList = null;
	Button mBtnQuery = null;
	TextView mTvCount = null;
	Button mBtnAddContract = null;
	Button mBtnDeleteContract = null;
	// Button mbtnMore = null;
	// AutoCompleteTextView mSearchText = null;
	ContractListHandler mHandler = null;
	ContractXListViewListener mListener = null;
	ContractAdapter mAdapter = null;
	int mPageIdx = 0;
	// String mSearchContext = "";
	boolean mIsRefresh = false;
	// ContractListThread mThread = null;
	ProgressDialog mProgressDlg = null;
	// ArrayList<String> mHistory = null;
	FunKeyReceiver mKeyReceiver = null;
	ContractQueryForm mQueryForm = null;
	ContractHttpResponseHandler mResponseHandler = null;
	// DeleteAllContractHttpResponseHandler
	// mDeleteAllContractResponseHandler=null;

	public static int RequestCodeUpdate = 1000;
	// public static int EXCEL_SELECT_CODE = 1010;
	// public static int TXT_SELECT_CODE = 1011;

	Button mBtnMenu = null;
	TextView mTvUser = null;
	RelativeLayout llBottonMenu = null;
	LinearLayout llDeleteMenu = null;
	Button mBtnConfirm = null;
	Button mBtnCancel = null;
	Boolean mDeleteMenuFlag = false;
	CheckBox mchkAllSelect = null;
	// Button mBtnImportContract = null;
	ContractShowData mContractShowData = null;
	DeleteContractShowData mDeleteContractShowData = null;
	DeleteContractHttpResponseHandler mDeleteResponseHandler = null;

	@Override
	protected void onCreate(Bundle savedInstanceState) {

		setContentView(R.layout.activity_contract_list);

		InitWidget();

		LoadContracts(null);

		super.onCreate(savedInstanceState);

	}

	protected void InitWidget() {

		// mQueryLister = new QueryLister();
		// mBtnQuery = (Button) this.findViewById(R.id.btnSearch);
		// mBtnQuery.setOnClickListener(mQueryLister);

		mTvCount = (TextView) this.findViewById(R.id.heanderCount);
		mBtnAddContract = (Button) this.findViewById(R.id.btnAddContract);
		mBtnAddContract.setOnClickListener(new OnClickListener() {
			public void onClick(View v) {
				Intent intent = new Intent(ContractListActivity.this,
						ContractRegisterActivity.class);
				ContractListActivity.this.startActivityForResult(intent,
						RequestCodeUpdate);
			}
		});

		llBottonMenu = (RelativeLayout) this.findViewById(R.id.rlmenu);
		llDeleteMenu = (LinearLayout) this.findViewById(R.id.lldeletemenu);
		mBtnDeleteContract = (Button) this.findViewById(R.id.btnDeleteContract);
		mBtnDeleteContract.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				int visible = llBottonMenu.getVisibility();
				llBottonMenu.setVisibility(llDeleteMenu.getVisibility());
				llDeleteMenu.setVisibility(visible);
				mDeleteMenuFlag = !mDeleteMenuFlag;
				mAdapter.SetSelectedFlag(mDeleteMenuFlag);
			}
		});

		mBtnConfirm = (Button) this.findViewById(R.id.btnDeleteConfirm);
		mBtnConfirm.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				DeleteContract();
			}
		});
		mBtnCancel = (Button) this.findViewById(R.id.btnDeleteCancel);
		mBtnCancel.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				CloseDeleteMenu();
			}
		});

		mchkAllSelect = (CheckBox) this.findViewById(R.id.chkAllSelected);
		mchkAllSelect.setOnCheckedChangeListener(new OnCheckedChangeListener() {
			@Override
			public void onCheckedChanged(CompoundButton buttonView,
					boolean isChecked) {
				// ToastUtil.Show( "ischecked" + isChecked);
				if (mContractList != null) {
					for (Contract c : mContractList) {
						c.setSelected(isChecked);
					}
					mAdapter.setContractList(mContractList);
				}
			}
		});

		InitLeftMenu();

		mHandler = new ContractListHandler();
		mlvContracts = (XListView) this.findViewById(R.id.lvContract);
		mListener = new ContractXListViewListener();
		mlvContracts.setXListViewListener(mListener);
		mlvContracts.setPullLoadEnable(true);
		mlvContracts.setPullRefreshEnable(true);
		mContractList = new ArrayList<Contract>();
		mAdapter = new ContractAdapter(this, mContractList, mDeleteMenuFlag);
		mlvContracts.setAdapter(mAdapter);
		mlvContracts.setOnItemClickListener(new OnItemClickListener() {
			public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,
					long arg3) {
				try {
					if (arg2 < 1 || mContractList == null
							|| mContractList.size() < 1)
						return;
					Intent intent = new Intent();
					intent.setClass(ContractListActivity.this,
							ContractRegisterActivity.class);
					Contract contract = mContractList.get(arg2 - 1);
					Bundle bd = new Bundle();
					bd.putSerializable("contract", contract);
					intent.putExtras(bd);
					ContractListActivity.this.startActivityForResult(intent,
							RequestCodeUpdate);
				} catch (Exception ex) {
				}
			}
		});

		mlvContracts
				.setOnCreateContextMenuListener(new OnCreateContextMenuListener() {
					public void onCreateContextMenu(ContextMenu menu, View v,
							ContextMenuInfo menuInfo) {
						try {
							MenuInflater inflater = new MenuInflater(
									ContractListActivity.this);
							inflater.inflate(R.menu.contractlistmenu, menu);
							Contract entity = mContractList
									.get(((AdapterContextMenuInfo) menuInfo).position - 1);
							menu.setHeaderTitle("编号" + entity.getProjectnum()
									+ " 名称" + entity.getProjectname());
						} catch (Exception ex) {
						}
					}
				});

		mContractShowData = new ContractShowData();
		mResponseHandler = new ContractHttpResponseHandler(mContractShowData);

		mDeleteContractShowData = new DeleteContractShowData();
		mDeleteResponseHandler = new DeleteContractHttpResponseHandler(
				mDeleteContractShowData);

		// mDeleteAllContractResponseHandler = new
		// DeleteAllContractHttpResponseHandler(mDeleteContractShowData);
	}

	@Override
	protected void onNewIntent(Intent intent) {
		super.onNewIntent(intent);

		if (mContractList != null) {
			mContractList.clear();
		}
		LoadContracts(intent);
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

		if (mLeftMenu != null && mLeftMenu.isMenuShowing()) {
			mLeftMenu.showContent();
		}

		// LoadContracts( null );
	}

	@Override
	protected void onPause() {
		if (mKeyReceiver != null) {
			unregisterReceiver(mKeyReceiver);
		}
		super.onPause();
	}

	@Override
	public void onBackPressed() {
		if (mLeftMenu.isMenuShowing()) {
			mLeftMenu.showContent();
		} else {
			super.onBackPressed();
		}
	}

	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {
		if (keyCode == KeyEvent.KEYCODE_BACK
				&& event.getAction() == KeyEvent.ACTION_DOWN) {
			AlertDialog dlg = null;
			AlertDialog.Builder builder = new AlertDialog.Builder(
					ContractListActivity.this);
			builder.setTitle("退出");
			builder.setMessage("确定退出系统吗?");
			builder.setPositiveButton("确定",
					new DialogInterface.OnClickListener() {
						public void onClick(DialogInterface dialog, int which) {

							logout();

							// ContractListActivity.this.finish();
							// System.exit(0);
						}
					});
			builder.setNegativeButton("取消",
					new DialogInterface.OnClickListener() {
						public void onClick(DialogInterface dialog, int which) {
							dialog.dismiss();
						}
					});
			dlg = builder.create();
			dlg.show();
		}
		return true;
	}

	protected void logout() {

		ShowProgressDialog("正在退出系统,请稍等");

		String url = String.format("%sAccount/LogoutRestfull",
				ContractApplication.Host);
		// String cookieString = ContractApplication.CookieId + "="
		// + ContractApplication.CookieValue;
		// org.apache.http.Header[] headers = { new BasicHeader("Cookie",
		// cookieString) };

		AsyncHttpUtil.post(ContractListActivity.this, url, null,
				new LogoutHttpResponseHandler(new LogoutShowData()));
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {

		if (resultCode == RESULT_OK && requestCode == RequestCodeUpdate) {
			mlvContracts.startRefresh();
			// AsynLoadArchives();

		}
		// else if (resultCode == RESULT_OK && requestCode == EXCEL_SELECT_CODE)
		// {
		// String path = data.getStringExtra("file");
		// ImportExcel(path);
		// }
		// else if( resultCode == RESULT_OK && requestCode == TXT_SELECT_CODE){
		// String path =data.getStringExtra("file");
		// ImportTxt(path);
		// }

		super.onActivityResult(requestCode, resultCode, data);
	}

	/*
	 *
	 */
	@Override
	public boolean onContextItemSelected(MenuItem item) {
		if (item.getItemId() == R.id.contractlistmenu_delete) {
			ShowProgressDialog(getString(R.string.deletetip));
			AdapterContextMenuInfo info = (AdapterContextMenuInfo) item
					.getMenuInfo();
			Contract contract = mContractList.get(info.position - 1);
			if( contract !=null) {
			//List<Integer> ids = new ArrayList<Integer>();
			//ids.add(contract.getContractid());
			//new DeleteContractThread(ContractListActivity.this, mHandler, ids)
			//		.start();
			contract.setSelected(true);
			DeleteContract();
			}

		} else if (item.getItemId() == R.id.contractlistmenu_edit) {
			Intent intent = new Intent();
			intent.setClass(ContractListActivity.this,
					ContractRegisterActivity.class);
			AdapterContextMenuInfo info = (AdapterContextMenuInfo) item
					.getMenuInfo();
			Contract contract = mContractList.get(info.position - 1);
			Bundle bd = new Bundle();
			bd.putSerializable("contract", contract);
			intent.putExtras(bd);
			ContractListActivity.this.startActivityForResult(intent,
					RequestCodeUpdate);
		}

		return super.onContextItemSelected(item);
	}

	protected void LoadContracts(Intent intent) {

		if (intent != null && intent.hasExtra("queryForm")) {
			mQueryForm = (ContractQueryForm) intent
					.getSerializableExtra("queryForm");
		}

		AsynLoadArchives();
	}

	protected void CloseDeleteMenu() {
		if (mContractList != null) {
			for (Contract c : mContractList) {
				c.setSelected(false);
			}
			mAdapter.setContractList(mContractList);
		}

		llBottonMenu.setVisibility(View.VISIBLE);
		llDeleteMenu.setVisibility(View.GONE);
		mDeleteMenuFlag = false;
		mchkAllSelect.setChecked(false);
		mAdapter.SetSelectedFlag(mDeleteMenuFlag);

	}

	protected void DeleteContract() {
		if (mContractList == null || mContractList.size() < 1)
			return;
		List<Integer> ids = new ArrayList<Integer>();
		for (Contract c : mContractList) {
			if (c.getSelected())
				ids.add(c.getContractid());
		}
		if (ids.size() < 1) {
			ToastUtil.Show("请选择要删除的记录!");
			return;
		}

		ShowProgressDialog(getString(R.string.deletetip));

		// new DeleteContractThread(ContractListActivity.this, mHandler, ids)
		// .start();
		String url = String.format("%s%s", ContractApplication.Host,
				"ContractRestfull/DeleteContracts");
		// RequestParams params = new RequestParams();
		// params.setUseJsonStreamer(true);
		com.google.gson.Gson gson = new Gson();
		String jsonString = gson.toJson(ids);
		// params.add("contractids", jsonString);

		String cookieString = ContractApplication.CookieId + "="
				+ ContractApplication.CookieValue;
		org.apache.http.Header[] headers = { new BasicHeader("Cookie",
				cookieString) };

		jsonString = "{contractids:" + jsonString + "}";

		StringEntity para = null;
		try {
			para = new StringEntity(jsonString);
		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		AsyncHttpUtil.post(ContractListActivity.this, url, headers, para,
				RequestParams.APPLICATION_JSON, mDeleteResponseHandler);

	}

	protected void InitLeftMenu() {
		mTvUser = (TextView) this.findViewById(R.id.tvUser);
		if (ContractApplication.mApp.mAccount != null) {
			mTvUser.setText("用户:"
					+ ContractApplication.mApp.mAccount.getUsername());
		}
		mBtnMenu = (Button) this.findViewById(R.id.btnMenu);
		mBtnMenu.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				if (mLeftMenu.isMenuShowing()) {
					mLeftMenu.showContent();

				} else {
					mLeftMenu.showMenu();
				}
			}
		});

		CreateLeftMenu();

		mLvLeftMenu.setOnItemClickListener(new OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> arg0, View arg1, int arg2,
					long arg3) {

				if (arg2 < 0 || ContractApplication.mApp.LeftMenuList == null)
					return;

				Menu menuL = ContractApplication.mApp.LeftMenuList.get(arg2);
				if (menuL == null)
					return;

				if (menuL.getMenuId() == R.string.menuRegister) {
					mLeftMenu.showContent();
					Intent intent = new Intent();
					intent.setClass(ContractListActivity.this,
							ContractRegisterActivity.class);
					ContractListActivity.this.startActivityForResult(intent,
							RequestCodeUpdate);
				} else if (menuL.getMenuId() == R.string.menuContractQuery) {
					mLeftMenu.showContent();
					Intent intent = new Intent();
					intent.setClass(ContractListActivity.this,
							ContractQueryActivity.class);
					ContractListActivity.this.startActivity(intent);
				}
			}
		});
	}

	protected void AsynLoadArchives() {
		mIsRefresh = false;
		mPageIdx = -1;
		mlvContracts.startLoadMore();
	}

	private void onLoad() {
		try {
			mlvContracts.stopRefresh();
			mlvContracts.stopLoadMore();
			SimpleDateFormat format = new SimpleDateFormat("HH:mm:ss",
					Locale.getDefault());
			String datestr = format.format(new Date());
			mlvContracts.setRefreshTime(datestr);
		} catch (Exception e) {
			ToastUtil.Show("onload出错了!");
		}
	}

	protected void ShowProgressDialog(String message) {
		if (mProgressDlg != null) {
			mProgressDlg.dismiss();
			mProgressDlg = null;
		}
		mProgressDlg = new ProgressDialog(ContractListActivity.this);
		mProgressDlg.setMessage(message);
		mProgressDlg.show();
	}

	protected void CloseProgressDialog() {
		if (mProgressDlg != null) {
			mProgressDlg.dismiss();
			mProgressDlg = null;
		}
	}

	private class ContractShowData implements
			IShowData<JsonResult<ContractPage>> {

		@Override
		public void ShowData(JsonResult<ContractPage> data) {
			CloseProgressDialog();

			try {

				if (data == null) {
					onLoad();
					return;
				}

				if (data.getCode() == 0) {
					if (data.getMessage() != null) {
						ToastUtil.Show(data.getMessage());
					}
					onLoad();
					return;
				}

				if (mIsRefresh) {
					List<Contract> temp = null;
					if (data.getData() != null
							&& data.getData().getData() != null) {
						temp = data.getData().getData();
					}

					if (temp == null) {
						mTvCount.setText("0/"
								+ data.getData().getTotalRecords());
						mPageIdx--;
						onLoad();
						if (mContractList != null) {
							mContractList.clear();
						}
						mAdapter.notifyDataSetChanged();
						return;
					}
					if (mContractList != null) {
						mContractList.addAll(temp);
					}
					mTvCount.setText(mContractList.size() + "/"
							+ data.getData().getTotalRecords());

					// mAdapter = new ContractAdapter(ContractListActivity.this,
					// mContractList , mDeleteMenuFlag );
					mchkAllSelect.setChecked(false);
					// mlvContracts.setAdapter(mAdapter);
					mAdapter.notifyDataSetChanged();
					onLoad();
				} else {
					List<Contract> temp = (List<Contract>) data.getData()
							.getData();

					if (temp == null) {
						mTvCount.setText("0/"
								+ data.getData().getTotalRecords());
						mPageIdx--;
						onLoad();
						mContractList.clear();
						mAdapter.notifyDataSetChanged();
						return;
					}
					mContractList.addAll(temp);
					mTvCount.setText(mContractList.size() + "/"
							+ data.getData().getTotalRecords());

					mchkAllSelect.setChecked(false);
					mAdapter.notifyDataSetChanged();
					onLoad();
				}
			} catch (Exception ex) {

				ToastUtil.Show(ex.getMessage());
			} finally {
			}
		}

		@Override
		public void Error(String msg) {
			CloseProgressDialog();

			if (msg != null && msg.isEmpty() == false) {
				ToastUtil.Show(msg);
			}

			onLoad();
		}
	}

	private class DeleteContractShowData implements
			IShowData<JsonResult<String>> {

		@Override
		public void ShowData(JsonResult<String> data) {
			if (mProgressDlg != null) {
				mProgressDlg.dismiss();
				mProgressDlg = null;
			}
			if (data == null) {
				onLoad();
				return;
			}

			if (data.getCode() == 0) {// 失败
				ToastUtil.Show(data.getMessage());
				onLoad();
				return;
			}

			CloseDeleteMenu();
			String message = data.getMessage();
			if (message != null && message.isEmpty() == false) {
				ToastUtil.Show(message);
			}
			mlvContracts.startRefresh();

		}

		@Override
		public void Error(String msg) {
			if (mProgressDlg != null) {
				mProgressDlg.dismiss();
				mProgressDlg = null;
			}
			if (msg != null && msg.isEmpty() == false) {
				ToastUtil.Show(msg);
			}
		}

	}

	private class LogoutShowData implements IShowData<JsonResult<String>> {

		@Override
		public void ShowData(JsonResult<String> data) {

			CloseProgressDialog();

			// ToastUtil.Show("ok");

			ContractListActivity.this.finish();
			System.exit(0);
		}

		@Override
		public void Error(String msg) {

			CloseProgressDialog();

			ContractListActivity.this.finish();
			System.exit(0);
		}
	}

	private class ContractListHandler extends Handler {
		@Override
		public void handleMessage(Message msg) {
			try {
				if (msg.what == ContractQueryThread.MSG_ContractQueryLoaded) {
					List<Contract> temp = (List<Contract>) msg.obj;
					if (temp == null) {
						mPageIdx--;
						onLoad();
						mTvCount.setText(mContractList.size() + "/" + msg.arg1);
						return;
					}
					mContractList.addAll(temp);
					mTvCount.setText(mContractList.size() + "/" + msg.arg1);

					onLoad();
					mAdapter.notifyDataSetChanged();
				} else if (msg.what == ContractQueryThread.MSG_ContractQueryRefresh) {
					List<Contract> temp = (List<Contract>) msg.obj;

					if (temp == null) {
						mTvCount.setText("0/" + msg.arg1);
						mPageIdx--;
						onLoad();
						mContractList.clear();
						mAdapter.notifyDataSetChanged();
						return;
					}
					mContractList.addAll(temp);
					mTvCount.setText(mContractList.size() + "/" + msg.arg1);

					// mAdapter = new ContractAdapter(ContractListActivity.this,
					// mContractList , mDeleteMenuFlag );
					mchkAllSelect.setChecked(false);
					// mlvContracts.setAdapter(mAdapter);
					mAdapter.notifyDataSetChanged();
					onLoad();
				} else if (msg.what == ContractQueryThread.MSG_ContractQueryError) {
					onLoad();
				}
				// else if (msg.what == ExportTxtThread.MsgExportOK) {
				// CloseProgressDialog();
				// String message = msg.obj.toString();
				// ToastUtil.Show( message);
				// } else if (msg.what == ExportTxtThread.MsgExportFalse) {
				// CloseProgressDialog();
				// String message = msg.obj.toString();
				// ToastUtil.Show( message);
				// }
				else if (msg.what == DeleteContractThread.msgDeleteOK) {
					CloseProgressDialog();
					CloseDeleteMenu();
					String message = msg.obj.toString();
					ToastUtil.Show(message);
					mlvContracts.startRefresh();

				} else if (msg.what == DeleteContractThread.msgDeleteFalse) {
					CloseProgressDialog();
					String message = msg.obj.toString();
					ToastUtil.Show(message);
				} else if (msg.what == FunKeyReceiver.MsgF1) {
					// ScanRFID();
				} else if (msg.what == FunKeyReceiver.MsgF2) {
					mlvContracts.startRefresh();
				} else if (msg.what == ScanRFIDThread.MsgScanOK) {
					CloseProgressDialog();
					String uid = msg.obj.toString();
					// mSearchText.setText(uid);
					mlvContracts.startRefresh();
				} else if (msg.what == ScanRFIDThread.MsgScanFalse) {
					CloseProgressDialog();
					String message = msg.obj.toString();
					// mSearchText.setText("");
					ToastUtil.Show(message);
				} else if (msg.what == DeleteAllThread.msgDeleteAllOK) {
					CloseProgressDialog();
					String message = msg.obj.toString();
					ToastUtil.Show(message);
					// mSearchText.setText("");
					mlvContracts.startRefresh();
				} else if (msg.what == DeleteAllThread.msgDeleteAllFalse) {
					CloseProgressDialog();
					String message = msg.obj.toString();
					ToastUtil.Show(message);
				} else if (msg.what == ImportExcelThread.ImportOk) {
					CloseProgressDialog();
					mlvContracts.startRefresh();
					ToastUtil.Show(msg.obj.toString());
				} else if (msg.what == ImportExcelThread.ImportFalse) {
					CloseProgressDialog();
					ToastUtil.Show(msg.obj.toString());
				}
				// else if ( msg.what == ImportTxtThread.ImportTxtOk){
				// CloseProgressDialog();
				// mlvContracts.startRefresh();
				// ToastUtil.Show(msg.obj.toString());
				// }
				// else if( msg.what == ImportTxtThread.ImportTxtFalse){
				// CloseProgressDialog();
				// ToastUtil.Show(msg.obj.toString());
				// }

			} catch (Exception e) {
				Toast.makeText(ContractListActivity.this, "handlemessage出错了!",
						Toast.LENGTH_SHORT).show();
			}
		}
	}

	/**
	 * 扫描RFID标签
	 */
	protected void ScanRFID() {
		ShowProgressDialog(getString(R.string.scantip));
		new ScanRFIDThread(mHandler, ContractListActivity.this).start();
	}

	class ContractXListViewListener implements IXListViewListener {
		public void onRefresh() {
			try {

				mIsRefresh = true;
				if (mContractList != null)
					mContractList.clear();
				mAdapter.notifyDataSetChanged();
				mPageIdx = 1;

				if (mQueryForm == null) {
					mQueryForm = new ContractQueryForm();
				}
				mQueryForm.setIsrefresh(mIsRefresh);
				mQueryForm.setPageidx(mPageIdx);

				AsyncQuery();
			} catch (Exception ex) {
			}
		}

		protected void AsyncQuery() {

			String url = String.format("%s/ContractRestfull/ContractList",
					ContractApplication.Host);
			// Header[] headers = null;
			RequestParams params = new RequestParams();
			params.put("seq", mQueryForm.getSeq());
			params.put("contractnum", mQueryForm.getContractNum());
			params.put("projectnum", mQueryForm.getProjectNo());
			params.put("projectname", mQueryForm.getProjectName());
			params.put("rfid", mQueryForm.getContractRFID());
			params.put("contractplace", mQueryForm.getContractPlace());
			params.put("bcompanay", mQueryForm.getbCompany());
			params.put("pageidx", mQueryForm.getPageidx());
			params.put("pagesize", mQueryForm.getPageSize());
			// String contentType = "";

			String cookieString = ContractApplication.CookieId + "="
					+ ContractApplication.CookieValue;
			org.apache.http.Header[] headers = { new BasicHeader("Cookie",
					cookieString) };

			// "application/json",
			AsyncHttpUtil.get(ContractListActivity.this, url, headers, params,
					mResponseHandler);
		}

		public void onLoadMore() {
			try {
				mPageIdx++;
				mIsRefresh = false;

				if (mQueryForm == null) {
					mQueryForm = new ContractQueryForm();
				}
				mQueryForm.setIsrefresh(mIsRefresh);
				mQueryForm.setPageidx(mPageIdx);

				AsyncQuery();

			} catch (Exception ex) {

			}
			// ContractQueryThread thread = new
			// ContractQueryThread(ContractListActivity.this, mHandler,
			// mQueryForm);
			// thread.start();
		}
	}

}
