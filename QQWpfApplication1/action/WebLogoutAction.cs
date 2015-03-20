using QQWpfApplication1.evt;
using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.module
{
    class WebLogoutAction:AbstractHttpAction
    {
        

	/**
	 * <p>Constructor for WebLogoutAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 */
	public WebLogoutAction(QQContext context, QQActionListener.OnActionEvent listener) :base(context, listener){
	}

	/** {@inheritDoc} */
	
	protected QQHttpRequest onBuildRequest() {
		QQSession session = getContext().getSession();

		QQHttpRequest req = createHttpRequest("GET", QQConstants.URL_LOGOUT);
		req.addGetValue("ids", ""); // 产生过会话才出现ID，如何获取？？
		req.addGetValue("clientid", session.getClientId() + "");
		req.addGetValue("psessionid", session.getSessionId());
		req.addGetValue("t", DateTime.Now.Ticks / 1000 + "");

		req.addHeader("Referer", QQConstants.REFFER);
		return req;
	}

	/** {@inheritDoc} */
	
	protected void onHttpStatusOK(QQHttpResponse response) {
		JSONObject json = new JSONObject(new JSONTokener(new StringReader(response.getResponseString())));
		String isOK = json.getString("result");
		if (json.getInt("retcode") == 0) {
			if (isOK.Equals("ok", StringComparison.OrdinalIgnoreCase)) {
				notifyActionEvent(QQActionEvent.Type.EVT_OK, isOK);
				return;
			}
		}

		notifyActionEvent(QQActionEvent.Type.EVT_ERROR, isOK);
	}


    }
}
