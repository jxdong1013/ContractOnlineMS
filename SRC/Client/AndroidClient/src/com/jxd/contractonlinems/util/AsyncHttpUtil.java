package com.jxd.contractonlinems.util;

import org.apache.http.Header;
import org.apache.http.HttpEntity;

import android.content.Context;

import com.loopj.android.http.AsyncHttpClient;
import com.loopj.android.http.AsyncHttpResponseHandler;
import com.loopj.android.http.RequestParams;

public class AsyncHttpUtil {
	public static AsyncHttpClient asyncHttpClient = new AsyncHttpClient();

	public static void get( Context context , String url , Header[] headers , RequestParams params , AsyncHttpResponseHandler responseHandler){
		asyncHttpClient.get( context , url, headers , params , responseHandler);

	}

	public static void post( Context context , String url , Header[] headers , RequestParams params , String contentType, AsyncHttpResponseHandler responseHandler){
		asyncHttpClient.post(context, url, headers, params, contentType, responseHandler);
	}

	public static void post( Context context , String url , RequestParams params , AsyncHttpResponseHandler responseHandler){
		asyncHttpClient.post(context , url , params , responseHandler );
	}

	public static void post(Context context , String url , Header[] headers ,  HttpEntity entity, String contentType , AsyncHttpResponseHandler responseHandler){
		asyncHttpClient.post(context, url, headers, entity, contentType, responseHandler);
	}
}
