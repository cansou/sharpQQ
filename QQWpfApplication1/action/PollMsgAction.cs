using QQWpfApplication1.action;
using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using QQWpfApplication1.bean;

namespace QQWpfApplication1.action
{
   public class PollMsgAction:AbstractHttpAction
    {
        
	public PollMsgAction(QQContext context, QQActionListener.OnActionEvent listener) :base(context, listener){
	}

	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		QQSession session = getContext().getSession();
		JSONObject json = new JSONObject();
		json.put("clientid", session.getClientId());
		json.put("psessionid", session.getSessionId());
		json.put("key", 0); // 暂时不知道什么用的
		json.put("ids", new JSONArray()); // 同上

		QQHttpRequest req = createHttpRequest("POST", QQConstants.URL_POLL_MSG);
		req.addPostValue("r", json.ToString());
		req.addPostValue("clientid", session.getClientId() + "");
		req.addPostValue("psessionid", session.getSessionId());
        //req.setReadTimeout(70 * 1000);
        //req.setConnectTimeout(10 * 1000);
		req.addHeader("Referer", QQConstants.REFFER);

		return req;
	}

	/** {@inheritDoc} */
    public override void onHttpFinish(QQHttpResponse response)
    {
		// 如果返回的内容为空，认为这次pollMsg仍然成功
		if (response.getResponseMessage().Length == 0) {
			notifyActionEvent(QQActionEvent.Type.EVT_OK, new List<QQNotifyEvent>());
		} else {
			base.onHttpFinish(response);
		}
	}

