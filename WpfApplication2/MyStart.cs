using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xml;

namespace WpfApplication2
{
    class MyStart:Window
    {
        private static SplashScreen splashScreen;
    [STAThread]
        static void Main(){
            //MessageBox.Show("---------------");
            splashScreen = new SplashScreen("images/gif.jpg");
            //splashScreen.Show(true);
            splashScreen.Show(true);
            //splashScreen.Close(TimeSpan.FromSeconds(4));
            MyApp app = new MyApp();
            app.Ss = splashScreen;
            app.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(unhandledException);
            app.LoadCompleted += app_LoadCompleted;
            app.StartupUri = new System.Uri("Window1.xaml", System.UriKind.Relative);

            app.Run();
            //app.Run(new MyStart().LoadXamlResource());
            
        }
       public Window LoadXamlResource()
        {
            Title = "Load Xaml Resource";

            Uri uri = new Uri("pack://application:,,,/LoadXamlResource.xml");
            Stream stream = Application.GetResourceStream(uri).Stream;
            FrameworkElement el = XamlReader.Load(stream) as FrameworkElement;
            Content = el;
            Button btn = el.FindName("MyButton") as Button;
            if (btn != null)
                btn.Click += ButtonOnClick;
            return this;
        }
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("The button labeled '" +
                            (args.Source as Button).Content +
                            "' has been clicked");
        }
    static void app_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {
        Thread.Sleep(3000);
    }

        private static void unhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("exception:    " + e.Exception.Message);
        }

    }
}
