using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class GetFriendAccoutAction:AbstractHttpAction
    {
        

	private QQUser buddy;

	/**
	 * <p>Constructor for GetFriendAccoutAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener} object.
	 * @param buddy a {@link iqq.im.bean.QQUser} object.
	 */
    public GetFriendAccoutAction(QQContext context, QQActionListener.OnActionEvent listener, QQUser buddy)
        : base(context, listener)
    {
		this.buddy = buddy;
	}

	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		QQSession session = getContext().getSession();
		// tuin=4245757755&verifysession=&type=1&code=&vfwebqq=**&t=1361631644492
		QQHttpRequest req = createHttpRequest("GET",
				QQConstants.URL_GET_USER_ACCOUNT);
		req.addGetValue("tuin", buddy.getUin() + "");
		req.addGetValue("vfwebqq", session.getVfwebqq());
		req.addGetValue("t", DateTime.Now.Ticks / 1000 + "");
		req.addGetValue("verifysession", ""); // 验证码？？
		req.addGetValue("type", 1 + "");
		req.addGetValue("code", "");

		req.addHeader("Referer", QQConstants.REFFER);
		return req;
	}

	/** {@inheritDoc} */
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		JSONObject json = new JSONObject(response.getResponseString());
		if (json.getInt("retcode") == 0) {
			JSONObject obj = json.getJSONObject("result");
			buddy.setQQ(obj.getLong("account"));
		}
		
		notifyActionEvent(QQActionEvent.Type.EVT_OK, buddy);
	}


    }
}
