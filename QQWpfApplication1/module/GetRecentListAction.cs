using QQWpfApplication1.bean;
using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class GetRecentListAction:AbstractHttpAction
    {
	public GetRecentListAction(QQContext context, QQActionListener.OnActionEvent listener) :base(context,listener){
	}

    public override QQHttpRequest onBuildRequest()
    {
		QQSession session = getContext().getSession();

		JSONObject json = new JSONObject();
		json.put("vfwebqq", session.getVfwebqq());
		json.put("clientid", session.getClientId()); 
		json.put("psessionid", session.getSessionId());
		
		QQHttpRequest req = createHttpRequest("POST", QQConstants.URL_GET_RECENT_LIST);
		req.addPostValue("r", json.ToString());
		req.addPostValue("clientid", session.getClientId()+"");
		req.addPostValue("psessionid", session.getSessionId());

		return req;
	}
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		JSONObject json = new JSONObject(response.getResponseString());
		List<Object> recents = new List<Object>();
		QQStore store = getContext().getStore();
        if (json.getInt("retcode") == 0) {
            JSONArray result = json.getJSONArray("result");
            for(int i=0; i<result.length(); i++){
            	 JSONObject rejson = result.getJSONObject(i);
            	 switch(rejson.getInt("type")){
            	 case 0:{	//好友
            		 QQBuddy buddy = store.getBuddyByUin(rejson.getLong("uin"));
            		 if(buddy != null){
            			 recents.Add(buddy);
            		 }
            	 } break;
            	 
            	 case 1: {	//群
            		 QQGroup group = store.getGroupByCode(rejson.getLong("uin"));
            		 if(group != null){
            			 recents.Add(group);
            		 }
            	 } break;
            	 
            	 case 2: {	//讨论组
            		 QQDiscuz discuz = store.getDiscuzByDid(rejson.getLong("uin"));
            		 if(discuz != null){
            			 recents.Add(discuz);
            		 }
                     break;
            	 }
            	 }
            }
            notifyActionEvent(QQActionEvent.Type.EVT_OK, recents);
        }else{
        	notifyActionEvent(QQActionEvent.Type.EVT_ERROR, new QQException(QQWpfApplication1.action.QQException.QQErrorCode.UNEXPECTED_RESPONSE));
        }
	}
	
	


    }
}
