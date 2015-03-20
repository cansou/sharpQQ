using QQWpfApplication1.bean;
using QQWpfApplication1.evt;
using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.module
{
    class ChannelLoginAction:AbstractHttpAction
    {

        
	private QQStatus status;

	/**
	 * <p>Constructor for ChannelLoginAction.</p>
	 *
	 * @param context a {@link iqq.im.core.QQContext} object.
	 * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
	 * @param status a {@link iqq.im.bean.QQStatus} object.
	 */
	public ChannelLoginAction(QQContext context, QQActionListener.OnActionEvent listener, QQStatus status) :base(context, listener){
		this.status = status;
	}

	/** {@inheritDoc} */
	public QQHttpRequest onBuildRequest()  {
		ApacheHttpService httpService = getContext().getSerivce();
		QQSession session = getContext().getSession();
		Random rand = new Random();
		if(session.getClientId() == 0){
			session.setClientId(rand.Next()); //random??
		}
		
		JSONObject json = new JSONObject();
		json.put("status", "online");
		json.put("ptwebqq", httpService.getCookie("ptwebqq",  QQConstants.URL_CHANNEL_LOGIN).Value);
		json.put("passwd_sig", "");
		json.put("clientid", session.getClientId()); 
		json.put("psessionid", session.getSessionId());
		
		QQHttpRequest req = createHttpRequest("POST", QQConstants.URL_CHANNEL_LOGIN);
		req.addPostValue("r", json.ToString());
		req.addPostValue("clientid", session.getClientId()+"");
		req.addPostValue("psessionid", session.getSessionId());
		
		req.addHeader("Referer", QQConstants.REFFER);
		return req;
	}

	/** {@inheritDoc} */
    protected override  void onHttpStatusOK(QQHttpResponse response)
    {
		//{"retcode":0,"result":{"uin":236557647,"cip":1991953329,"index":1075,"port":51494,"status":"online","vfwebqq":"41778677efd86bae2ed575eea02349046a36f3f53298a34b97d75297ec1e67f6ee5226429daa6aa7","psessionid":"8368046764001d636f6e6e7365727665725f77656271714031302e3133332e342e31373200005b9200000549016e04004f95190e6d0000000a4052347371696a62724f6d0000002841778677efd86bae2ed575eea02349046a36f3f53298a34b97d75297ec1e67f6ee5226429daa6aa7","user_state":0,"f":0}}
		JSONObject json = new JSONObject(new JSONTokener(new StringReader(response.getResponseString())));
		QQSession session = getContext().getSession();
        QQAccount account = (QQAccount)getContext().getAccount();
		if(json.getInt("retcode") == 0){
			JSONObject ret = json.getJSONObject("result");
			account.setUin(ret.getLong("uin"));
			account.setQQ(ret.getLong("uin"));
			session.setSessionId(ret.getString("psessionid"));
			session.setVfwebqq(ret.getString("vfwebqq"));
            account.setStatus(QQStatus.ONLINE);
			session.setState(QQSession.State.ONLINE);
			session.setIndex(ret.getInt("index"));
			session.setPort(ret.getInt("port"));
			notifyActionEvent(QQActionEvent.Type.EVT_OK, null);
		}else{
			notifyActionEvent(QQActionEvent.Type.EVT_ERROR, new QQException(QQWpfApplication1.evt.QQException.QQErrorCode.INVALID_RESPONSE));	//TODO ..
		}
	}


    }
}
