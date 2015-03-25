using QQWpfApplication1.bean;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace QQWpfApplication1.action
{
    public class GetGroupFaceAction:AbstractHttpAction
    {
        

	private QQGroup group;

	/**
	 * <p>Constructor for GetGroupFaceAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener} object.
	 * @param group a {@link iqq.im.bean.QQGroup} object.
	 */
	public GetGroupFaceAction(QQContext context, QQActionListener.OnActionEvent listener,
			QQGroup group) :base(context, listener){
		this.group = group;
	}

	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		QQSession session = getContext().getSession();
		QQHttpRequest req = createHttpRequest("GET",
				QQConstants.URL_GET_USER_FACE);
		req.addGetValue("uin", group.getCode() + "");
		req.addGetValue("vfwebqq", session.getVfwebqq());
		req.addGetValue("t", DateTime.Now.Ticks / 1000 + "");
		req.addGetValue("cache", "0");
		req.addGetValue("type", "4");
		req.addGetValue("fid", "0");
		return req;
	}

	/** {@inheritDoc} */
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		try {
			BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = new MemoryStream(response.getResponseData());
            bmp.EndInit();
			group.setFace(bmp);
		} catch (IOException e) {
			new QQException(QQWpfApplication1.action.QQException.QQErrorCode.IO_ERROR, e);
		}
		notifyActionEvent(QQActionEvent.Type.EVT_OK, group);
	}


    }
}
