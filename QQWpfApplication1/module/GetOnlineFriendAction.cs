using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class GetOnlineFriendAction:AbstractHttpAction
    {
        
	public GetOnlineFriendAction(QQContext context, QQActionListener.OnActionEvent listener) :base(context,listener){
	}

	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		QQSession session = getContext().getSession();

		QQHttpRequest req = createHttpRequest("GET",
				QQConstants.URL_GET_ONLINE_BUDDY_LIST);
		req.addGetValue("clientid", session.getClientId() + "");
		req.addGetValue("psessionid", session.getSessionId());
		req.addGetValue("t", DateTime.Now.Ticks / 1000 + "");

		req.addHeader("Referer", QQConstants.REFFER);
		return req;
	}

	/** {@inheritDoc} */
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		JSONObject json = new JSONObject(response.getResponseString());
		QQStore store = getContext().getStore();
		if (json.getInt("retcode") == 0) {
			JSONArray result = json.getJSONArray("result");
			for (int i = 0; i < result.length(); i++) {
				JSONObject obj = result.getJSONObject(i);
				long uin = obj.getLong("uin");
				String status = obj.getString("status");
				int clientType = obj.getInt("client_type");
				
				QQBuddy buddy = store.getBuddyByUin(uin);
                //buddy.setStatus(QQStatus.valueOfRaw(status));
                //buddy.setClientType(QQClientType.valueOfRaw(clientType));
			}
			
		}

		notifyActionEvent(QQActionEvent.Type.EVT_OK, store.getOnlineBuddyList());
	}


    }
}
