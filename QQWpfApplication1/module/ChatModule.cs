using QQWpfApplication1.bean;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class ChatModule:AbstractModule
    {
        
	private AbstractActionFuture doSendMsg( QQMsg msg, QQActionListener.OnActionEvent listener) {
		return pushHttpAction(new SendMsgAction(getContext(), listener, msg));
	}
	
	/**
	 * <p>sendMsg.</p>
	 *
	 * @param msg a {@link iqq.im.bean.QQMsg} object.
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @return a {@link iqq.im.evt.AbstractActionFuture} object.
	 */
    private void  doSendMsg(QQMsg msg, AbstractActionFuture future)
    {
        doSendMsg(msg, delegate(QQActionEvent evt)
        {
                future.notifyActionEvent(evt.getType(), evt.getTarget());
        });
    }
	public AbstractActionFuture sendMsg( QQMsg msg, QQActionListener.OnActionEvent listener) {
		if(msg.getType() == QQMsg.Type.SESSION_MSG) {
			 AbstractActionFuture future = new AbstractActionFuture(listener);
			QQStranger stranger = (QQStranger) msg.getTo();
			if(stranger.getGroupSig() == null || stranger.getGroupSig().Equals("")) {
				getSessionMsgSig(stranger, delegate(QQActionEvent evt) {
						if(evt.getType() == QQActionEvent.Type.EVT_OK) {
								doSendMsg(msg, future);
						}else if(evt.getType() == QQActionEvent.Type.EVT_ERROR){
							future.notifyActionEvent(evt.getType(), evt.getTarget());
						}
				});
			}
			return future;
		} else if(msg.getType() == QQMsg.Type.GROUP_MSG || msg.getType() == QQMsg.Type.DISCUZ_MSG) {
			if(getContext().getSession().getCfaceKey() == null || getContext().getSession().getCfaceKey().Equals("")) {
				 AbstractActionFuture future = new AbstractActionFuture(listener);
				getCFaceSig(delegate(QQActionEvent evt) {
						if(evt.getType() == QQActionEvent.Type.EVT_OK) {
								doSendMsg(msg, future);
						}else if(evt.getType() == QQActionEvent.Type.EVT_ERROR){
							future.notifyActionEvent(evt.getType(), evt.getTarget());
						}
				});
				return future;
			}
		}
		return doSendMsg(msg, listener);
	}
	
	/**
	 * <p>getSessionMsgSig.</p>
	 *
	 * @param user a {@link iqq.im.bean.QQStranger} object.
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @return a {@link iqq.im.evt.AbstractActionFuture} object.
	 */
	public AbstractActionFuture getSessionMsgSig(QQStranger user, QQActionListener.OnActionEvent listener) {
		return pushHttpAction(new GetSessionMsgSigAction(getContext(), listener, user));
	}

	/**
	 * <p>uploadOffPic.</p>
	 *
	 * @param user a {@link iqq.im.bean.QQUser} object.
	 * @param file a {@link java.io.FileInfo} object.
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @return a {@link iqq.im.evt.AbstractActionFuture} object.
	 */
    //public AbstractActionFuture uploadOffPic(QQUser user, FileInfo file, QQActionListener.OnActionEvent listener) {
    //    return pushHttpAction(new UploadOfflinePictureAction(getContext(), listener, user, file));
    //}
	
	/**
	 * <p>uploadCFace.</p>
	 *
	 * @param file a {@link java.io.FileInfo} object.
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @return a {@link iqq.im.evt.AbstractActionFuture} object.
	 */
    //public AbstractActionFuture uploadCFace(FileInfo file, QQActionListener.OnActionEvent listener) {
    //    return pushHttpAction(new UploadCustomFaceAction(getContext(),
    //            listener, file));
    //}
	
	/**
	 * <p>getCFaceSig.</p>
	 *
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @return a {@link iqq.im.evt.AbstractActionFuture} object.
	 */
    public AbstractActionFuture getCFaceSig(QQActionListener.OnActionEvent listener)
    {
        return pushHttpAction(new GetCustomFaceSigAction(getContext(), listener));
    }
	
	/**
	 * <p>sendShake.</p>
	 *
	 * @param user a {@link iqq.im.bean.QQUser} object.
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @return a {@link iqq.im.evt.AbstractActionFuture} object.
	 */
    //public AbstractActionFuture sendShake(QQUser user, QQActionListener.OnActionEvent listener){
    //    return pushHttpAction(new ShakeWindowAction(getContext(), listener, user));
    //}
	
	/**
	 * <p>getOffPic.</p>
	 *
	 * @param offpic a {@link iqq.im.bean.content.OffPicItem} object.
	 * @param msg a {@link iqq.im.bean.QQMsg} object.
	 * @param picout a {@link java.io.OutputStream} object.
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @return a {@link iqq.im.evt.AbstractActionFuture} object.
	 */
    //public AbstractActionFuture getOffPic(OffPicItem offpic, QQMsg msg,
    //                                byte[] picout, QQActionListener.OnActionEvent listener){
    //    return pushHttpAction(new GetOffPicAction(getContext(), listener, offpic, msg, picout));
    //}
	
	/**
	 * <p>getUserPic.</p>
	 *
	 * @param cface a {@link iqq.im.bean.content.CFaceItem} object.
	 * @param msg a {@link iqq.im.bean.QQMsg} object.
	 * @param picout a {@link java.io.OutputStream} object.
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @return a {@link iqq.im.evt.AbstractActionFuture} object.
	 */
    public AbstractActionFuture getUserPic(CFaceItem cface, QQMsg msg,
            byte[] picout, QQActionListener.OnActionEvent listener)
    {
        return pushHttpAction(new GetUserPicAction(getContext(), listener, cface, msg, picout));
    }
	
	/**
	 * <p>getGroupPic.</p>
	 *
	 * @param cface a {@link iqq.im.bean.content.CFaceItem} object.
	 * @param msg a {@link iqq.im.bean.QQMsg} object.
	 * @param picout a {@link java.io.OutputStream} object.
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @return a {@link iqq.im.evt.AbstractActionFuture} object.
	 */
	public AbstractActionFuture getGroupPic(CFaceItem cface, QQMsg msg,
			byte[] picout, QQActionListener.OnActionEvent listener){
		return pushHttpAction(new GetGroupPicAction(getContext(), listener, cface, msg, picout));
	}
	
	/**
	 * <p>sendInputNotify.</p>
	 *
	 * @param user a {@link iqq.im.bean.QQUser} object.
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @return a {@link iqq.im.evt.AbstractActionFuture} object.
	 */
    //public AbstractActionFuture sendInputNotify(QQUser user, QQActionListener.OnActionEvent listener){
    //    return pushHttpAction(new SendInputNotifyAction(getContext(), listener, user));
    //}

    }
}
