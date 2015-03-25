using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;
using System.Globalization;
using System.Web;

namespace QQWpfApplication1.action
{
    class QQEncryptor
    {


        private static MSScriptControl.ScriptControlClass engine;
        /**
         * 登录邮箱时用到的，auth_token
         * 
         * @param str
         *            a {@link java.lang.String} object.
         * @return a long.
         */
        public static long time33(String str)
        {
            long hash = 0;
            for (int i = 0, Length = str.Length; i < Length; i++)
            {
                hash = hash * 33 + long.Parse(str.Substring(i,1));
            }
            return hash % 4294967296L;
        }


        /**
         * 
         * 计算登录时密码HASH值
         * 
         * @param uin
         *            a long.
         * @param plain
         *            a {@link java.lang.String} object.
         * @param verify
         *            a {@link java.lang.String} object.
         * @return a {@link java.lang.String} object.
         */
        public static String encrypt(long uin, String plain, String verify)
        {
            byte[] data = concat(md5(Encoding.UTF8.GetBytes(plain)), long2bytes(uin));
            String code = byte2HexString(md5(data));
            data = md5(Encoding.UTF8.GetBytes((code + verify.ToUpper())));
            return byte2HexString(data);
        }

        public static String encryptQm(long uin, String password, String verify)
        {
            String su = "";
            initScriptEngine();
            if (engine != null)
            {
                    // ";º ·"
                    String byte2Hex= byte2HexString(long2bytes(uin));
                    su = (String)engine.Eval("getPassword('" + password + "','" + byte2Hex.ToUpper() + "','" + verify + "')");
            }
            return su;
        }
        public static String getHash(String uin, String ptwebqq)
        {
            String su = "";
            initScriptEngine();
            if (engine != null)
            {
                    // ";º ·"
                    su = (String)engine.Eval("getHash('" + uin + "','" + ptwebqq + "')");
            }
            return su;

        }
        private static byte[] concat(byte[] bytes1, byte[] bytes2)
        {
            byte[] big = new byte[bytes1.Length + bytes2.Length];

            Array.Copy(bytes1, 0, big, 0, bytes1.Length);
            Array.Copy(bytes2, 0, big, bytes1.Length, bytes2.Length);
            return big;
        }

        /**
         * 计算一个字节数组的Md5值
         * 
         */
        private static byte[] md5(byte[] bytes)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = null;
                result = md5.ComputeHash(bytes);
            return result;
        }

        /**
         * 把字节数组转换为16进制表示的字符串
         * 
         */
        private static String byte2HexString(byte[] b) {
		StringBuilder sb = new StringBuilder();
		char[] hex = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
		if (b == null)
			return "null";

		int offset = 0;
		int len = b.Length;

		// 检查索引范围
		int end = offset + len;
		if (end > b.Length)
			end = b.Length;

		sb.Remove(0, sb.Length);
		for (int i = offset; i < end; i++) {
			sb.Append(hex[(b[i] & 0xF0) >> 4]).Append(hex[b[i] & 0xF]);
		}
		return sb.ToString();
	}

        /**
         * 计算GTK(gtk啥东东？)
         * 
         * @param skey
         *            a {@link java.lang.String} object.
         * @return a {@link java.lang.String} object.
         */
        public static String gtk(String skey)
        {
            int hash = 5381;
            for (int i = 0; i < skey.Length; i++)
            {
                hash += (hash << 5) + int.Parse(skey.Substring(i,1));
            }
            return (hash & 0x7fffffff)+"";
        }

        /**
         * 把整形数转换为字节数组
         * 
         * @param i
         *            a long.
         * @return an array of byte.
         */
        public static byte[] long2bytes(long i)
        {
            byte[] b = new byte[8];
            for (int m = 0; m < 8; m++, i >>= 8)
            {
                b[7 - m] = (byte)(i & 0x000000FF); // 奇怪, 在C# 整型数是低字节在前 byte[]
                // bytes =
                // BitConverter.GetBytes(i);
                // 而在JAVA里，是高字节在前
            }
            return b;
        }

