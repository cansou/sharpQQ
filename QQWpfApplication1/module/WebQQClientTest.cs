using QQWpfApplication1.bean;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace QQWpfApplication1.action
{
    class WebQQClientTest
    {
        
	
	WebQQClient client;
	
	public WebQQClientTest(String user, String pwd, String wbUser, String wbPwd){
		QQAccount account = new QQAccount();
		account.setUsername(user);
		account.setPassword(pwd);
		account.setWbUsername(wbUser);
		account.setWbPassword(wbPwd);
		client = new WebQQClient(new QQNotifyHandlerProxy(this), new ThreadActorDispatcher());
		client.setAccount(account);
	}

    /**
     * 程序入口
     *
     */
//    public void loginWb(){
//    	client.preloginWb(new QQActionListener() {
//			
//			@Override
//			public void onActionEvent(QQActionEvent evt) {
//				// TODO Auto-generated method stub
//				if (evt.getType() == Type.EVT_OK) {
//					//到这里就算是登录成功了
//					ArrayList<String> list = (ArrayList<String>) evt.getTarget();
//					foreach (String string : list) {
//						Console.WriteLine(string);
//					}
//					Console.WriteLine("就算是登录成功微博了");
//					
//					new Thread(new Runnable() {
//						
//						@Override
//						public void run() {
//							// TODO Auto-generated method stub
//							while(true){
//								try {
//									String sendMsg = new BufferedReader(new InputStreamReader(System.in)).readLine();
//									if(sendMsg.contains("#")){
//										client.pollWbMsg("5175429989", new QQActionListener() {
//											
//											@Override
//											public void onActionEvent(QQActionEvent evt) {
//												// TODO Auto-generated method stub
//												if (evt.getType() == Type.EVT_OK) {
//													Console.WriteLine("pllmsg：   "+evt.getTarget());
//												}
//											}
//										});
//									}else{
//										client.sendWbMsg(sendMsg, "5175429989", new QQActionListener() {
//											
//											@Override
//											public void onActionEvent(QQActionEvent evt) {
//												// TODO Auto-generated method stub
//												if (evt.getType() == Type.EVT_OK) {
//													Console.WriteLine("发送成功");
//												}
//											}
//										});
//									}
//									
//								} catch (IOException e) {
//									// TODO Auto-generated catch block
//									e.printStackTrace();
//								}
//							}
//							
//						}
//					}).start();
//					
//				}else{
//					Console.WriteLine(evt.getTarget());
//				}
//			}
//		});
//    }
	
	/**
	 * 聊天消息通知，使用这个注解可以收到QQ消息
     *
     * 接收到消息然后组装消息发送回去
	 * 
	 * @throws QQException
	 */
	public void processBuddyMsg(QQNotifyEvent evt) {
		QQMsg msg = (QQMsg) evt.getTarget();
		
		List<ContentItem> items = msg.getContentList();
		foreach(ContentItem item in items) {
			if(item.getType() == ContentItem.Type.FACE) {
			}else if(item.getType() == ContentItem.Type.OFFPIC) {
			}else if(item.getType() == ContentItem.Type.TEXT) {
				Console.WriteLine(" Text:" + ((TextItem)item).getContent());
			}
		}

        // 组装QQ消息发送回去
        QQMsg sendMsg = new QQMsg();
        sendMsg.setTo(msg.getFrom());                       // QQ好友UIN
        sendMsg.setType(QQMsg.Type.BUDDY_MSG);              // 发送类型为好友
        // QQ内容
        sendMsg.addContentItem(new TextItem("hello"));      // 添加文本内容
        sendMsg.addContentItem(new FaceItem(0));            // QQ id为0的表情
        sendMsg.addContentItem(new FontItem());             // 使用默认字体
        client.sendMsg(sendMsg, null);                      // 调用接口发送消息
	}
	
	/**
	 * 被踢下线通知
	 * 
	 */
	protected void processKickOff(QQNotifyEvent evt){
	}
	
	/**
	 * 需要验证码通知
	 * 
	 * @throws IOException
	 */
	public  void processVerify(QQNotifyEvent evt) {
		QQNotifyEventArgs.ImageVerify verify = (QQNotifyEventArgs.ImageVerify) evt.getTarget();
        BufferedStream stream = new BufferedStream(verify.image.StreamSource);
      FileStream file = File.Create("C:\\Users\\leegean\\Desktop");
        int read = -1;
        while((read = stream.ReadByte())!=-1){
            file.WriteByte((byte)read);
        }
        file.Dispose();
		Console.WriteLine(verify.reason);
		Console.WriteLine("请输入在项目根目录下verify.png图片里面的验证码:");
        
		String code = Console.ReadLine();
		client.submitVerify(code, evt);
	}
    //public  void processWbVerify(QQNotifyEvent evt) {
    //    WbVerifyImage verify = (WbVerifyImage) evt.getTarget();
    //    ImageIO.write(verify.getImage(), "png", new File("verify.png"));
    //    System.out.print("请输入在项目根目录下verify.png图片里面的验证码:");
    //    String code = new BufferedReader(new InputStreamReader(System.in)).readLine();
    //    client.loginWb(verify.getFuture(), verify);
    //}
	/**
	 * 登录
	 */
	public void login() {
        client.login(QQStatus.ONLINE, delegate(QQActionEvent evt)
        {
            Console.WriteLine("LOGIN_STATUS:" + evt.getType() + ":" + evt.getTarget());
            if (evt.getType() == QQActionEvent.Type.EVT_OK)
            {
                //到这里就算是登录成功了

                //获取下用户信息
                client.getUserInfo(client.getAccount(), delegate(QQActionEvent evt1)
                {
                    Console.WriteLine("LOGIN_STATUS:" + evt1.getType() + ":" + evt1.getTarget());
                });

                // 获取好友列表..TODO.
                // 不一定调用，可能会有本地缓存
                client.getBuddyList(delegate(QQActionEvent evt1)
                {
                    // TODO Auto-generated method stub
                    Console.WriteLine("******** " + evt1.getType()
                            + " ********");
                    if (evt1.getType() == QQActionEvent.Type.EVT_OK)
                    {
                        Console.WriteLine("******** 好友列表  ********");
                        List<QQCategory> qqCategoryList = (List<QQCategory>)evt
                                .getTarget();

                        foreach (QQCategory c in qqCategoryList)
                        {
                            Console.WriteLine("分组名称:" + c.getName());
                            List<QQBuddy> buddyList = c.getBuddyList();
                            foreach (QQBuddy b in buddyList)
                            {
                                Console.WriteLine("---- QQ nick:"
                                        + b.getNickname()
                                        + " markname:"
                                        + b.getMarkname() + " uin:"
                                        + b.getUin() + " isVip:"
                                        + b.isVip() + " vip_level:"
                                        + b.getVipLevel());
                            }

                        }
                    }
                    else if (evt1.getType() == QQActionEvent.Type.EVT_ERROR)
                    {
                        Console.WriteLine("** 好友列表获取失败，处理重新获取");
                    }
                });
                // 获取群列表
                client.getGroupList(delegate(QQActionEvent evt1)
                {
                    if (evt1.getType() == QQActionEvent.Type.EVT_OK)
                    {
                        foreach (QQGroup g in client.getGroupList())
                        {
                            client.getGroupInfo(g, null);
                            Console.WriteLine("Group: " + g.getName());
                        }
                    }
                    else if (evt1.getType() == QQActionEvent.Type.EVT_ERROR)
                    {
                        Console.WriteLine("** 群列表获取失败，处理重新获取");
                    }
                });


                //启动轮询时，需要获取所有好友、群成员、讨论组成员
                //所有的逻辑完了后，启动消息轮询
                client.beginPollMsg();
            }
        });
	}
	
    //public void loginQm(){

    //    final QQActionListener listener = new QQActionListener() {
    //        public void onActionEvent(QQActionEvent evt) {
    //            Console.WriteLine("LOGIN_STATUS:" + evt.getType() + ":" + evt.getTarget());
    //            if (evt.getType() == Type.EVT_OK) {
    //                //到这里就算是登录成功了
    //                Console.WriteLine("就算是登录成功了");
					
    //            }
    //        }
    //    };
		
//		Mozilla/5.0 (Linux; U; Android 4.3; en-us; SM-N900T Build/JSS15J) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30
//		String ua = "Mozilla/5.0 (@os.name; @os.version; @os.arch) AppleWebKit/537.36 (KHTML, like Gecko) @appName Safari/537.36";
//		ua = ua.replaceAll("@appName", QQConstants.USER_AGENT);
//		ua = ua.replaceAll("@os.name", System.getProperty("os.name"));
//		ua = ua.replaceAll("@os.version", System.getProperty("os.version"));
//		ua = ua.replaceAll("@os.arch", System.getProperty("os.arch"));
//		client.setHttpUserAgent(ua);
        //client.loginQm(listener);
    //}

    /**
     * 新邮件通知
     *
     * 这个暂时没有启用
     *
     * @throws QQException
     */

    }
}
