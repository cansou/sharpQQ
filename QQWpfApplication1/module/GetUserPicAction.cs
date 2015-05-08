using QQWpfApplication1.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class GetUserPicAction:AbstractHttpAction
    {
        
	private CFaceItem cface;
	private QQMsg msg;
	private byte[] picOut;
	
	/**
	 * <p>Constructor for GetUserPicAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener} object.
	 * @param cface a {@link iqq.im.bean.content.CFaceItem} object.
	 * @param msg a {@link iqq.im.bean.QQMsg} object.
	 * @param picOut a {@link java.io.OutputStream} object.
	 */
	public GetUserPicAction(QQContext context, QQActionListener.OnActionEvent listener, CFaceItem cface, QQMsg msg, byte[] picOut) :base(context,listener){
		this.cface = cface;
		this.msg = msg;
		this.picOut = picOut;
	}

	/* (non-Javadoc)
	 * @see iqq.im.action.AbstractHttpAction#onHttpStatusOK(iqq.im.http.QQHttpResponse)
	 */
	/** {@inheritDoc} */
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
		QQHttpRequest req = createHttpRequest("GET", QQConstants.URL_GET_CFACE2);
		
//		clientid=12202920
//		count=5
//		guid=4D72EF8CF64D53DECB31ABC2B601AB23.jpg
//		lcid=16059	//msg_id
//		psessionid=8368046764001e636f6e6e7365727665725f77656271714031302e3133332e34312e32303200002a5400000a2c026e04004f95190e6d0000000a40345a4e79386b71416e6d000000280adff44c88196358dadc9fa075334fd6293f7e6a0020a86cad689c240384e54cbb329be8dd5f0c3f
//		time=1
//		to=3559750777 //from_uin
		
		QQSession session  = getContext().getSession();
		req.addGetValue("clientid", session.getClientId() + "");
		req.addGetValue("to", msg.getFrom().getUin() + "");
		req.addGetValue("guid", cface.getFileName());
		req.addGetValue("psessionid", session.getSessionId());
		req.addGetValue("count", "5");
		req.addGetValue("lcid", msg.getId() + "");
		req.addGetValue("time", "1");
        //req.setOutputStream(picOut);
		return req;
	}
	
	


    }
}
