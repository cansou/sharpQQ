using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class GetFriendSignAction:AbstractHttpAction
    {
        

	private QQUser buddy;

	/**
	 * <p>Constructor for GetFriendSignAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener} object.
	 * @param buddy a {@link iqq.im.bean.QQUser} object.
	 */
	public GetFriendSignAction(QQContext context, QQActionListener.OnActionEvent listener,
			QQUser buddy) :base(context,listener){
		this.buddy = buddy;
	}

	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		QQSession session = getContext().getSession();

		QQHttpRequest req = createHttpRequest("GET",
				QQConstants.URL_GET_USER_SIGN);
		req.addGetValue("tuin", buddy.getUin() + "");
		req.addGetValue("vfwebqq", session.getVfwebqq());
		req.addGetValue("t", DateTime.Now.Ticks/ 1000 + "");

		req.addHeader("Referer", QQConstants.REFFER);
		return req;
	}

	/** {@inheritDoc} */
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		JSONObject json = new JSONObject(response.getResponseString());
		if (json.getInt("retcode") == 0) {
			JSONArray result = json.getJSONArray("result");
			JSONObject obj = result.getJSONObject(0);
			buddy.setSign(obj.getString("lnick"));
		}

		notifyActionEvent(QQActionEvent.Type.EVT_OK, buddy);
	}


    }
}
