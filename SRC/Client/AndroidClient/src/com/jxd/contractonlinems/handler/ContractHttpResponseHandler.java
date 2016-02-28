package com.jxd.contractonlinems.handler;

import org.apache.http.Header;

import com.google.gson.reflect.TypeToken;
import com.jxd.contractonlinems.bean.ContractPage;
import com.jxd.contractonlinems.bean.JsonResult;
import com.jxd.contractonlinems.common.IShowData;
import com.loopj.android.http.BaseJsonHttpResponseHandler;

public class ContractHttpResponseHandler extends BaseJsonHttpResponseHandler<JsonResult<ContractPage>>{
	IShowData<JsonResult<ContractPage>> mShowData=null;

	public ContractHttpResponseHandler ( IShowData<JsonResult<ContractPage>> showData){
		mShowData =showData;
	}

	@Override
	public void onFailure(int statusCode , Header[] headers , Throwable throwable ,
			String responseString , JsonResult<ContractPage> response) {
		if( throwable!=null ){
			mShowData.Error( throwable.getMessage());
			return;
		}
		//mShowData.Error(responseString);
	}

	@Override
	public void onSuccess(int statusCode, Header[] headers, String responseString , JsonResult< ContractPage > response) {
		mShowData.ShowData( response );
	}

	@Override
	protected JsonResult<ContractPage> parseResponse(String jsonString, boolean isFailure)
			throws Throwable {
		return new com.google.gson.Gson().fromJson(jsonString, new TypeToken<JsonResult<ContractPage>>(){}.getType() );

	}

}
