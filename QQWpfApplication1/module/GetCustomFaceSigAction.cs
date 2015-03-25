using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class GetCustomFaceSigAction:AbstractHttpAction
    {
        
	public GetCustomFaceSigAction(QQContext context, QQActionListener.OnActionEvent listener) :base(context,listener){
	}

	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		QQSession session = getContext().getSession();

		QQHttpRequest req = createHttpRequest("GET",
				QQConstants.URL_CUSTOM_FACE_SIG);
		req.addGetValue("clientid", session.getClientId() + "");
		req.addGetValue("psessionid", session.getSessionId());
		req.addGetValue("t", DateTime.Now.Ticks / 1000 + "");

		req.addHeader("Referer", QQConstants.REFFER);
		return req;
	}

	/** {@inheritDoc} */
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		QQSession session = getContext().getSession();

		JSONObject json = new JSONObject(response.getResponseString());
		if (json.getInt("retcode") == 0) {
			JSONObject obj = json.getJSONObject("result");
			session.setCfaceKey(obj.getString("gface_key"));
			session.setCfaceSig(obj.getString("gface_sig"));
			notifyActionEvent(QQActionEvent.Type.EVT_OK, session);
		}else{
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, 
				new QQException(QQWpfApplication1.action.QQException.QQErrorCode.UNEXPECTED_RESPONSE, response.getResponseString()));
		}
	}


    }
}
