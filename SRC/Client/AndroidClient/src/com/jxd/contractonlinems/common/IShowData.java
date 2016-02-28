package com.jxd.contractonlinems.common;

import com.jxd.contractonlinems.bean.JsonResult;

public interface IShowData<T> {
	void ShowData( T data );
	void Error(String msg);
}
