using CefSharp;
using CefSharp.WinForms;
using System;
using System.Windows.Forms;

namespace FormsApp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            if (!Cef.IsInitialized)
            {
                var setting = new CefSettings()
                {
                    Locale = "zh-CN",
                    AcceptLanguageList = "zh-CN",
                    MultiThreadedMessageLoop = true
                };
                //开启媒体流
                setting.CefCommandLineArgs.Add("enable-media-stream", "enable-media-stream");
                var bInit = Cef.Initialize(setting);
                if (bInit == false)
                {
                    MessageBox.Show("可加载异常");
                    Application.Exit();
                }
            }
            this.panel1.BackColor = System.Drawing.Color.Red;
        }

        public static ChromiumWebBrowser browser;

        private void button1_Click(object sender, EventArgs e)
        {
            browser.Load(this.textBox1.Text);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //this.panel1.Controls.Add(this.label1);
            //this.panel1.Controls.Add(this.textBox1);
            //this.panel1.Controls.Add(this.button1);
            this.AcceptButton = button1;
            try
            {
                browser = new ChromiumWebBrowser();
                this.panel1.Controls.Clear();
                this.panel1.Controls.Add(browser);
                browser = new ChromiumWebBrowser("http://localhost:9000/home/index")
                {
                    Dock = DockStyle.Fill,
                };
                CefSharpSettings.LegacyJavascriptBindingEnabled = true;
                browser.RegisterJsObject("WebReqWF", new WebReqWF());
                if (this.panel1.Controls.Contains(browser))
                    this.panel1.Controls.Remove(browser);
                this.panel1.Controls.Clear();
                this.panel1.Controls.Add(browser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
    }

    public interface IWebReqWF
    {
        string Load();
        void Hello();
    }

    public class WebReqWF : IWebReqWF
    {
        public string Load()
        {
            return "";
        }

        public void Hello()
        {
            MessageBox.Show("你好");
        }
    }
}
