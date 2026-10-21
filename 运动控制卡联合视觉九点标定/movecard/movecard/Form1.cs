using HalconDotNet;
using MultiCardCS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MultiCardCS.MultiCardCS;
using static System.Windows.Forms.AxHost;
using H = HalconDotNet.HOperatorSet;


namespace movecard
{
    public partial class Form1 : Form
    {
        MultiCardCS.MultiCardCS card = new MultiCardCS.MultiCardCS();
        int ret = 0;
        //提取轴号
        int axis = 0;

        int val1 = 0;
        int val2 = 0;
        int val3 = 0;

        HTuple FileHandle1 = new HTuple(), SerializedItemHandle2=new HTuple();
        HTuple hv_FileHandle1 = new HTuple(), hv_HomMat2D1 = new HTuple(), hv_SerializedItemHandle2=new HTuple();
        HTuple qx =new HTuple(),qy=new HTuple();
        

        public struct Point5D
        {
            public double X;
            public double Y;
            public double Z;
            public double R;
            public double W;
            public double U;
            public double Xcent;
            public double Ycent;

        }

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            Thread thread = new Thread(monitor);
            thread.IsBackground = true;
            thread.Start();


        }

        private void button7_Click(object sender, EventArgs e)
        {

            ret = card.GA_Open(1, "192.168.0.2", 9000, "192.168.0.1", 9000);

            if (ret == 0)
            {
                MessageBox.Show("板卡打开成功");
            }
            else
            {
                MessageBox.Show("板卡打开失败");
            }
            ret += card.GA_Reset();
            InitLimits();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            close_card();
        }
        private void close_card()
        {
            ret = card.GA_Close();
            if (ret == 0)
            {
                MessageBox.Show("板卡关闭成功");
            }
            else
            {
                MessageBox.Show("板卡关闭失败");
            }
        }
        private void InitLimits()
        {
            int ret = 0;

            // 假设：X0接正限位，X1接负限位（需根据实际接线修改）
            // 1. 设置轴1正限位映射（通用输入IO，主卡0，X0对应索引0）
            ret = card.GA_SetHardLimP(1, 1, 0, 1);
            if (ret != 0) MessageBox.Show("正限位映射失败");

            // 2. 设置轴1负限位映射（通用输入IO，主卡0，X1对应索引1）
            ret = card.GA_SetHardLimN(1, 1, 0, 0);
            if (ret != 0) MessageBox.Show("负限位映射失败");

            // 3. 设置轴1限位触发电平 (假设使用常开NO传感器)
            // 参数：轴号, 正限位电平(0常开/1常闭), 负限位电平(0常开/1常闭)
            ret = card.GA_SetLmtSnsSingle(1, 0, 0);

            // 如果您的传感器是常闭(NC)，请改为：
            // ret = card.GA_SetLmtSnsSingle(1, 1, 1);

            // 4. 启用轴1的硬限位（-1表示正负限位同时启用）
            ret = card.GA_LmtsOn(1, -1);
            if (ret != 0) MessageBox.Show("硬限位使能失败");

            //Y
            ret = card.GA_SetHardLimP(2, 1, 0, 3);
            ret = card.GA_SetHardLimN(2, 1, 0, 2);
            ret = card.GA_SetLmtSnsSingle(2, 0, 0);
            ret = card.GA_LmtsOn(2, -1); //正负都有效

            //Z
            ret = card.GA_SetHardLimP(3, 1, 0, 5);
            ret = card.GA_SetHardLimN(3, 1, 0, 4);
            ret = card.GA_SetLmtSnsSingle(3, 0, 0);
            ret = card.GA_LmtsOn(3, -1);


            ret += card.GA_StepSns(6); //反向脉冲

            //常开设置低电平回原状态
            ret += card.GA_HomeSns(0);

            //建立坐标系
            Create_xyz();
        }

        private async void Jog_move(object sender, MouseEventArgs e)
        {
            Button bt = new Button();
            bt = sender as Button;

            await Task.Run(() =>
            {
                move(bt.Name);
            });


        }

        private void Jog_stop(object sender, MouseEventArgs e)
        {
            ret += card.GA_Stop(15, 15);
        }

        //运动方法
        private void move(string name)
        {
            //提取数字

            foreach (char c in name)
            {
                if (char.IsDigit(c))
                {
                    axis = c - '0';
                    break;
                }
            }
            if (axis == 0)
            {

                return;
            }

            double vel = 0;
            if (name.Contains("left") || name.Contains("y2_down") || name.Contains("z3_up"))
                vel = -200;
            else if (name.Contains("right") || name.Contains("y2_up") || name.Contains("z3_down"))
                vel = 200;


            TJogPrm tjp = new TJogPrm();
            tjp.dSmooth = 0;
            tjp.dAcc = 0.5;
            tjp.dDec = 0.5;

            ret += card.GA_PrfJog((short)axis);
            ret += card.GA_AxisOn((short)axis);
            ret += card.GA_SetJogPrm((short)axis, ref tjp);
            ret += card.GA_SetVel((short)axis, vel);
            ret += card.GA_Update(1 << axis - 1);



        }

