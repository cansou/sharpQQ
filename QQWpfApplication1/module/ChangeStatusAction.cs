using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class ChangeStatusAction:AbstractHttpAction
    {

        

	private QQStatus status;

	/**
	 * <p>Constructor for ChangeStatusAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener} object.
	 * @param status a {@link iqq.im.bean.QQStatus} object.
	 */
	public ChangeStatusAction(QQContext context, QQActionListener.OnActionEvent listener,
			QQStatus status) :base(context,listener){
		this.status = status;
	}

	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		QQSession session = getContext().getSession();

		QQHttpRequest req = createHttpRequest("GET",
				QQConstants.URL_CHANGE_STATUS);
        //req.addGetValue("newstatus", status.getValue());
		req.addGetValue("clientid", session.getClientId() + "");
		req.addGetValue("psessionid", session.getSessionId());
		req.addGetValue("t",DateTime.Now.Ticks / 1000 + "");

		req.addHeader("Referer", QQConstants.REFFER);
		return req;
	}

	/** {@inheritDoc} */
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		JSONObject json = new JSONObject(response.getResponseString());
		if (json.getInt("retcode") == 0) {
			getContext().getAccount().setStatus(status);
			notifyActionEvent(QQActionEvent.Type.EVT_OK, status);
		}else{
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, 
				new QQException(QQWpfApplication1.action.QQException.QQErrorCode.UNEXPECTED_RESPONSE, response.getResponseString()));
		}
	}


    }
}
