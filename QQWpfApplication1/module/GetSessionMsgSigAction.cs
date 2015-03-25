using QQWpfApplication1.bean;
using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class GetSessionMsgSigAction:AbstractHttpAction
    {
        
	private QQStranger user;

	/**
	 * <p>Constructor for GetSessionMsgSigAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener} object.
	 * @param user a {@link iqq.im.bean.QQStranger} object.
	 */
	public GetSessionMsgSigAction(QQContext context, QQActionListener.OnActionEvent listener,
			QQStranger user) :base(context,listener){
		this.user = user;
	}

	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		QQSession session = getContext().getSession();
		QQHttpRequest req = createHttpRequest("GET",
				QQConstants.URL_GET_SESSION_MSG_SIG);
		if(user is QQGroupMember) {
			QQGroupMember mb = (QQGroupMember) user;
			mb.setServiceType(0);
			req.addGetValue("id", mb.getGroup().getGin() + "");
			req.addGetValue("service_type", "0"); // 0为群，1为讨论组
		} else if(user is QQDiscuzMember) {
			QQDiscuzMember mb = (QQDiscuzMember) user;
			mb.setServiceType(1);
			req.addGetValue("id", mb.getDiscuz().getDid() + "");
			req.addGetValue("service_type", "1"); // 0为群，1为讨论组
		} else {
		}
		req.addGetValue("to_uin", user.getUin() + "");
		req.addGetValue("clientid", session.getClientId() + "");
		req.addGetValue("psessionid", session.getSessionId());
		req.addGetValue("t", DateTime.Now.Ticks / 1000 + "");
		return req;
	}

    public override void onHttpStatusOK(QQHttpResponse response)
    {
		// {"retcode":0,"result":{"type":0,"value":"sig","flags":{"text":1,"pic":1,"file":1,"audio":1,"video":1}}}
		JSONObject json = new JSONObject(response.getResponseString());
		int retcode = json.getInt("retcode");

		if (retcode == 0) {
			JSONObject result = json.getJSONObject("result");
			if (result.has("value")) {
				user.setGroupSig(result.getString("value"));
				notifyActionEvent(QQActionEvent.Type.EVT_OK, user);
				return;
			}
		}

		notifyActionEvent(QQActionEvent.Type.EVT_ERROR, new QQException(
				QQWpfApplication1.action.QQException.QQErrorCode.UNEXPECTED_RESPONSE, json.ToString()));
	}

    }
}
