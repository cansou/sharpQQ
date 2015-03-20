using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace QQWpfApplication1.action
{
    class StringHelper
    {

        

	/**
	 * 转义HTML中特殊的字符
	 *
	 * @param html HTML 内容
	 * @return 结果字符串
	 */
	public static String qouteHtmlSpecialChars(String html)
	{
		if(html==null)	return null;
		String[] specialChars = { "&", "\"", "'", "<", ">"};
		String[] qouteChars = {"&amp;", "&quot;", "&apos;", "&lt;", "&gt;"};
		for(int i=0; i<specialChars.Length; i++){
			html = html.Replace(specialChars[i], qouteChars[i]);
		}
		return html;
	}
	
	/**
	 * 反转义HTML中特殊的字符
	 *
	 * @param html HTML 内容
	 * @return 结果字符串
	 */
	public static String unqouteHtmlSpecialChars(String html)
	{
		if(html==null)	return null;
		String[] specialChars = { "&", "\"", "'", "<", ">", " "};
		String[] qouteChars = {"&amp;", "&quot;", "&apos;", "&lt;", "&gt;", "&nbsp;"};
		for(int i=0; i<specialChars.Length; i++){
			html = html.Replace(qouteChars[i], specialChars[i]);
		}
		return html;
	}
	
	
	/**
	 * 去掉HTML标签
	 *
	 * @param html HTML 内容
	 * @return 去除HTML标签后的结果
	 */
	public static String stripHtmlSpecialChars(String html)
	{
		if(html==null)	return null;
		 html=html.Replace("</?[^>]+>",""); 
		 html=html.Replace("&nbsp;"," "); 
		 
		 return html;
	}
	
	
	/**
	 * 以一种简单的方式格式化字符串
	 * 如
	 * <pre>
	 * String s = StringHelper.format("{0} is {1}", "apple", "fruit");
	 * System.out.println(s);
	 * //输出  apple is fruit.
	 * </pre>
	 *
	 * @param pattern 需要进行格式化的字符串
	 * @param args 用于格式化的参数
	 * @return 结果字符串
	 */
	public static String format(String pattern, Object [] args)
	{
		for(int i=0; i<args.Length; i++) {
			pattern = pattern.Replace("{"+i+"}", HttpUtility.UrlEncode(args[i].ToString(), Encoding.UTF8));
		}
		return pattern;
	}
	
    }
}
