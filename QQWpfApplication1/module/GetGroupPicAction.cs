using QQWpfApplication1.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class GetGroupPicAction:AbstractHttpAction
    {

        
	private CFaceItem cface;
	private QQMsg msg;
	private byte[] picOut;
	
	/**
	 * <p>Constructor for GetGroupPicAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener} object.
	 * @param cface a {@link iqq.im.bean.content.CFaceItem} object.
	 * @param msg a {@link iqq.im.bean.QQMsg} object.
	 * @param picOut a {@link java.io.OutputStream} object.
	 */
	public GetGroupPicAction(QQContext context, QQActionListener.OnActionEvent listener,
										CFaceItem cface, QQMsg msg, byte[] picOut):base(context,listener) {
		this.cface = cface;
		this.msg = msg;
		this.picOut = picOut;
	}
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		notifyActionEvent(QQActionEvent.Type.EVT_OK, cface);
	}

	/* (non-Javadoc)
	 * @see iqq.im.action.AbstractHttpAction#onBuildRequest()
	 */
	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		QQHttpRequest req = createHttpRequest("GET", QQConstants.URL_GET_GROUP_PIC);
		
//		fid	3648788200
//		gid	2890126166
//		pic	{F2B04C26-9087-437D-4FD9-6A0ED84155FD}.jpg
//		rip	123.138.154.167
//		rport	8000
//		t	1365343106
//		type	0
//		uin	3559750777
//		vfwebqq	70b5f77bfb1db1367a2ec483ece317ea9ef119b9b59e542b2e8586f7ede6030ff56f7ba8798ba34b
//		"cface",
//        {
//            "name": "{F2B04C26-9087-437D-4FD9-6A0ED84155FD}.jpg",
//            "file_id": 3648788200,
//            "key": "pcm4N6IKmQ852Pus",
//            "server": "123.138.154.167:8000"
//        }
		
		QQSession session  = getContext().getSession();
		req.addGetValue("fid", cface.getFileId() + "");
		req.addGetValue("gid", (msg.getGroup() != null ? 
						msg.getGroup().getCode(): msg.getDiscuz().getDid()) + "");
		req.addGetValue("pic", cface.getFileName());
		String[] parts = cface.getServer().Split(':');
		req.addGetValue("rip", parts[0]);
		req.addGetValue("rport", parts[1]);
		req.addGetValue("t", DateTime.Now.Ticks/1000+ "");
		req.addGetValue("type", msg.getGroup() != null ? "0" : "1");
		req.addGetValue("uin", msg.getFrom().getUin() + "");
		req.addGetValue("vfwebqq", session.getVfwebqq());
		
        //req.setOutputStream(picOut);
		return req;
	}
	
	


    }
}
