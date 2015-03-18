using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.evt
{
    class ApacheHttpService
    {

        
	private static final Logger LOG = LoggerFactory.getLogger(ApacheHttpService.class);
	private DefaultHttpAsyncClient asyncHttpClient;
	private QQHttpCookieJar cookieJar;
	private String userAgent;

	/** {@inheritDoc} */
	@Override
	public void setHttpProxy(ProxyType proxyType, String proxyHost, int proxyPort, String proxyAuthUser, String proxyAuthPassword) {
		// TODO ...
	}

	/** {@inheritDoc} */
	@Override
	public void setUserAgent(String userAgent) {
		this.userAgent = userAgent;
	}

	/** {@inheritDoc} */
	@Override
	public QQHttpRequest createHttpRequest(String method, String url) {
		QQHttpRequest req = new QQHttpRequest(url, method);
		// req.addHeader("User-Agent", userAgent != null ? userAgent :
		// QQConstants.USER_AGENT);
		// req.addHeader("Referer", QQConstants.REFFER);
		return req;
	}

	/** {@inheritDoc} */
	@Override
	public Future<QQHttpResponse> executeHttpRequest(QQHttpRequest request, QQHttpListener listener) throws QQException {
		// if(asyncHttpClient.getConnectionManager().getStatus()==IOReactorStatus.SHUT_DOWN){
		// try {
		// asyncHttpClient = new DefaultHttpAsyncClient();
		// } catch (IOReactorException e) {
		// // TODO Auto-generated catch block
		// e.printStackTrace();
		// }
		// }
		URI uri = URI.create(request.getUrl());
		HttpHost httphost = URIUtils.extractHost(uri);
		if (httphost == null) {
			LOG.error("host is null, url: " + uri.toString());
			httphost = new HttpHost(uri.getHost());
		}

		try {

			if (request.getMethod().equals("POST")) {
				HttpPost httppost = new HttpPost(uri);

				if (request.getReadTimeout() > 0) {
					HttpConnectionParams.setSoTimeout(httppost.getParams(), request.getReadTimeout());
				}
				if (request.getConnectTimeout() > 0) {
					HttpConnectionParams.setConnectionTimeout(httppost.getParams(), request.getConnectTimeout());
				}

				if (request.getFileMap().size() > 0) {
					MultipartEntity entity = new MultipartEntity();
					String charset = request.getCharset();

					Map<String, String> postMap = request.getPostMap();
					for (String key : postMap.keySet()) {
						String value = postMap.get(key);
						value = value == null ? "" : value;
						entity.addPart(key, new StringBody(value, Charset.forName(charset)));
					}

					Map<String, File> fileMap = request.getFileMap();
					for (String key : fileMap.keySet()) {
						File value = fileMap.get(key);
						entity.addPart(new FormBodyPart(key, new FileBody(value, getMimeType(value))));
					}
					httppost.setEntity(entity);
				} else if (request.getPostMap().size() > 0) {
					List<NameValuePair> list = new ArrayList<NameValuePair>();

					Map<String, String> postMap = request.getPostMap();
					for (String key : postMap.keySet()) {
						String value = postMap.get(key);
						value = value == null ? "" : value;
						list.add(new BasicNameValuePair(key, value));
					}
					httppost.setEntity(new UrlEncodedFormEntity(list, request.getCharset()));
				}
				Map<String, String> headerMap = request.getHeaderMap();
				for (String key : headerMap.keySet()) {
					httppost.addHeader(key, headerMap.get(key));
				}
				QQHttpPostRequestProducer producer = new QQHttpPostRequestProducer(httphost, httppost, listener);
				QQHttpResponseConsumer consumer = new QQHttpResponseConsumer(request, listener, cookieJar);
				QQHttpResponseCallback callback = new QQHttpResponseCallback(listener);
				Future<QQHttpResponse> future = asyncHttpClient.execute(producer, consumer, callback);
				return new ProxyFuture(future, consumer, producer);

			} else if (request.getMethod().equals("GET")) {
				HttpGet httpget = new HttpGet(uri);
				Map<String, String> headerMap = request.getHeaderMap();
				for (String key : headerMap.keySet()) {
					httpget.addHeader(key, headerMap.get(key));
				}
				if (request.getReadTimeout() > 0) {
					HttpConnectionParams.setSoTimeout(httpget.getParams(), request.getReadTimeout());
				}
				if (request.getConnectTimeout() > 0) {
					HttpConnectionParams.setConnectionTimeout(httpget.getParams(), request.getConnectTimeout());
				}

				return asyncHttpClient.execute(new QQHttpGetRequestProducer(httphost, httpget), new QQHttpResponseConsumer(request, listener, cookieJar), new QQHttpResponseCallback(listener));

			} else {
				throw new QQException(QQErrorCode.IO_ERROR, "not support http method:" + request.getMethod());
			}
		} catch (IOException e) {
			throw new QQException(QQErrorCode.IO_ERROR);
		}
	}

	/** {@inheritDoc} */
	@Override
	public Cookie getCookie(String name, String url) {
		List<Cookie> cookies = asyncHttpClient.getCookieStore().getCookies();
		CookieSpec defaultCookieSpec = asyncHttpClient.getCookieSpecs().getCookieSpec(CookiePolicy.BEST_MATCH);
		
		if(url==null||url.length()==0){
			for (Cookie cookie : cookies) {
						if(cookie.getName().equals(name)) {
							return cookie;
						}
			}
		}else{
			URI uri = URI.create(url);
			String path = uri != null ? uri.getPath() : null;
			String hostName = uri.getHost();
			int port = uri.getPort();
			CookieOrigin cookieOrigin = new CookieOrigin(hostName, port >= 0 ? port : 0, path == null || path.length() == 0 ? path : "/", true);
			Date now = new Date();
			for (Cookie cookie : cookies) {
				if (!cookie.isExpired(now)) {
					if (defaultCookieSpec.match(cookie, cookieOrigin)) {
						if(cookie.getName().equals(name)) {
							return cookie;
						}
					}
				}
			}
		}
		
		return null;
	}

	/** {@inheritDoc} */
	@Override
	public void init(QQContext context) throws QQException {
		super.init(context);
		try {
			SSLContext sslContext = new QQSSLSocketFactory().getSSLContext();
			SSLContext.setDefault(sslContext);

			asyncHttpClient = new DefaultHttpAsyncClient();
			asyncHttpClient.getCookieStore().addCookie(new BasicClientCookie("SUB", "_2AkMj4X_qf8NjqwJRmPkTxWjibot0yADEiebDAH_sJxJjHnUY7EYbX9N4kXblopB0OG7Pey_MSsIi"));
			asyncHttpClient.getCookieStore().addCookie(new BasicClientCookie("SUBP", "0033WrSXqPxfM72-Ws9jqgMF55z29P9D9W5kuhy4zgpV4ioJq2zdHLG1"));
			asyncHttpClient.getCookieStore().addCookie(new BasicClientCookie("SINAGLOBAL", "171.113.253.82_1421734110.235142"));
			asyncHttpClient.getCookieStore().addCookie(new BasicClientCookie("Apache", "171.113.253.82_1421734110.235145"));

			HttpParams httpParams = asyncHttpClient.getParams();
			HttpConnectionParams.setSoTimeout(httpParams, QQConstants.HTTP_TIME_OUT);
			HttpConnectionParams.setConnectionTimeout(httpParams, QQConstants.HTTP_TIME_OUT);
			HttpConnectionParams.setTcpNoDelay(httpParams, true);
			HttpConnectionParams.setSocketBufferSize(httpParams, 4096);
			HttpProtocolParams.setVersion(httpParams, HttpVersion.HTTP_1_1);

			asyncHttpClient.getConnectionManager().getSchemeRegistry().register(new AsyncScheme("https", 443, new SSLLayeringStrategy(sslContext)));
			asyncHttpClient.setRedirectStrategy(new QQDefaultRedirectStrategy());
			asyncHttpClient.start();
			cookieJar = new QQHttpCookieJar();
		} catch (IOReactorException e) {
			throw new QQException(QQErrorCode.INIT_ERROR, e);
		}
	}

	/** {@inheritDoc} */
	@Override
	public void destroy() throws QQException {
		super.destroy();
		try {
			asyncHttpClient.shutdown();
		} catch (InterruptedException e) {
			throw new QQException(QQErrorCode.UNKNOWN_ERROR, e);
		}
	}

	private String getMimeType(File file) {
		FileNameMap fileNameMap = URLConnection.getFileNameMap();
		return fileNameMap.getContentTypeFor(file.toString());
	}

	// //////////////////////////////////////////////////////////////////////
	private static final String CANCEL_EX_STRING = "http canceled by user!!!";

	/**
	 * <p>
	 * checkCanceled.
	 * </p>
	 * 
	 * @param isCanceled
	 *            a boolean.
	 * @throws java.io.IOException
	 *             if any.
	 */
	public static void checkCanceled(boolean isCanceled) throws IOException {
		if (isCanceled) {
			throw new IOException(CANCEL_EX_STRING);
		}
	}

	static class QQDefaultRedirectStrategy extends DefaultRedirectStrategy {
		@Override
		protected URI createLocationURI(String url) throws ProtocolException {
			// 腾讯的某些URL含有 {} ，URI解析会报错，在这之前替换下
			url = url.replaceAll("\\{", "%7b");
			url = url.replaceAll("\\}", "%7d");
			return super.createLocationURI(url);
		}

	}

	static class QQHttpResponseConsumer extends AsyncByteConsumer<QQHttpResponse> {
		private QQHttpListener httpListener;
		private QQHttpResponse httpResponse;
		private QQHttpCookieJar httpCookieJar;
		private OutputStream httpOutStream;
		private long readLength;
		private long contentLength;
		private volatile boolean isCanceled;

		public QQHttpResponseConsumer(QQHttpRequest httpRequest, QQHttpListener httpListener, QQHttpCookieJar cookieJar) {
			this.httpListener = httpListener;
			this.readLength = 0;
			this.contentLength = 0;
			this.httpResponse = new QQHttpResponse();
			this.httpCookieJar = cookieJar;
			this.isCanceled = false;
			if (httpRequest.getOutputStream() != null) {
				httpOutStream = httpRequest.getOutputStream();
			} else {
				httpOutStream = new ByteArrayOutputStream();
			}
		}

		@Override
		protected void onResponseReceived(final HttpResponse response) {
			httpResponse.setResponseCode(response.getStatusLine().getStatusCode());
			httpResponse.setResponseMessage(response.getStatusLine().getReasonPhrase());

			Map<String, List<String>> fields = new HashMap<String, List<String>>();
			for (Header header : response.getAllHeaders()) {
				List<String> values = fields.get(header.getName());
				if (values == null) {
					values = new ArrayList<String>();
					fields.put(header.getName(), values);
				}
				values.add(header.getValue());
			}
			httpResponse.setHeaderFields(fields);
			contentLength = httpResponse.getContentLength();
			readLength = 0;

			List<String> setCookies = fields.get("Set-Cookie");
			if (setCookies != null) {
				httpCookieJar.updateCookies(setCookies);
			}

			if (httpListener != null) {
				httpListener.onHttpHeader(httpResponse);
				httpListener.onHttpRead(readLength, contentLength);
			}
		}

		@Override
		protected void releaseResources() {
		}

		@Override
		protected QQHttpResponse buildResult(final HttpContext context) {
			if (httpOutStream instanceof ByteArrayOutputStream) {
				ByteArrayOutputStream out = (ByteArrayOutputStream) httpOutStream;
				httpResponse.setResponseData(out.toByteArray());
				try {
					httpOutStream.close();
				} catch (IOException e) {
					// ingore
				}
			}
			if (httpListener != null) {
				httpListener.onHttpFinish(httpResponse);
			}
			return httpResponse;
		}

		@Override
		protected void onByteReceived(ByteBuffer buffer, IOControl control) throws IOException {
			checkCanceled(isCanceled);

			byte[] tmp = new byte[buffer.remaining()];
			buffer.get(tmp);
			httpOutStream.write(tmp);
			readLength += tmp.length;
			if (httpListener != null) {
				httpListener.onHttpRead(readLength, contentLength);
			}

			checkCanceled(isCanceled);
		}

		public void cancelIt() {
			isCanceled = true;
		}

	}

	static class QQHttpPostRequestProducer extends BasicAsyncRequestProducer {
		private InputStream httpInStream;
		private long contentLength;
		private long writeLength;
		private QQHttpListener httpListener;
		private volatile boolean isCanceled;

		public QQHttpPostRequestProducer(HttpHost target, HttpEntityEnclosingRequest request, QQHttpListener listener) throws IOException {
			super(target, request);
			HttpEntity entity = request.getEntity();
			// TODO
			// 暂时把所有的请求先读入内存，在存在大文件的时候可能OutOfMemory,以后重写一个基于MultiPartInputStream来优化
			ByteArrayOutputStream byteOutStream = new ByteArrayOutputStream();
			entity.writeTo(byteOutStream);
			httpInStream = new ByteArrayInputStream(byteOutStream.toByteArray());

			byteOutStream.close();
			contentLength = entity.getContentLength();
			writeLength = 0;
			httpListener = listener;

			isCanceled = false;

			if (httpListener != null) {
				httpListener.onHttpWrite(writeLength, contentLength);
			}
		}

		@Override
		public synchronized void produceContent(ContentEncoder encoder, IOControl ioctrl) throws IOException {
			checkCanceled(isCanceled);

			byte[] tmp = new byte[4096];
			int len = httpInStream.read(tmp);
			ByteBuffer buffer = ByteBuffer.wrap(tmp, 0, len);
			encoder.write(buffer);
			writeLength += len;

			if (httpListener != null) {
				httpListener.onHttpWrite(writeLength, contentLength);
			}
			checkCanceled(isCanceled);
		}

		public void cancelIt() {
			isCanceled = true;
		}
	}

	static class QQHttpGetRequestProducer extends BasicAsyncRequestProducer {
		public QQHttpGetRequestProducer(final HttpHost target, final HttpRequest request) {
			super(target, request);
		}
	}

	static class QQHttpResponseCallback implements FutureCallback<QQHttpResponse> {
		private QQHttpListener httpListener;

		public QQHttpResponseCallback(QQHttpListener httpListener) {
			this.httpListener = httpListener;
		}

		@Override
		public void cancelled() {
		}

		@Override
		public void completed(QQHttpResponse response) {
		}

		@Override
		public void failed(Exception ex) {
			ex.printStackTrace();
			if (ex instanceof IOException && CANCEL_EX_STRING.equals(ex.getMessage())) {
				return;
			}
			if (httpListener != null) {
				httpListener.onHttpError(ex);
			}
		}
	}

	static class ProxyFuture implements Future<QQHttpResponse> {
		private Future<QQHttpResponse> proxy;
		private QQHttpResponseConsumer consumer;
		private QQHttpPostRequestProducer producer;

		public ProxyFuture(Future<QQHttpResponse> proxy, QQHttpResponseConsumer consumer, QQHttpPostRequestProducer producer) {
			this.proxy = proxy;
			this.consumer = consumer;
			this.producer = producer;
		}

		@Override
		public boolean cancel(boolean mayInterruptIfRunning) {
			consumer.cancel();
			try {
				producer.close();
			} catch (IOException e) {
				// Ignore
			}
			consumer.cancelIt();
			producer.cancelIt();
			return proxy.cancel(mayInterruptIfRunning);
		}

		@Override
		public QQHttpResponse get() throws InterruptedException, ExecutionException {
			return proxy.get();
		}

		@Override
		public QQHttpResponse get(long timeout, TimeUnit unit) throws InterruptedException, ExecutionException, TimeoutException {
			return proxy.get();
		}

		@Override
		public boolean isCancelled() {
			return proxy.isCancelled();
		}

		@Override
		public boolean isDone() {
			return proxy.isDone();
		}

	}


    }
}
