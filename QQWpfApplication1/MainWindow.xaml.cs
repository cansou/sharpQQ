using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QQWpfApplication1.json;

namespace QQWpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
         private static System.Net.Http.Headers.HttpResponseHeaders headers;
        private static IEnumerator<KeyValuePair<string, IEnumerable<string>>> pair;
        private static IEnumerator<KeyValuePair<string, IEnumerable<string>>> enumerator;
        public MainWindow()
        {
            InitializeComponent();
        }
             public  void get(String url)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer.Add(new Cookie("name", "leegean", "/","baidu.com"));
            HttpClient httpClient = new HttpClient(handler);
            System.Net.Http.Headers.HttpRequestHeaders reqHeader = httpClient.DefaultRequestHeaders;
            reqHeader.Host = "baidu.com";
            // 创建一个异步GET请求，当请求返回时继续处理
            httpClient.GetAsync(url).ContinueWith(
                (requestTask) =>
                {
                    if (requestTask.IsFaulted)
                    {
                        Console.WriteLine(requestTask.Exception);
                    }
                    else
                    {
                        HttpResponseMessage response = requestTask.Result;
                        headers = response.Headers;
                        enumerator = headers.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            KeyValuePair<String, IEnumerable<String>> pair = enumerator.Current;
                            Console.WriteLine(pair.Key + "        " + pair.Value);
                        }
                        CookieCollection cookies = handler.CookieContainer.GetCookies(new Uri(url));

                        foreach (Cookie cookie in cookies)
                        {
                            Console.WriteLine(cookie.Name + "        " + cookie.Value);
                        }
                        
                        // 确认响应成功，否则抛出异常
                        if (response.IsSuccessStatusCode)
                        {
                            response.Content.ReadAsStringAsync().ContinueWith(
                                (readTask) => Console.WriteLine(readTask.Result.Substring(0, 100)));
                        }
                        else if (response.StatusCode != HttpStatusCode.OK)
                        {
                            Console.WriteLine(response.StatusCode +"      "+response.ReasonPhrase);
                        }
                    }
                    

                   
                });



            Console.WriteLine("Hit enter to exit...");
            //Console.ReadLine();
        }



        //解码gif图片
        public static void post(Dictionary<string, string> content)
        {

                        //设置必要参数
                            //示例API可以参考：http://dev.jiepang.com/doc/get/users/show
            var url = "http://apistore.baidu.com/astore/toolshttpproxysend";

                //设置HttpClientHandler的AutomaticDecompression
                var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
                //创建HttpClient（注意传入HttpClientHandler）
                using (var http = new HttpClient(handler))
                {
                    //使用FormUrlEncodedContent做HttpContent
                    var contentEncoded = new FormUrlEncodedContent(content);
                    //await异步等待回应
                    http.PostAsync(url, contentEncoded).ContinueWith((requestTask) =>
                    {

                         HttpResponseMessage response = requestTask.Result;
                    //response.EnsureSuccessStatusCode();
                    // 确认响应成功，否则抛出异常
                         if (response.IsSuccessStatusCode)
                         {
                                     response.Content.ReadAsStringAsync().ContinueWith(
                              (readTask) => Console.WriteLine(readTask.Result.Substring(0, 100)));

                         }
                        
                    });


                    //var response = await http.PostAsync(url, content);

                    ////await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）
                    //Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
         }

        public static HttpClientHandler handler { get; set; }

        private void onclick(object sender, RoutedEventArgs e)
        {
            get("http://apistore.baidu.com/microserviced/iplookups?ip=117.89.35.58");
            //get("http://www.baidu.com");
            Dictionary<String, String> data= new Dictionary<String, String> {
            {"reqMethod","GET"},
            {"reqUrl","http://apistore.baidu.com/microservice/iplookup"},
            {"reqUrlParams[0][key]","ip"},
            {"reqUrlParams[0][value]","117.89.35.58"},
            {"token","e82c0f5f0ef3383b271a957dc4f4b781}"}
            };
            //post(data);
            String str = "{\"retcode\":0,\"result\":{\"gmasklist\":[{\"gid\":1000,\"mask\":0},{\"gid\":1638195794,\"mask\":0},{\"gid\":321105219,\"mask\":0}], \"gnamelist\":[{\"flag\":16777217,\"name\":\"iQQ\",\"gid\":1638195794,\"code\":2357062609},{\"flag\":1048577,\"name\":\"iQQ核心开发区\",\"gid\":321105219,\"code\":640215156}],\"gmarklist\":[]}}";

            JSONTokener token = new JSONTokener(new StringReader(str));
            JSONObject json = new JSONObject(token);
            //Console.WriteLine(jsonO.optJSONObject("result").optJSONArray("gmasklist"));

            int retcode = json.getInt("retcode");
            if (retcode == 0)
            {
                // 处理好友列表
                JSONObject results = json.getJSONObject("result");
                JSONArray groupJsonList = results.getJSONArray("gnamelist");	// 群列表
                JSONArray groupMaskJsonList = results.getJSONArray("gmasklist");	//禁止接收群消息标志：正常 0， 接收不提醒 1， 完全屏蔽 2

                for (int i = 0; i < groupJsonList.length(); i++)
                {
                    JSONObject groupJson = groupJsonList.getJSONObject(i);
                    groupJson.getLong("gid");
                    groupJson.getLong("code");
                    groupJson.getInt("flag");
                    groupJson.getString("name");
                }

                for (int i = 0; i < groupMaskJsonList.length(); i++)
                {
                    JSONObject maskObj = groupMaskJsonList.getJSONObject(i);
                    long gid = maskObj.getLong("gid");
                    int mask = maskObj.getInt("mask");
                    Console.WriteLine(gid+"    "+mask);
                }
                //post();

            }
        }
    }


    
}