	/** {@inheritDoc} */
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		QQStore store = getContext().getStore();
		List<QQNotifyEvent> notifyEvents = new List<QQNotifyEvent>();
		JSONObject json = new JSONObject(new JSONTokener(new StringReader(response.getResponseString())));
		int retcode = json.getInt("retcode");
		if (retcode == 0) {
			// 有可能为 {"retcode"in0,"result"in"ok"}
			if (!json.isNull("result") && json.get("result") is JSONArray) {
				JSONArray results = json.getJSONArray("result");
				// 消息下载来的列表中是倒过来的，那我直接倒过来取，编位回来
				for (int i = results.length() - 1; i >= 0; i--) {
					JSONObject poll = results.getJSONObject(i);
					String pollType = poll.getString("poll_type");
					JSONObject pollData = poll.getJSONObject("value");
					if (pollType.Equals("input_notify")) {
						long fromUin = pollData.getLong("from_uin");
						QQBuddy buddy = store.getBuddyByUin(fromUin);
						notifyEvents.Add(new QQNotifyEvent(QQNotifyEvent.Type.BUDDY_INPUT, buddy));
					} else if (pollType.Equals("message")) {
						// 好友消息
						notifyEvents.Add(processBuddyMsg(pollData));
					} else if (pollType.Equals("group_message")) {
						// 群消息
						// 被管理员禁言10分钟{"retcode"in0,"result"in[{"poll_type"in"group_message","value"in{"msg_id"in20986,"from_uin"in848492696,"to_uin"in1002053815,"msg_id2"in128032,"msg_type"in43,"reply_ip"in176756755,"group_code"in2227416282,"send_uin"in1900986400,"seq"in221,"time"in1419225537,"info_seq"in126658727,"content"in[["font",{"size"in10,"color"in"000000","style"in[0,0,0],"name"in"\u5B8B\u4F53"}],"\u674E\u519B(1002053815) \u88AB\u7BA1\u7406\u5458\u7981\u8A0010\u5206\u949F "]}}]}
						// 被管理员禁言1小时
						// {"retcode"in0,"result"in[{"poll_type"in"group_message","value"in{"msg_id"in49336,"from_uin"in848492696,"to_uin"in1002053815,"msg_id2"in198014,"msg_type"in43,"reply_ip"in176756884,"group_code"in2227416282,"send_uin"in1900986400,"seq"in224,"time"in1419225732,"info_seq"in126658727,"content"in[["font",{"size"in10,"color"in"000000","style"in[0,0,0],"name"in"\u5B8B\u4F53"}],"\u674E\u519B(1002053815) \u88AB\u7BA1\u7406\u5458\u7981\u8A001\u5C0F\u65F6 "]}}]}
						// 被管理员禁言解除
						// {"retcode"in0,"result"in[{"poll_type"in"group_message","value"in{"msg_id"in60841,"from_uin"in848492696,"to_uin"in1002053815,"msg_id2"in340248,"msg_type"in43,"reply_ip"in176498277,"group_code"in2227416282,"send_uin"in1900986400,"seq"in223,"time"in1419225681,"info_seq"in126658727,"content"in[["font",{"size"in10,"color"in"000000","style"in[0,0,0],"name"in"\u5B8B\u4F53"}],"\u674E\u519B(1002053815) \u88AB\u7BA1\u7406\u5458\u89E3\u9664\u7981\u8A00 "]}}]}
						// 群单纯图片消息
						// {"retcode"in0,"result"in[{"poll_type"in"group_message","value"in{"msg_id"in14154,"from_uin"in848492696,"to_uin"in1002053815,"msg_id2"in483355,"msg_type"in43,"reply_ip"in176489607,"group_code"in2227416282,"send_uin"in2388435354,"seq"in226,"time"in1419225894,"info_seq"in126658727,"content"in[["font",{"size"in10,"color"in"000000","style"in[0,0,0],"name"in"\u5FAE\u8F6F\u96C5\u9ED1"}],["cface",{"name"in"{99820253-D7C7-8EDC-3F1E-53B08AE5C390}.jpg","file_id"in2610173877,"key"in"                ","server"in"183.60.50.34in80"}]," "]}}]}
						// 群文字与表情消息
						// {"retcode"in0,"result"in[{"poll_type"in"group_message","value"in{"msg_id"in14155,"from_uin"in848492696,"to_uin"in1002053815,"msg_id2"in279741,"msg_type"in43,"reply_ip"in176886363,"group_code"in2227416282,"send_uin"in2388435354,"seq"in227,"time"in1419226485,"info_seq"in126658727,"content"in[["font",{"size"in10,"color"in"000000","style"in[0,0,0],"name"in"\u5FAE\u8F6F\u96C5\u9ED1"}],"aa",["face",14]," "]}}]}
						// 超级表情
						// {"retcode"in0,"result"in[{"poll_type"in"group_message","value"in{"msg_id"in57841,"from_uin"in2901943685,"to_uin"in1002053815,"msg_id2"in683745,"msg_type"in32,"reply_ip"in179898740,"group_code"in1226655265,"t_gcode"in260334785,"send_uin"in569398403,"seq"in19,"time"in1425015233,"info_seq"in37,"content"in[""]}}]}

						notifyEvents.Add(processGroupMsg(pollData));
					} else if (pollType.Equals("discu_message")) {
						// 讨论组消息
						notifyEvents.Add(processDiscuzMsg(pollData));
					} else if (pollType.Equals("sess_message")) {
						// 临时会话消息
						notifyEvents.Add(processSessionMsg(pollData));
					} else if (pollType.Equals("shake_message")) {
						// 窗口震动
						long fromUin = pollData.getLong("from_uin");
						QQUser user = getContext().getStore().getBuddyByUin(fromUin);
						notifyEvents.Add(new QQNotifyEvent(QQNotifyEvent.Type.SHAKE_WINDOW, user));
					} else if (pollType.Equals("kick_message")) {
						// 被踢下线
						getContext().getAccount().setStatus(QQStatus.OFFLINE);
						getContext().getSession().setState(QQSession.State.KICKED);
						notifyEvents.Add(new QQNotifyEvent(QQNotifyEvent.Type.KICK_OFFLINE, pollData.getString("reason")));
					} else if (pollType.Equals("buddies_status_change")) {
						notifyEvents.Add(processBuddyStatusChange(pollData));
					} else if (pollType.Equals("group_web_message")) {
						// 点歌
						// {"retcode"in0,"result"in[{"poll_type"in"group_web_message","value"in{"msg_id"in65528,"from_uin"in2901943685,"to_uin"in1002053815,"msg_id2"in751439,"msg_type"in45,"reply_ip"in176886319,"group_code"in1226655265,"group_type"in1,"ver"in1,"send_uin"in3706930015,"xml"in"\u0001\u0000\u0001 \u0000\t\u00D4 \u0000\u000F\u00C9\u00CB\u00D0\u00C4\u032B\u01BD\u00D1\u00F3-\u00C0\u00D7\u00E6\u00C3\u0000\u00EDtencentin//miniplayer/?cmd=1\u0026fuin=569398403\u0026frienduin=569398403\u0026groupid=4150334785\u0026groupcode=260334785\u0026action=\u0027accept\u0027\u0026mdlurl=\u0027httpin//scenecgi.chatshow.qq.com/fcgi-bin/gm_qry_music_info.fcg?songcount=1\u0026songidlist=644128\u0026version=207\u0026cmd=1\u0027\u0000\u00EDtencentin//miniplayer/?cmd=1\u0026fuin=569398403\u0026frienduin=569398403\u0026groupid=4150334785\u0026groupcode=260334785\u0026action=\u0027refuse\u0027\u0026mdlurl=\u0027httpin//scenecgi.chatshow.qq.com/fcgi-bin/gm_qry_music_info.fcg?songcount=1\u0026songidlist=644128\u0026version=207\u0026cmd=1\u0027"}}]}
						
					} else if (pollType.Equals("system_message")) {
						
						// 其他人 添加你为好友
						// {"retcode"in0,"result"in[{"poll_type"in"system_message","value"in{"seq"in49219,"type"in"Added_buddy_sig","uiuin"in"","from_uin"in2388435354,"account"in569398403,"sig"in"Ub\u008A\u00B8\u00D4\u0001\u008B\u0092Q\u001D\"*W\u00F6b\u00EA\u001E\u00F0\u009A\u0016gqb\u00C7","stat"in20}}]}
					} else if (pollType.Equals("sys_g_msg")) {
						// 设置你为群管理员
						// {"retcode"in0,"result"in[{"poll_type"in"sys_g_msg","value"in{"msg_id"in58825,"from_uin"in848492696,"to_uin"in1002053815,"msg_id2"in584526,"msg_type"in44,"reply_ip"in180061855,"type"in"group_admin_op","gcode"in2227416282,"t_gcode"in126658727,"op_type"in1,"uin"in1002053815,"t_uin"in1002053815,"uin_flag"in1}}]}
						// 取消你为群管理员
						// {"retcode"in0,"result"in[{"poll_type"in"sys_g_msg","value"in{"msg_id"in9211,"from_uin"in848492696,"to_uin"in1002053815,"msg_id2"in265670,"msg_type"in44,"reply_ip"in176757008,"type"in"group_admin_op","gcode"in2227416282,"t_gcode"in126658727,"op_type"in0,"uin"in1002053815,"t_uin"in1002053815,"uin_flag"in0}}]}
						// 设置群成员为群管理员
						// {"retcode"in0,"result"in[{"poll_type"in"sys_g_msg","value"in{"msg_id"in40322,"from_uin"in848492696,"to_uin"in1002053815,"msg_id2"in183897,"msg_type"in44,"reply_ip"in179406747,"type"in"group_admin_op","gcode"in2227416282,"t_gcode"in126658727,"op_type"in1,"uin"in3904214993,"t_uin"in2564781987,"uin_flag"in1}}]}
						// 取消群成员为群管理员
//						{"retcode"in0,"result"in[{"poll_type"in"sys_g_msg","value"in{"msg_id"in13045,"from_uin"in848492696,"to_uin"in1002053815,"msg_id2"in15288,"msg_type"in44,"reply_ip"in176756443,"type"in"group_admin_op","gcode"in2227416282,"t_gcode"in126658727,"op_type"in0,"uin"in3904214993,"t_uin"in2564781987,"uin_flag"in0}}]}

						// 你已经被移除群
						// {"retcode"in0,"result"in[{"poll_type"in"sys_g_msg","value"in{"msg_id"in5515,"from_uin"in848492696,"to_uin"in1002053815,"msg_id2"in634857,"msg_type"in34,"reply_ip"in176488602,"type"in"group_leave","gcode"in2227416282,"t_gcode"in126658727,"op_type"in3,"old_member"in1002053815,"t_old_member"in"","admin_uin"in2388435354,"t_admin_uin"in"","admin_nick"in"\u521B\u5EFA\u8005"}}]}
						// 某人申请加入群
						// {"retcode"in0,"result"in[{"poll_type"in"sys_g_msg","value"in{"msg_id"in9564,"from_uin"in848492696,"to_uin"in1002053815,"msg_id2"in366710,"msg_type"in35,"reply_ip"in176884836,"type"in"group_request_join","gcode"in2227416282,"t_gcode"in126658727,"request_uin"in3904214993,"t_request_uin"in"","msg"in"gggggg"}}]}
					} else {
					}
				}
			}
			// end recode == 0
		} else if (retcode == 102) {
			// 接连正常，没有消息到达 {"retcode"in102,"errmsg"in""}
			// 继续进行下一个消息请求

		} else if (retcode == 110 || retcode == 109) { // 客户端主动退出
			getContext().getSession().setState(QQSession.State.OFFLINE);
		} else if (retcode == 116) {
			// 需要更新Ptwebqq值，暂时不知道干嘛用的
			// {"retcode"in116,"p"in"2c0d8375e6c09f2af3ce60c6e081bdf4db271a14d0d85060"}
			// if (a.retcode === 116) alloy.portal.setPtwebqq(a.p)
			getContext().getSession().setPtwebqq(json.getString("p"));
		} else if (retcode == 121 || retcode == 120 || retcode == 100) { // 121,120
																			// in
																			// ReLinkFailure
																			// 100
																			// in
																			// NotReLogin
			// 服务器需求重新认证
			// {"retcode"in121,"t"in"0"}
			getContext().getSession().setState(QQSession.State.OFFLINE);
			QQException ex = new QQException(QQException.QQErrorCode.INVALID_LOGIN_AUTH);
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, ex);
			return;
			// notifyEvents.Add(new
			// QQNotifyEvent(QQNotifyEvent.Type.NEED_REAUTH, null));
		} else {

			// 返回错误，核心遇到未知recode
			// getCxt().getSession().setState(QQSession.State.ERROR);
			notifyEvents.Add(new QQNotifyEvent(QQNotifyEvent.Type.UNKNOWN_ERROR, json));
		}
		notifyActionEvent(QQActionEvent.Type.EVT_OK, notifyEvents);
	}

	/**
	 * <p>
	 * processBuddyStatusChange.
	 * </p>
	 * 
	 * @param pollData
	 *            a {@link org.json.JSONObject} object.
	 * @throws org.json.JSONException
	 *             if any.
	 * @return a {@link iqq.im.event.QQNotifyEvent} object.
	 */
	public QQNotifyEvent processBuddyStatusChange(JSONObject pollData)  {
		long uin = pollData.getLong("uin");
		QQBuddy buddy = getContext().getStore().getBuddyByUin(uin);
		String status = pollData.getString("status");
		int clientType = pollData.getInt("client_type");
        //buddy.setStatus(QQStatus.valueOfRaw(status));
        //buddy.setClientType(QQClientType.valueOfRaw(clientType));
		return new QQNotifyEvent(QQNotifyEvent.Type.BUDDY_STATUS_CHANGE, buddy);
	}

	/**
	 * <p>
	 * processBuddyMsg.
	 * </p>
	 * 
	 * @param pollData
	 *            a {@link org.json.JSONObject} object.
	 * @throws org.json.JSONException
	 *             if any.
	 * @throws iqq.im.QQException
	 *             if any.
	 * @return a {@link iqq.im.event.QQNotifyEvent} object.
	 */
	public QQNotifyEvent processBuddyMsg(JSONObject pollData) {
		QQStore store = getContext().getStore();

		long fromUin = pollData.getLong("from_uin");
		QQMsg msg = new QQMsg();
		msg.setId(pollData.getLong("msg_id"));
		msg.setId2(pollData.getLong("msg_id2"));
		msg.parseContentList(pollData.getJSONArray("content").ToString());
		msg.setType(QQMsg.Type.BUDDY_MSG);
		msg.setTo(getContext().getAccount());
		msg.setFrom(store.getBuddyByUin(fromUin));
		msg.setDate(new DateTime(pollData.getLong("time") * 1000));
		if (msg.getFrom() == null) {
			QQUser member = store.getStrangerByUin(fromUin); // 搜索陌生人列表
			if (member == null) {
				member = new QQHalfStranger();
				member.setUin(fromUin);
				store.addStranger((QQStranger) member);
			}
			msg.setFrom(member);
		}
		return new QQNotifyEvent(QQNotifyEvent.Type.CHAT_MSG, msg);
	}

	/**
	 * <p>
	 * processGroupMsg.
	 * </p>
	 * 
	 * @param pollData
	 *            a {@link org.json.JSONObject} object.
	 * @throws org.json.JSONException
	 *             if any.
	 * @throws iqq.im.QQException
	 *             if any.
	 * @return a {@link iqq.im.event.QQNotifyEvent} object.
	 */
	public QQNotifyEvent processGroupMsg(JSONObject pollData){
		// {"retcode"in0,"result"in[{"poll_type"in"group_message",
		// "value"in{"msg_id"in6175,"from_uin"in3924684389,"to_uin"in1070772010,"msg_id2"in992858,"msg_type"in43,"reply_ip"in176621921,
		// "group_code"in3439321257,"send_uin"in1843694270,"seq"in875,"time"in1365934781,"info_seq"in170125666,"content"in[["font",{"size"in10,"color"in"3b3b3b","style"in[0,0,0],"name"in"\u5FAE\u8F6F\u96C5\u9ED1"}],"eeeeeeeee "]}}]}
		QQStore store = getContext().getStore();
		QQMsg msg = new QQMsg();
		msg.setId(pollData.getLong("msg_id"));
		msg.setId2(pollData.getLong("msg_id2"));
		int msgType = pollData.getInt("msg_type");
		long gin = pollData.getLong("from_uin");
		long fromUin = pollData.getLong("send_uin");
		long groupCode = pollData.getLong("group_code");
		QQGroup group = store.getGroupByCode(groupCode);
		long groupID= -1;
		if(group==null){
			group = new QQGroup();
			group.setGin(gin);
            group.setCode(groupCode);
            // put to store
            store.addGroup(group);
		}
		switch (msgType) {
            case 32:{
			// 魔法、超级、涂鸦表情
			groupID = pollData.getLong("t_gcode"); // 真实群号码
			break;
		}
            case 43:
			// 一般消息
			groupID = pollData.getLong("info_seq"); // 真实群号码

			
			msg.parseContentList(pollData.getJSONArray("content").ToString());
			msg.setType(QQMsg.Type.GROUP_MSG);
			msg.setDate(new DateTime(pollData.getLong("time") * 1000));
			break;
            default:
			break;
		}
		if (group.getGid() <= 0) {
			group.setGid(groupID);
		}
		msg.setGroup(group);
		if (group != null) {
			msg.setFrom(group.getMemberByUin(fromUin));
		}
		msg.setTo(getContext().getAccount());
		if (msg.getFrom() == null) {
			QQGroupMember member = new QQGroupMember();
			member.setUin(fromUin);
			msg.setFrom(member);
			if (group != null) {
				group.getMembers().Add(member);
			}
		}

		return new QQNotifyEvent(QQNotifyEvent.Type.CHAT_MSG, msg);
	}

	/**
	 * <p>
	 * processDiscuzMsg.
	 * </p>
	 * 
	 * @param pollData
	 *            a {@link org.json.JSONObject} object.
	 * @throws org.json.JSONException
	 *             if any.
	 * @throws iqq.im.QQException
	 *             if any.
	 * @return a {@link iqq.im.event.QQNotifyEvent} object.
	 */
	public QQNotifyEvent processDiscuzMsg(JSONObject pollData)  {
		QQStore store = getContext().getStore();

		QQMsg msg = new QQMsg();
		long fromUin = pollData.getLong("send_uin");
		long did = pollData.getLong("did");

		msg.parseContentList(pollData.getJSONArray("content").ToString());
		msg.setType(QQMsg.Type.DISCUZ_MSG);
		msg.setDiscuz(store.getDiscuzByDid(did));
		msg.setTo(getContext().getAccount());
		msg.setDate(new DateTime(pollData.getLong("time") * 1000));

		if (msg.getDiscuz() != null) {
			msg.setFrom(msg.getDiscuz().getMemberByUin(fromUin));
		}

		if (msg.getFrom() == null) {
			QQDiscuzMember member = new QQDiscuzMember();
			member.setUin(fromUin);
			msg.setFrom(member);
			if (msg.getDiscuz() != null) {
				msg.getDiscuz().getMembers().Add(member);
			}
		}
		return new QQNotifyEvent(QQNotifyEvent.Type.CHAT_MSG, msg);
	}

	/**
	 * <p>
	 * processSessionMsg.
	 * </p>
	 * 
	 * @param pollData
	 *            a {@link org.json.JSONObject} object.
	 * @throws org.json.JSONException
	 *             if any.
	 * @throws iqq.im.QQException
	 *             if any.
	 * @return a {@link iqq.im.event.QQNotifyEvent} object.
	 */
	public QQNotifyEvent processSessionMsg(JSONObject pollData)  {
		// {"retcode"in0,"result"in[{"poll_type"in"sess_message",
		// "value"in{"msg_id"in25144,"from_uin"in167017143,"to_uin"in1070772010,"msg_id2"in139233,"msg_type"in140,"reply_ip"in176752037,"time"in1365931836,"id"in2581801127,"ruin"in444674479,"service_type"in1,
		// "flags"in{"text"in1,"pic"in1,"file"in1,"audio"in1,"video"in1},"content"in[["font",{"size"in9,"color"in"000000","style"in[0,0,0],"name"in"Tahoma"}],"2\u8F7D3 ",["face",1]," "]}}]}
		QQStore store = getContext().getStore();

		QQMsg msg = new QQMsg();
		long fromUin = pollData.getLong("from_uin");
		long fromQQ = pollData.getLong("ruin"); // 真实QQ
		int serviceType = pollData.getInt("service_type"); // Groupin0,Discussin1
		long typeId = pollData.getLong("id"); // Group ID or Discuss ID

		msg.parseContentList(pollData.getJSONArray("content").ToString());
		msg.setType(QQMsg.Type.SESSION_MSG);
		msg.setTo(getContext().getAccount());
		msg.setDate(new DateTime(pollData.getLong("time") * 1000));

		QQUser user = store.getBuddyByUin(fromUin); // 首先看看是不是自己的好友
		if (user != null) {
			msg.setType(QQMsg.Type.BUDDY_MSG); // 是自己的好友
		} else {
			if (serviceType == 0) { // 是群成员
				QQGroup group = store.getGroupByCode(typeId);
				foreach (QQUser u in group.getMembers()) {
					if (u.getUin() == fromUin) {
						user = u;
						break;
					}
				}
			} else if (serviceType == 1) { // 是讨论组成员
				QQDiscuz discuz = store.getDiscuzByDid(typeId);
				foreach (QQUser u in discuz.getMembers()) {
					if (u.getUin() == fromUin) {
						user = u;
						break;
					}
				}
			} else {
				user = store.getStrangerByUin(fromUin); // 看看陌生人列表中有木有
			}
			if (user == null) { // 还没有就新建一个陌生人，原理来说不应该这样。后面我就不知道怎么回复这消息了，但是消息是不能丢失的
				user = new QQStranger();
				user.setQQ(pollData.getLong("ruin"));
				user.setUin(fromUin);
				user.setNickname(pollData.getLong("ruin") + "");
				store.addStranger((QQStranger) user);
			}
		}
		user.setQQ(fromQQ); // 带上QQ号码
		msg.setFrom(user);
		return new QQNotifyEvent(QQNotifyEvent.Type.CHAT_MSG, msg);
	}

    }
}
