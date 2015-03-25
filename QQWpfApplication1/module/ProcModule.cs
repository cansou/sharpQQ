using QQWpfApplication1.bean;
using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace QQWpfApplication1.action
{
    class ProcModule:AbstractModule
    {
        
	public AbstractActionFuture login(QQActionListener.OnActionEvent listener) {
        AbstractActionFuture future = new AbstractActionFuture(listener);
		doGetLoginSig(future);
		return future;
	}
	
	/**
	 * <p>loginWithVerify.</p>
	 *
	 * @param verifyCode a {@link java.lang.String} object.
	 * @param future a {@link iqq.im.evt.future.AbstractActionFuture} object.
	 * @return a {@link iqq.im.evt.AbstractActionFuture} object.
	 */
	public AbstractActionFuture loginWithVerify(String verifyCode, AbstractActionFuture future) {
		doWebLogin(verifyCode, future);
		return future;
	}
	
	
	private void doGetLoginSig( AbstractActionFuture future){
		LoginModule login = (LoginModule) getContext().getModule(AbstractModule.Type.LOGIN);
		login.getLoginSig(delegate(QQActionEvent evt){

            if(evt.getType()==QQActionEvent.Type.EVT_OK){
					doCheckVerify(future);
				}else if(evt.getType()==QQActionEvent.Type.EVT_ERROR){
					future.notifyActionEvent(
							QQActionEvent.Type.EVT_ERROR,
							(QQException) evt.getTarget());
				}
		});
	}
	
	private void doGetVerify( String reason,  AbstractActionFuture future){
        QQAccount account = (QQAccount)(getContext().getAccount());
		LoginModule login = (LoginModule) getContext().getModule(AbstractModule.Type.LOGIN);
		login.getCaptcha(account.getUin(), delegate(QQActionEvent evt) {
				if(evt.getType()==QQActionEvent.Type.EVT_OK){
					QQNotifyEventArgs.ImageVerify verify = new QQNotifyEventArgs.ImageVerify();
					
					verify.type = QQNotifyEventArgs.ImageVerify.VerifyType.LOGIN;
					verify.image = (BitmapImage) evt.getTarget();
					verify.reason = reason;
					verify.future = future;
					
					getContext().fireNotify(new QQNotifyEvent(QQNotifyEvent.Type.CAPACHA_VERIFY, verify));
				}else if(evt.getType()==QQActionEvent.Type.EVT_ERROR){
					future.notifyActionEvent(
							QQActionEvent.Type.EVT_ERROR,
							(QQException) evt.getTarget());
				}
		});
		
		
	}

	private void doCheckVerify( AbstractActionFuture future) {
        LoginModule login = (LoginModule)getContext().getModule(AbstractModule.Type.LOGIN);
        QQAccount account = (QQAccount)getContext().getAccount();
		login.checkVerify(account.getUsername(), delegate(QQActionEvent evt) {
				if (evt.getType() == QQActionEvent.Type.EVT_OK) {
					CheckVerifyArgs args = 
						(CheckVerifyArgs) (evt.getTarget());
					account.setUin(args.uin);
					if (args.result == 0) {
						doWebLogin(args.code, future);
					} else {
						doGetVerify("为了保证您账号的安全，请输入验证码中字符继续登录。", future);
					}
				}else if(evt.getType() == QQActionEvent.Type.EVT_ERROR){
					future.notifyActionEvent(
							QQActionEvent.Type.EVT_ERROR,
							evt.getTarget());
				}

		});
	}

	private void doWebLogin(String verifyCode,  AbstractActionFuture future) {
        LoginModule login = (LoginModule)getContext().getModule(AbstractModule.Type.LOGIN);
        QQAccount account = (QQAccount)getContext().getAccount();
		login.webLogin(account.getUsername(), account.getPassword(),
				account.getUin(), verifyCode, delegate(QQActionEvent evt) {
						if (evt.getType() == QQActionEvent.Type.EVT_OK) {
							doCheckLoginSig( (String) evt.getTarget(),future);
						} else if (evt.getType() == QQActionEvent.Type.EVT_ERROR) {
							QQException ex = (QQException) (evt.getTarget());
							if(ex.getError()==QQWpfApplication1.action.QQException.QQErrorCode.WRONG_CAPTCHA){
								doGetVerify(ex.Message, future);
							}else{
								future.notifyActionEvent(
										QQActionEvent.Type.EVT_ERROR,
										(QQException) evt.getTarget());
							}
						}
				});
	}
	
	private void doCheckLoginSig(String checkSigUrl,  AbstractActionFuture future){
        LoginModule login = (LoginModule)getContext().getModule(AbstractModule.Type.LOGIN);
		login.checkLoginSig(checkSigUrl, delegate(QQActionEvent evt) {
				if (evt.getType() == QQActionEvent.Type.EVT_OK) {
					doChannelLogin(future);
				} else if (evt.getType() == QQActionEvent.Type.EVT_ERROR) {
						future.notifyActionEvent(
								QQActionEvent.Type.EVT_ERROR,
								(QQException) evt.getTarget());
				}
				
		});
	}

	private void doChannelLogin( AbstractActionFuture future) {
        LoginModule login = (LoginModule)getContext().getModule(AbstractModule.Type.LOGIN);
		login.channelLogin(getContext().getAccount().getStatus(), delegate(QQActionEvent evt) {
				if (evt.getType() == QQActionEvent.Type.EVT_OK) {
					future.notifyActionEvent(QQActionEvent.Type.EVT_OK, null);
				} else if (evt.getType() == QQActionEvent.Type.EVT_ERROR) {
					future.notifyActionEvent(QQActionEvent.Type.EVT_ERROR,
							(QQException) evt.getTarget());
				}
		});
	}
	
	/**
	 * <p>relogin.</p>
	 *
	 * @param status a {@link iqq.im.bean.QQStatus} object.
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @return a {@link iqq.im.evt.AbstractActionFuture} object.
	 */
    public AbstractActionFuture relogin(QQStatus status, QQActionListener.OnActionEvent listener)
    {
		getContext().getAccount().setStatus(status);
		getContext().getSession().setState(QQSession.State.LOGINING);

        AbstractActionFuture future = new AbstractActionFuture(listener);
        LoginModule loginModule = (LoginModule)getContext().getModule(AbstractModule.Type.LOGIN);
        loginModule.channelLogin(status, delegate(QQActionEvent evt)
        {
				if(evt.getType() == QQActionEvent.Type.EVT_ERROR) {
					login(listener);
				} else {
					listener(evt);
				}
		});
        return future;
	}
	
	/**
	 * <p>relogin.</p>
	 */
	public void relogin() {
		QQSession session = getContext().getSession();
		if(session.getState() == QQSession.State.LOGINING) return;
		// 登录失效，重新登录
		relogin(getContext().getAccount().getStatus(), delegate(QQActionEvent evt) {
				if(evt.getType() == QQActionEvent.Type.EVT_OK) {
					// 重新登录成功重新POLL
					getContext().fireNotify(new QQNotifyEvent(QQNotifyEvent.Type.RELOGIN_SUCCESS, null));
				} else if(evt.getType() == QQActionEvent.Type.EVT_ERROR) {
					getContext().fireNotify(new QQNotifyEvent(QQNotifyEvent.Type.UNKNOWN_ERROR, null));
				}
		});
	}

	/**
	 * <p>doPollMsg.</p>
	 */
	public void doPollMsg() {
        LoginModule login = (LoginModule)getContext().getModule(AbstractModule.Type.LOGIN);
		login.pollMsg(delegate(QQActionEvent evt) {
				// 回调通知事件函数
				if (evt.getType() == QQActionEvent.Type.EVT_OK) {
					List<QQNotifyEvent> evts = (List<QQNotifyEvent>) evt.getTarget();
					foreach (QQNotifyEvent e in evts) {
						getContext().fireNotify(e);
					}
					
					// 准备提交下次poll请求
					QQSession session = getContext().getSession();
					if(session.getState() == QQSession.State.ONLINE) {
						doPollMsg();
					} else if(session.getState() != QQSession.State.KICKED) {
						relogin();
					}
				}else if(evt.getType() == QQActionEvent.Type.EVT_ERROR){
					QQSession session = getContext().getSession();
                    QQAccount account = (QQAccount)getContext().getAccount();
					session.setState(QQSession.State.OFFLINE);
					account.setStatus(QQStatus.OFFLINE);
					//因为自带了错误重试机制，如果出现了错误回调，表明已经超时多次均失败，这里直接返回网络错误的异常
					QQException ex = (QQException) evt.getTarget();
					QQWpfApplication1.action.QQException.QQErrorCode code = ex.getError();
					if(code == QQWpfApplication1.action.QQException.QQErrorCode.INVALID_LOGIN_AUTH) {
						relogin();
					} else if(code == QQWpfApplication1.action.QQException.QQErrorCode.IO_ERROR || code == QQWpfApplication1.action.QQException.QQErrorCode.IO_TIMEOUT){
						//粗线了IO异常，直接报网络错误
						getContext().fireNotify(new QQNotifyEvent(QQNotifyEvent.Type.NET_ERROR, ex));
					}else{
						relogin();
						doPollMsg();
					}
				}else if(evt.getType() == QQActionEvent.Type.EVT_RETRY){
				}
		});
	}

	/**
	 * <p>doLogout.</p>
	 *
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @return a {@link iqq.im.evt.AbstractActionFuture} object.
	 */
    public AbstractActionFuture doLogout(QQActionListener.OnActionEvent listener)
    {
        AbstractActionFuture future = new AbstractActionFuture(listener);
		LoginModule loginModule = (LoginModule) getContext().getModule(AbstractModule.Type.LOGIN);
        loginModule.logout(listener);
        return future;
	}

    }
}
