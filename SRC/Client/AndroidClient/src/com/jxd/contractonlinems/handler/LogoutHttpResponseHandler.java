package com.jxd.contractonlinems.handler;

import org.apache.http.Header;

import com.google.gson.reflect.TypeToken;
import com.jxd.contractonlinems.bean.JsonResult;
import com.jxd.contractonlinems.common.IShowData;
import com.loopj.android.http.BaseJsonHttpResponseHandler;

public class LogoutHttpResponseHandler extends BaseJsonHttpResponseHandler<JsonResult<String>>{
		IShowData<JsonResult<String>> mShowData=null;

		public LogoutHttpResponseHandler ( IShowData<JsonResult<String>> showData){
			mShowData =showData;
		}

		@Override
		public void onFailure(int statusCode , Header[] headers , Throwable throwable ,
				String responseString , JsonResult<String> response) {
//			if( throwable!=null ){
				mShowData.Error( throwable.getMessage());
//				return;
//			}
		}

		@Override
		public void onSuccess(int statusCode, Header[] headers, String responseString , JsonResult< String > response) {
			mShowData.ShowData( response );
		}

		@Override
		protected JsonResult<String> parseResponse(String jsonString, boolean isFailure)
				throws Throwable {
			return new com.google.gson.Gson().fromJson(jsonString, new TypeToken<JsonResult<String>>(){}.getType() );

		}
}
