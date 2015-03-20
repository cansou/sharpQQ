using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Regex p = new Regex("aa(bb)");
            MatchCollection mc = p.Matches("hhaabbhh", 0);
           Console.WriteLine( mc.Count);
           Match m = p.Match("hhaabbhh");
           if (m.Success)
           {

               Console.WriteLine(m.Groups[1].Value+"          "+new Random().NextDouble());
           }

           Dictionary<String, Object>  dic = new Dictionary<String, Object>();
           dic.Add("aa", "bb");
           Dictionary<String, Object>.KeyCollection.Enumerator enu =  dic.Keys.GetEnumerator();
           //if (enu.MoveNext())
           //{
           //    Console.WriteLine(enu.Current);

           //};
           Console.WriteLine(enu.Current);
           String str = "{\"retcode\":10,\"result\":{\"gmasklist\":[{\"gid\":1000,\"mask\":0},{\"gid\":1638195794,\"mask\":0},{\"gid\":321105219,\"mask\":0}], \"gnamelist\":[{\"flag\":16777217,\"name\":\"iQQ\",\"gid\":1638195794,\"code\":2357062609},{\"flag\":1048577,\"name\":\"iQQ核心开发区\",\"gid\":321105219,\"code\":640215156}],\"gmarklist\":[]}}";

           JSONTokener token = new JSONTokener(new StringReader(str));
           JSONObject json = new JSONObject(token);
           Console.WriteLine(json.ToString());

           Console.WriteLine((Type.FONT==(Type)Enum.Parse(typeof(Type), "1", true))+"");
           Console.WriteLine((Enum.Parse(typeof(Type), "text", true)) + "");
        }
    }
    public enum Type
    {
        /**字体*/
        FONT = 3,
        /** 文字*/
        TEXT,
        /**表情*/
        FACE,
        /**离线图片*/
        OFFPIC,
        /**群图片*/
        CFACE,
    }
}
