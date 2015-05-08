using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace WpfApplication2
{
    class MyApp : Application
    {
        SplashScreen ss;

        public SplashScreen Ss
        {
            get { return ss; }
            set { ss = value; }
        }
       protected  override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            e.Cancel = true;
            MessageBox.Show(e.ReasonSessionEnding+"");
        }
    }
}
