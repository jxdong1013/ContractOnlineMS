package com.jxd.contractonlinems.test;

import java.text.SimpleDateFormat;
import java.util.Date;

import android.text.format.DateUtils;

import com.jxd.contractonlinems.bean.Contract;

public class test {
	public static void main(String[] args){
		Contract b = new Contract();
		 String s = "{contractid:1,createtime:1419930356000}";
	
		 Date startd = new Date(1970, 1,1 , 0, 0);
		 
		 Long ds = 1419930356000l;
		 long tt = ds- startd.getTime();
		 Date eee = new Date(tt);
		 
		 SimpleDateFormat f =new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
		 String fff= f.format(eee);
		 
		 //b =new com.google.gson.Gson().fromJson(s,  Contract.class);
	}
}
