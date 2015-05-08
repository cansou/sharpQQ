using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class GetFriendInfoAction:AbstractHttpAction
    {
        
	private QQUser buddy;
	/**
	 * <p>Constructor for GetFriendInfoAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener} object.
	 * @param buddy a {@link iqq.im.bean.QQUser} object.
	 */
	public GetFriendInfoAction(QQContext context, QQActionListener.OnActionEvent listener, QQUser buddy) :base(context,listener){
		this.buddy = buddy;
	}
	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		QQSession session = getContext().getSession();
		/*
		tuin	236557647
		verifysession	
		code	
		vfwebqq	efa425e6afa21b3ca3ab8db97b65afa0535feb4af47a38cadcf1a4b1650169b4b4eee9955f843990
		t	1346856270187*/
		
		
		
		QQHttpRequest req = createHttpRequest("GET", QQConstants.URL_GET_FRIEND_INFO);
		req.addGetValue("tuin", buddy.getUin() + "");
		req.addGetValue("verifysession", "");	//难道有验证码？？？
		req.addGetValue("code", "");
		req.addGetValue("vfwebqq", session.getVfwebqq()); 
		req.addGetValue("t", DateTime.Now.Ticks/1000+"");

		req.addHeader("Referer", QQConstants.REFFER);
		return req;
	}
	
	
	/** {@inheritDoc} */
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		JSONObject json = new JSONObject(response.getResponseString());
        if (json.getInt("retcode") == 0) {
            JSONObject obj = json.getJSONObject("result");
                //buddy.setBirthday(DateTime.FromBinary(obj.getJSONObject("birthday")));
            buddy.setOccupation(obj.getString("occupation"));
            buddy.setPhone(obj.getString("phone"));
            //buddy.setAllow(QQAllow.values()[obj.getInt("allow")]);
            buddy.setCollege(obj.getString("college"));
            if (!obj.isNull("reg_time")) {
                buddy.setRegTime(obj.getInt("reg_time"));
            }
            buddy.setUin(obj.getLong("uin"));
            buddy.setConstel(obj.getInt("constel"));
            buddy.setBlood(obj.getInt("blood"));
            buddy.setHomepage(obj.getString("homepage"));
            buddy.setStat(obj.getInt("stat"));
            buddy.setVipLevel(obj.getInt("vip_info")); // VIP等级 0为非VIP
            buddy.setCountry(obj.getString("country"));
            buddy.setCity(obj.getString("city"));
            buddy.setPersonal(obj.getString("personal"));
            buddy.setNickname(obj.getString("nick"));
            buddy.setChineseZodiac(obj.getInt("shengxiao"));
            buddy.setEmail(obj.getString("email"));
            buddy.setProvince(obj.getString("province"));
            buddy.setGender(obj.getString("gender"));
            buddy.setMobile(obj.getString("mobile"));
            if (!obj.isNull("client_type")) {
                //buddy.setClientType(QQClientType.valueOfRaw(obj.getInt("client_type")));
            }
        }
        
        notifyActionEvent(QQActionEvent.Type.EVT_OK, buddy);
	}

    }
}
