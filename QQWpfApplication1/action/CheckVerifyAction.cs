using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace QQWpfApplication1.action
{
    class CheckVerifyAction:AbstractHttpAction
    {
        
	private String qqAccount;
	/**
	 * <p>Constructor for CheckVerifyAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @param qqAccount a {@link java.lang.String} object.
	 */
	public CheckVerifyAction(QQContext context, QQActionListener.OnActionEvent listener, String qqAccount):base(context, listener) {
		this.qqAccount = qqAccount;
	}

	/** {@inheritDoc} */

    public override void onHttpStatusOK(QQHttpResponse response)
    {
		Regex p = new Regex(QQConstants.REGXP_CHECK_VERIFY);
        String msg = response.getResponseMessage();
        Match m = p.Match(msg);
        if(m.Success){
        	Console.WriteLine(msg);
            String qqHex = m.Groups[3].Value;
			qqHex = qqHex.Replace("\\x", "");
        	QQActionEventArgs.CheckVerifyArgs args = new QQActionEventArgs.CheckVerifyArgs();
        	args.result = int.Parse(m.Groups[1].Value);
        	args.code   = m.Groups[2].Value;
            args.uin = long.Parse(qqHex, NumberStyles.AllowHexSpecifier);
        	notifyActionEvent(QQActionEvent.Type.EVT_OK, args);
        }else{
        	notifyActionEvent(QQActionEvent.Type.EVT_ERROR, QQWpfApplication1.action.QQException.QQErrorCode.UNEXPECTED_RESPONSE);
        }
	}

	/** {@inheritDoc} */

    public override QQHttpRequest onBuildRequest()
    {
//		http://check.ptlogin2.qq.com/check?regmaster=&pt_tea=1&uin=1002053815&appid=715030901&js_ver=10106&js_type=1&login_sig=aRWz77AEo9rkn2UWz1DVJpU9cb5Lq*QY5dXw5i0WCkkbGzBUCDCyJKRTcGugwGzY&u1=http%3A%2F%2Fui.ptlogin2.qq.com%2Flogin_proxy.html&r=0.6111077913083136
		String url = StringHelper.format(QQConstants.URL_CHECK_VERIFY, new Object[]{qqAccount, 
				getContext().getSession().getLoginSig(), new Random().NextDouble()});
		return createHttpRequest("GET", url);
	}

    }
}
