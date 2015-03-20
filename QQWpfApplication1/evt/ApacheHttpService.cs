using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.evt
{
    class ApacheHttpService
    {

        
	private HttpClient asyncHttpClient;
	private String userAgent;

	/** {@inheritDoc} */
	
	public void setUserAgent(String userAgent) {
		this.userAgent = userAgent;
	}

	/** {@inheritDoc} */
	
	public QQHttpRequest createHttpRequest(String method, String url) {
		QQHttpRequest req = new QQHttpRequest(url, method);
		// req.addHeader("User-Agent", userAgent != null ? userAgent :
		// QQConstants.USER_AGENT);
		// req.addHeader("Referer", QQConstants.REFFER);
		return req;
	}

	/** {@inheritDoc} */
	
	public void executeHttpRequest(QQHttpRequest request, HttpAdaptor listener)  {
	}

	/** {@inheritDoc} */
	

	/** {@inheritDoc} */
	
	public void init(QQContext context)  {
	}

	/** {@inheritDoc} */
	
	public void destroy()  {
	}




    internal Cookie getCookie(string p1, string p2)
    {
        throw new NotImplementedException();
    }
    }
}
