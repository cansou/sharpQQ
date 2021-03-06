﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.action
{
   public class QQNotifyEvent
    {
        
	private Type type;
	private Object target;
	
	/**
	 * <p>Constructor for QQNotifyEvent.</p>
	 *
	 * @param type a {@link iqq.im.event.QQNotifyEvent.Type} object.
	 * @param target a {@link java.lang.Object} object.
	 */
	public QQNotifyEvent(Type type, Object target) {
		this.type = type;
		this.target = target;
	}
	
	/**
	 * <p>Getter for the field <code>type</code>.</p>
	 *
	 * @return a {@link iqq.im.event.QQNotifyEvent.Type} object.
	 */
	public Type getType() {
		return type;
	}
	/**
	 * <p>Getter for the field <code>target</code>.</p>
	 *
	 * @return a {@link java.lang.Object} object.
	 */
	public Object getTarget() {
		return target;
	}

	public enum Type{
		/**重新登录成功**/
		RELOGIN_SUCCESS,
		/**网络连接出错，客户端已经掉线*/
		NET_ERROR,
		/**未知错误，如retcode多次出现未知值*/
		UNKNOWN_ERROR,
		/**客户端被踢下线，可能是其他地方登陆*/
		KICK_OFFLINE,
		/**对方正在输入*/
		BUDDY_INPUT,
		/**窗口震动*/
		SHAKE_WINDOW,
		/**聊天消息，包括好友，群，临时会话，讨论组消息*/
		CHAT_MSG,
		/**好友通知，如其他人请求添加好友，添加其他用户请求通过或者拒绝*/
		BUDDY_NOTIFY,
		/**群通知，如管理员通过或拒绝了添加群请求，群成员退出等*/
		GROUP_NOTIFY,
		/**文件传输通知，如对方请求发送文件，对方已同意接受文件等*/
		FILE_NOTIFY,
		/**视频通知，如对方请求和你视频，对方同意视频等。。*/
		AV_NOTIFY,
		/**系统广播*/
		SYSTEM_NOTIFY,
		/**好友状态改变*/
		BUDDY_STATUS_CHANGE,
		/**验证请求，需要用户输入验证码以继续*/
		CAPACHA_VERIFY,
		/**新邮件通知*/
		EMAIL_NOTIFY, WB_CAPACHA_VERIFY,
	}

	/* (non-Javadoc)
	 * @see java.lang.Object#toString()
	 */
	/** {@inheritDoc} */
	public String toString() {
		return "QQNotifyEvent [type=" + type + ", target=" + target + "]";
	}

    }
}
