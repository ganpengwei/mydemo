using Nancy.Hosting.Self;
using System;
using System.Windows.Forms;

namespace FormsApp
{
    public partial class Form1 : Form
    {
        private static Form2 f2 = null;

        public Form1()
        {
            Run();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("a");
            f2 = new Form2();
            f2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            f2.Close();
        }

        private static NancyHost _host = null;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_host != null)
                {
                    MessageBox.Show("以启动无需再启动");
                    return;
                }
                int port = 9000;
                string url = string.Format("http://localhost:{0}", port);
                _host = new NancyHost(new Uri(url));
                _host.Start();
                //Process.Start(url);
                MessageBox.Show("启动成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("启动Nancy失败" + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("你确定要停止Nancy吗！", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                if (_host != null)
                {
                    _host.Stop();
                    _host.Dispose();
                    _host = null;
                }
                MessageBox.Show("停止成功");
            }
        }

        public static void Run()
        {
            /**
             * 当前用户是管理员的时候，直接启动应用程序
             * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
             */
            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员
            //如果不是管理员，则以管理员方式运行
            if (!principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                //创建启动对象
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                //设置启动动作,确保以管理员身份运行
                startInfo.Verb = "runas";
                try
                {
                    System.Diagnostics.Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                System.Environment.Exit(0);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("你确定要关闭吗！", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                if (_host != null)
                {
                    _host.Stop();
                    _host.Dispose();
                    _host = null;
                }
                e.Cancel = false;  //点击OK   
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗口加载成功后执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }
    }
}
