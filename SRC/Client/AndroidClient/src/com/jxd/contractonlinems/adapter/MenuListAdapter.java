package com.jxd.contractonlinems.adapter;

import java.util.List;

import com.jxd.contractonlinems.R;
import com.jxd.contractonlinems.bean.Menu;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

/**
 *
 * @Description: TODO

 * @File: MenuListAdapter.java

 * @Paceage com.meiya.ui

 * @Version
 */
public class MenuListAdapter extends BaseAdapter {
	List<Menu> mList=null;

	//int [] listTextSrc = new int[]{R.string.menuRegister,R.string.menuBorrow,
	//		R.string.menuBack,R.string.menuInventory,
	//		R.string.menuContractQuery,R.string.menuSetting,
	//		};

	//int [] listImageSrc = new int[]{R.drawable.navigation_contract ,
	//		R.drawable.navigation_borrow,R.drawable.navigation_back,
	//		R.drawable.navigation_inventory,R.drawable.navigation_search ,
	//		R.drawable.navigation_setting
	//		};
	private Context mContext;

	private LayoutInflater inflater;

	public MenuListAdapter(List<Menu> list  ,Context mContext){
		this.mContext = mContext;
		this.inflater = LayoutInflater.from(mContext);
		mList = list;
	}

	public MenuListAdapter(){}


	public int getCount() {
		//return listTextSrc.length;
		return mList ==null ? 0:  mList.size();
	}


	public Object getItem(int arg0) {
		//return mContext.getResources().getString(listTextSrc[arg0]);
		return mList.get(arg0);
	}


	public long getItemId(int arg0) {
		return arg0;
	}


	public View getView(int position, View convertView, ViewGroup arg2) {
		ViewHolder viewHolder;
		if(convertView == null){
			viewHolder = new ViewHolder();
			convertView = inflater.inflate(R.layout.menu_item_list, null);
			viewHolder.ivSelected = (ImageView)convertView.findViewById(R.id.ivSelected);
			viewHolder.ivIcon = (ImageView)convertView.findViewById(R.id.ivIcon);
			viewHolder.tvMenu = (TextView)convertView.findViewById(R.id.tvMenu);
		}else{
			viewHolder = (ViewHolder) convertView.getTag();
		}
		//viewHolder.tvMenu.setText(mContext.getResources().getString(listTextSrc[position]));
		viewHolder.tvMenu.setText(  mList.get(position).getMenuName());
		//viewHolder.ivIcon.setBackgroundResource(listImageSrc[position]);
		viewHolder.ivIcon.setBackgroundResource(mList.get(position).getImage());
		if( mList.get(position).isSelected()){
			viewHolder.ivSelected.setVisibility(View.VISIBLE);
		}else{
			viewHolder.ivSelected.setVisibility(View.GONE);
		}

		convertView.setTag(viewHolder);
		return convertView;
	}

	class ViewHolder {
		ImageView ivSelected;
		ImageView ivIcon;
		TextView tvMenu;
	}

}
