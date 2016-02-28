package com.jxd.contractonlinems.util;

import java.util.List;

import com.jxd.contractonlinems.bean.Menu;

public class CommonUtil {
	/*
	 * 设置 菜单的选择状态
	 */
	public static void SetLeftMenuSelected( List<Menu> list , int menuid , boolean isSelected){
		if( list ==null) return;
		for( Menu m : list){
			if( m.getMenuId() == menuid ){
				m.setSelected(isSelected);
			}else {
				m.setSelected(!isSelected);
			}
		}
	}
}
