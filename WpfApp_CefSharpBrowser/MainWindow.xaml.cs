using CefSharp;
using System.Windows;

namespace WpfApp_CefSharpBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Browser_Sample_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                // You can inject JS that you want.
                Browser_Sample.ExecuteScriptAsync(@"
                    document.addEventListener('click', function(e) {
                        var parent = e.target.parentElement;

                        // run some validation with if(){..}
                        // some more javascript

                        CefSharp.PostMessage(parent.outerHTML);
                    }, false);
                ");
            }
        }

        private void Browser_Sample_JavascriptMessageReceived(object sender, JavascriptMessageReceivedEventArgs e)
        {
            if (e.Message != null)
            {
                // You can get data from your injected JS on frameloadend event.
                // Extract data from e.Message.toString() and use delegates/callbacks/Invokes 
                // to reference the main UI thread for updating the necessary controls.
                MessageBox.Show($"Well done! Clicked item data is : {e.Message}");
            }
        }
    }
}
