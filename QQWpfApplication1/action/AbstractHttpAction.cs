using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace QQWpfApplication1.evt
{
    public abstract class AbstractHttpAction:QQHttpListener
    {
        private event QQActionListener.OnActionEvent evtHandlers;
	private QQContext context;
	private int retryTimes;

    public AbstractHttpAction(QQContext context, QQActionListener.OnActionEvent handler)
    {
		this.context = context;
        evtHandlers += handler;
		this.retryTimes = 0;
	}

	/** {@inheritDoc} */
	
	public void onHttpFinish(QQHttpResponse response) {
		try {


			if (response.getResponseCode() == QQHttpResponse.S_OK) {
				onHttpStatusOK(response);
			} else {
				onHttpStatusError(response);
			}
		} catch (QQException e) {
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, e);
		} catch (JSONException e) {
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, new QQException(QQWpfApplication1.evt.QQException.QQErrorCode.JSON_ERROR, e));
		} catch (Exception e) {
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, new QQException(QQWpfApplication1.evt.QQException.QQErrorCode.UNKNOWN_ERROR, e));
		}
	}

	/** {@inheritDoc} */
	
	public void onHttpError(Exception t) {
		if (!doRetryIt(getErrorCode(t), t)) {
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, new QQException(getErrorCode(t), t));
		}
	}

	/** {@inheritDoc} */
	
	
	public void notifyActionEvent(QQActionEvent.Type type, Object target) {
		if (evtHandlers != null) {
			evtHandlers(new QQActionEvent(type, target));
		}

	}

	/** {@inheritDoc} */
	
	public QQHttpRequest buildRequest() {
		try {
			return onBuildRequest();
		} catch (JSONException e) {
			throw new QQException(QQWpfApplication1.evt.QQException.QQErrorCode.JSON_ERROR, e);
		} catch (Exception e) {
			throw new QQException(QQWpfApplication1.evt.QQException.QQErrorCode.UNKNOWN_ERROR, e);
		}
	}

	protected QQHttpRequest createHttpRequest(String method, String url) {
		
		return getContext().getSerivce().createHttpRequest(method, url);
	}

    public QQContext getContext()
    {
        return context;
    }
	protected void onHttpStatusError(QQHttpResponse response)  {
		if (!doRetryIt(QQWpfApplication1.evt.QQException.QQErrorCode.ERROR_HTTP_STATUS, null)) {
			throw new QQException(QQWpfApplication1.evt.QQException.QQErrorCode.ERROR_HTTP_STATUS);
		}
	}

	protected void onHttpStatusOK(QQHttpResponse response) {
		notifyActionEvent(QQActionEvent.Type.EVT_OK, null);
	}
	protected QQHttpRequest onBuildRequest() {
		return null;
	}
	

	private Boolean doRetryIt(QQWpfApplication1.evt.QQException.QQErrorCode code, Exception t) {

		++retryTimes;
		if (retryTimes < QQConstants.MAX_RETRY_TIMES) {
			notifyActionEvent(QQActionEvent.Type.EVT_RETRY, new QQException(code, t));
				// 等待几秒再重试
				Thread.Sleep(1500);
			getContext().pushActor(new HttpActor(HttpActor.Type.BUILD_REQUEST, getContext(), this));
			return true;
		}

		return false;
	}

	private QQWpfApplication1.evt.QQException.QQErrorCode getErrorCode(Exception e) {
		if (e is Exception) {
			return QQWpfApplication1.evt.QQException.QQErrorCode.IO_TIMEOUT;
		} else if (e is Exception) {
			return QQWpfApplication1.evt.QQException.QQErrorCode.IO_ERROR;
		} else {
			return QQWpfApplication1.evt.QQException.QQErrorCode.UNKNOWN_ERROR;
		}
	}

    }
}
