using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.action
{
    public class QQNotifyHandlerProxy:QQNotifyListener
    {
        
	private Object proxyObject;
	private Dictionary<QQNotifyEvent.Type, MethodInfo> methodDictionary;
	/**
	 * <p>Constructor for QQNotifyHandlerProxy.</p>
	 *
	 * @param proxyObject a {@link java.lang.Object} object.
	 */
	public QQNotifyHandlerProxy(Object proxyObject){
		this.proxyObject = proxyObject;
		this.methodDictionary = new Dictionary<QQNotifyEvent.Type, MethodInfo>();
		 foreach (MethodInfo m in proxyObject.GetType().GetMethods() ){
             if(m.Name.Contains("processBuddyMsg")){
                 this.methodDictionary.Add(QQNotifyEvent.Type.CHAT_MSG, m);
             }else  if(m.Name.Contains("processKickOff")){
                 this.methodDictionary.Add(QQNotifyEvent.Type.KICK_OFFLINE, m);
             }else  if(m.Name.Contains("processVerify")){
                 this.methodDictionary.Add(QQNotifyEvent.Type.CAPACHA_VERIFY, m);
             }else  if(m.Name.Contains("processWbVerify")){
                 this.methodDictionary.Add(QQNotifyEvent.Type.WB_CAPACHA_VERIFY, m);
             }
		 }
	}
	
	/** {@inheritDoc} */
	public void onNotifyEvent(QQNotifyEvent evt) {
		MethodInfo m =  methodDictionary[evt.getType()];
		if(m != null){
			try {
				m.Invoke(proxyObject, new Object[]{evt});
			} catch (Exception e) {
			}
		}else{
		}
	}


    }
}