        private void monitor()
        {

            //获取是他的实时规划位置和规划速度
            double px = 0, py = 0, pz = 0;
            double vx = 0, vy = 0, vz = 0;
            int clock = 0;

            while (true)
            {
                try
                {
                    card.GA_GetPrfPos(1, ref px, 1, ref clock);
                    card.GA_GetPrfVel(1, ref vx, 1, ref clock);
                    card.GA_GetPrfPos(2, ref py, 1, ref clock);
                    card.GA_GetPrfVel(2, ref vy, 1, ref clock);
                    card.GA_GetPrfPos(3, ref pz, 1, ref clock);
                    card.GA_GetPrfVel(3, ref vz, 1, ref clock);

                    // 三个标签用一次 BeginInvoke 批量更新，减少 UI 线程压力
                    this.BeginInvoke(new Action(() =>
                    {
                        lbl_x_current_pos.Text = Math.Round(PulseConvertMm(px), 3).ToString();
                        lbl_x_current_speed.Text = Math.Round(MmPerSConvertPulsePerMS(vx), 3).ToString();

                        lbl_y_current_pos.Text = Math.Round(PulseConvertMm(py), 3).ToString();
                        lbl_y_current_speed.Text = Math.Round(MmPerSConvertPulsePerMS(vy), 3).ToString();

                        lbl_z_current_pos.Text = Math.Round(PulseConvertMm(pz), 3).ToString();
                        lbl_z_current_speed.Text = Math.Round(MmPerSConvertPulsePerMS(vz), 3).ToString();
                    }));
                }
                catch { /* 板卡没开时忽略 */ }

                Thread.Sleep(30);   // 10ms 太密，UI 反而卡；30~50ms 就够实时了
            }

        }

        protected double PulseConvertMm(double pulse, double daocheng = 4, double perpulse = 6400)
        {
            double equivalent = Math.Round((daocheng) / perpulse, 5);
            double distance = Math.Round((pulse * equivalent), 5);
            return distance;
        }

        protected double MmPerSConvertPulsePerMS(double mm, double daocheng = 4, double perpulse = 6400)
        {
            double equivalent = Math.Round((daocheng) / perpulse, 5);
            double Rate = Math.Pow(10, 3);
            return Convert.ToDouble(Math.Round(mm / equivalent / Rate, 5));
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            try
            {
                await Task.Run(() =>
                {

                    for (int i = 1; i <= 3; i++)
                    {
                        Task.Run(() => homeSmart(i));
                        Thread.Sleep(100);
                    }


                });
            }
            finally
            {
                button1.Enabled = true;
            }



        }
        //创建坐标系
        private void Create_xyz()
        {
            TCrdPrm crdPrm;
            crdPrm.dimension = 3;//三维坐标

            crdPrm.profile = new short[32];//建立xyz轴的映射关系
            crdPrm.profile[0] = 1;//x轴
            crdPrm.profile[1] = 2;//y轴
            crdPrm.profile[2] = 3;//z轴

            crdPrm.synVelMax = 100;//最大合成速度100脉冲/毫秒
            crdPrm.synAccMax = 0.2;//最大合成加速度0.2脉冲/毫秒^2

            crdPrm.evenTime = 0;//最小匀速时间固定为0


            crdPrm.setOriginFlag = 1; //用户指定原点位置1

            crdPrm.originPos = new int[32];//用户指定原点位置
            crdPrm.originPos[0] = 0;
            crdPrm.originPos[1] = 0;
            crdPrm.originPos[2] = 0;

            ret = card.GA_SetCrdPrm(1, ref crdPrm);//建立坐标系
        }

        private void homeSmart(int axis)
        {
            card.GA_HomeStop((short)axis);
            //在原点上
            if (is_home(axis))
            {
                home_move(axis, 80, 12800);
            }
            //在负限位
            else if (is_homeN(axis))
            {
                home_move(axis, 80, 25000);
            }
            //在正限位
            else if (is_homeP(axis))
            {
                home_move(axis, 80, -96000);
            }
            //在之间
            else
            {
                home_move(axis, 80, -100000);
                if (is_homeN(axis))
                {
                    card.GA_Stop(0X0001 << (axis - 1), 0X0001 << (axis - 1));
                    home_move(axis, 80, 25000);
                }
                else if (is_home(axis))
                {
                    card.GA_Stop(0X0001 << (axis - 1), 0X0001 << (axis - 1));
                    home_move(axis, 80, 12800);
                }
            }

            //开始回原
            ret = card.GA_HomeStart((short)axis); //启动轴回零
            WaitHomeStsFish((short)axis);//等待回原完成
            short count = 0;
            card.GA_ZeroPos(3, count);
        }
        private bool WaitHomeStsFish(short axisname, int outtime = 3)
        {
            short nStatus = 0;
            short nAxisNum = (short)axisname;
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            do
            {
                card.GA_HomeGetSts(nAxisNum, ref nStatus);
                var bovertm = sw.ElapsedMilliseconds > outtime * 1000;
                if (bovertm)
                {
                    sw.Stop();
                    return false;
                }
                if (stop) { EmergencyStop(); return false; }
            }
            while (nStatus != 2);
            sw.Stop();
            return true;
        }

