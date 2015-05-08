using QQWpfApplication1.bean;
using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class GetGroupMemberStatusAction:AbstractHttpAction
    {
        
	private QQGroup group;
	
	/**
	 * <p>Constructor for GetGroupMemberStatusAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener} object.
	 * @param group a {@link iqq.im.bean.QQGroup} object.
	 */
	public GetGroupMemberStatusAction(QQContext context,
			QQActionListener.OnActionEvent listener, QQGroup group) :base(context,listener){
		this.group = group;
	}
	
	/** {@inheritDoc} */
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		JSONObject json = new JSONObject(response.getResponseString());
		if(json.getInt("retcode") == 0){
			json = json.getJSONObject("result");
			
			// 消除所有成员状态，如果不在线的，webqq是不会返回的。
			foreach(QQGroupMember member in  group.getMembers()){
				member.setStatus(QQStatus.OFFLINE);
                //member.setClientType(QQClientType.UNKNOWN);
			}
			
			//result/stats
			JSONArray stats = json.getJSONArray("stats");
			for(int i=0; i<stats.length(); i++){
				// 下面重新设置最新状态
				JSONObject stat = stats.getJSONObject(i);
				QQGroupMember member = group.getMemberByUin(stat.getLong("uin"));
				if(member != null){
                    //member.setClientType(QQClientType.valueOfRaw(stat.getInt("client_type")));
                    //member.setStatus(QQStatus.valueOfRaw(stat.getInt("stat")));
				}
			}
			
			notifyActionEvent(QQActionEvent.Type.EVT_OK, group);
		}else{
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, QQWpfApplication1.action.QQException.QQErrorCode.UNEXPECTED_RESPONSE);
		}
	}

	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		QQHttpRequest req = createHttpRequest("GET", QQConstants.URL_GET_GROUP_INFO_EXT);
		req.addGetValue("gcode", group.getCode() + "");
		req.addGetValue("vfwebqq", getContext().getSession().getVfwebqq());
		req.addGetValue("t", DateTime.Now.Ticks/1000 + "");
		return req;
	}
	

	
	

    }
}
