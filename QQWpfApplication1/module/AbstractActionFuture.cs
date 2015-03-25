using QQWpfApplication1;
using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.action
{
    public class AbstractActionFuture
    {
        
	private QQActionListener.OnActionEvent proxyListener;
	private SizeQueue<QQActionEvent> evtQueue;

	/**
	 * <p>Constructor for AbstractActionFuture.</p>
	 *
	 * @param proxyListener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 */
	public AbstractActionFuture(QQActionListener.OnActionEvent proxyListener) {
		this.proxyListener = proxyListener;
		this.evtQueue = new SizeQueue<QQActionEvent>(int.MaxValue);
	}


	/** {@inheritDoc} */


	/** {@inheritDoc} */
	public QQActionEvent waitFinalEvent()  {
		QQActionEvent evt = null;
        while ((evt = evtQueue.Dequeue()) != null)
        {
			if( isFinalEvent(evt) ){
				return evt;
			}
		}
		throw new QQException(QQWpfApplication1.action.QQException.QQErrorCode.UNKNOWN_ERROR);
	}

	
	private Boolean isFinalEvent(QQActionEvent evt){
		QQActionEvent.Type type = evt.getType();
		return type == QQActionEvent.Type.EVT_ERROR
                || type == QQActionEvent.Type.EVT_OK;
	}

	/**
	 * <p>notifyActionEvent.</p>
	 *
	 * @param type a {@link iqq.im.evt.QQActionEvent.Type} object.
	 * @param target a {@link java.lang.Object} object.
	 */
	public void notifyActionEvent(QQActionEvent.Type type, Object target){
        QQActionEvent evt = new QQActionEvent(type, target, this);
        if (proxyListener != null) proxyListener(evt);
        evtQueue.Enqueue(evt);
	}

    }
}
