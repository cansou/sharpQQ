using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
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

namespace WpfApplication2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded+=MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Thread.Sleep(4000);
            throw new ApplicationException("========");
            
            //tab1.AddHandler(CheckBox.CheckedEvent, new RoutedEventHandler(onclick));
            //MessageBox.Show("hello boy!");
        }

        private void onclick(object sender, RoutedEventArgs e)
        {
            tab1.IsSelected = true;
            MessageBox.Show("hello boy!");

            foreach(Window win in Application.Current.Windows){
                MessageBox.Show(win.Title);
            }
            Assembly assembly = Assembly.GetExecutingAssembly();
            String resourceName = assembly.GetName().Name + ".g";
            ResourceManager rm = new ResourceManager(resourceName, assembly);
            ResourceSet rs = rm.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            foreach (DictionaryEntry entry in rs)
            {
                MessageBox.Show(entry.Key.ToString()+"        "+entry.Value);
            }
            rs.Close();

        }
    }
}
