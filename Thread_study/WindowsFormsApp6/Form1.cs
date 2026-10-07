using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        Thread thread = null;
        Action<object> act=null;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = button3.Enabled = false;
            act = fun;
        }
        //启动
        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button1.Enabled = false;
            button3.Enabled = true;
            thread = new Thread(fun);
            thread.IsBackground = true;
            thread.Start(1);
        }
        private void fun(object obj)
        {
            for (int i = 0; i<progressBar1.Maximum;i++)
            {
                progressBar1.Invoke(new Action(() => {

                    progressBar1.Step = 1;
                    progressBar1.PerformStep();

                }));
                
                Thread.Sleep(10);
            }
        }
        //暂停
        private void button2_Click(object sender, EventArgs e)
        {
            thread.Abort();
            progressBar1.Value = 0;
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            
        }
        //停止
        private void button3_Click(object sender, EventArgs e)
        {
            if(button3.Text=="停止")
            {
                button2.Enabled = false;
                thread.Suspend();
                button3.Text = "复位";
            }
            else
            {
                button2.Enabled = true;
                thread.Resume();
                button3.Text = "停止";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Enabled =button2.Enabled = button3.Enabled = false;
            //线程池调用
            //ThreadPool.QueueUserWorkItem(threadpool_func,1);
            //异步调用
            act.BeginInvoke(1, threadpool_func, 1);
        }

        private void threadpool_func(object obj)
        {
            for (int i = 0; i < progressBar2.Maximum; i++)
            {
                progressBar2.Invoke(new Action(() => {

                    progressBar2.Step = 1;
                    progressBar2.PerformStep();

                }));

                Thread.Sleep(10);
            }
        }
    }
}
