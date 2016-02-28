package com.jxd.contractonlinems.bean;

public class Menu {
	private int menuId=0;
	private String menuName="";
	private int image=0;
	private boolean isSelected=false;

	public Menu( int menuid , String name , int image , boolean isSelected ){
		this.menuId= menuid;
		this.menuName = name;
		this.image = image;
		this.isSelected= isSelected;
	}

	public int getMenuId() {
		return menuId;
	}
	public void setMenuId(int menuId) {
		this.menuId = menuId;
	}
	public String getMenuName() {
		return menuName;
	}
	public void setMenuName(String menuName) {
		this.menuName = menuName;
	}
	public int getImage() {
		return image;
	}
	public void setImage(int image) {
		this.image = image;
	}
	public boolean isSelected() {
		return isSelected;
	}
	public void setSelected(boolean isSelected) {
		this.isSelected = isSelected;
	}
}
