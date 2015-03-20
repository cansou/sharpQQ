using QQWpfApplication1.evt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.module
{
    class CheckLoginSigAction:AbstractHttpAction
    {
        private String checkSigUrl;
	public CheckLoginSigAction(QQContext context, QQActionListener.OnActionEvent listener, String checkSigUrl) :base(context,listener){
		this.checkSigUrl = checkSigUrl;
	}
	/** {@inheritDoc} */
	protected void onHttpStatusOK(QQHttpResponse response)  {
		notifyActionEvent(QQActionEvent.Type.EVT_OK, null);
	}
	/** {@inheritDoc} */
	protected QQHttpRequest onBuildRequest()  {
		return createHttpRequest("GET", checkSigUrl);
	}
    }
}
