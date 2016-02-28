package com.jxd.contractonlinems.widget;

import java.util.Locale;

import com.jxd.contractonlinems.R;
import com.jxd.contractonlinems.app.ContractApplication;
import com.jxd.contractonlinems.util.PreferencesUtils;
import com.jxd.contractonlinems.util.ToastUtil;

import android.app.Activity;
import android.app.Dialog;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.Window;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.LinearLayout.LayoutParams;

/*
 * ’⁄’÷≤„µØ≥ˆøÚ
 */
public class MaskDialogView {
	private Dialog dialog;

	public MaskDialogView(Activity mActivity) {
		dialog = new Dialog(mActivity, R.style.mask_dialog);

		final LinearLayout popView = (LinearLayout) LayoutInflater.from(
				mActivity).inflate(R.layout.mask_dialog_view, null);

		// πÿ±’∞¥≈•
		ImageView viewClose = (ImageView) popView.findViewById(R.id.win_close);
		viewClose.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				hide();
			}
		});

		dialog.setContentView(popView, new LinearLayout.LayoutParams(
				LayoutParams.MATCH_PARENT, LayoutParams.MATCH_PARENT));
		dialog.setFeatureDrawableAlpha(Window.FEATURE_OPTIONS_PANEL, 0);

		EditText eturl = (EditText) popView.findViewById(R.id.eturl);
		eturl.setText(ContractApplication.Host);
		// EditText etPort = (EditText)popView.findViewById(R.id.etport);
		// etPort.setText(ContractApplication.Port);

		// ±£¥Ê ∞¥≈•
		Button btnSaveSetting = (Button) popView
				.findViewById(R.id.btnSaveConfig);
		btnSaveSetting.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View arg0) {
				EditText et1 = (EditText) popView.findViewById(R.id.eturl);
				String url = et1.getText().toString().trim();
				// EditText et2 = (EditText)popView.findViewById(R.id.etport);
				// String port =et2.getText().toString();
				if (url == null || url.isEmpty()) {
					ToastUtil.Show("«Î ‰»ÎÕ¯÷∑°£");
					return;
				}
				// if(port==null || port.isEmpty()){
				// ToastUtil.Show("«Î ‰»Î∂Àø⁄∫≈");
				// return;
				// }
				String temp = url.toLowerCase(Locale.getDefault());
				if (false == temp.startsWith("http://")) {
					url = "http://" + url;
				}
				if (false == url.endsWith("/")) {
					url += "/";
				}

				PreferencesUtils
						.putString(ContractApplication.mApp, "Url", url);
				// PreferencesUtils.putString(ContractApplication.mApp ,"Port",
				// port);
				ContractApplication.Host = url;
				// ContractApplication.Port = port;
				hide();
			}
		});
	}

	public void SetUrl() {
		EditText eturl = (EditText) dialog.findViewById(R.id.eturl);
		eturl.setText(ContractApplication.Host);
	}

	public void show() {
		dialog.show();
	}

	public void hide() {
		dialog.dismiss();
	}
}