        /**
         * 把一个16进制字符串转换为字节数组，字符串没有空格，所以每两个字符 一个字节
         * 
         * @param s
         *            a {@link java.lang.String} object.
         * @return an array of byte.
         */
        public static byte[] hexString2Byte(String s) {
		int len = s.Length;
		byte[] ret = new byte[len >> 1];
		for (int i = 0; i <= len - 2; i += 2) {
			ret[i >> 1] = (byte) (int.Parse(s.Substring(i, i + 2).Trim(), NumberStyles.AllowHexSpecifier) & 0xFF);
		}
		return ret;
	}

        private static MSScriptControl.ScriptControlClass initScriptEngine()
        {
            if (engine != null) return engine;
            engine = new MSScriptControl.ScriptControlClass();
            engine.Language = "javascript";
                string jsStr = "";

                FileInfo file = new FileInfo("C:\\Users\\leegean\\documents\\visual studio 2013\\Projects\\QQWpfApplication1\\WpfApplication1\\qq.js");
                if (file.Exists)
                {
                    StreamReader reader = file.OpenText();
                    jsStr = reader.ReadToEnd();
                    reader.Close();
                }
                engine.Eval(jsStr);
                return engine;
        }

        /**
         * 把Unicode编码转换为汉字
         * 
         * @param source
         * @return
         */
        public static String convertUnicodeToChar(String source)
        {
            if (null == source || " ".Equals(source))
            {
                return source;
            }

            StringBuilder sb = new StringBuilder();
            int i = 0;
            while (i < source.Length)
            {
                if (source.ToCharArray()[i] == '\\')
                {
                    int j = int.Parse(source.Substring(i + 2, i + 6), NumberStyles.AllowHexSpecifier);
                    sb.Append((char)j);
                    i += 6;
                }
                else
                {
                    sb.Append(source.ToCharArray()[i]);
                    i++;
                }
            }
            return sb.ToString();
        }

        public static String utf8_to_b64(String str)
        {
                return Convert.ToBase64String(HttpUtility.UrlEncodeToBytes(str, Encoding.UTF8));
        }

        public static String _siteId(String str)
        {
            String su = "";
                    su = (String)engine.Eval("_siteId('" + str + "')");
            return su;
        }

        public static String talkMsg(String str)
        {

            String su = "";
                    su = (String)engine.Eval("talkMsg('" + str + "')");
            return su;
        }
        public static String getBkn(String skey)
        {

            String su = "";
                    su = (String)engine.Eval("Encryption.getBkn('" + skey + "')");
            return su;
        }

    }

//    public class JSEvaluator
//    {
//        public static int EvalToInteger(string statement)
//        {
//            string s = EvalToString(statement);
//            return int.Parse(s.ToString());
//        }

//        public static double EvalToDouble(string statement)
//        {
//            string s = EvalToString(statement);
//            return double.Parse(s);
//        }

//        public static string EvalToString(string statement)
//        {
//            object o = EvalToObject(statement);
//            return o.ToString();
//        }


//        // current version with JScriptCodeProvider BEGIN  
//        ///*  

//        public static object EvalToObject(string statement)
//        {
//            return _EvaluatorType.InvokeMember(
//                  "Eval",
//                  BindingFlags.InvokeMethod,
//                  null,
//                  _Evaluator,
//                  new object[] { statement }
//                 );
//        }

//        static JSEvaluator()
//        {
//            JScriptCodeProvider compiler = new JScriptCodeProvider();

//            CompilerParameters parameters;
//            parameters = new CompilerParameters();
//            parameters.GenerateInMemory = true;

//            CompilerResults results;
//            results = compiler.CompileAssemblyFromSource(
//                                            parameters, _jscriptSource);

//            Assembly assembly = results.CompiledAssembly;
//            _EvaluatorType = assembly.GetType("JSEvaluator.JSEvaluator");

//            _Evaluator = Activator.CreateInstance(_EvaluatorType);
//        }

//        private static object _Evaluator = null;
//        private static Type _EvaluatorType = null;
//        private static readonly string _jscriptSource =
//          @"package JSEvaluator 
//      { 
//         class JSEvaluator 
//         { 
//          public function Eval(expr : String) : Object 
//          { 
//           return Eval(expr); 
//          } 
//         } 
//      }";

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
    //}  
}
