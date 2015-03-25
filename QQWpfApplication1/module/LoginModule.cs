using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace QQWpfApplication1.action
{
    public class LoginModule:AbstractModule
    {

        public AbstractActionFuture checkVerify(String qqAccount, QQActionListener.OnActionEvent listener)
        {
             return  pushHttpAction(new CheckVerifyAction(getContext(), listener, qqAccount));
        }

        public AbstractActionFuture webLogin(String username, String password, long uin, String verifyCode, QQActionListener.OnActionEvent listener)
        {
             return  pushHttpAction(new WebLoginAction(getContext(), listener, username, password, uin, verifyCode));
        }

        public AbstractActionFuture channelLogin(QQStatus status, QQActionListener.OnActionEvent listener)
        {
             return  pushHttpAction(new ChannelLoginAction(getContext(), listener, status));
        }
        public AbstractActionFuture getCaptcha(long uin, QQActionListener.OnActionEvent listener)
        {
             return  pushHttpAction(new GetCaptchaImageAction(getContext(), listener, uin));
        }
        public AbstractActionFuture pollMsg(QQActionListener.OnActionEvent listener)
        {
             return  pushHttpAction(new PollMsgAction(getContext(), listener));
        }

        public AbstractActionFuture logout(QQActionListener.OnActionEvent listener)
        {
             return  pushHttpAction(new WebLogoutAction(getContext(), listener));
        }

        public AbstractActionFuture getLoginSig(QQActionListener.OnActionEvent listener)
        {
             return  pushHttpAction(new GetLoginSigAction(getContext(), listener));
        }
        public AbstractActionFuture checkLoginSig(String checkUrl, QQActionListener.OnActionEvent listener)
        {
             return  pushHttpAction(new CheckLoginSigAction(getContext(), listener, checkUrl));
        }


    }
}
