using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace QQWpfApplication1.action
{
    public class GetFriendFaceAction:AbstractHttpAction
    {
        

	private QQUser user;

	/**
	 * <p>Constructor for GetFriendFaceAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener} object.
	 * @param user a {@link iqq.im.bean.QQUser} object.
	 */
	public GetFriendFaceAction(QQContext context, QQActionListener.OnActionEvent listener,
			QQUser user) :base(context,listener){
		this.user = user;
	}
    public override QQHttpRequest onBuildRequest()
    {
		QQSession session = getContext().getSession();
		QQHttpRequest req = createHttpRequest("GET", QQConstants.URL_GET_USER_FACE);
		req.addGetValue("uin", user.getUin() + "");
		req.addGetValue("vfwebqq", session.getVfwebqq());
		req.addGetValue("t", DateTime.Now.Ticks / 1000 + "");
		req.addGetValue("cache", 0 + ""); // ??
		req.addGetValue("type", 1 + ""); // ??
		req.addGetValue("fid", 0 + ""); // ??

		req.addHeader("Referer", QQConstants.REFFER);
		return req;
	}

    public override void onHttpStatusOK(QQHttpResponse response)
    {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = new MemoryStream(response.getResponseData());
            bmp.EndInit();
			user.setFace(bmp);
		notifyActionEvent(QQActionEvent.Type.EVT_OK, user);
	}


    }
}
