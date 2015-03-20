using QQWpfApplication1.evt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.module
{
    class GetCaptchaImageAction:AbstractHttpAction
    {
        
	private long uin;
	/**
	 * <p>Constructor for GetCaptchaImageAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @param uin a long.
	 */
	public GetCaptchaImageAction(QQContext context, QQActionListener.OnActionEvent listener, long uin):base(context, listener) {
		this.uin = uin;
	}
	/** {@inheritDoc} */
	protected void onHttpStatusOK(QQHttpResponse response){
		try {
			BinaryReader reader = new BinaryReader(response.getResponseData());
			notifyActionEvent(QQActionEvent.Type.EVT_OK, reader);
		} catch (IOException e) {
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, new QQException(QQWpfApplication1.evt.QQException.QQErrorCode.UNKNOWN_ERROR, e));
		}
	}
	/** {@inheritDoc} */
	protected QQHttpRequest onBuildRequest() {
		QQHttpRequest req = createHttpRequest("GET", QQConstants.URL_GET_CAPTCHA);
		req.addGetValue("aid", QQConstants.APPID);
		req.addGetValue("r", new Random().NextDouble() + "");
		req.addGetValue("uin", uin + "");
		return req;
	}

	

    }
}
