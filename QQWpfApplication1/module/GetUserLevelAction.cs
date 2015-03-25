using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class GetUserLevelAction:AbstractHttpAction
    {
        
	private QQUser user;
	/**
	 * <p>Constructor for GetUserLevelAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener} object.
	 * @param user a {@link iqq.im.bean.QQUser} object.
	 */
	public GetUserLevelAction(QQContext context, QQActionListener.OnActionEvent listener, QQUser user) :base(context,listener){
		this.user = user;
	}

	/** {@inheritDoc} */
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		JSONObject json = new JSONObject(response.getResponseString());
		if (json.getInt("retcode") == 0) {
			JSONObject result = json.getJSONObject("result");
			QQLevel level = user.getLevel();
			level.setLevel(result.getInt("level"));
			level.setDays(result.getInt("days"));
			level.setHours(result.getInt("hours"));
			level.setRemainDays(result.getInt("remainDays"));
			notifyActionEvent(QQActionEvent.Type.EVT_OK, user);
		}else{
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, 
					new QQException(QQWpfApplication1.action.QQException.QQErrorCode.UNEXPECTED_RESPONSE, response.getResponseString()));
		}
	}

	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		QQHttpRequest req = createHttpRequest("GET", QQConstants.URL_GET_USER_LEVEL);
		QQSession session  = getContext().getSession();
		req.addGetValue("tuin", user.getUin() + "");
		req.addGetValue("t", DateTime.Now.Ticks/1000+ "");
		req.addGetValue("vfwebqq", session.getVfwebqq());
		return req;
	}

    }
}
