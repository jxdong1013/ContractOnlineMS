package com.jxd.contractonlinems.activity;

import java.io.File;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

import com.jxd.contractonlinems.R;

import android.app.AlertDialog;
import android.app.AlertDialog.Builder;
import android.app.ListActivity;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;

public class FileManagerActivity extends ListActivity {
	private List<String> items = null;
	private List<String> paths = null;
	private String rootPath = "/";
	private String curPath = "/";
	private TextView mPath;
	private File file;
	private String selectPath;

	@Override
	protected void onCreate(Bundle icicle) {
		super.onCreate(icicle);

		//requestWindowFeature(Window.FEATURE_NO_TITLE);

		setContentView(R.layout.fileselect);
		mPath = (TextView) findViewById(R.id.mPath);

		curPath = GetSDCardPath();

		//getFileDir(rootPath);
		getFileDir(curPath);
	}

	protected String GetSDCardPath(){
		boolean hasSD = Environment.getExternalStorageState().equals(
				android.os.Environment.MEDIA_MOUNTED);
		String path = "";
		if( hasSD){
			path = Environment.getExternalStorageDirectory().getPath();
		}else{
			path="/";
		}
		return path;
	}

	private void getFileDir(String filePath) {
		mPath.setText(filePath);
		items = new ArrayList<String>();
		paths = new ArrayList<String>();
		File f = new File(filePath);
		File[] files = f.listFiles();

		if (!filePath.equals(rootPath)) {
			items.add("b1");
			paths.add(rootPath);
			items.add("b2");
			paths.add(f.getParent());
		}

		if( files !=null){
			for (int i = 0; i < files.length; i++) {
				File file = files[i];
				items.add(file.getName());
				paths.add(file.getPath());
			}
		}

		setListAdapter(new MyAdapter(this, items, paths));
	}

	@Override
	protected void onListItemClick(ListView l, View v, int position, long id) {
		file = new File(paths.get(position));
		if (file.isDirectory()) {
			curPath = paths.get(position);
			getFileDir(paths.get(position));
		} else {
			selectPath = paths.get(position);
			//showSimpleChoiceDialog();
			Intent data = FileManagerActivity.this.getIntent();
			data.putExtra("file", selectPath);
			setResult(RESULT_OK, data);
			finish();
		}
	}



	private void showSimpleChoiceDialog() {
		final String[] methods = new String[] { "选择", "预览" };
		Builder dialog = new AlertDialog.Builder(FileManagerActivity.this);
		dialog.setTitle("简单选择对话框");

		dialog.setItems(methods, new DialogInterface.OnClickListener() {
			public void onClick(DialogInterface dialog, int which) {
				switch (which) {
				case 0:
					//Intent data = new Intent(FileManagerActivity.this,
					//		MainAcivity.class);
					Intent data = FileManagerActivity.this.getIntent();
					data.putExtra("file", selectPath);
					setResult(RESULT_OK, data);
					finish();

					break;
				case 1:
					openFile(file);
					break;

				default:
					break;
				}

			}
		});
		dialog.show();

	}

	private void openFile(File f) {
		Intent intent = new Intent();
		intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
		intent.setAction(android.content.Intent.ACTION_VIEW);

		String type = getMIMEType(f);
		intent.setDataAndType(Uri.fromFile(f), type);
		startActivity(intent);
	}

	private String getMIMEType(File f) {
		String type = "";
		String fName = f.getName();
		String end = fName
				.substring(fName.lastIndexOf(".") + 1, fName.length())
				.toLowerCase(Locale.getDefault());

		if (end.equals("m4a") || end.equals("mp3") || end.equals("mid")
				|| end.equals("xmf") || end.equals("ogg") || end.equals("wav")) {
			type = "audio";
		} else if (end.equals("3gp") || end.equals("mp4")) {
			type = "video";
		} else if (end.equals("jpg") || end.equals("gif") || end.equals("png")
				|| end.equals("jpeg") || end.equals("bmp")) {
			type = "image";
		} else {
			type = "*";
		}
		type += "/*";
		return type;
	}

	public class MyAdapter extends BaseAdapter {
		private LayoutInflater mInflater;
		private Bitmap mIcon1;
		private Bitmap mIcon2;
		private Bitmap mIcon3;
		private Bitmap mIcon4;
		private List<String> items;
		private List<String> paths;

		public MyAdapter(Context context, List<String> it, List<String> pa) {
			mInflater = LayoutInflater.from(context);
			items = it;
			paths = pa;
			mIcon1 = BitmapFactory.decodeResource(context.getResources(),
					R.drawable.back01);
			mIcon2 = BitmapFactory.decodeResource(context.getResources(),
					R.drawable.back02);
			mIcon3 = BitmapFactory.decodeResource(context.getResources(),
					R.drawable.folder);
			mIcon4 = BitmapFactory.decodeResource(context.getResources(),
					R.drawable.doc);
		}

		public int getCount() {
			return items.size();
		}

		public Object getItem(int position) {
			return items.get(position);
		}

		public long getItemId(int position) {
			return position;
		}

		public View getView(int position, View convertView, ViewGroup parent) {
			ViewHolder holder;

			if (convertView == null) {
				convertView = mInflater.inflate(R.layout.file_row, null);
				holder = new ViewHolder();
				holder.text = (TextView) convertView.findViewById(R.id.text);
				holder.icon = (ImageView) convertView.findViewById(R.id.icon);

				convertView.setTag(holder);
			} else {
				holder = (ViewHolder) convertView.getTag();
			}

			File f = new File(paths.get(position).toString());
			if (items.get(position).toString().equals("b1")) {
				holder.text.setText("返回根目录..");
				holder.icon.setImageBitmap(mIcon1);
			} else if (items.get(position).toString().equals("b2")) {
				holder.text.setText("返回上一层..");
				holder.icon.setImageBitmap(mIcon2);
			} else {
				holder.text.setText(f.getName());
				if (f.isDirectory()) {
					holder.icon.setImageBitmap(mIcon3);
				} else {
					holder.icon.setImageBitmap(mIcon4);
				}
			}
			return convertView;
		}

		private class ViewHolder {
			TextView text;
			ImageView icon;
		}
	}

}