        private bool home_move(int axis, int vel, int pos)
        {
            short axis_temp = (short)axis;
            card.GA_AxisOn(axis_temp);
            TTrapPrm TrapPrm;
            TrapPrm.acc = 0.5;//设置点位运动加速度为 0.5 脉冲/毫秒^2
            TrapPrm.dec = 0.5;//设置点位运动减速度为 0.5 脉冲/毫秒^2
            TrapPrm.velStart = 0;//设置点位运动起始速度为 0 脉冲/毫秒
            TrapPrm.smoothTime = 0;//设置点位运动平滑时间为 0
            ret += card.GA_PrfTrap(axis_temp);
            ret += card.GA_SetTrapPrm(axis_temp, ref TrapPrm);
            ret += card.GA_SetVel(axis_temp, vel);

            double dprfpos = 0;
            int clock = 0;
            ret += card.GA_GetPrfPos(axis_temp, ref dprfpos, 1, ref clock);
            ret += card.GA_SetPos(axis_temp, (int)dprfpos + pos);


            ret += card.GA_Update((1 << (axis_temp - 1)));

            WaitMotorRunFish(axis_temp); //等待回原
            if (ret == 0) return true;
            else return false;

        }
        bool stop = false;//停止信号
        private bool WaitMotorRunFish(int axis_name, int outtime = 100)
        {
            int axisstatus = 0;
            int clock = 0;
            short index = (short)axis_name;
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            do
            {
                card.GA_GetSts(index, ref axisstatus, 1, ref clock);
                var bovertm = sw.ElapsedMilliseconds > outtime * 1000;
                if (bovertm)
                {
                    sw.Stop();
                    return false;
                }
                if (stop) { EmergencyStop(); return false; }
            }
            while ((axisstatus & MultiCardCS.MultiCardCS.AXIS_STATUS_ARRIVE) == 0);
            sw.Stop();
            return true;
        }
        public void EmergencyStop()
        {
            //MC_Stop停止一个或多个轴的规划运动，停止坐标系运动
            stop = true;
            card.GA_Stop(0XFFFFF, 0XFFFFF);//停止所有轴
            Thread.Sleep(1000);
            stop = false;
        }
        int val = 0;
        private bool is_home(int axis)
        {
            int val = 0;
            ret = card.GA_GetDiRaw(3, ref val);
            int temp = (int)Math.Pow(2, axis - 1);
            if ((temp & val) != 0)
            {
                return true;
            }

            return false;
        }
        private bool is_homeP(int axis)//正限位
        {
            int val = 0;
            ret = card.GA_GetDiRaw(0, ref val);
            int temp = (int)Math.Pow(2, axis - 1);
            if ((temp & val) != 0)
            {
                return true;
            }

            return false;
        }
        private bool is_homeN(int axis)//负限位
        {
            int val = 0;
            ret = card.GA_GetDiRaw(1, ref val);
            int temp = (int)Math.Pow(2, axis - 1);
            if ((temp & val) != 0)
            {
                return true;
            }

            return false;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                homeSmart(1);
            });
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                homeSmart(2);
            });
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                homeSmart(3);
            });
        }
        //添加行
        private void add_row_Click(object sender, EventArgs e)
        {
            // ... 获取实际行数用于编号 ...
            int actualRowCount = dataGridView1.Rows.Count;
            if (dataGridView1.AllowUserToAddRows)
            {
                actualRowCount--;
            }
            int newNumber = actualRowCount + 1;

            // 添加新行
            int rowIndex = dataGridView1.Rows.Add();

            // 给新行的“编号”列赋值
            dataGridView1.Rows[rowIndex].Cells["index"].Value = newNumber;
            dataGridView1.Rows[rowIndex].Cells["ColType"].Value = "直线插补";


            // 设置焦点到新行的“插补类型”列
            dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex].Cells["ColType"];
        }
        //删除行
        private void delete_row_Click(object sender, EventArgs e)
        {
            // 1. 检查是否有选中
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("请先选中要删除的单元格或行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. 确认删除
            if (MessageBox.Show("确定要删除选中的行吗？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            // 3. 收集需要删除的行号（使用 HashSet 去重）
            HashSet<int> rowIndexes = new HashSet<int>();
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                if (cell.RowIndex >= 0 && !dataGridView1.Rows[cell.RowIndex].IsNewRow)
                {
                    rowIndexes.Add(cell.RowIndex);
                }
            }

            // 4. 倒序删除行（从下往上删，防止索引错乱）
            var sortedIndexes = rowIndexes.OrderByDescending(i => i).ToList();
            foreach (int index in sortedIndexes)
            {
                dataGridView1.Rows.RemoveAt(index);
            }

            // 5. 【关键步骤】删除完成后，立即调用重新编号的方法！
            ReNumberRows();
        }
        /// <summary>
        /// 重新为第一列（编号）排序赋值，确保从 1 开始连续递增
        /// </summary>
        private void ReNumberRows()
        {
            int num = 1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // 排除掉底部的自动新增空行 (IsNewRow)
                if (!row.IsNewRow)
                {
                    // ⚠️ 注意：这里的 "编号" 必须与设计器里该列的 (Name) 属性完全一致
                    // 如果你之前把 (Name) 改成了 index，这里请改成 row.Cells["index"]
                    // 或者直接用索引：row.Cells[0].Value = num;
                    row.Cells["index"].Value = num;
                    num++;
                }
            }
        }
        //获取当前XY平面的点
        private void save_point_Click(object sender, EventArgs e)
        {
            // 1. 如果没有当前行，先添加一行
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                dataGridView1.Rows.Add();
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1];
            }

            DataGridViewRow currentRow = dataGridView1.CurrentRow;

            // 2. 获取坐标
            double x = ExtractNumber(lbl_x_current_pos.Text);
            double y = ExtractNumber(lbl_y_current_pos.Text);
            double z = ExtractNumber(lbl_z_current_pos.Text);

            try
            {
                // 3. 按顺序寻找空位填入
                if (currentRow.Cells[2].Value == null || string.IsNullOrEmpty(currentRow.Cells[2].Value.ToString()))
                {
                    // X1 为空，存入 X1, Y1
                    currentRow.Cells[2].Value = x;
                    currentRow.Cells[3].Value = y;
                    currentRow.Cells[8].Value = z;
                }
                else if (currentRow.Cells[4].Value == null || string.IsNullOrEmpty(currentRow.Cells[4].Value.ToString()))
                {
                    // X1 已存，X2 为空，存入 X2, Y2
                    currentRow.Cells[4].Value = x;
                    currentRow.Cells[5].Value = y;
                }
                else if (currentRow.Cells[6].Value == null || string.IsNullOrEmpty(currentRow.Cells[6].Value.ToString()))
                {
                    // X1, X2 已存，X3 为空，存入 X3, Y3
                    currentRow.Cells[6].Value = x;
                    currentRow.Cells[7].Value = y;
                }
                else
                {
                    // 4. 三个点全存满了，自动新建一行，存入 X1, Y1
                    int newRowIndex = dataGridView1.Rows.Add();
                    DataGridViewRow newRow = dataGridView1.Rows[newRowIndex];

                    newRow.Cells[1].Value = "直线插补";
                    newRow.Cells[2].Value = x;
                    newRow.Cells[3].Value = y;
                    newRow.Cells[8].Value = z;

                    ReNumberRows(); // 重新排编号
                    dataGridView1.CurrentCell = newRow.Cells[1];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message);
            }
        }
        // 从字符串中提取数字（支持负数和小数）
        private double ExtractNumber(string input)
        {
            if (string.IsNullOrEmpty(input)) return 0.0;

            // 匹配负号、数字和小数点
            Match match = Regex.Match(input, @"-?\d+(\.\d+)?");
            if (match.Success && double.TryParse(match.Value, out double result))
            {
                return result;
            }
            return 0.0;
        }
        //启动插补运动
        private void bt_start_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(new Action(() => { Run(); }), TaskCreationOptions.LongRunning);
        }
        //运行轨迹
        bool interplation_stop = default(bool);
        ManualResetEvent pause = new ManualResetEvent(true);
        private void Run()
        {
            int loop =0;
            //定义一个电集合
            List<Point5D> points = new List<Point5D>();
            //遍历data数据集
            foreach(DataGridViewRow item in dataGridView1.Rows)
            {
                if(interplation_stop) return;
                points.Clear();

                string typeStr = item.Cells[1].Value?.ToString();

                ////把点位添加到集合中去
                if (typeStr == "直线插补")
                {
                    dataGridView1.Rows[loop].Selected = false;
                    
                    // 解析点1
                    double.TryParse(item.Cells[2].Value?.ToString(), out double x1);
                    double.TryParse(item.Cells[3].Value?.ToString(), out double y1);
                    Point5D p1 = new Point5D { X = x1, Y = y1 };
                    points.Add(p1);

                    // 解析点2
                    double.TryParse(item.Cells[4].Value?.ToString(), out double x2);
                    double.TryParse(item.Cells[5].Value?.ToString(), out double y2);
                    Point5D p2 = new Point5D { X = x2, Y = y2 };
                    points.Add(p2);

                }
                else if (typeStr == "圆弧插补")
                {
                    dataGridView1.Rows[loop].Selected = false;
                    //把点位添加到集合中去
                    // 解析点1
                    double.TryParse(item.Cells[2].Value?.ToString(), out double x1);
                    double.TryParse(item.Cells[3].Value?.ToString(), out double y1);
                    Point5D p1 = new Point5D { X = x1, Y = y1 };
                    points.Add(p1);

                    // 解析点2
                    double.TryParse(item.Cells[4].Value?.ToString(), out double x2);
                    double.TryParse(item.Cells[5].Value?.ToString(), out double y2);
                    Point5D p2 = new Point5D { X = x2, Y = y2 };
                    points.Add(p2);

                    // 解析点3
                    double.TryParse(item.Cells[6].Value?.ToString(), out double x3);
                    double.TryParse(item.Cells[7].Value?.ToString(), out double y3);
                    Point5D p3 = new Point5D { X = x3, Y = y3 };
                    points.Add(p3);
                }
                dataGridView1.Rows[loop == 0 ? loop : loop - 1].DefaultCellStyle.BackColor = Color.White;
                dataGridView1.Rows[loop].DefaultCellStyle.BackColor = Color.Red;
                loop++;
                pause.WaitOne();//暂停一下
                //执行插补运动
                if (typeStr == "直线插补")
                {
                    pause.WaitOne();

                    //先运动z轴位置
                    double.TryParse(item.Cells[8].Value?.ToString(), out double z);
                    //home_move(3, 50, (int)MmConvertPulse(z));

                    //执行xy轴插补运动
                    LineInterpolation(points, true);
                    pause.WaitOne();

                }
                else if(typeStr == "圆弧插补")
                {
                    pause.WaitOne();

                    //先运动z轴位置
                    double.TryParse(item.Cells[8].Value?.ToString(), out double z);
                    //home_move(3, 50, (int)MmConvertPulse(z));

                    //直线返回原点
                    //back_arc(points, true);
                    //执行xy轴插补运动
                    ArcInterpolation(points, true);
                    pause.WaitOne();

                }
            }
        }
        private bool back_arc(List<Point5D> points, bool iswait = false)
        {
            //清除插补数据
            Create_xyz();
            ret = card.GA_CrdClear(1, 0);
            int segNum = 0;

            card.GA_AxisOn(1);
            card.GA_AxisOn(2);
            //两轴直线插补

            double vel = 50;
            double acc = 0.5;

            ret = card.GA_LnXY(1, (int)MmConvertPulse(points[2].X), (int)MmConvertPulse(points[2].Y), vel, acc, 0, 0, segNum);
            segNum++;
            ret = card.GA_LnXY(1, (int)MmConvertPulse(points[0].X), (int)MmConvertPulse(points[0].Y), vel, acc, 0, 0, segNum);
            segNum++;

            //将数据全部压入
            ret = card.GA_CrdData(1, 0, 0);
            ret = card.GA_CrdStart(1, 0);

            bool ret1 = false;
            ret1 = WaitCoordRunFinish(iswait);
            if (ret1 == true) return true;
            else return false;
        }
        private bool LineInterpolation(List<Point5D> points,bool iswait=false)
        {
            //清除插补数据
            Create_xyz();
            ret = card.GA_CrdClear(1, 0);
            int segNum = 0;

            card.GA_AxisOn(1);
            card.GA_AxisOn(2);
            //两轴直线插补

            foreach (Point5D point in points)
                {
                    double vel = 50;
                    double acc = 0.5;
                int pulse_x = (int)MmConvertPulse(point.X);
                int pulse_y = (int)MmConvertPulse(point.Y);


                    ret = card.GA_LnXY(1,pulse_x,pulse_y,vel,acc,0,0,segNum);
                    segNum++;
                }

            //将数据全部压入
            ret = card.GA_CrdData(1, 0, 0);
            ret = card.GA_CrdStart(1, 0);

            bool ret1 = false;
            ret1 = WaitCoordRunFinish(iswait);
            if (ret1 == true) return true;
            else return false;
        }

        private bool ArcInterpolation(List<Point5D> points, bool iswait = false)
        {
            // ===== 1. 读取当前位置，判断从哪头开始画弧 =====
            double curX = 0, curY = 0;
            int pclock = 0;
            card.GA_GetPrfPos(1, ref curX, 1, ref pclock);
            card.GA_GetPrfPos(2, ref curY, 1, ref pclock);
            double curXmm = PulseConvertMm(curX);
            double curYmm = PulseConvertMm(curY);

            // 计算当前点到 P0 和 P2 的距离
            double distToP0 = Math.Sqrt(Math.Pow(curXmm - points[0].X, 2) + Math.Pow(curYmm - points[0].Y, 2));
            double distToP2 = Math.Sqrt(Math.Pow(curXmm - points[2].X, 2) + Math.Pow(curYmm - points[2].Y, 2));

            Point5D startPt, midPt, endPt;

            // 容差设为 0.2mm
            if (distToP0 < 0.2)
            {
                // 情况A：当前在 P0，正向走 P0 -> P1 -> P2
                startPt = points[0];
                midPt = points[1];
                endPt = points[2];
            }
            else if (distToP2 < 0.2)
            {
                // 情况B：当前在 P2，反向走 P2 -> P1 -> P0
                startPt = points[2];
                midPt = points[1];
                endPt = points[0];
            }
            else
            {
                // 情况C：当前位置不在两端，先直线走到比较近的那个点
                Point5D targetPt = (distToP0 < distToP2) ? points[0] : points[2];
                List<Point5D> targetList = new List<Point5D> { targetPt };

                if (!LineInterpolation(targetList, true))
                    return false;

                // 走到之后，重新分配起点和终点
                if (targetPt.X == points[0].X && targetPt.Y == points[0].Y)
                {
                    startPt = points[0];
                    midPt = points[1];
                    endPt = points[2];
                }
                else
                {
                    startPt = points[2];
                    midPt = points[1];
                    endPt = points[0];
                }
            }

            // ===== 2. 计算圆心与方向 =====
            // 圆心由三点决定，顺序无所谓
            var xy = CalculateCircleCenter(points[0].X, points[0].Y, points[1].X, points[1].Y, points[2].X, points[2].Y);

            // 动态叉积：向量 (中-起) 和 (终-起)
            double cross = (midPt.X - startPt.X) * (endPt.Y - startPt.Y)
                         - (midPt.Y - startPt.Y) * (endPt.X - startPt.X);

            if (Math.Abs(cross) < 1e-9)
            {
                this.BeginInvoke(new Action(() => MessageBox.Show("三点共线，无法圆弧插补")));
                return false;
            }

            // 0/1 方向根据你之前的实测结果保持不变
            short circleDir = cross > 0 ? (short)0 : (short)1;

            // ===== 3. 下发插补数据 =====
            Create_xyz();
            ret = card.GA_CrdClear(1, 0);
            int segNum = 0;

            card.GA_AxisOn(1);
            card.GA_AxisOn(2);

            int xpos = (int)MmConvertPulse(endPt.X); // 终点坐标（绝对）
            int ypos = (int)MmConvertPulse(endPt.Y);

            // 【核心修改】：计算圆心相对于起点的偏移量
            double centerOffsetX = xy.x - startPt.X;
            double centerOffsetY = xy.y - startPt.Y;

            int xcenter = (int)MmConvertPulse(centerOffsetX); // 相对增量
            int ycenter = (int)MmConvertPulse(centerOffsetY);

            // 准备下发指令
            ret = card.GA_ArcXYC(1, xpos, ypos, xcenter, ycenter, circleDir, 50, 0.5, 0, 0, segNum);
            segNum++;

            if (ret != 0)
            {
                this.BeginInvoke(new Action(() => MessageBox.Show("圆弧指令下发失败，错误码：" + ret)));
                return false;
            }

            ret = card.GA_CrdData(1, 0, 0);
            ret = card.GA_CrdStart(1, 0);

            return WaitCoordRunFinish(iswait);
        }
        (double x, double y) CalculateCircleCenter(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            // 检查三点是否共线
            if (Math.Abs((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1)) < 1e-10)
            {
                throw new ArgumentException("三点共线，无法形成圆");
            }

            // 计算垂直平分线的交点（圆心）
            double cx, cy;

            if (Math.Abs(y2 - y1) < 1e-10) // P1P2水平
            {
                double m2 = -(x3 - x2) / (double)(y3 - y2);
                double b2 = (y2 + y3) / 2 - m2 * (x2 + x3) / 2;
                cx = (x1 + x2) / 2.0;
                cy = m2 * cx + b2;
            }
            else if (Math.Abs(y3 - y2) < 1e-10) // P2P3水平
            {
                double m1 = -(x2 - x1) / (double)(y2 - y1);
                double b1 = (y1 + y2) / 2 - m1 * (x1 + x2) / 2;
                cx = (x2 + x3) / 2.0;
                cy = m1 * cx + b1;
            }
            else
            {
                // 通用计算公式
                double m1 = -(x2 - x1) / (double)(y2 - y1);
                double m2 = -(x3 - x2) / (double)(y3 - y2);

                double b1 = (y1 + y2) / 2 - m1 * (x1 + x2) / 2;
                double b2 = (y2 + y3) / 2 - m2 * (x2 + x3) / 2;

                cx = (b2 - b1) / (m1 - m2);
                cy = m1 * cx + b1;
            }

            return (cx, cy);
        }
        protected double MmConvertPulse(double mm, double daocheng = 4, double perpulse = 6400)
        {
            double equivalent = Math.Round(daocheng / perpulse, 5); // mm/脉冲
            return Math.Round(mm / equivalent); // 脉冲
        }
        /// <summary>
        /// 等待坐标系完成 可等待
        /// </summary>
        /// <returns></returns>
        private bool WaitCoordRunFinish(bool iswait)
        {
            int ret = 0;
            short prun = 0;
            int seg = 0;
            short fifoinfo = 0;
            if (iswait)
            {
                do
                {
                    ret = card.GA_CrdStatus(1, ref prun, ref seg, fifoinfo);
                    Thread.Sleep(1);
                    if (stop) return false;
                }
                while ((prun & MultiCardCS.MultiCardCS.CRDSYS_STATUS_FIFO_FINISH_0) != MultiCardCS.MultiCardCS.CRDSYS_STATUS_FIFO_FINISH_0);
                return true;
            }
            else
            {
                ret = card.GA_CrdStatus(1, ref prun, ref seg, fifoinfo);
                if ((prun & MultiCardCS.MultiCardCS.CRDSYS_STATUS_FIFO_FINISH_0) == MultiCardCS.MultiCardCS.CRDSYS_STATUS_FIFO_FINISH_0) { return true; }
                else return false;
            }
        }
        //停止插补运动
        private void bt_stop_Click(object sender, EventArgs e)
        {
            card.GA_Stop(0xFFFF, 0xFFFF);
        }
        //保存九个点
        double[] Row = new double[9];//y
        double[] Col = new double[9];//x
        //进行点位映射
        private async void to_qpoint_Click(object sender, EventArgs e)
        {
            await Task.Run(()=>point_mapping(372, 114));
        }
        //保存拍照位置
        private void button5_Click(object sender, EventArgs e)
        {
            double x1 = ExtractNumber(lbl_x_current_pos.Text);
            double y1 = ExtractNumber(lbl_y_current_pos.Text);

            string path = Path.Combine(Application.StartupPath, "CapturePoint.bin");

            try
            {
                using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                using (var bw = new BinaryWriter(fs))
                {
                    bw.Write(x1);
                    bw.Write(y1);
                    bw.Write(DateTime.Now.ToBinary());
                }
                // 保存成功提示
                MessageBox.Show($"拍照位置保存成功！\nX = {x1:F3}\nY = {y1:F3}",
                                "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败：" + ex.Message);
            }

        }
        //读取拍照位置
        double camera_x1 = 0;
        double camera_y1 = 0;
        private void button6_Click(object sender, EventArgs e)
        {
           
            DateTime captureTime; // 声明时间变量

            string path = Path.Combine(Application.StartupPath, "CapturePoint.bin");

            if (!File.Exists(path))
            {
                MessageBox.Show("还没有保存过拍照位置。");
                return;
            }

            try
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (var br = new BinaryReader(fs))
                {
                    if (fs.Length < 24)
                        throw new InvalidDataException("点位文件数据不完整。");

                    camera_x1 = br.ReadDouble();
                    camera_y1 = br.ReadDouble();
                    // 时间戳可选读，这里跳过
                    // DateTime t = DateTime.FromBinary(br.ReadInt64());
                    // 读取时间戳并赋值给 captureTime
                    captureTime = DateTime.FromBinary(br.ReadInt64());
                }

                

                // 读取成功提示（现在 captureTime 有值了，不会报错）
                MessageBox.Show($"拍照位置读取成功！\nX = {camera_x1:F3}\nY = {camera_y1:F3}\n保存时间：{captureTime:yyyy-MM-dd HH:mm:ss}",
                                "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // 读取成功提示


                // 后续直接用 x1 / y1 即可
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取失败：" + ex.Message);
            }
        }

        private void point_mapping(HTuple ypos, HTuple xpos)
        {
            H.AffineTransPoint2d(hv_HomMat2D1, xpos, ypos, out qx, out qy);
            //调用直线插补运动到对应的点位上去；
            //获取当前位子
            List<Point5D> affnic_points = new List<Point5D>();
            //// 解析点1
            double x1=ExtractNumber(lbl_x_current_pos.Text);
            double y1= ExtractNumber(lbl_y_current_pos.Text);
            Point5D p1 = new Point5D { X = x1, Y = y1 };
            affnic_points.Add(p1);

            // 解析点2
            double x2 = qx.D;
            double y2 = qy.D;
            Point5D p2 = new Point5D { X = x2, Y = y2 };
            affnic_points.Add(p2);
            LineInterpolation(affnic_points,true);
        }
        //移动到拍照位置
        private void button9_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                point_move(camera_x1, camera_y1);
                //// 显示到界面（可选）
                //lbl_x_current_pos.BeginInvoke(new Action(()=> lbl_x_current_pos.Text = camera_x1.ToString("F3")));
                //lbl_y_current_pos.BeginInvoke(new Action(() => lbl_y_current_pos.Text = camera_y1.ToString("F3")));
            });
            
        }
        //绘制图形
        private async void button10_Click(object sender, EventArgs e)
        {
            HTuple hv_Row, hv_Col;
            HOperatorSet.ReadTuple("C:/Users/20765/Desktop/sorted_row.tup", out hv_Row);
            HOperatorSet.ReadTuple("C:/Users/20765/Desktop/sorted_col.tup", out hv_Col);

            double[] rows = hv_Row.ToDArr();   // 行坐标（y 方向）
            double[] cols = hv_Col.ToDArr();   // 列坐标（x 方向）
            
            hv_Row.Dispose();
            hv_Col.Dispose();

            if (rows.Length == 0)
            {
                MessageBox.Show("未检测到有效轮廓，请检查图像/阈值");
                return;
            }

            // 3. 后台线程逐点运动，避免卡 UI
            await Task.Run(() =>
            {
                var path = new List<Point5D>();
                HTuple drawqx = new HTuple(), drawqy = new HTuple();
                for (int i = 0; i < rows.Length; i++)
                {
                    H.AffineTransPoint2d(hv_HomMat2D1, cols[i], rows[i], out drawqx, out drawqy);
                    path.Add(new Point5D { X = drawqx, Y = drawqy });
                }// 记得先过仿射变换
                LineInterpolationPath(path, true);
            });

        }
        private bool LineInterpolationPath(List<Point5D> path, bool iswait = true)
        {
            // ===== ★ 只加这一段：进入前把状态停干净 =====
            short st = 0, fifo = 0;
            int sg = 0;
            card.GA_CrdStatus(1, ref st, ref sg, fifo);
            if (st != 0)   // 有 running 标志
            {
                card.GA_Stop(0x0003, 0x0003);
                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < 2000)
                {
                    card.GA_CrdStatus(1, ref st, ref sg, fifo);
                    if (st == 0) break;
                    Thread.Sleep(5);
                }
            }
            // ===== 加完 =====

            Create_xyz();
            card.GA_CrdClear(1, 0);
            int segNum = 0;
            card.GA_AxisOn(1);
            card.GA_AxisOn(2);

            const double vel = 100, acc = 0.5;

            foreach (var p in path)
            {
                int pulse_x = (int)MmConvertPulse(p.X);
                int pulse_y = (int)MmConvertPulse(p.Y);
                ret = card.GA_LnXY(1, pulse_x, pulse_y, vel, acc, 0, 0, segNum);
                if (ret != 0) return false;
                segNum++;
            }

            ret = card.GA_CrdData(1, 0, 0);
            ret = card.GA_CrdStart(1, 0);
            return WaitCoordRunFinish(iswait);
        }


        //以当前位置为起点运动到指定点位
        private void point_move(double x,double y)
        {
            List<Point5D> affnic_points = new List<Point5D>();
            //// 解析点1
            double x1 = ExtractNumber(lbl_x_current_pos.Text);
            double y1 = ExtractNumber(lbl_y_current_pos.Text);
            Point5D p1 = new Point5D { X = x1, Y = y1 };
            affnic_points.Add(p1);

            // 解析点2
            double x2 = x;
            double y2 = y;
            Point5D p2 = new Point5D { X = x2, Y = y2 };
            affnic_points.Add(p2);
            LineInterpolation(affnic_points, true);
        }

       

        int i = 0;
        private void save_ninepoint_Click(object sender, EventArgs e)
        {
            if (i >= 9)
            {
                MessageBox.Show("九点记录成功");
                return;
            }
            
            Row[i] = ExtractNumber(lbl_x_current_pos.Text);
            Col[i] = ExtractNumber(lbl_y_current_pos.Text);
            
            i++;
            if (i == 9)
            {
                // 把 double[] 转换成 Halcon 能直接识别的字符串格式
                string halconX = "[" + string.Join(", ", Row) + "]";
                string halconY = "[" + string.Join(", ", Col) + "]";

                // 拼接要保存的内容
                string content = "====== 请把以下代码复制到 Halcon 中 ======\r\n\r\n" +
                                 "MachineX := " + halconX + "\r\n\r\n" +
                                 "MachineY := " + halconY;

                // 保存到桌面（你也可以改成 D 盘或你的项目路径）
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = System.IO.Path.Combine(desktopPath, "Halcon标定数据.txt");

                // 写入文件
                System.IO.File.WriteAllText(filePath, content);

                MessageBox.Show("9个点记录完毕！\n标定数据已保存到电脑桌面的 [Halcon标定数据.txt] 文件中。", "提示");
            }
        }

        private void read_file_Click(object sender, EventArgs e)
        {
            H.OpenFile("C:/Users/20765/Desktop/九点标定", "input_binary", out FileHandle1);
            H.FreadSerializedItem(FileHandle1, out SerializedItemHandle2);
            HOperatorSet.OpenFile("C:/Users/20765/Desktop/九点标定", "input_binary",out hv_FileHandle1);
            hv_SerializedItemHandle2.Dispose();
            HOperatorSet.FreadSerializedItem(hv_FileHandle1, out hv_SerializedItemHandle2);
            hv_HomMat2D1.Dispose();
            HOperatorSet.DeserializeHomMat2d(hv_SerializedItemHandle2, out hv_HomMat2D1);
            HOperatorSet.CloseFile(hv_FileHandle1);
            if(hv_HomMat2D1 != null)
            {
                MessageBox.Show("反射关系读取成功");
            }
        }
    }
}
