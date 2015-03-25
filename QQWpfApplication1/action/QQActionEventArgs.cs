using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    class QQActionEventArgs
    {


        public class ProgressArgs
        {
            /**当前进度*/
            public long current;
            /**总的进度*/
            public long total;
            public String toString()
            {
                return "ProgressArgs [current=" + current + ", total=" + total + "]";
            }
        }

        public class CheckVerifyArgs
        {
            public int result;
            public String code;
            public long uin;
        }
	

    }
}
