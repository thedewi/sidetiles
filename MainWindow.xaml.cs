using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using CefSharp;
using WpfAppBar;
using Path = System.IO.Path;

namespace SideTiles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _barred;

        public MainWindow()
        {
            InitializeComponent();
            ShowInTaskbar = false;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            foreach (var xamlFilename in GetXamlFilenames())
            {
                using (var stream = new FileStream(xamlFilename, FileMode.Open))
                {
                    var uiElement = (UIElement) XamlReader.Load(stream);
                    if (uiElement is IWebBrowser browser
                        && ReadAllTextOrDefault(xamlFilename + ".js") is string js)
                    {
                        browser.FrameLoadEnd += (sender, args) => browser.GetMainFrame().ExecuteJavaScriptAsync(js);
                    }

                    Grid.Children.Add(uiElement);
                }
            }
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            AppBarFunctions.SetAppBar(this, _barred ? ABEdge.None : ABEdge.Right);
            _barred = !_barred;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            AppBarFunctions.SetAppBar(this, ABEdge.None);
        }

        private static IEnumerable<string> GetXamlFilenames()
        {
            var dataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Assembly.GetExecutingAssembly().GetName().Name
            );
            if (!Directory.Exists(dataPath))
                yield break;
            foreach (var xamlFilename in Directory.GetFiles(dataPath, @"*.xaml").OrderBy(fn => fn))
                yield return xamlFilename;
        }

        private static string ReadAllTextOrDefault(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch
            {
                return null;
            }
        }

        private void TaskbarIcon_OnTrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}