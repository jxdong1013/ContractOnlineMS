package com.jxd.contractonlinems.util;

public class ByteUtil {
	// byte 转十六进制
	public static String Bytes2HexString(byte[] b, int size) {
		String ret = "";
		//for (int i = 0; i < size; i++) {
		for(int i= size-1; i>=0; i--){
			String hex = Integer.toHexString(b[i] & 0xFF);
			if (hex.length() == 1) {
				hex = "0" + hex;
			}
			ret += hex.toUpperCase();
		}
		return ret;
	}

	public static void main(String[] args ){
		System.out.println("8393567D000104E0");
		 byte[] a = new byte[]{ (byte)0x83,(byte)0x93,0x56,0x7D,0x00,0x01,0x04,(byte)0xE0};

		 String af = Bytes2HexString(a, a.length);

		 System.out.println(af);

	}
}
