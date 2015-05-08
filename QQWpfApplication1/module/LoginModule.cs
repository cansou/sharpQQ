using QQWpfApplication1.evt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace QQWpfApplication1.module
{
    class LoginModule:AbstractModule
    {

        public void checkVerify(String qqAccount, QQActionListener.OnActionEvent listener)
        {
             pushHttpAction(new CheckVerifyAction(getContext(), listener, qqAccount));
        }

        public void webLogin(String username, String password, long uin, String verifyCode, QQActionListener.OnActionEvent listener)
        {
             pushHttpAction(new WebLoginAction(getContext(), listener, username, password, uin, verifyCode));
        }

        public void channelLogin(QQStatus status, QQActionListener.OnActionEvent listener)
        {
             pushHttpAction(new ChannelLoginAction(getContext(), listener, status));
        }
        public void getCaptcha(long uin, QQActionListener.OnActionEvent listener)
        {
             pushHttpAction(new GetCaptchaImageAction(getContext(), listener, uin));
        }
        public void pollMsg(QQActionListener.OnActionEvent listener)
        {
             pushHttpAction(new PollMsgAction(getContext(), listener));
        }

        public void logout(QQActionListener.OnActionEvent listener)
        {
             pushHttpAction(new WebLogoutAction(getContext(), listener));
        }

        public void getLoginSig(QQActionListener.OnActionEvent listener)
        {
             pushHttpAction(new GetLoginSigAction(getContext(), listener));
        }
        public void checkLoginSig(String checkUrl, QQActionListener.OnActionEvent listener)
        {
             pushHttpAction(new CheckLoginSigAction(getContext(), listener, checkUrl));
        }
	

    }
}
