using QQWpfApplication1.evt;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.bean
{
    public class QQNotifyEventArgs
    {


        /**
         * 需要用户识别验证码通知
         * 登录，加好友，获取QQ号可能都需要验证码
         * @author solosky
         */
        public class ImageVerify
        {
            public enum VerifyType { LOGIN, ADD_FRIEND, GET_UIN };
            /**验证的类型，登陆，添加好友，获取qq号可能会出现验证码*/
            public VerifyType type;
            /**验证码图片对象**/
            public Bitmap image;
            /**需要验证的原因*/
            public String reason;
            /**future对象，在验证流程内部使用*/
            public AbstractActionFuture future;
            /**每一个验证码对应HTTP中cookie中名字为verifysession的值*/
            public String vsession;
            /**验证码字符*/
            public String vcode;
        }

        /**
         * 登录进度通知
         * @author solosky
         */
        public enum LoginProgress
        {
            CHECK_VERIFY,
            UI_LOGIN,
            UI_LOGIN_VERIFY,
            CHANNEL_LOGIN,
        }

    }
}
