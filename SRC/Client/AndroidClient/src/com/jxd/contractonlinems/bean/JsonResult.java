package com.jxd.contractonlinems.bean;

public class JsonResult<T> {
	private T Data;
	private Integer Code;
	private String Message;
	public T getData() {
		return Data;
	}
	public void setData(T data) {
		Data = data;
	}
	public Integer getCode() {
		return Code;
	}
	public void setCode(Integer code) {
		Code = code;
	}
	public String getMessage() {
		return Message;
	}
	public void setMessage(String message) {
		Message = message;
	}
}
