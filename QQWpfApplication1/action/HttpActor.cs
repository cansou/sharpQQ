using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class HttpActor
    {

        
	private Type type;
	private QQContext context;
	private AbstractHttpAction action;
	private QQHttpResponse response;
	private Exception throwable;
	private long current;
	private long total;
	
	/** {@inheritDoc} */
	
	public void execute() {
		try {
			switch (type) {
			case Type.BUILD_REQUEST: {
				ApacheHttpService service =context.getSerivce();
				QQHttpRequest request = action.onBuildRequest();
				service.executeHttpRequest(request, new HttpAdaptor(context, action));
				}
				break;


            case Type.ON_HTTP_ERROR:
				action.onHttpError(throwable);
				break;

            case Type.ON_HTTP_FINISH:
				action.onHttpFinish(response);
				break;
				
			}
		} catch (QQException e) {
			action.notifyActionEvent(QQActionEvent.Type.EVT_ERROR, e);
		}
	}
	
	
	public enum Type{
		BUILD_REQUEST,
		CANCEL_REQUEST,
		ON_HTTP_ERROR,
		ON_HTTP_FINISH,
		ON_HTTP_HEADER,
		ON_HTTP_WRITE,
		ON_HTTP_READ
	}

    public HttpActor() { }
	/**
	 * <p>Constructor for HttpActor.</p>
	 *
	 * @param type a {@link iqq.im.actor.HttpActor.Type} object.
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param action a {@link iqq.im.action.AbstractHttpAction} object.
	 */
	public HttpActor(Type type, QQContext context, AbstractHttpAction action) {
		this.type = type;
		this.context = context;
		this.action = action;
	}


	/**
	 * <p>Constructor for HttpActor.</p>
	 *
	 * @param type a {@link iqq.im.actor.HttpActor.Type} object.
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param action a {@link iqq.im.action.AbstractHttpAction} object.
	 * @param response a {@link iqq.im.http.QQHttpResponse} object.
	 */
	public HttpActor(Type type, QQContext context, AbstractHttpAction action, QQHttpResponse response) {
		this.type = type;
		this.context = context;
		this.action = action;
		this.response = response;
	}
	

	/**
	 * <p>Constructor for HttpActor.</p>
	 *
	 * @param type a {@link iqq.im.actor.HttpActor.Type} object.
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param action a {@link iqq.im.action.AbstractHttpAction} object.
	 * @param throwable a {@link java.lang.Throwable} object.
	 */
	public HttpActor(Type type, QQContext context, AbstractHttpAction action, Exception throwable) {
		this.type = type;
		this.context = context;
		this.action = action;
		this.throwable = throwable;
	}


	/**
	 * <p>Constructor for HttpActor.</p>
	 *
	 * @param type a {@link iqq.im.actor.HttpActor.Type} object.
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param action a {@link iqq.im.action.AbstractHttpAction} object.
	 * @param current a long.
	 * @param total a long.
	 */
	public HttpActor(Type type, QQContext context, AbstractHttpAction action, long current, long total) {
		this.type = type;
		this.context = context;
		this.action = action;
		this.current = current;
		this.total = total;
	}


	public class HttpAdaptor: QQHttpListener{
		private QQContext context;
		private AbstractHttpAction action;
		
		public HttpAdaptor(QQContext context, AbstractHttpAction action) {
			this.context = context;
			this.action = action;
		}

		
		public void onHttpFinish(QQHttpResponse response) {
			context.pushActor(new HttpActor(Type.ON_HTTP_FINISH, context, action, response));
		}

		
		public void onHttpError(Exception t) {
			context.pushActor(new HttpActor(Type.ON_HTTP_ERROR, context, action, t));
			
		}

		
	}


	/** {@inheritDoc} */
	
	public String toString() {
		return "HttpActor [type=" + type + ", action=" + action + "]";
	}

    }
}
