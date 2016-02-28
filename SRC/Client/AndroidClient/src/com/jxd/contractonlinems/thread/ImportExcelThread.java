package com.jxd.contractonlinems.thread;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.LinkedList;
import java.util.List;
import java.util.Locale;

import com.jxd.contractonlinems.biz.ContractBiz;
import com.jxd.contractonlinems.bean.Contract;
//import com.jxd.contractonlinems.bean.ContractStateEnum;
import com.jxd.contractonlinems.util.ExcelUtil;
import com.jxd.contractonlinems.util.ToastUtil;
import com.jxd.contractonlinems.app.ContractApplication;

import android.content.Context;
import android.os.Handler;
import android.os.Message;

public class ImportExcelThread extends Thread {
	Context _context = null;
	String _path = "";
	Handler _handler = null;
	public static int ImportOk = 36;
	public static int ImportFalse = 37;

	public ImportExcelThread(Context context, Handler handler, String path) {
		_context = context;
		_handler = handler;
		_path = path;
	}

	@Override
	public void run() {

		try {
			LinkedList<Contract> list = ParseExcelData(_path);
			if (list == null || list.size() < 1)
			{
				Message msg=_handler.obtainMessage(ImportFalse);
				msg.obj="导入失败，Excel文件中没有数据。";
				_handler.sendMessage(msg);
				return;
			}

			ContractBiz biz = new ContractBiz(_context);
			int count = list.size();
			for (int i = 0; i < count; i++) {
				Contract c = list.get(i);
				if (biz.ExistContractByProjectNumAndProjectName(
						c.getProjectnum(), c.getProjectname())) {
					biz.UpdateByProjectNumAndProjectName(c);
				} else {
					biz.Insert(list.get(i));
				}
			}

			Message msg = _handler.obtainMessage(ImportOk);
			msg.obj = "导入" + count + "条记录";
			_handler.sendMessage(msg);

			super.run();
		} catch (Exception ex) {
			Message msg = _handler.obtainMessage(ImportFalse);
			msg.obj = "导入失败";
			_handler.sendMessage(msg);
		}
	}

	protected boolean CheckEmptyLine(List<String> line) {
		int count = line.size();
		boolean isEmpty = true;
		for (int i = 0; i < count; i++) {
			String temp = line.get(i);
			if (temp != null && temp.length() > 0) {
				isEmpty = false;
				break;
			}
		}
		return isEmpty;
	}

	/*
	 *
	 */
	protected Boolean CheckRepeatContract(List<Contract> list, Contract contract) {
		if (list == null || list.size() < 1)
			return false;
		for (Contract item : list) {
			if (item.getSeq().equals(contract.getSeq())
					&& item.getContractnum().equals(contract.getContractnum())
					&& item.getProjectnum().equals(contract.getProjectnum()))
				return true;
		}
		return false;
	}

	protected LinkedList<Contract> ParseExcelData(String path) {
		List<LinkedList<String>> data = ExcelUtil.ReadExcel(path, 0);
		if (data == null || data.size() <= 1) {
			ToastUtil.Show("没有数据!");
			return null;
		}

		int count = data.size();
		LinkedList<Contract> contracts = new LinkedList<Contract>();
		for (int i = 1; i < count; i++) {
			LinkedList<String> line = data.get(i);

			if (CheckEmptyLine(line))
				continue;

			Contract contract = new Contract();
			int cols = line.size();
			for (int k = 0; k < cols; k++) {
				if (ContractApplication.ExcelMap.containsKey(k)) {
					String name = ContractApplication.ExcelMap.get(k);
					String value = line.get(k);
					try {
						// Field field =
						// contract.getClass().getDeclaredField(name);
						String methodName = "set"
								+ name.substring(0, 1).toUpperCase(Locale.getDefault())
								+ name.substring(1);
						try {
							Method method = contract.getClass()
									.getDeclaredMethod(methodName,
											new Class[] { String.class });
							method.invoke(contract, value);

						} catch (NoSuchMethodException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						} catch (InvocationTargetException e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}

					} catch (IllegalArgumentException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					} catch (IllegalAccessException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}

			}

			//contract.setContractState(ContractStateEnum.InStore.getCode().toString());
			contract.setOperatorname(ContractApplication.mApp.mAccount.getUsername());
			contract.setUnshelve(String.valueOf(false));

			if (CheckRepeatContract(contracts, contract))
				continue;
			contracts.add(contract);
		}
		return contracts;
	}

}
