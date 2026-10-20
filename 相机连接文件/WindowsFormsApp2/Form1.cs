using HalconDotNet;
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
using H = HalconDotNet.HOperatorSet;
namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        HTuple windowdle = null;//
        HObject image = null;
        bool open_or_close = false;
        HTuple cameraHandle = null;
        HTuple width = 0, height = 0;
        public Form1()
        {
            InitializeComponent();
            //允许跨线程调用
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        //窗体加载事件
        private void Form1_Load(object sender, EventArgs e)
        {
            //获取窗体句柄
            windowdle = hWindowControl1.HalconWindow;
        }

       
        //打开相机
        private void bt_open_Click(object sender, EventArgs e)
        {
            if(!open_or_close)
            {
                //new Thread(() => OpenCamera()) { IsBackground =true};
                new Thread(() => OpenCamera()) { IsBackground = true }.Start();
            }

        }

        void OpenCamera()
        {
            if(open_or_close)
            {
                MessageBox.Show(this, "相机已经打开", "温馨提示");
                return;
            }
            else
            {


                H.OpenFramegrabber("DirectShow(1)", 1, 1, 0, 0, 0, 0, "default", 8, "rgb", -1, "false", "default", "1", 0, -1, out cameraHandle);
                //H.OpenFramegrabber("MVision", 1, 1, 0, 0, 0, 0, "progressive", 8, "default", -1, "false", "auto", "default", 0, -1, out cameraHandle);
                H.GrabImageStart(cameraHandle, -1);
                open_or_close = true;

                while(open_or_close)
                {
                    //进行异步抓图
                    H.GrabImageAsync(out image,cameraHandle,-1);
                    if (width == 0 && height == 0)
                    {
                        H.GetImageSize(image, out width, out height);
                        hWindowControl1.ImagePart=new Rectangle(0,0,width-1,height-1);
                    }
                    H.DispImage(image,windowdle);
                }
            }
        }



        private void read_image_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string filepath = dialog.FileName;
                    if (!string.IsNullOrEmpty(filepath))
                    {
                        H.ReadImage(out image, filepath);
                        H.GetImageSize(image, out width, out height);
                        hWindowControl1.ImagePart = new Rectangle(0, 0, width - 1, height); // 图片右侧被截断，补全为 height 及闭合括号
                        H.DispObj(image, windowdle);
                    }
                }
            }
        }

        private void algoma_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                new HDevelopExport().Action(image, windowdle);
            }
        }

        private void hWindowControl1_HMouseMove(object sender, HMouseEventArgs e)
        {

        }



        //关闭相机
        private void bt_close_Click(object sender, EventArgs e)
        {
            if(open_or_close)
            {
                open_or_close = false;
                Thread.Sleep(1000);
                H.CloseFramegrabber(cameraHandle);
                H.ClearWindow(windowdle);
            }
        }
    }
}
