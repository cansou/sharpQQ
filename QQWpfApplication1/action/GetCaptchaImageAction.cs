using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace QQWpfApplication1.action
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
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		try {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = new MemoryStream(response.getResponseData());
            bmp.EndInit();
            notifyActionEvent(QQActionEvent.Type.EVT_OK, bmp);
		} catch (IOException e) {
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, new QQException(QQWpfApplication1.action.QQException.QQErrorCode.UNKNOWN_ERROR, e));
		}
	}
	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		QQHttpRequest req = createHttpRequest("GET", QQConstants.URL_GET_CAPTCHA);
		req.addGetValue("aid", QQConstants.APPID);
		req.addGetValue("r", new Random().NextDouble() + "");
		req.addGetValue("uin", uin + "");
		return req;
	}

	

    }
}
