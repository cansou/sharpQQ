using QQWpfApplication1.action;
using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace QQWpfApplication1.action
{
    class WebLoginAction:AbstractHttpAction
    {

        
	private String username;
	private String password;
	private long   uin;
	private String verifyCode;

	/**
	 * <p>Constructor for WebLoginAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @param username a {@link java.lang.String} object.
	 * @param password a {@link java.lang.String} object.
	 * @param uin a long.
	 * @param verifyCode a {@link java.lang.String} object.
	 */
	public WebLoginAction(QQContext context, QQActionListener.OnActionEvent listener,
			String username, String password, long uin, String verifyCode) :base(context, listener){
		this.username = username;
		this.password = password;
		this.uin = uin;
		this.verifyCode = verifyCode;
	}

	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
	
	//尝试登录，准备传递的参数值
	QQHttpRequest req = createHttpRequest("GET", QQConstants.URL_UI_LOGIN);
	req.addGetValue("u", username);
	req.addGetValue("p", QQEncryptor.encryptQm(uin, password, verifyCode));
	req.addGetValue("verifycode", verifyCode);
	req.addGetValue("webqq_type", "10");
	req.addGetValue("remember_uin","1");
	req.addGetValue("login2qq", "1");
	req.addGetValue("aid", "1003903");
	req.addGetValue("u1", "http://web.qq.com/loginproxy.html?login2qq=1&webqq_type=10");
	req.addGetValue("h", "1");
	req.addGetValue("ptredirect", "0");
	req.addGetValue("ptlang", "2052");
	req.addGetValue("daid", "164");
	req.addGetValue("from_ui", "1");
	req.addGetValue("pttype", "1");
	req.addGetValue("dumy", "");
	req.addGetValue("fp", "loginerroralert");
	req.addGetValue("action", "2-12-26161");
	req.addGetValue("mibao_css", "m_webqq");
	req.addGetValue("t", "1");
	req.addGetValue("g", "1");
	req.addGetValue("js_type", "0");
	req.addGetValue("js_ver", QQConstants.JSVER);
	req.addGetValue("login_sig", getContext().getSession().getLoginSig());
	
	//2015-03-02 登录协议增加的参数
	req.addGetValue("pt_uistyle", "5");
	req.addGetValue("pt_randsalt", "0");
	req.addGetValue("pt_vcode_v1", "0");
	ApacheHttpService httpService = getContext().getSerivce();
	Cookie ptvfsession = httpService.getCookie("ptvfsession", QQConstants.URL_UI_LOGIN);
	if(ptvfsession == null){//验证session在获取验证码阶段得到的。
		ptvfsession = httpService.getCookie("verifysession", QQConstants.URL_UI_LOGIN);
	}
	if(ptvfsession != null)
	{
		 req.addGetValue("pt_verifysession_v1", ptvfsession.Value);
	}

	req.addHeader("Referer", QQConstants.REFFER);
	return req;
}

	/** {@inheritDoc} */
    public override void onHttpStatusOK(QQHttpResponse response)
    {
        Regex pt = new Regex(QQConstants.REGXP_LOGIN);
        Match mc = pt.Match(response.getResponseString());
	     if(mc.Success){
	    	int ret = int.Parse(mc.Groups[1].Value);
	    	switch(ret){
		    	case 0: notifyActionEvent(QQActionEvent.Type.EVT_OK, mc.Groups[3].Value); break;	
		    	case 3: throw new QQException(QQWpfApplication1.action.QQException.QQErrorCode.WRONG_PASSWORD, mc.Groups[5].Value);
                case 4: throw new QQException(QQWpfApplication1.action.QQException.QQErrorCode.WRONG_CAPTCHA, mc.Groups[5].Value);
                case 7: throw new QQException(QQWpfApplication1.action.QQException.QQErrorCode.IO_ERROR, mc.Groups[5].Value);
                default: throw new QQException(QQWpfApplication1.action.QQException.QQErrorCode.INVALID_USER, mc.Groups[5].Value);
	    	}
	     }else{
	    	 throw new QQException(QQWpfApplication1.action.QQException.QQErrorCode.UNEXPECTED_RESPONSE);
	     }		
	}


    }
}
