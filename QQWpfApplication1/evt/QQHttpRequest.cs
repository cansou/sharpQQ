using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QQWpfApplication1.action
{
   public class QQHttpRequest
    {

        /**
         * 请求的头部
         */
        private Dictionary<String, String> headerDictionary;

        /**
         * 请求的值集合
         */
        private Dictionary<String, String> postDictionary;

        /**
         * Get方式的值集合
         */
        private Dictionary<String, String> getDictionary;

        /**
         * 编码
         */
        private String charset;


private   string method;
private   string url;

public    QQHttpRequest(string method,string url)
    {
        // TODO: Complete member initialization
this.method = method;
this.url = url;

     this.headerDictionary = new Dictionary<String, String>();
            this.postDictionary = new Dictionary<String, String>();
            this.getDictionary = new Dictionary<String, String>();
    }

        public void addHeader(String key, String value)
        {
            this.headerDictionary.Add(key, value);
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
         * <p>Getter for the field <code>inputStream</code>.</p>
         *
         * @return the inputStream
         */

        public String getUrl()
        {
            if (this.getDictionary.Count > 0)
            {
                StringBuilder buffer = new StringBuilder(url);
                buffer.Append("?");
                Dictionary<String, String>.KeyCollection it = this.getDictionary.Keys;
                Encoding charset = Encoding.UTF8;
                foreach(String k in it)
                {
                    String key = k;
                    String value = this.getDictionary[key];
                    try
                    {
                        key = HttpUtility.UrlEncode(key, charset);
                        value = HttpUtility.UrlEncode(value == null ? "" : value, charset);
                        buffer.Append(key);
                        buffer.Append("=");
                        buffer.Append(value);
                            buffer.Append("&");
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                return buffer.ToString().Substring(0, buffer.Length-1);
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
         * <p>Getter for the field <code>postDictionary</code>.</p>
         *
         * @return a {@link java.util.Dictionary} object.
         */
        public Dictionary<String, String> getPostDictionary()
        {
            return postDictionary;
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

    }
}
