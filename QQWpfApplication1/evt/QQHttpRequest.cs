using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.evt
{
    class QQHttpRequest
    {

        /**
         * URL
         */
        private String url;

        /**
         * Method
         */
        private String method;

        /**
         * 超时时间
         */
        private int timeout;

        /**
         * 请求的头部
         */
        private Dictionary<String, String> headerDictionary;

        /**
         * 请求的值集合
         */
        private Dictionary<String, String> postDictionary;

        /***
         * Post的文件列表
         */

        /**
         * Get方式的值集合
         */
        private Dictionary<String, String> getDictionary;

        /**
         * 请求的数据流
         */
        private Stream inputStream;

        /**
         * 保存的输出流
         */

        /**
         * 编码
         */
        private String charset;

        /**
         * 连接超时
         */
        private int connectTimeout;

        /***
         * 读取超时
         */
        private int readTimeout;
        /**
         * 默认的构造函数
         *
         * @param url			地址
         * @param method		方法
         */
        public QQHttpRequest(String url, String method)
        {
            this.url = url;
            this.method = method;
            this.headerDictionary = new Dictionary<String, String>();
            this.postDictionary = new Dictionary<String, String>();
            this.getDictionary = new Dictionary<String, String>();
        }

        /**
         * 设置URL
         *
         * @param url the url to set
         */
        public void setUrl(String url)
        {
            this.url = url;
        }

        /**
         * 设置请求的方法
         *
         * @param method the method to set
         */
        public void setMethod(String method)
        {
            this.method = method;
        }

        /**
         * 设置超时时间
         *
         * @param timeout the timeout to set
         */
        public void setTimeout(int timeout)
        {
            this.timeout = timeout;
        }

        /**
         * 添加请求头
         *
         * @param key a {@link java.lang.String} object.
         * @param value a {@link java.lang.String} object.
         */
        public void addHeader(String key, String value)
        {
            this.headerDictionary.Add(key, value);
        }


        /**
         * 以key=&gt;value的方式设置请求体，仅在方法为POST的方式下有用，默认为utf8编码
         *
         * @param keymap a {@link java.util.Dictionary} object.
         */
        public void setBody(Dictionary<String, String> keymap)
        {
            this.postDictionary = keymap;
        }

        /**
         * 添加POST的值
         *
         * @param key a {@link java.lang.String} object.
         * @param value a {@link java.lang.String} object.
         */
        public void addPostValue(String key, String value)
        {
            this.postDictionary.Add(key, value);
        }

        /**
         * 添加POST文件
         *
         * @param key a {@link java.lang.String} object.
         * @param file a {@link java.io.File} object.
         */

        /**
         * 添加POST的值
         *
         * @param key a {@link java.lang.String} object.
         * @param value a {@link java.lang.String} object.
         */
        public void addGetValue(String key, String value)
        {
            this.getDictionary.Add(key, value);
        }

        /**
         * 设置请求的数据流
         *
         * @param inputStream a {@link java.io.InputStream} object.
         */
        public void setBody(Stream inputStream)
        {
            this.inputStream = inputStream;
        }

        /**
         * <p>Getter for the field <code>headerDictionary</code>.</p>
         *
         * @return the headerDictionary
         */
        public Dictionary<String, String> getHeaderDictionary()
        {
            return headerDictionary;
        }

        /**
         * <p>Setter for the field <code>headerDictionary</code>.</p>
         *
         * @param headerDictionary the headerDictionary to set
         */
        public void setHeaderDictionary(Dictionary<String, String> headerDictionary)
        {
            this.headerDictionary = headerDictionary;
        }


        /**
         * <p>Getter for the field <code>inputStream</code>.</p>
         *
         * @return the inputStream
         */
        public Stream getInputStream()
        {
            if (this.inputStream != null)
            {
                return this.inputStream;
            }
            else if (this.postDictionary.Count > 0)
            {
                addHeader("Content-Type", "application/x-www-form-urlencoded");
                StringBuilder buffer = new StringBuilder();
                Dictionary<String, String>.KeyCollection.Enumerator it = this.postDictionary.Keys.GetEnumerator();
                String charset = "utf8";
                while (it.MoveNext())
                {
                    String key = it.Current;
                    String value;
                        this.postDictionary.TryGetValue(key, out value);
                    try
                    {
                        key = URl.encode(key, charset);
                        value = URLEncoder.encode(value == null ? "" : value, charset);
                        buffer.append(key);
                        buffer.append("=");
                        buffer.append(value);
                        buffer.append("&");
                    }
                    catch (Exception e)
                    {
                        throw new RuntimeException(e);
                    }
                }
                try
                {
                    return new ByteArrayInputStream(buffer.toString().getBytes(charset));
                }
                catch (UnsupportedEncodingException e)
                {
                    throw new RuntimeException(e);
                }
            }
            else
            {
                return null;
            }
        }

        /**
         * <p>Setter for the field <code>inputStream</code>.</p>
         *
         * @param inputStream the inputStream to set
         */
        public void setInputStream(InputStream inputStream)
        {
            this.inputStream = inputStream;
        }

        /**
         * <p>Getter for the field <code>url</code>.</p>
         *
         * @return the url
         */
        public String getUrl()
        {
            if (this.getDictionary.size() > 0)
            {
                StringBuffer buffer = new StringBuffer(url);
                buffer.append("?");
                Iterator<String> it = this.getDictionary.keySet().iterator();
                String charset = "utf8";
                while (it.hasNext())
                {
                    String key = it.next();
                    String value = this.getDictionary.get(key);
                    try
                    {
                        key = URLEncoder.encode(key, charset);
                        value = URLEncoder.encode(value == null ? "" : value, charset);
                        buffer.append(key);
                        buffer.append("=");
                        buffer.append(value);
                        if (it.hasNext())
                            buffer.append("&");
                    }
                    catch (Exception e)
                    {
                        throw new RuntimeException(e);
                    }
                }
                return buffer.toString();
            }
            else
            {
                return url;
            }
        }

        /**
         * <p>Getter for the field <code>method</code>.</p>
         *
         * @return the method
         */
        public String getMethod()
        {
            return method;
        }

        /**
         * <p>Getter for the field <code>timeout</code>.</p>
         *
         * @return the timeout
         */
        public int getTimeout()
        {
            return timeout;
        }

        /**
         * <p>Getter for the field <code>outputStream</code>.</p>
         *
         * @return a {@link java.io.OutputStream} object.
         */
        public OutputStream getOutputStream()
        {
            return outputStream;
        }

        /**
         * <p>Setter for the field <code>outputStream</code>.</p>
         *
         * @param outputStream a {@link java.io.OutputStream} object.
         */
        public void setOutputStream(OutputStream outputStream)
        {
            this.outputStream = outputStream;
        }

        /**
         * <p>Getter for the field <code>postDictionary</code>.</p>
         *
         * @return a {@link java.util.Dictionary} object.
         */
        public Dictionary<String, String> getPostDictionary()
        {
            return postDictionary;
        }

        /**
         * <p>Getter for the field <code>fileDictionary</code>.</p>
         *
         * @return a {@link java.util.Dictionary} object.
         */
        public Dictionary<String, File> getFileDictionary()
        {
            return fileDictionary;
        }

        /**
         * <p>Getter for the field <code>charset</code>.</p>
         *
         * @return a {@link java.lang.String} object.
         */
        public String getCharset()
        {
            return charset == null ? "utf-8" : charset;
        }

        /**
         * <p>Setter for the field <code>charset</code>.</p>
         *
         * @param charset a {@link java.lang.String} object.
         */
        public void setCharset(String charset)
        {
            this.charset = charset;
        }

        /**
         * <p>Getter for the field <code>connectTimeout</code>.</p>
         *
         * @return the connectTimeout
         */
        public int getConnectTimeout()
        {
            return connectTimeout;
        }

        /**
         * <p>Setter for the field <code>connectTimeout</code>.</p>
         *
         * @param connectTimeout the connectTimeout to set
         */
        public void setConnectTimeout(int connectTimeout)
        {
            this.connectTimeout = connectTimeout;
        }

        /**
         * <p>Getter for the field <code>readTimeout</code>.</p>
         *
         * @return the readTimeout
         */
        public int getReadTimeout()
        {
            return readTimeout;
        }

        /**
         * <p>Setter for the field <code>readTimeout</code>.</p>
         *
         * @param readTimeout the readTimeout to set
         */
        public void setReadTimeout(int readTimeout)
        {
            this.readTimeout = readTimeout;
        }
	

    }
}
