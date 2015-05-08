using QQWpfApplication1.action;
using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.action
{
    public class ApacheHttpService
    {

        
	private HttpClient httpClient;
	private String userAgent;

	/** {@inheritDoc} */
	
	public void setUserAgent(String userAgent) {
		this.userAgent = userAgent;
	}

	/** {@inheritDoc} */
	

	/** {@inheritDoc} */

    public void executeHttpRequest(QQHttpRequest request, HttpActor.HttpAdaptor listener)
    {
        HttpMethod method;
        HttpRequestMessage   req =  new HttpRequestMessage();
        if(request.getMethod().Equals("get",StringComparison.OrdinalIgnoreCase)){
            method = HttpMethod.Get;
        }else{
            method = HttpMethod.Post;
            req.Content = new FormUrlEncodedContent(request.getPostDictionary());
        }
        req.Method = method;
        req.RequestUri = new Uri(request.getUrl());
      
        httpClient.SendAsync(req).ContinueWith(
            (requestTask) =>
            {
                if (requestTask.IsFaulted)
                {//处理异常
                    Console.WriteLine(requestTask.Exception);
                }
                else
                {
                    HttpResponseMessage response = requestTask.Result;
                    HttpResponseHeaders  headers = response.Headers;
                    foreach (KeyValuePair<String, IEnumerable<String>> pair in headers)
                    {
                        Console.WriteLine(pair.Key + " :    " + pair.Value);
                    }
                    response.Content.ReadAsByteArrayAsync().ContinueWith(
               (readTask) =>
               {
                   QQHttpResponse resp = new QQHttpResponse(response);
                   resp.respData = readTask.Result;
                   listener.onHttpFinish(resp);
               });
                    
                  
                }
            });
	}

	/** {@inheritDoc} */
	

	/** {@inheritDoc} */
	
	public void init(QQContext context)  {

        handler = new HttpClientHandler();
        httpClient = new HttpClient(handler);
	}

	/** {@inheritDoc} */
	
	public void destroy()  {
	}




    public Cookie getCookie(string name, String uri)
    {
        CookieCollection cookies = handler.CookieContainer.GetCookies(new Uri(uri));
        foreach(Cookie cookie in cookies){
            if(!cookie.Expired&&cookie.Name.Equals(name))return cookie;
        }
        return null;
    }



    
public  HttpClientHandler handler { get; set; }}


}
