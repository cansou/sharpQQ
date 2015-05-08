using QQWpfApplication1.action;
using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QQWpfApplication1.evt

namespace QQWpfApplication1.action

{
    public abstract class AbstractHttpAction:QQHttpListener
    {
        private event QQActionListener.OnActionEvent evtHandlers;
	private QQContext context;
	private int retryTimes;
    private AbstractActionFuture future;

    public AbstractActionFuture Future
    {
        get { return future; }
    }
    public AbstractHttpAction(QQContext context, QQActionListener.OnActionEvent handler)
    {
		this.context = context;
        evtHandlers += handler;
		this.retryTimes = 0;
        this.future = new AbstractActionFuture(handler);
	}

	/** {@inheritDoc} */
    public QQHttpRequest createHttpRequest(String method, String url)
    {
        return new QQHttpRequest(method, url);
    }
     public  abstract QQHttpRequest onBuildRequest();
  
	public virtual void onHttpFinish(QQHttpResponse resp) {
		try {
           HttpStatusCode statusCode = resp.getStatusCode();
           Console.WriteLine(this.GetType()+"  :   "+resp.getResponseMessage());
            // 确认响应成功
            if (statusCode == HttpStatusCode.OK)
            {
                onHttpStatusOK(resp);
            }
            else
            {
                onHttpStatusError(resp);
            }

		} catch (QQException e) {
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, e);
		} catch (JSONException e) {
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, new QQException(QQWpfApplication1.action.QQException.QQErrorCode.JSON_ERROR, e));
		} catch (Exception e) {
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, new QQException(QQWpfApplication1.action.QQException.QQErrorCode.UNKNOWN_ERROR, e));
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
        //if (evtHandlers != null) {
        //    evtHandlers(new QQActionEvent(type, target));
        //}
        future.notifyActionEvent(type, target);
	}

    public QQContext getContext()
    {
        return context;
    }
    public  virtual void onHttpStatusError(QQHttpResponse response)
    {
		if (!doRetryIt(QQWpfApplication1.action.QQException.QQErrorCode.ERROR_HTTP_STATUS, null)) {
			throw new QQException(QQWpfApplication1.action.QQException.QQErrorCode.ERROR_HTTP_STATUS);
		}
	}

    public   abstract void onHttpStatusOK(QQHttpResponse response);
    

	private Boolean doRetryIt(QQWpfApplication1.action.QQException.QQErrorCode code, Exception t) {

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

	private QQWpfApplication1.action.QQException.QQErrorCode getErrorCode(Exception e) {
		if (e is Exception) {
			return QQWpfApplication1.action.QQException.QQErrorCode.IO_TIMEOUT;
		} else if (e is Exception) {
			return QQWpfApplication1.action.QQException.QQErrorCode.IO_ERROR;
		} else {
			return QQWpfApplication1.action.QQException.QQErrorCode.UNKNOWN_ERROR;
		}
	}

    }
}
