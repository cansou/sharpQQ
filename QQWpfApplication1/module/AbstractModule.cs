using QQWpfApplication1.evt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.module
{
    abstract class  AbstractModule
    {
        
	private QQContext cxt;

	public void init(QQContext context) {
		this.cxt = context;
	}

    public abstract void destroy();
	
	
	/**
	 * <p>Getter for the field <code>context</code>.</p>
	 *
	 * @return a {@link iqq.im.core.QQContext} object.
	 */
	public QQContext getContext(){
		return this.cxt;
	}
	
	/**
	 * <p>pushHttpAction.</p>
	 *
	 * @param action a {@link iqq.im.action.HttpAction} object.
	 * @return a {@link iqq.im.event.QQActionFuture} object.
	 */
	protected void pushHttpAction(AbstractHttpAction action){
		getContext().pushActor(new HttpActor(HttpActor.Type.BUILD_REQUEST, getContext(), action));
	}
	

        public enum Type
        {
            PROC,			//登陆和退出流程执行
            LOGIN,			//核心模块，处理登录和退出的逻辑
            USER,			//个人信息管理模块
            BUDDY,			//好友管理模块
            CATEGORY,		//分组管理模块
            GROUP,			//群管理模块
            DISCUZ,			//讨论组模块
            CHAT,			//聊天模块
            EMAIL,		//邮件模块

            QM_LOGIN,
            QM_PROC, WB_LOGIN, WB_PROC, QM_MGR
        }
    }
}
