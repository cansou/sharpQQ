using QQWpfApplication1.action;
using QQWpfApplication1.bean;
using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.action
{
    class GetBuddyListAction:AbstractHttpAction
    {
	public GetBuddyListAction(QQContext context, QQActionListener.OnActionEvent listener) :base(context, listener){
		
	}

	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		QQSession session = getContext().getSession();
        QQAccount account = (QQAccount)getContext().getAccount();
		ApacheHttpService httpService = getContext().getSerivce();
		Cookie ptwebqq = httpService.getCookie("ptwebqq", QQConstants.URL_GET_USER_CATEGORIES);
		
		JSONObject json = new JSONObject();
		json.put("h", "hello");
		json.put("vfwebqq", session.getVfwebqq()); // 同上
		json.put("hash", QQEncryptor.getHash(account.getUin() + "", ptwebqq.Value));

		QQHttpRequest req = createHttpRequest("POST",
				QQConstants.URL_GET_USER_CATEGORIES);
		req.addPostValue("r", json.ToString());

		req.addHeader("Referer", QQConstants.REFFER);

		return req;
	}

	/** {@inheritDoc} */
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		JSONObject json = new JSONObject(response.getResponseString());
		int retcode = json.getInt("retcode");
		QQStore store = getContext().getStore();
		if (retcode == 0) {
			// 处理好友列表
			JSONObject results = json.getJSONObject("result");
			// 获取JSON列表信息
			JSONArray jsonCategories = results.getJSONArray("categories");
			// 获取JSON好友基本信息列表 flag/uin/categories
			JSONArray jsonFriends = results.getJSONArray("friends");
			// face/flag/nick/uin
			JSONArray jsonInfo = results.getJSONArray("info");
			// uin/markname/
			JSONArray jsonMarknames = results.getJSONArray("marknames");
			// vip_level/u/is_vip
			JSONArray jsonVipinfo = results.getJSONArray("vipinfo");

			// 默认好友列表
			QQCategory c = new QQCategory();
			c.setIndex(0);
			c.setName("我的好友");
			c.setSort(0);
			store.addCategory(c);
			// 初始化好友列表
			for (int i = 0; i < jsonCategories.length(); i++) {
				JSONObject jsonCategory = jsonCategories.getJSONObject(i);
				QQCategory qqc = new QQCategory();
				qqc.setIndex(jsonCategory.getInt("index"));
				qqc.setName(jsonCategory.getString("name"));
				qqc.setSort(jsonCategory.getInt("sort"));
				store.addCategory(qqc);
			}
			// 处理好友基本信息列表 flag/uin/categories
			for (int i = 0; i < jsonFriends.length(); i++) {
				QQBuddy buddy = new QQBuddy();
				JSONObject jsonFriend = jsonFriends.getJSONObject(i);
				long uin = jsonFriend.getLong("uin");
				buddy.setUin(uin);
				buddy.setStatus(QQStatus.OFFLINE);
				// 添加到列表中
				int category = jsonFriend.getInt("categories");
				QQCategory qqCategory = store.getCategoryByIndex(category);
				buddy.setCategory(qqCategory);
				qqCategory.getBuddyList().Add(buddy);

				// 记录引用
				store.addBuddy(buddy);
			}
			// face/flag/nick/uin
			for (int i = 0; i < jsonInfo.length(); i++) {
				JSONObject info = jsonInfo.getJSONObject(i);
				long uin = info.getLong("uin");
				QQBuddy buddy = store.getBuddyByUin(uin);
				buddy.setNickname(info.getString("nick"));
			}
			// uin/markname
			for (int i = 0; i < jsonMarknames.length(); i++) {
				JSONObject jsonMarkname = jsonMarknames.getJSONObject(i);
				long uin = jsonMarkname.getLong("uin");
				QQBuddy buddy = store.getBuddyByUin(uin);
				if(buddy != null){
					buddy.setMarkname(jsonMarkname.getString("markname"));
				}
			}
			// vip_level/u/is_vip
			for (int i = 0; i < jsonVipinfo.length(); i++) {
				JSONObject vipInfo = jsonVipinfo.getJSONObject(i);
				long uin = vipInfo.getLong("u");
				QQBuddy buddy = store.getBuddyByUin(uin);
				buddy.setVipLevel(vipInfo.getInt("vip_level"));
				int isVip = vipInfo.getInt("is_vip");
				if(isVip != 0) {
					buddy.setVip(true);
				} else {
					buddy.setVip(false);
				}
			}

			notifyActionEvent(QQActionEvent.Type.EVT_OK, store.getCategoryList());

		} else {
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, null);
		}

	}


    }
}
