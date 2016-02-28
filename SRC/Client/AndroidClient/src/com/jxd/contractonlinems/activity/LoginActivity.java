package com.jxd.contractonlinems.activity;

import org.apache.http.Header;
import com.jxd.contractonlinems.R;
import com.jxd.contractonlinems.app.ContractApplication;
import com.jxd.contractonlinems.bean.JsonResult;
import com.jxd.contractonlinems.bean.Person;
import com.jxd.contractonlinems.util.AsyncHttpUtil;
import com.jxd.contractonlinems.util.PreferencesUtils;
import com.jxd.contractonlinems.util.ToastUtil;
import com.jxd.contractonlinems.util.WifiUtil;
import com.jxd.contractonlinems.widget.MaskDialogView;
import com.loopj.android.http.BaseJsonHttpResponseHandler;
import com.loopj.android.http.RequestParams;
import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.google.gson.reflect.TypeToken;

public class LoginActivity extends Activity {
	Button btnLogin;
	EditText etAccount=null;
	EditText etPassword = null;
	LoginAsyncResponseHandler responseHandler=null;
	ProgressDialog mProgressDlg = null;
	TextView mTvSetting =null;
	MaskDialogView mDialogView = null;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		this.setContentView(R.layout.activity_login);

		super.onCreate(savedInstanceState);

		responseHandler = new LoginAsyncResponseHandler();

		etAccount=(EditText)this.findViewById(R.id.account);

		String account = PreferencesUtils.getString(getApplication(), "username");
		etAccount.setText(account);

		etPassword = (EditText)this.findViewById(R.id.password);
		btnLogin=(Button)this.findViewById(R.id.login);
		btnLogin.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				if( WifiUtil.CheckWifi()== false){
					ToastUtil.Show("请链接wifi无线网络.");
					return;
				}
				if( ContractApplication.Host == null || ContractApplication.Host==""){
					ToastUtil.Show("请配置地址");
					return;
				}

				String account = etAccount.getText().toString();
				String password = etPassword.getText().toString();
				if( account ==null || account.isEmpty() || password ==null|| password.isEmpty() ){
					ToastUtil.Show("用户名或密码为空!");
					return;
				}
				String url = String.format( "%s%s" , ContractApplication.Host ,"account/LoginRestfull");// "http://192.168.1.6/contractweb/account/LoginRestfull";
				RequestParams params = new RequestParams();
				params.put("userName", account);
				params.put("password", password);

				if (mProgressDlg != null) {
					mProgressDlg.dismiss();
					mProgressDlg = null;
				}
				mProgressDlg = new ProgressDialog(LoginActivity.this);
				mProgressDlg.setMessage(getString(R.string.loginTip));
				mProgressDlg.show();

				AsyncHttpUtil.post(LoginActivity.this, url,  params,  responseHandler);
			}
		});

		mTvSetting = (TextView) this.findViewById(R.id.tv_url_config);
		mTvSetting.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View arg0) {

				if( mDialogView ==null){
					mDialogView = new MaskDialogView( LoginActivity.this);

				}else{
					mDialogView.SetUrl();
				}
				mDialogView .show();
			}
		});
	}


	class LoginAsyncResponseHandler extends BaseJsonHttpResponseHandler<JsonResult<Person>>{

		@Override
		public void onFailure(int statusCode , Header[] headers , Throwable throwable ,
				String responseString , JsonResult<Person> response ) {
			if(mProgressDlg!=null){
				mProgressDlg.dismiss();
				mProgressDlg=null;
			}

			if( throwable !=null && throwable.getMessage() !=null){
				ToastUtil.Show(throwable.getMessage());
				return;
			}
		}

		@Override
		public void onSuccess(int statusCode, Header[] headers, String responseString , JsonResult<Person> response) {
			if(mProgressDlg!=null){
				mProgressDlg.dismiss();
				mProgressDlg=null;
			}

			if( response == null ){
				ToastUtil.Show("登录失败!");
				return ;
			}
			if( response.getCode() != 1 ){
				if( response.getMessage() !=null ){
					ToastUtil.Show(response.getMessage());
				}
				return;
			}
			if( response.getData() ==null ){
				ToastUtil.Show("获取用户信息失败。");
				return;
			}

			PreferencesUtils.putString( ContractApplication.mApp.getApplicationContext() , "username", response.getData().getUsername());
			ContractApplication.mApp.mAccount = response.getData();

			if( headers !=null){
			for( int i=0;i< headers.length ;i++){
				if(headers[i].getName().equalsIgnoreCase("Set-Cookie")){
					String cookieString =  headers[i].getValue();
					   ContractApplication.CookieId = cookieString.split("=")[0];
				       ContractApplication.CookieValue =  cookieString.split("=")[1];
				}
			}
			}

			Intent intent= new Intent();
			intent.setClass(LoginActivity.this, ContractListActivity.class);
			LoginActivity.this.startActivity(intent);
			LoginActivity.this.finish();
		}

		@Override
		protected JsonResult<Person> parseResponse(String jsonString , boolean isFailure)
				throws Throwable {
			return new com.google.gson.Gson().fromJson(jsonString, new TypeToken<JsonResult<Person>>(){}.getType() );
		}
	}

}
