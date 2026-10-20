namespace movecard
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.y2_up = new System.Windows.Forms.Button();
            this.x1_left = new System.Windows.Forms.Button();
            this.y2_down = new System.Windows.Forms.Button();
            this.x1_right = new System.Windows.Forms.Button();
            this.z3_up = new System.Windows.Forms.Button();
            this.z3_down = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_z_current_speed = new System.Windows.Forms.Label();
            this.lbl_y_current_speed = new System.Windows.Forms.Label();
            this.lbl_x_current_speed = new System.Windows.Forms.Label();
            this.lbl_z_current_pos = new System.Windows.Forms.Label();
            this.lbl_y_current_pos = new System.Windows.Forms.Label();
            this.lbl_x_current_pos = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.point_x1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.point_y1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.point_x2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.point_y2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.point_x3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.point_y3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.point_z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.save_point = new System.Windows.Forms.Button();
            this.add_row = new System.Windows.Forms.Button();
            this.delete_row = new System.Windows.Forms.Button();
            this.bt_start = new System.Windows.Forms.Button();
            this.bt_stop = new System.Windows.Forms.Button();
            this.save_ninepoint = new System.Windows.Forms.Button();
            this.read_file = new System.Windows.Forms.Button();
            this.to_qpoint = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // y2_up
            // 
            this.y2_up.Location = new System.Drawing.Point(813, 15);
            this.y2_up.Name = "y2_up";
            this.y2_up.Size = new System.Drawing.Size(56, 45);
            this.y2_up.TabIndex = 0;
            this.y2_up.Text = "↑";
            this.y2_up.UseVisualStyleBackColor = true;
            this.y2_up.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Jog_move);
            this.y2_up.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Jog_stop);
            // 
            // x1_left
            // 
            this.x1_left.Location = new System.Drawing.Point(759, 64);
            this.x1_left.Name = "x1_left";
            this.x1_left.Size = new System.Drawing.Size(56, 45);
            this.x1_left.TabIndex = 1;
            this.x1_left.Text = "←";
            this.x1_left.UseVisualStyleBackColor = true;
            this.x1_left.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Jog_move);
            this.x1_left.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Jog_stop);
            // 
            // y2_down
            // 
            this.y2_down.Location = new System.Drawing.Point(813, 115);
            this.y2_down.Name = "y2_down";
            this.y2_down.Size = new System.Drawing.Size(56, 45);
            this.y2_down.TabIndex = 2;
            this.y2_down.Text = "↓";
            this.y2_down.UseVisualStyleBackColor = true;
            this.y2_down.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Jog_move);
            this.y2_down.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Jog_stop);
            // 
            // x1_right
            // 
            this.x1_right.Location = new System.Drawing.Point(867, 64);
            this.x1_right.Name = "x1_right";
            this.x1_right.Size = new System.Drawing.Size(56, 45);
            this.x1_right.TabIndex = 3;
            this.x1_right.Text = "→";
            this.x1_right.UseVisualStyleBackColor = true;
            this.x1_right.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Jog_move);
            this.x1_right.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Jog_stop);
            // 
            // z3_up
            // 
            this.z3_up.Location = new System.Drawing.Point(964, 15);
            this.z3_up.Name = "z3_up";
            this.z3_up.Size = new System.Drawing.Size(56, 45);
            this.z3_up.TabIndex = 4;
            this.z3_up.Text = "↑";
            this.z3_up.UseVisualStyleBackColor = true;
            this.z3_up.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Jog_move);
            this.z3_up.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Jog_stop);
            // 
            // z3_down
            // 
            this.z3_down.Location = new System.Drawing.Point(964, 115);
            this.z3_down.Name = "z3_down";
            this.z3_down.Size = new System.Drawing.Size(56, 45);
            this.z3_down.TabIndex = 5;
            this.z3_down.Text = "↓";
            this.z3_down.UseVisualStyleBackColor = true;
            this.z3_down.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Jog_move);
            this.z3_down.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Jog_stop);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(623, 15);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(130, 45);
            this.button7.TabIndex = 6;
            this.button7.Text = "打开板卡";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(623, 80);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(130, 45);
            this.button8.TabIndex = 7;
            this.button8.Text = "关闭把卡";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(590, 443);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 36);
            this.label1.TabIndex = 8;
            this.label1.Text = "轴号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(590, 479);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 36);
            this.label2.TabIndex = 9;
            this.label2.Text = "X";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(590, 515);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 36);
            this.label3.TabIndex = 10;
            this.label3.Text = "Y";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(590, 551);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 36);
            this.label4.TabIndex = 11;
            this.label4.Text = "Z";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(740, 443);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(144, 36);
            this.label8.TabIndex = 12;
            this.label8.Text = "规划位置";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Location = new System.Drawing.Point(890, 443);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(144, 36);
            this.label12.TabIndex = 16;
            this.label12.Text = "规划速度";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_z_current_speed
            // 
            this.lbl_z_current_speed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_z_current_speed.Location = new System.Drawing.Point(890, 551);
            this.lbl_z_current_speed.Name = "lbl_z_current_speed";
            this.lbl_z_current_speed.Size = new System.Drawing.Size(144, 36);
            this.lbl_z_current_speed.TabIndex = 36;
            this.lbl_z_current_speed.Text = "0.000";
            // 
            // lbl_y_current_speed
            // 
            this.lbl_y_current_speed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_y_current_speed.Location = new System.Drawing.Point(890, 515);
            this.lbl_y_current_speed.Name = "lbl_y_current_speed";
            this.lbl_y_current_speed.Size = new System.Drawing.Size(144, 36);
            this.lbl_y_current_speed.TabIndex = 35;
            this.lbl_y_current_speed.Text = "0.000";
            // 
            // lbl_x_current_speed
            // 
            this.lbl_x_current_speed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_x_current_speed.Location = new System.Drawing.Point(890, 479);
            this.lbl_x_current_speed.Name = "lbl_x_current_speed";
            this.lbl_x_current_speed.Size = new System.Drawing.Size(144, 36);
            this.lbl_x_current_speed.TabIndex = 34;
            this.lbl_x_current_speed.Text = "0.000";
            // 
            // lbl_z_current_pos
            // 
            this.lbl_z_current_pos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_z_current_pos.Location = new System.Drawing.Point(740, 551);
            this.lbl_z_current_pos.Name = "lbl_z_current_pos";
            this.lbl_z_current_pos.Size = new System.Drawing.Size(144, 36);
            this.lbl_z_current_pos.TabIndex = 33;
            this.lbl_z_current_pos.Text = "0.000";
            // 
            // lbl_y_current_pos
            // 
            this.lbl_y_current_pos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_y_current_pos.Location = new System.Drawing.Point(740, 515);
            this.lbl_y_current_pos.Name = "lbl_y_current_pos";
            this.lbl_y_current_pos.Size = new System.Drawing.Size(144, 36);
            this.lbl_y_current_pos.TabIndex = 32;
            this.lbl_y_current_pos.Text = "0.000";
            // 
            // lbl_x_current_pos
            // 
            this.lbl_x_current_pos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_x_current_pos.Location = new System.Drawing.Point(740, 479);
            this.lbl_x_current_pos.Name = "lbl_x_current_pos";
            this.lbl_x_current_pos.Size = new System.Drawing.Size(144, 36);
            this.lbl_x_current_pos.TabIndex = 31;
            this.lbl_x_current_pos.Text = "0.000";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(904, 211);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 40);
            this.button1.TabIndex = 37;
            this.button1.Text = "全部回原";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(768, 211);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 40);
            this.button2.TabIndex = 38;
            this.button2.Text = "Z3回原";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(768, 166);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(130, 40);
            this.button3.TabIndex = 39;
            this.button3.Text = "X1回原";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(904, 166);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(130, 40);
            this.button4.TabIndex = 40;
            this.button4.Text = "Y2回原";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index,
            this.ColType,
            this.point_x1,
            this.point_y1,
            this.point_x2,
            this.point_y2,
            this.point_x3,
            this.point_y3,
            this.point_z});
            this.dataGridView1.Location = new System.Drawing.Point(12, 15);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(544, 319);
            this.dataGridView1.TabIndex = 41;
            // 
            // index
            // 
            this.index.HeaderText = "编号";
            this.index.MinimumWidth = 6;
            this.index.Name = "index";
            // 
            // ColType
            // 
            this.ColType.HeaderText = "插补类型";
            this.ColType.Items.AddRange(new object[] {
            "直线插补",
            "圆弧插补"});
            this.ColType.MinimumWidth = 6;
            this.ColType.Name = "ColType";
            // 
            // point_x1
            // 
            this.point_x1.HeaderText = "X1";
            this.point_x1.MinimumWidth = 6;
            this.point_x1.Name = "point_x1";
            // 
            // point_y1
            // 
            this.point_y1.HeaderText = "Y1";
            this.point_y1.MinimumWidth = 6;
            this.point_y1.Name = "point_y1";
            // 
            // point_x2
            // 
            this.point_x2.HeaderText = "X2";
            this.point_x2.MinimumWidth = 6;
            this.point_x2.Name = "point_x2";
            // 
            // point_y2
            // 
            this.point_y2.HeaderText = "Y2";
            this.point_y2.MinimumWidth = 6;
            this.point_y2.Name = "point_y2";
            // 
            // point_x3
            // 
            this.point_x3.HeaderText = "X3";
            this.point_x3.MinimumWidth = 6;
            this.point_x3.Name = "point_x3";
            // 
            // point_y3
            // 
            this.point_y3.HeaderText = "Y3";
            this.point_y3.MinimumWidth = 6;
            this.point_y3.Name = "point_y3";
            // 
            // point_z
            // 
            this.point_z.HeaderText = "Z";
            this.point_z.MinimumWidth = 6;
            this.point_z.Name = "point_z";
            // 
            // save_point
            // 
            this.save_point.Location = new System.Drawing.Point(662, 168);
            this.save_point.Name = "save_point";
            this.save_point.Size = new System.Drawing.Size(94, 38);
            this.save_point.TabIndex = 42;
            this.save_point.Text = "保存点";
            this.save_point.UseVisualStyleBackColor = true;
            this.save_point.Click += new System.EventHandler(this.save_point_Click);
            // 
            // add_row
            // 
            this.add_row.Location = new System.Drawing.Point(562, 166);
            this.add_row.Name = "add_row";
            this.add_row.Size = new System.Drawing.Size(94, 38);
            this.add_row.TabIndex = 43;
            this.add_row.Text = "添加行";
            this.add_row.UseVisualStyleBackColor = true;
            this.add_row.Click += new System.EventHandler(this.add_row_Click);
            // 
            // delete_row
            // 
            this.delete_row.Location = new System.Drawing.Point(562, 213);
            this.delete_row.Name = "delete_row";
            this.delete_row.Size = new System.Drawing.Size(94, 38);
            this.delete_row.TabIndex = 44;
            this.delete_row.Text = "删除行";
            this.delete_row.UseVisualStyleBackColor = true;
            this.delete_row.Click += new System.EventHandler(this.delete_row_Click);
            // 
            // bt_start
            // 
            this.bt_start.Location = new System.Drawing.Point(623, 266);
            this.bt_start.Name = "bt_start";
            this.bt_start.Size = new System.Drawing.Size(63, 47);
            this.bt_start.TabIndex = 45;
            this.bt_start.Text = "启动";
            this.bt_start.UseVisualStyleBackColor = true;
            this.bt_start.Click += new System.EventHandler(this.bt_start_Click);
            // 
            // bt_stop
            // 
            this.bt_stop.Location = new System.Drawing.Point(709, 266);
            this.bt_stop.Name = "bt_stop";
            this.bt_stop.Size = new System.Drawing.Size(63, 47);
            this.bt_stop.TabIndex = 46;
            this.bt_stop.Text = "停止";
            this.bt_stop.UseVisualStyleBackColor = true;
            this.bt_stop.Click += new System.EventHandler(this.bt_stop_Click);
            // 
            // save_ninepoint
            // 
            this.save_ninepoint.Location = new System.Drawing.Point(916, 329);
            this.save_ninepoint.Name = "save_ninepoint";
            this.save_ninepoint.Size = new System.Drawing.Size(94, 38);
            this.save_ninepoint.TabIndex = 47;
            this.save_ninepoint.Text = "记录九点";
            this.save_ninepoint.UseVisualStyleBackColor = true;
            this.save_ninepoint.Click += new System.EventHandler(this.save_ninepoint_Click);
            // 
            // read_file
            // 
            this.read_file.Location = new System.Drawing.Point(789, 329);
            this.read_file.Name = "read_file";
            this.read_file.Size = new System.Drawing.Size(121, 38);
            this.read_file.TabIndex = 48;
            this.read_file.Text = "读取标定文件";
            this.read_file.UseVisualStyleBackColor = true;
            this.read_file.Click += new System.EventHandler(this.read_file_Click);
            // 
            // to_qpoint
            // 
            this.to_qpoint.Location = new System.Drawing.Point(789, 373);
            this.to_qpoint.Name = "to_qpoint";
            this.to_qpoint.Size = new System.Drawing.Size(121, 38);
            this.to_qpoint.TabIndex = 49;
            this.to_qpoint.Text = "点位映射";
            this.to_qpoint.UseVisualStyleBackColor = true;
            this.to_qpoint.Click += new System.EventHandler(this.to_qpoint_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(789, 285);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(121, 38);
            this.button5.TabIndex = 50;
            this.button5.Text = "记录拍照位置";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(916, 285);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(118, 38);
            this.button6.TabIndex = 51;
            this.button6.Text = "读取拍照位置";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(665, 329);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(118, 38);
            this.button9.TabIndex = 52;
            this.button9.Text = "移动到拍照位置";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 621);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.to_qpoint);
            this.Controls.Add(this.read_file);
            this.Controls.Add(this.save_ninepoint);
            this.Controls.Add(this.bt_stop);
            this.Controls.Add(this.bt_start);
            this.Controls.Add(this.delete_row);
            this.Controls.Add(this.add_row);
            this.Controls.Add(this.save_point);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_z_current_speed);
            this.Controls.Add(this.lbl_y_current_speed);
            this.Controls.Add(this.lbl_x_current_speed);
            this.Controls.Add(this.lbl_z_current_pos);
            this.Controls.Add(this.lbl_y_current_pos);
            this.Controls.Add(this.lbl_x_current_pos);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.z3_down);
            this.Controls.Add(this.z3_up);
            this.Controls.Add(this.x1_right);
            this.Controls.Add(this.y2_down);
            this.Controls.Add(this.x1_left);
            this.Controls.Add(this.y2_up);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button y2_up;
        private System.Windows.Forms.Button x1_left;
        private System.Windows.Forms.Button y2_down;
        private System.Windows.Forms.Button x1_right;
        private System.Windows.Forms.Button z3_up;
        private System.Windows.Forms.Button z3_down;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl_z_current_speed;
        private System.Windows.Forms.Label lbl_y_current_speed;
        private System.Windows.Forms.Label lbl_x_current_speed;
        private System.Windows.Forms.Label lbl_z_current_pos;
        private System.Windows.Forms.Label lbl_y_current_pos;
        private System.Windows.Forms.Label lbl_x_current_pos;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button save_point;
        private System.Windows.Forms.Button add_row;
        private System.Windows.Forms.Button delete_row;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColType;
        private System.Windows.Forms.DataGridViewTextBoxColumn point_x1;
        private System.Windows.Forms.DataGridViewTextBoxColumn point_y1;
        private System.Windows.Forms.DataGridViewTextBoxColumn point_x2;
        private System.Windows.Forms.DataGridViewTextBoxColumn point_y2;
        private System.Windows.Forms.DataGridViewTextBoxColumn point_x3;
        private System.Windows.Forms.DataGridViewTextBoxColumn point_y3;
        private System.Windows.Forms.DataGridViewTextBoxColumn point_z;
        private System.Windows.Forms.Button bt_start;
        private System.Windows.Forms.Button bt_stop;
        private System.Windows.Forms.Button save_ninepoint;
        private System.Windows.Forms.Button read_file;
        private System.Windows.Forms.Button to_qpoint;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button9;
    }
}

