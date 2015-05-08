using QQWpfApplication1.bean;
using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class UpdateGroupMessageFilterAction:AbstractHttpAction
    {

        


	/**
	 * <p>Constructor for UpdateGroupMessageFilterAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener} object.
	 */
	public UpdateGroupMessageFilterAction(QQContext context, QQActionListener.OnActionEvent listener) :base(context,listener){
	}

	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		// retype:1 app:EQQ
		// itemlist:{"groupmask":{"321105219":"1","1638195794":"0","cAll":0,"idx":1075,"port":37883}}
		// vfwebqq:8b26c442e239630f250e1e74d135fd85ab78c38e7b8da1c95a2d1d560bdebd2691443df19d87e70d
		QQStore store = getContext().getStore();
		QQSession session = getContext().getSession();
		QQHttpRequest req = createHttpRequest("POST", QQConstants.URL_GROUP_MESSAGE_FILTER);
		req.addPostValue("retype", "1");	// 群？？？
		req.addPostValue("app", "EQQ");
		
		JSONObject groupmask = new JSONObject();
		groupmask.put("cAll", 0);
		groupmask.put("idx", session.getIndex());
		groupmask.put("port", session.getPort());
		foreach(QQGroup g in store.getGroupList()) {
			if(g.getGin() > 0) {
				groupmask.put(g.getGin() + "", g.getMask() + "");
			}
		}
		JSONObject itemlist = new JSONObject();
		itemlist.put("groupmask", groupmask);
		req.addPostValue("itemlist", itemlist.ToString());
		req.addPostValue("vfwebqq", getContext().getSession().getVfwebqq());
		
		return req;
	}

	/** {@inheritDoc} */
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		// {"result":null,"retcode":0}
		JSONObject json = new JSONObject(response.getResponseString());
		if(json.getInt("retcode") == 0){
			notifyActionEvent(QQActionEvent.Type.EVT_OK, getContext().getStore().getGroupList());
		} else {
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, QQWpfApplication1.action.QQException.QQErrorCode.UNEXPECTED_RESPONSE);
		}
	}


    }
}
