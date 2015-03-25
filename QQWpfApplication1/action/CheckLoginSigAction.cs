using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    class CheckLoginSigAction:AbstractHttpAction
    {
        private String checkSigUrl;
	public CheckLoginSigAction(QQContext context, QQActionListener.OnActionEvent listener, String checkSigUrl) :base(context,listener){
		this.checkSigUrl = checkSigUrl;
	}
	/** {@inheritDoc} */
    public override void onHttpStatusOK(QQHttpResponse response)
    {
		notifyActionEvent(QQActionEvent.Type.EVT_OK, null);
	}
	/** {@inheritDoc} */
    public override QQHttpRequest onBuildRequest()
    {
		return createHttpRequest("GET", checkSigUrl);
	}
    }
}
