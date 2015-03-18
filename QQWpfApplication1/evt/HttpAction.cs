using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QQWpfApplication1.evt;

namespace QQWpfApplication1.evt
{
    interface HttpAction:QQActionListener
    {
        
	/**
	 * <p>buildRequest.</p>
	 *
	 * @throws iqq.im.QQException if any.
	 * @return a {@link iqq.im.http.QQHttpRequest} object.
	 */
	public QQHttpRequest buildRequest() ;
	/**
	 * <p>cancelRequest.</p>
	 *
	 * @throws iqq.im.QQException if any.
	 */
	public void cancelRequest() ;
	/**
	 * <p>isCancelable.</p>
	 *
	 * @return a boolean.
	 */
	public Boolean isCancelable();
	/**
	 * <p>notifyActionEvent.</p>
	 *
	 * @param type a {@link iqq.im.event.QQActionEvent.Type} object.
	 * @param target a {@link java.lang.Object} object.
	 */
	public void notifyActionEvent(QQActionEvent.Type type, Object target);
	/**
	 * <p>getActionListener.</p>
	 *
	 * @return a {@link iqq.im.QQActionListener} object.
	 */
	public QQActionListener getActionListener();
	/**
	 * <p>setActionListener.</p>
	 *
	 * @param listener a {@link iqq.im.QQActionListener} object.
	 */
	public void setActionListener(QQActionListener listener);
	/**
	 * <p>setActionFuture.</p>
	 *
	 * @param future a {@link iqq.im.event.QQActionFuture} object.
	 */
	public void setActionFuture(AbstractActionFuture future);
	/**
	 * <p>setResponseFuture.</p>
	 *
	 * @param future a {@link java.util.concurrent.Future} object.
	 */

    }
}
