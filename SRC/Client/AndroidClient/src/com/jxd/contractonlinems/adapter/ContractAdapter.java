package com.jxd.contractonlinems.adapter;

import java.util.ArrayList;
import java.util.List;
import com.jxd.contractonlinems.bean.Contract;
//import com.jxd.contractonlinems.bean.ContractStateEnum;
import com.jxd.contractonlinems.R;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.CompoundButton.OnCheckedChangeListener;
import android.widget.TextView;

public class ContractAdapter extends BaseAdapter {
	List<Contract> mlist = new ArrayList<Contract>();
	protected LayoutInflater mInflater = null;
	protected boolean mSelectFlag=false;//是否显示选择控件的开关
	protected List<Integer>  mSelectedList=null;//记录被选中的列表

	public ContractAdapter(Context context, List<Contract> list , boolean selectFlag ) {
		mInflater = LayoutInflater.from(context);
		mlist = list;
		mSelectFlag = selectFlag;
		mSelectedList = new ArrayList<Integer>();
	}

	public void setContractList(List<Contract> list){
		mlist = list;
		this.notifyDataSetChanged();
	}

	public void SetSelectedFlag( boolean flag){
		mSelectFlag= flag;
		this.notifyDataSetChanged();
	}

	@Override
	public int getCount() {
		return mlist ==null? 0: mlist.size();
	}

	@Override
	public Object getItem(int position) {
		return mlist ==null?null: mlist.get(position);
	}

	@Override
	public long getItemId(int position) {
		return position;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		ViewHolder holder = new ViewHolder();
		final Contract item = mlist.get(position);
		if (convertView == null) {
			convertView = mInflater.inflate(R.layout.contractlistviewitem, null);

			holder.chkSelected = (CheckBox)convertView.findViewById(R.id.liContractSelect);

			holder.tvprojectname = (TextView) convertView
					.findViewById(R.id.liProjectName);

			holder.tvcontractbcompany = (TextView) convertView
					.findViewById(R.id.liContractBCompany);

			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}

		final Integer idx = position;
		holder.chkSelected.setVisibility( mSelectFlag ? View.VISIBLE : View.GONE);
		holder.chkSelected.setOnCheckedChangeListener(new OnCheckedChangeListener() {

			@Override
			public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
				try{
				if( mSelectedList == null) mSelectedList = new ArrayList<Integer>();
				if( isChecked){
					 if( mSelectedList.contains(idx) == false) mSelectedList.add( idx );
				}
				else {
					if( mSelectedList.contains(idx)) mSelectedList.remove(idx);
				}
				item.setSelected(isChecked);
				}catch(Exception ex){
				}
			}
		});

		holder.chkSelected.setChecked( item.getSelected() );
		//holder.tvcontractnum.setText(item.getContractnum());
		holder.tvprojectname.setText(item.getProjectname());
		holder.tvcontractbcompany.setText(item.getBcompany());

		return convertView;
	}

	class ViewHolder {
		public CheckBox chkSelected=null;
		//public TextView tvcontractnum = null;
		public TextView tvprojectname= null;
		//public TextView tvcontractstate = null;
		//public TextView tvcontractplace = null;
		//public TextView tvcontractrfid = null;
		public TextView tvcontractbcompany =null;
		//public TextView tvoperatetime_date = null;
		//public TextView tvoperatetime_time = null;
	}
}
