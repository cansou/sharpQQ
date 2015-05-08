using QQWpfApplication1.action;
using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace QQWpfApplication1.action
{
    public class GetLoginSigAction:AbstractHttpAction
    {
        
	public GetLoginSigAction(QQContext context, QQActionListener.OnActionEvent listener) :base(context, listener){
	}

	/** {@inheritDoc} */
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		Regex pt = new Regex(QQConstants.REGXP_LOGIN_SIG);
		Match mc = pt.Match(response.getResponseString());
		if(mc.Success){
			QQSession session = getContext().getSession();
            session.setLoginSig(mc.Groups[1].Value);
			notifyActionEvent(QQActionEvent.Type.EVT_OK, session.getLoginSig());
		}else{
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, new QQException(QQWpfApplication1.action.QQException.QQErrorCode.INVALID_RESPONSE, "Login Sig Not Found!!"));
		}
	}

	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		return createHttpRequest("GET", QQConstants.URL_LOGIN_PAGE);
	}

    }
}
