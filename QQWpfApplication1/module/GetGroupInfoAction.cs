using QQWpfApplication1.bean;
using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class GetGroupInfoAction:AbstractHttpAction
    {

        
	private QQGroup group;
	/**
	 * <p>Constructor for GetGroupInfoAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener} object.
	 * @param group a {@link iqq.im.bean.QQGroup} object.
	 */
	public GetGroupInfoAction(QQContext context, QQActionListener.OnActionEvent listener, QQGroup group) :base(context,listener){
		this.group = group;
	}

    public override void onHttpStatusOK(QQHttpResponse response)
    {
		JSONObject json = new JSONObject(response.getResponseString());
		if(json.getInt("retcode") == 0){
			json = json.getJSONObject("result");
			JSONObject ginfo = json.getJSONObject("ginfo");
			group.setMemo(ginfo.getString("memo"));
			group.setLevel(ginfo.getInt("level"));
			group.setCreateTime(DateTime.FromBinary(ginfo.getInt("createtime")));
			
			JSONArray members = ginfo.getJSONArray("members");
			for(int i=0; i<members.length(); i++){
				JSONObject memjson = members.getJSONObject(i);
				QQGroupMember member = group.getMemberByUin(memjson.getLong("muin"));
				if(member == null) {
					member = new QQGroupMember();
					group.getMembers().Add(member);
				}
				member.setUin(memjson.getLong("muin"));
				member.setGroup(group); 
				//memjson.getLong("mflag"); //TODO ...
			}
			
			//result/minfo
			JSONArray minfos = json.getJSONArray("minfo");
			for(int i=0; i<minfos.length(); i++){
				JSONObject minfo = minfos.getJSONObject(i);
				QQGroupMember member = group.getMemberByUin(minfo.getLong("uin"));
				member.setNickname(minfo.getString("nick"));
				member.setProvince(minfo.getString("province"));
				member.setCountry(minfo.getString("country"));
				member.setCity(minfo.getString("city"));
				member.setGender(minfo.getString("gender"));
			}
			
			//result/stats
			JSONArray stats = json.getJSONArray("stats");
			for(int i=0; i<stats.length(); i++){
				// 下面重新设置最新状态
				JSONObject stat = stats.getJSONObject(i);
				QQGroupMember member = group.getMemberByUin(stat.getLong("uin"));
                //member.setClientType(QQClientType.valueOfRaw(stat.getInt("client_type")));
                //member.setStatus(QQStatus.valueOfRaw(stat.getInt("stat")));
			}
			
			//results/cards
			if(json.has("cards")){
				JSONArray cards = json.getJSONArray("cards");
				for(int i=0; i<cards.length(); i++){
					JSONObject card = cards.getJSONObject(i);
					QQGroupMember member = group.getMemberByUin(card.getLong("muin"));
					if( card != null && card.has("card") && member != null ) {
						member.setCard(card.getString("card"));
					}
				}
			}
			
			//results/vipinfo
			JSONArray vipinfos = json.getJSONArray("vipinfo");
			for(int i=0; i<vipinfos.length(); i++){
				JSONObject vipinfo = vipinfos.getJSONObject(i);
				QQGroupMember member = group.getMemberByUin(vipinfo.getLong("u"));
				member.setVipLevel(vipinfo.getInt("vip_level"));
				member.setVip(vipinfo.getInt("is_vip") == 1);
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
		req.addGetValue("cb", "undefined");
		req.addGetValue("vfwebqq", getContext().getSession().getVfwebqq());
		req.addGetValue("t", DateTime.Now.Ticks/1000 + "");
		
		req.addHeader("Referer", "http://s.web2.qq.com/proxy.html?v=20110412001&callback=1&id=3");
		return req;
	}
	
	


    }
}
