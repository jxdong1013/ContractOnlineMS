package com.jxd.contractonlinems.widget;

import com.jxd.contractonlinems.R;

import android.content.Context;
import android.graphics.Rect;
import android.graphics.drawable.Drawable;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.AttributeSet;
import android.view.MotionEvent;
import android.widget.EditText;

public class RightClickEditText extends EditText {
	protected Drawable mRightD;
	protected Rect mBound=null;

	public RightClickEditText(Context context) {
		super(context);
		InitEditText();
	}
	public RightClickEditText(Context context, AttributeSet attrs) {
		super(context, attrs);
		InitEditText();
	}
	public RightClickEditText(Context context, AttributeSet attrs, int defStyle) {
		super(context, attrs, defStyle);
		InitEditText();
	}

	protected void InitEditText(){
//		this.mRightD = getResources().getDrawable(R.drawable.close);
		this.SetEditTextDrawable();
		this.addTextChangedListener(new TextWatcher() {
			public void onTextChanged(CharSequence s, int start, int before, int count) {
				SetEditTextDrawable();
			}
			public void beforeTextChanged(CharSequence s, int start, int count,
					int after) {
			}
			public void afterTextChanged(Editable s) {
			}
		});
	}

	protected void SetEditTextDrawable(){
		 if ( this.getText().toString().length() == 0){
             setCompoundDrawablesWithIntrinsicBounds(null, null, null, null);
		 } else {
			 setCompoundDrawablesWithIntrinsicBounds(null, null, this.mRightD , null);
		 }
	}

	@Override
	protected void finalize() throws Throwable {
		if(this.mRightD!=null){
			this.mRightD=null;
			this.mBound=null;
		}
		super.finalize();
	}

	@Override
	public void setCompoundDrawablesWithIntrinsicBounds(Drawable left,
			Drawable top, Drawable right, Drawable bottom) {
		if( right !=null ){
			mRightD = right;
		}
		super.setCompoundDrawablesWithIntrinsicBounds(left, top, right, bottom);
	}
//	@Override
//	public void setCompoundDrawables(Drawable left, Drawable top,
//			Drawable drawableright, Drawable bottom) {
//		if( drawableright !=null ) {
//			//this.mRightD = drawableright;
//		}
//		super.setCompoundDrawables(left, top, drawableright , bottom);
//	}

	@Override
	public boolean onTouchEvent(MotionEvent event) {
		if(this.mRightD !=null && event.getAction()== MotionEvent.ACTION_UP ){
			mBound = mRightD.getBounds();
			int x =(int) event.getX();
			int y = (int) event.getY();
			int right = this.getRight();
			int top = this.getTop();
			int topPadding = this.getPaddingTop();
			int height = this.getHeight();
			int left = this.getLeft();
			int bottomPadding = this.getPaddingBottom();
			int rightPanding = this.getPaddingRight();
			int leftPadding = this.getPaddingLeft();
			int drawwidth= mBound.width();
			if( x >= right - left - leftPadding - rightPanding- drawwidth &&
					x <= right - rightPanding &&
					y >=  topPadding &&
					y <= height - bottomPadding ){
				this.setText("");
				event.setAction(MotionEvent.ACTION_CANCEL);
			}
		}

		return super.onTouchEvent(event);
	}


}
