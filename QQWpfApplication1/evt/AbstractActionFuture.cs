using QQWpfApplication1;
using QQWpfApplication1.evt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.evt
{
    abstract class AbstractActionFuture:QQActionListener
    {
        
	private QQActionListener proxyListener;
	private SizeQueue<QQActionEvent> evtQueue;
    private volatile Boolean hasEvent;

	/**
	 * <p>Constructor for AbstractActionFuture.</p>
	 *
	 * @param proxyListener a {@link iqq.im.QQActionListener} object.
	 */
	public AbstractActionFuture(QQActionListener proxyListener) {
        this.hasEvent = true;
		this.proxyListener = proxyListener;
		this.evtQueue = new SizeQueue<QQActionEvent>(int.MaxValue);
	}

	/** {@inheritDoc} */
	public QQActionEvent waitEvent()  {
		if( !hasEvent ) {
			return null;
		}
		try {
			QQActionEvent evt = evtQueue.Dequeue();
			hasEvent = !isFinalEvent(evt);
			return evt;
		} catch (Exception e) {
			throw new QQException(QQException.QQErrorCode.WAIT_INTERUPPTED, e);
		}
	}


	/** {@inheritDoc} */
	public QQActionEvent waitFinalEvent()  {
		QQActionEvent evt = null;
		while( (evt = waitEvent()) != null){
			if( isFinalEvent(evt) ){
				return evt;
			}
		}
		throw new QQException(QQWpfApplication1.evt.QQException.QQErrorCode.UNKNOWN_ERROR);
	}

	
	private Boolean isFinalEvent(QQActionEvent evt){
		QQActionEvent.Type type = evt.getType();
		return type==QQActionEvent.Type.EVT_CANCELED
                || type == QQActionEvent.Type.EVT_ERROR
                || type == QQActionEvent.Type.EVT_OK;
	}

	/** {@inheritDoc} */
	public void onActionEvent(QQActionEvent evt) {
		if (proxyListener != null){
			proxyListener.onActionEvent(evt);
		}
		evtQueue.Enqueue(evt);
	}

	
	/**
	 * <p>notifyActionEvent.</p>
	 *
	 * @param type a {@link iqq.im.evt.QQActionEvent.Type} object.
	 * @param target a {@link java.lang.Object} object.
	 */
	public void notifyActionEvent(QQActionEvent.Type type, Object target){
		onActionEvent(new QQActionEvent(type, target, this));
	}

	/**
	 * <p>Getter for the field <code>proxyListener</code>.</p>
	 *
	 * @return the proxyListener
	 */
	public QQActionListener getProxyListener() {
		return proxyListener;
	}

    }
}
