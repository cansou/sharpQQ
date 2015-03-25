using Microsoft.JScript;
using Microsoft.JScript.Vsa;
using QQWpfApplication1.json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

           //Console.WriteLine((Type.FONT==(Type)Enum.Parse(typeof(Type), "1", true))+"");
           //Console.WriteLine((Enum.Parse(typeof(Type), "text", true)) + "");


           string jsStr =
   "( {'timeString':'Time is: ' + new Date(),'dateValue':new Date()} )";

           FileInfo file = new FileInfo("C:\\Users\\leegean\\documents\\visual studio 2013\\Projects\\QQWpfApplication1\\WpfApplication1\\qq.js");
           if (file.Exists)
           {
               StreamReader reader = file.OpenText();
               jsStr = reader.ReadToEnd();
               reader.Close();
           }
           //Microsoft.JScript.JSObject obj =
           //    (Microsoft.JScript.JSObject)JSEvaluator.EvalToObject(jsStr);
   //        MessageBox.Show(obj["timeString"].ToString());
           //MessageBox.Show(obj["dateValue"].ToString());
           //Microsoft.JScript.DateObject tmpV =
           //    (Microsoft.JScript.DateObject)obj["dateValue"];
           //DateTime dt =
           //    (DateTime)Microsoft.JScript.Convert.Coerce(tmpV, typeof(DateTime));
           //MessageBox.Show(dt.ToString());  

           MSScriptControl.ScriptControlClass scc = new MSScriptControl.ScriptControlClass();
           scc.Language = "javascript";
            scc.Eval(jsStr);
            Console.WriteLine(scc.Eval("getPassword('lj19861001','1002053815','qwer');").ToString());
        }
    }
    public class JSEvaluator
    {
        public static int EvalToInteger(string statement)
        {
            string s = EvalToString(statement);
            return int.Parse(s.ToString());
        }

        public static double EvalToDouble(string statement)
        {
            string s = EvalToString(statement);
            return double.Parse(s);
        }

        public static string EvalToString(string statement)
        {
            object o = EvalToObject(statement);
            return o.ToString();
        }


        // current version with JScriptCodeProvider BEGIN  
        ///*  

        public static object EvalToObject(string statement)
        {
            return _evaluatorType.InvokeMember(
                  "Eval",
                  BindingFlags.InvokeMethod,
                  null,
                  _evaluator,
                  new object[] { statement }
                 );
        }

        static JSEvaluator()
        {
            JScriptCodeProvider compiler = new JScriptCodeProvider();

            CompilerParameters parameters;
            parameters = new CompilerParameters();
            parameters.GenerateInMemory = true;

            CompilerResults results;
            results = compiler.CompileAssemblyFromSource(
                                            parameters, _jscriptSource);

            Assembly assembly = results.CompiledAssembly;
            _evaluatorType = assembly.GetType("JSEvaluator.JSEvaluator");

            _evaluator = Activator.CreateInstance(_evaluatorType);
        }

        private static object _evaluator = null;
        private static Type _evaluatorType = null;
        private static readonly string _jscriptSource =
          @"package JSEvaluator 
      { 
         class JSEvaluator 
         { 
          public function Eval(expr : String) : Object 
          { 
           return eval(expr); 
          } 
         } 
      }";

        //*/  
        // current version with JScriptCodeProvider END  


        // deprecated version with Vsa BEGIN  
        /* 
 
        public static Microsoft.JScript.Vsa.VsaEngine Engine = 
                      Microsoft.JScript.Vsa.VsaEngine.CreateEngine(); 
 
        public static object EvalToObject(string JScript) 
        { 
          object Result = null; 
          try 
          { 
            Result = Microsoft.JScript.Eval.JScriptEvaluate( 
                                                    JScript, Engine); 
          } 
          catch (Exception ex) 
          { 
            return ex.Message; 
          } 
          return Result; 
        } 
 
        */
        // deprecated version with Vsa END  
    }  
}
