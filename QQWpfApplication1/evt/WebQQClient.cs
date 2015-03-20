using QQWpfApplication1.bean;
using QQWpfApplication1.module;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.evt
{
    class WebQQClient:QQContext
    {
        
	private ApacheHttpService service;
	private Dictionary<AbstractModule.Type, AbstractModule> modules;
	private ThreadActorDispatcher actorDispatcher;
	private QQAccount account;
	public void setAccount(QQAccount account) {
		this.account = account;
	}
	private QQSession session;

internal QQSession Session
{
  get { return session; }
  set { session = value; }
}
	private QQStore store;
	private QQNotifyListener notifyListener;

	/**
	 * 构造方法，初始化模块和服务
	 * 账号/密码    监听器     线程执行器
	 *
	 * @param username a {@link java.lang.String} object.
	 * @param password a {@link java.lang.String} object.
	 * @param notifyListener a {@link iqq.im.QQNotifyListener} object.
	 * @param actorDispatcher a {@link iqq.im.actor.ThreadActorDispatcher} object.
	 */
	public WebQQClient(
			QQNotifyListener notifyListener, ThreadActorDispatcher actorDispatcher) {
		this.modules = new Dictionary<AbstractModule.Type, AbstractModule>();

        //this.modules.Add(AbstractModule.Type.LOGIN, new LoginModule());
        //this.modules.Add(AbstractModule.Type.PROC, new ProcModule());
        //this.modules.Add(AbstractModule.Type.USER, new UserModule());
        //this.modules.Add(AbstractModule.Type.BUDDY, new BuddyModule());
        //this.modules.Add(AbstractModule.Type.CATEGORY, new CategoryModule());
        //this.modules.Add(AbstractModule.Type.GROUP, new GroupModule());
        //this.modules.Add(AbstractModule.Type.CHAT, new ChatModule());
        //this.modules.Add(AbstractModule.Type.DISCUZ, new DiscuzModule());
        //this.modules.Add(AbstractModule.Type.EMAIL, new EmailModule());
		
        //this.modules.Add(AbstractModule.Type.QM_LOGIN, new QmLoginModule());
        //this.modules.Add(AbstractModule.Type.QM_PROC, new QmProcModule());
        //this.modules.Add(AbstractModule.Type.QM_MGR, new QmMgrModule());
		
        //this.modules.Add(AbstractModule.Type.WB_LOGIN, new WbLoginModule());
        //this.modules.Add(AbstractModule.Type.WB_PROC, new WbProcModule());

		this.service = new ApacheHttpService();

		this.session = new QQSession();
		this.store = new QQStore();
		this.notifyListener = notifyListener;
		this.actorDispatcher = actorDispatcher;
		
		this.init();
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取某个类型的模块，AbstractModule.Type
	 */
	public AbstractModule getModule(AbstractModule.Type type) {
		return modules[type];
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取某个类型的服务，ApacheHttpService.Type
	 */
	public ApacheHttpService getSerivce() {
		return this.service;
	}

	/**
	 * {@inheritDoc}
	 *
	 * 设置HTTP的用户信息
	 */
	public void setHttpUserAgent(String userAgent) {
		this.service.setUserAgent(userAgent);
		
	}


	/**
	 * {@inheritDoc}
	 *
	 * 获取自己的账号实体
	 */
	public QQAccount getAccount() {
		return account;
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取QQ存储信息，包括获取过后的好友/群好友
	 * 还有一些其它的认证信息
	 */
	public QQStore getStore() {
		return store;
	}

	/**
	 * {@inheritDoc}
	 *
	 * 放入一个QQActor到队列，将会在线程执行器里面执行
	 */
	public void pushActor(HttpActor actor) {
		actorDispatcher.pushActor(actor);
		
	}

    /**
     * 初始化所有模块和服务
     */
	private void init() {
		try {
            this.service.init(this);
            Dictionary<Type, AbstractModule>.ValueCollection values = modules.Values;
			Dictionary<Type, AbstractModule>.ValueCollection .Enumerator enu =  values.GetEnumerator();
            while(enu.MoveNext()){
                enu.Current.init(this);
            }

			actorDispatcher.init(this);
			store.init(this);
		} catch (QQException e) {

		}
	}

	/**
	 * 销毁所有模块和服务
	 */
	public void destroy() {
            Dictionary<Type, AbstractModule>.ValueCollection values = modules.Values;
			Dictionary<Type, AbstractModule>.ValueCollection .Enumerator enu =  values.GetEnumerator();
            while(enu.MoveNext()){
                enu.Current.destroy();
            }
			

			actorDispatcher.destroy();
			store.destroy();
	}

	/**
	 * {@inheritDoc}
	 *
	 * 登录接口
	 */
	public AbstractActionFuture login(QQStatus status, QQActionListener.OnActionEvent listener) {
		//检查客户端状态，是否允许登陆
		if (session.getState() == QQWpfApplication1.evt.QQSession.State.ONLINE) {
			throw new ApplicationException("client is aready online !!!");
		}

		getAccount().setStatus(status);
		session.setState(QQSession.State.LOGINING);
		ProcModule procModule = (ProcModule) getModule(AbstractModule.Type.PROC);
		return procModule.login(listener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 重新登录
	 */
	public void relogin(QQStatus status, QQActionListener.OnActionEvent  listener) {
		if (session.getState() == QQWpfApplication1.evt.QQSession.State.ONLINE) {
			throw new ApplicationException("client is aready online !!!");
		}
		
		getAccount().setStatus(status);
		getSession().setState(QQSession.State.LOGINING);
		ProcModule procModule = (ProcModule) getModule(AbstractModule.Type.PROC);
		procModule.relogin(status, listener);
	}

	/**
	 * <p>getCaptcha.</p>
	 *
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 */
	public void getCaptcha(QQActionListener.OnActionEvent listener) {
		LoginModule loginModule = (LoginModule) getModule(AbstractModule.Type.LOGIN);
		loginModule.getCaptcha(getAccount().getUin(), listener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取会话信息
	 */
	
	public QQSession getSession() {
		return session;
	}

	/**
	 * {@inheritDoc}
	 *
	 * 通知事件
	 */
	
	public void fireNotify(QQNotifyEvent evt) {
		if (notifyListener != null) {
			try {
				notifyListener.onNotifyEvent(evt);
			} catch (Exception e) {
			}
		}
		// 重新登录成功，重新poll
        if (evt.getType() == QQNotifyEvent.Type.RELOGIN_SUCCESS)
        {
			beginPollMsg();
		}
	}

	/**
	 * {@inheritDoc}
	 *
	 * 轮询QQ消息
	 */
	
	public void beginPollMsg() {
		if (session.getState() == QQSession.State.OFFLINE) {
			throw new ApplicationException("client is aready offline !!!");
		}
		
		ProcModule procModule = (ProcModule) getModule(AbstractModule.Type.PROC);
		procModule.doPollMsg();

        // 轮询邮件
        // EmailModule emailModule = (EmailModule) getModule(AbstractModule.Type.EMAIL);
		// emailModule.doPoll();
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取所有好友
	 */
	
	public AbstractActionFuture getBuddyList(QQActionListener.OnActionEvent listener) {
		CategoryModule categoryModule = (CategoryModule) getModule(AbstractModule.Type.CATEGORY);
		return categoryModule.getCategoryList(listener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取群列表
	 */
	
	public AbstractActionFuture getGroupList(QQActionListener.OnActionEvent listener) {
		GroupModule groupModule = (GroupModule) getModule(AbstractModule.Type.GROUP);
		return groupModule.getGroupList(listener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取在线好友列表
	 */
	
	public AbstractActionFuture getOnlineList(QQActionListener.OnActionEvent qqActionListener) {
		BuddyModule buddyModule = getModule(AbstractModule.Type.BUDDY);
		return buddyModule.getOnlineBuddy(qqActionListener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取最近好友列表
	 */
	
	public AbstractActionFuture getRecentList(QQActionListener.OnActionEvent qqActionListener) {
		BuddyModule buddyModule = getModule(AbstractModule.Type.BUDDY);
		return buddyModule.getRecentList(qqActionListener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 使用UIN获取QQ号码
	 */
	
	public AbstractActionFuture getUserQQ(QQUser user, QQActionListener.OnActionEvent qqActionListener) {
		UserModule userModule = getModule(AbstractModule.Type.USER);
		return userModule.getUserAccount(user, qqActionListener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 退出登录
	 */
	
	public AbstractActionFuture logout( QQActionListener.OnActionEvent.OnActionEvent listener) {
		if (session.getState() == QQSession.State.OFFLINE) {
			throw new ApplicationException("client is aready offline !!!");
		}
		
		ProcModule procModule = (ProcModule) getModule(AbstractModule.Type.PROC);
		return procModule.doLogout(delegate(QQActionEvent evt) {
				// 无论退出登录失败还是成功，都需要释放资源
				if (evt.getType() == QQActionEvent.Type.EVT_OK
						|| evt.getType() == QQActionEvent.Type.EVT_ERROR) {
					session.setState(QQSession.State.OFFLINE);
					destroy();
				}
				
				if (listener != null) {
					listener(evt);
				}
		});
	}

	/**
	 * {@inheritDoc}
	 *
	 * 改变状态
	 */
	
	public AbstractActionFuture changeStatus( QQStatus status,  QQActionListener.OnActionEvent listener) {
		UserModule userModule = (UserModule) getModule(AbstractModule.Type.USER);
		return userModule.changeStatus(status , listener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取群图标
	 */
	
	public AbstractActionFuture getGroupFace(QQGroup group, QQActionListener.OnActionEvent qqActionListener) {
		GroupModule mod = getModule(AbstractModule.Type.GROUP);
		return mod.getGroupFace(group, qqActionListener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取群信息，好友列表
	 */
	
	public AbstractActionFuture getGroupInfo(QQGroup group, QQActionListener.OnActionEvent qqActionListener) {
		GroupModule mod = getModule(AbstractModule.Type.GROUP);
		return mod.getGroupInfo(group, qqActionListener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取群号码
	 */
	
	public AbstractActionFuture getGroupGid(QQGroup group, QQActionListener.OnActionEvent qqActionListener){
		GroupModule mod = getModule(AbstractModule.Type.GROUP);
		return mod.getGroupGid(group, qqActionListener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取用户头像
	 */
	
	public AbstractActionFuture getUserFace(QQUser user, QQActionListener.OnActionEvent qqActionListener) {
		UserModule mod = getModule(AbstractModule.Type.USER);
		return mod.getUserFace(user, qqActionListener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取个人签名
	 */
	
	public AbstractActionFuture getUserSign(QQUser user, QQActionListener.OnActionEvent qqActionListener) {
		UserModule mod = getModule(AbstractModule.Type.USER);
		return mod.getUserSign(user, qqActionListener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取QQ等级
	 */
	
	public AbstractActionFuture getUserLevel(QQUser user, QQActionListener.OnActionEvent qqActionListener){
		UserModule mod = getModule(AbstractModule.Type.USER);
		return mod.getUserLevel(user, qqActionListener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取自己或者好友信息
	 */
	
	public AbstractActionFuture getUserInfo(QQUser user, QQActionListener.OnActionEvent qqActionListener) {
		UserModule mod = getModule(AbstractModule.Type.USER);
		return mod.getUserInfo(user, qqActionListener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取陌生人信息
	 */
	
	public AbstractActionFuture getStrangerInfo(QQUser user, QQActionListener.OnActionEvent qqActionListener) {
		UserModule mod = getModule(AbstractModule.Type.USER);
		return mod.getStrangerInfo(user, qqActionListener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 发送QQ消息
	 */
	
	public AbstractActionFuture sendMsg(QQMsg msg, QQActionListener.OnActionEvent qqActionListener) {
		ChatModule mod = getModule(AbstractModule.Type.CHAT);
		return mod.sendMsg(msg, qqActionListener);
	}
	public AbstractActionFuture sendWbMsg(String msg, String acceptor, QQActionListener.OnActionEvent qqActionListener) {
		WbProcModule procModule = (WbProcModule) getModule(AbstractModule.Type.WB_PROC);
		return procModule.sendWbMsg(qqActionListener, msg, acceptor);
	}
	public AbstractActionFuture searchQmGroupMember(QmMemSearchCondition condition,QQActionListener.OnActionEvent qqActionListener) {
		QmMgrModule mgrModule = (QmMgrModule) getModule(AbstractModule.Type.QM_MGR);
		return mgrModule.searchGroupMember(condition, qqActionListener);
	}
	public AbstractActionFuture setQmGroupCard(String group, String uin, String card,QQActionListener.OnActionEvent qqActionListener) {
		QmMgrModule mgrModule = (QmMgrModule) getModule(AbstractModule.Type.QM_MGR);
		return mgrModule.setGroupCard(group, uin, card, qqActionListener);
	}
	public AbstractActionFuture deleteQmGroupMember(String group, ArrayList<String> memsDeleted, boolean acceptApply,QQActionListener.OnActionEvent qqActionListener) {
		QmMgrModule mgrModule = (QmMgrModule) getModule(AbstractModule.Type.QM_MGR);
		return mgrModule.deleteGroupMember(group, memsDeleted, acceptApply, qqActionListener);
	}
	
	/**
	 * {@inheritDoc}
	 *
	 * 发送一个震动
	 */
	
	public AbstractActionFuture sendShake(QQUser user, QQActionListener.OnActionEvent qqActionListener) {
		ChatModule mod = getModule(AbstractModule.Type.CHAT);
		return mod.sendShake(user, qqActionListener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取离线图片
	 */
	
	public AbstractActionFuture getOffPic(OffPicItem offpic, QQMsg msg, Stream picout, 
					QQActionListener.OnActionEvent listener) {
		ChatModule mod = getModule(AbstractModule.Type.CHAT);
		return mod.getOffPic(offpic, msg, picout, listener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取聊天图片
	 */
	
	public AbstractActionFuture getUserPic(CFaceItem cface, QQMsg msg,
					Stream picout, QQActionListener.OnActionEvent listener){
		ChatModule mod = getModule(AbstractModule.Type.CHAT);
		return mod.getUserPic(cface, msg, picout, listener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取群聊天图片
	 */
	
	public AbstractActionFuture getGroupPic(CFaceItem cface, QQMsg msg,
			Stream picout, QQActionListener.OnActionEvent listener){
		ChatModule mod = getModule(AbstractModule.Type.CHAT);
		return mod.getGroupPic(cface, msg, picout, listener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 上传离线图片
	 */
	
	public AbstractActionFuture uploadOffPic(QQUser user, File file, QQActionListener.OnActionEvent listener){
		ChatModule mod = getModule(AbstractModule.Type.CHAT);
		return mod.uploadOffPic(user, file, listener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 上传好友图片
	 */
	
	public AbstractActionFuture uploadCustomPic(File file, QQActionListener.OnActionEvent listener){
		ChatModule mod = getModule(AbstractModule.Type.CHAT);
		return mod.uploadCFace(file, listener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 发送正在输入通知
	 */
	
	public AbstractActionFuture sendInAddNotify(QQUser user, QQActionListener.OnActionEvent listener){
		ChatModule mod = getModule(AbstractModule.Type.CHAT);
		return mod.sendInAddNotify(user, listener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取讨论组列表
	 */
	
	public AbstractActionFuture getDiscuzList(QQActionListener.OnActionEvent qqActionListener) {
		DiscuzModule mod = getModule(AbstractModule.Type.DISCUZ);
		return mod.getDiscuzList(qqActionListener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取讨论组信息
	 */
	
	public AbstractActionFuture getDiscuzInfo(QQDiscuz discuz, QQActionListener.OnActionEvent qqActionListener) {
		DiscuzModule mod = getModule(AbstractModule.Type.DISCUZ);
		return mod.getDiscuzInfo(discuz, qqActionListener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 临时消息信道，用于发送群 U 2 U会话消息
	 */
	
	public AbstractActionFuture getSessionMsgSig(QQStranger user, QQActionListener.OnActionEvent qqActionListener) {
		ChatModule mod = getModule(AbstractModule.Type.CHAT);
		return mod.getSessionMsgSig(user, qqActionListener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取群成员状态
	 */
	public AbstractActionFuture getGroupMemberStatus(QQGroup group, QQActionListener.OnActionEvent listener) {
		GroupModule mod = getModule(AbstractModule.Type.GROUP);
		return mod.getMemberStatus(group, listener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 提交验证码
	 */
	
	public void submitVerify(String code, QQNotifyEvent verifyEvent) {
		QQNotifyEventArgs.ImageVerify verify = 
			(QQNotifyEventArgs.ImageVerify) verifyEvent.getTarget();
		
		if(verify.type==QQWpfApplication1.bean.QQNotifyEventArgs.ImageVerify.VerifyType.LOGIN){
			ProcModule mod = (ProcModule)getModule(AbstractModule.Type.PROC);
			mod.loginWithVerify(code, (AbstractActionFuture)verify.future);
		}
	}

	/**
	 * {@inheritDoc}
	 *
	 * 刷新验证码
	 */
	
	public AbstractActionFuture freshVerify(QQNotifyEvent verifyEvent, QQActionListener.OnActionEvent listener) {
		LoginModule mod = getModule(AbstractModule.Type.LOGIN);
		return mod.getCaptcha(account.getUin(), listener);
	}


	/** {@inheritDoc} */
	
	public AbstractActionFuture updateGroupMessageFilter(QQActionListener.OnActionEvent listener) {
		GroupModule mod = getModule(AbstractModule.Type.GROUP);
		return mod.updateGroupMessageFilter(listener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 搜索群列表
	 */
	
	public AbstractActionFuture searchGroupGetList(QQGroupSearchList resultList, QQActionListener.OnActionEvent listener)
	{
		GroupModule mod = getModule(AbstractModule.Type.GROUP);
		return mod.searchGroup(resultList, listener);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 退出验证码输入
	 */
	
	public void cancelVerify(QQNotifyEvent verifyEvent) {
		QQNotifyEventArgs.ImageVerify verify = 
			(QQNotifyEventArgs.ImageVerify) verifyEvent.getTarget();
		verify.future.cancel();
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取好友列表，但必须已经使用接口获取过
	 */
	
	public List<QQBuddy> getBuddyList() {
		return getStore().getBuddyList();
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取群列表，但必须已经使用接口获取过
	 */
	
	public List<QQGroup> getGroupList() {
		return getStore().getGroupList();
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取讨论组列表，但必须已经使用接口获取过
	 */
	
	public List<QQDiscuz> getDiscuzList() {
		return getStore().getDiscuzList();
	}

	/**
	 * {@inheritDoc}
	 *
	 * 根据UIN获得好友
	 */
	
	public QQBuddy getBuddyByUin(long uin) {
		return getStore().getBuddyByUin(uin);
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取自己的状态
	 */
	
	public boolean isOnline() {
		return getSession().getState() == QQSession.State.ONLINE;
	}

	/**
	 * {@inheritDoc}
	 *
	 * 获取是否正在登录的状态
	 */
	
	public boolean isLogining() {
		return getSession().getState() == QQSession.State.LOGINING;
	}
	
	public AbstractActionFuture loginQm( QQActionListener.OnActionEvent listener) {

		QmProcModule procModule = (QmProcModule) getModule(AbstractModule.Type.QM_PROC);
		return procModule.login(listener);
	}
	public AbstractActionFuture preloginWb( QQActionListener.OnActionEvent listener) {

		WbProcModule procModule = (WbProcModule) getModule(AbstractModule.Type.WB_PROC);
		return procModule.prelogin(listener);
	}
	public AbstractActionFuture loginWb(AbstractActionFuture future, WbVerifyImage verifyImage) {

		WbProcModule procModule = (WbProcModule) getModule(AbstractModule.Type.WB_PROC);
		return procModule.login(future, verifyImage);
	}

    }
}
