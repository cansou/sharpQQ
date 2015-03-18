using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QQWpfApplication1.evt;

namespace QQWpfApplication1.evt
{
    public class QQActionEvent
    {
	private Type type;
	private Object target;
	private AbstractActionFuture future;
	
	/**
	 * <p>Constructor for QQActionEvent.</p>
	 *
	 * @param type a {@link iqq.im.event.QQActionEvent.Type} object.
	 * @param target a {@link java.lang.Object} object.
	 * @param future a {@link iqq.im.event.QQActionFuture} object.
	 */
    public QQActionEvent(Type type, Object target, AbstractActionFuture future)
    {
		this.type = type;
		this.target = target;
		this.future = future;
	}
	
	
	/**
	 * <p>Getter for the field <code>type</code>.</p>
	 *
	 * @return a {@link iqq.im.event.QQActionEvent.Type} object.
	 */
	public Type getType() {
		return type;
	}
	/**
	 * <p>Getter for the field <code>target</code>.</p>
	 *
	 * @return a {@link java.lang.Object} object.
	 */
	public Object getTarget() {
		return target;
	}


	public static enum Type{
		EVT_OK,
		EVT_ERROR,
		EVT_WRITE,
		EVT_READ,
		EVT_CANCELED,
		EVT_RETRY,
	}


	/** {@inheritDoc} */
	public String ToString() {
		return "QQActionEvent [type=" + type + ", target=" + target + "]";
	}

    }
}
