using Automation.BDaq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using testsjk;

namespace SkateboardControl_System
{
    public partial class FifthForm5 : Form
    {
        #region
        private const int CHANEL_COUNT_MAX = 16;//最大通道数16
        //定义一个数组，存放各个通道的模拟量
        public double[] m_dataScaled = new double[CHANEL_COUNT_MAX];
        //设置开始的通道（从0开始，因为采集板提供调用函数用）
        private int comboBox_chanStart = 0;
        //设置使用的通道数（根据连接的通道数进行设置） 
        private int chanCountSet = 4;
        //得到数字信号状态，通过获取 MainFrom 中的各个状态按钮的值
        // 例如： MainFrom.Data_pictureBox00
        public Thread insertData_thread  = null; //声明一个
        List<Sy5_List> sy5_Lists = new List<Sy5_List>();
        List<int> lists = new List<int>();
        int gongkuang;
        #endregion

        DataTable dt = null;
        /// <summary>
        /// 无参构造
        /// </summary>
        public FifthForm5()
        {
            InitializeComponent();
            this.button1.Enabled = false;
            this.hight_one.Enabled = false;
            this.higth_two.Enabled = false;
            this.hight_three.Enabled = false;
        }
        string tb = "[SY5_Fdck]";
        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="deviceNumber"></param>
        public FifthForm5(int deviceNumber)
        {
            InitializeComponent();
            this.button1.Enabled = false;
            this.hight_one.Enabled = false;
            this.higth_two.Enabled = false;
            this.hight_three.Enabled = false;
            instantAiCtrl1.SelectedDevice = new DeviceInformation(deviceNumber);
        }

        /// <summary>
        /// 模拟数据采集的定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Show_Tick(object sender, EventArgs e)
        {
            PerformanceCounter performanceCounter = new PerformanceCounter();
            ErrorCode err;
            performanceCounter.Start();
            err = instantAiCtrl1.Read(comboBox_chanStart, chanCountSet, m_dataScaled);
            for (int i = 0;i<m_dataScaled.Length; i++)
            {
                m_dataScaled[i]=(double)Math.Round(Convert.ToDecimal(m_dataScaled[i]), 2, MidpointRounding.AwayFromZero)*36;
            }
            if (err != ErrorCode.Success)
            {
                HandleError(err);
                timer_Show.Stop();
            }
            performanceCounter.Stop();
            //将m_dataScaled[0]四舍五入
            //Math.Round(Convert.ToDecimal(m_dataScaled[0]), 2, MidpointRounding.AwayFromZero)            
            this.label1.Text ="  "+m_dataScaled[0];
            this.label2.Text ="  "+m_dataScaled[1];
            this.label3.Text ="  "+m_dataScaled[2];
            this.label4.Text ="  "+m_dataScaled[3];
            this.label12.Text = "  " + m_dataScaled[4];
            this.label20.Text = "  " + m_dataScaled[5];
            //this.label5.Text = "状态：：："+ MainFrom.Data_pictureBox00;
            //设置数字数据状态图片
            if (MainFrom.Data_pictureBox12 == 1)
            {
                gongkuang = 1;
                this.pictureBox1.Image = Properties.Resources.ledLow;
            }
            else
            {
                this.pictureBox1.Image = Properties.Resources.ledHigh;
            }
            if (MainFrom.Data_pictureBox13 == 1)
            {
                gongkuang = 2;
                this.pictureBox2.Image = Properties.Resources.ledLow;
            }
            else
            {
                this.pictureBox2.Image = Properties.Resources.ledHigh;
            }
            if (MainFrom.Data_pictureBox14 == 1)
            {
                gongkuang = 3;
                this.pictureBox3.Image = Properties.Resources.ledLow;
            }
            else
            {
                this.pictureBox3.Image = Properties.Resources.ledHigh;
            }
            if (MainFrom.Data_pictureBox15 == 1)
            {
                this.pictureBox4.Image = Properties.Resources.ledLow;
            }
            else
            {
                this.pictureBox4.Image = Properties.Resources.ledHigh;
            }
            if (MainFrom.Data_pictureBox02 == 1)
            {
                gongkuang = 7;
                this.pictureBox5.Image = Properties.Resources.ledLow;
            }
            else
            {
                this.pictureBox5.Image = Properties.Resources.ledHigh;
            }
            // 用于控制按钮的显示（采集按钮，以便程序采集的控制）
            /* Data_pictureBox02 = (~portData >> 2) & 0x1;  //02应急
             Data_pictureBox12 = (~portData >> 2) & 0x1;  //12陆上
             Data_pictureBox13 = (~portData >> 3) & 0x1;  //13低速
             Data_pictureBox14 = (~portData >> 4) & 0x1;  //14中速
             Data_pictureBox15 = (~portData >> 5) & 0x1;  //15高速*/
            if (MainFrom.Data_pictureBox15 == 1 && MainFrom.Data_pictureBox02 ==0)
            {
                this.button1.Enabled = false;
                this.hight_one.Enabled = true;
                this.higth_two.Enabled = true;
                this.hight_three.Enabled = true;
            }
            else if (MainFrom.Data_pictureBox15 == 1 && MainFrom.Data_pictureBox02 == 1)
            {
                this.button1.Enabled = true;
                this.hight_one.Enabled = false;
                this.higth_two.Enabled = false;
                this.hight_three.Enabled = false;
            }
            else if (MainFrom.Data_pictureBox15 == 0)
            {
                this.button1.Enabled = true;
                this.hight_one.Enabled = false;
                this.higth_two.Enabled = false;
                this.hight_three.Enabled = false;
            }
        }

        /// <summary>
        /// 采集器加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstantAiForm_Load(object sender, EventArgs e)
        {
            if (!instantAiCtrl1.Initialized)
            {
                MessageBox.Show("No device be selected or device open failed!", "AI_InstantAI");
                //this.Close();
                return;
            }
            //表数据初始化
            this.dataGridView1.DataSource = new BindingList<Sy5_List>(sy5_Lists);  //把dattable绑定datagridview
            this.dataGridView1.Columns[0].HeaderText = "左a1";
            this.dataGridView1.Columns[1].HeaderText = "左a2";
            this.dataGridView1.Columns[2].HeaderText = "左a3";
            this.dataGridView1.Columns[3].HeaderText = "右a1";
            this.dataGridView1.Columns[4].HeaderText = "右a2";
            this.dataGridView1.Columns[5].HeaderText = "右a3";
            //选择整行显示数据
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //选择是否只读
            this.dataGridView1.ReadOnly = true;
            //选择是否添加一行空白行
            this.dataGridView1.AllowUserToAddRows = false;

            //数据格式调整
            // 表格上下左右自适应
            //dataGridView1.Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            // 列手工排序
            dataGridView1.AllowUserToOrderColumns = true;
            // 列头系统样式，设置为false，自定义才生效
            dataGridView1.EnableHeadersVisualStyles = false;
            // 列头高度大小模式
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            // 列头高度大小
            dataGridView1.ColumnHeadersHeight = 30;
            // 列头居中
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            // 列头边框样式
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            // 列头背景色
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0099FF");
            // 列头前景色
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#fff");
            // 列宽自适应
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            // 网格线颜色
            dataGridView1.GridColor = ColorTranslator.FromHtml("#006CB3");
            // 背景色
            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#E7F5FF");
            // 行头边框样式
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            // 行头背景色
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0099FF");
            // 行高（要在窗体初始化的地方InitializeComponent调用才生效）
            dataGridView1.RowTemplate.Height = 30;
            // 单元格内容居中
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // 单元格背景色
            dataGridView1.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#97D5FF");
            // 隔行背景色
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E1F3FF");
        }

        /// <summary>
        /// 采集器加载遇到错误执行
        /// </summary>
        /// <param name="err"></param>
        private void HandleError(ErrorCode err)
        {
            if ((err >= ErrorCode.ErrorHandleNotValid) && (err != ErrorCode.Success))
            {
                MessageBox.Show("Sorry ! some errors happened, the error code is: " + err.ToString(), "AI_InstantAI");
            }
        }

        /// <summary>
        /// 控制模拟数据开始接受数据，（定时器的开始与暂停）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_open_Click(object sender, EventArgs e)
        {
            dt = Export_All_SY.DBQuery(tb);
            if (this.button_open.Text == "开")
            {
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("数据库中已有数据，请进行下一个实验或点击重新实验");
                    return;
                }
                this.button1.Enabled = true;
                this.hight_one.Enabled = true;
                this.higth_two.Enabled = true;
                this.hight_three.Enabled = true;
                timer_Show.Enabled = true;
                this.button_open.BackColor = Color.SandyBrown;
                this.button_open.Text = "关";
            }
            else if(this.button_open.Text == "关")
            {
                if(this.button1.Text == "停止采集")
                {
                    MessageBox.Show("请先关闭数据采集的线程开关！！！","提示");
                    return;
                }
                timer_Show.Enabled = false;
                this.button_open.BackColor = Color.FloralWhite;
                this.label1.Text = "  " + 0;
                this.label2.Text = "  " + 0;
                this.label3.Text = "  " + 0;
                this.label4.Text = "  " + 0;
                this.label12.Text = "  " + 0;
                this.label20.Text = "  " + 0;
                this.button_open.Text = "开";
                this.button1.Enabled = false;
                this.hight_one.Enabled = false;
                this.higth_two.Enabled = false;
                this.hight_three.Enabled = false;
                this.pictureBox1.Image = Properties.Resources.Start;
                this.pictureBox2.Image = Properties.Resources.Start;
                this.pictureBox3.Image = Properties.Resources.Start;
                this.pictureBox4.Image = Properties.Resources.Start;
                this.pictureBox5.Image = Properties.Resources.Start;
            }
        }

        /// <summary>
        /// 开始数据采集的按钮，控制数据采集插入到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (MainFrom.Data_pictureBox01 !=1)
            {
                MessageBox.Show("请切换到程控状态！！！", "提示");
                return;
            }
                /*
                    在此填写数据的插入功能
                 */
                Show_DataToPanel();
                MessageBox.Show("数据采集成功，请继续。。。", "提示");
        }
        /// <summary>
        /// 功能：用于将采集到的数据保存到List并显示在面板界面
        /// </summary>
        private void Show_DataToPanel()
        {
            Sy5_List sy5_List = new Sy5_List();
            sy5_List.Left_a1 = (float)m_dataScaled[0];
            sy5_List.Left_a2 = (float)m_dataScaled[1];
            sy5_List.Left_a3 = (float)m_dataScaled[2];
            sy5_List.Right_a1 = (float)m_dataScaled[3];
            sy5_List.Right_a2 = (float)m_dataScaled[4];
            sy5_List.Right_a3 = (float)m_dataScaled[5];
            sy5_Lists.Add(sy5_List);
            lists.Add(gongkuang);
            //////////////////////////
            this.dataGridView1.DataSource = new BindingList<Sy5_List>(sy5_Lists);  //把dattable绑定datagridview
            this.dataGridView1.Columns[0].HeaderText = "左a1";
            this.dataGridView1.Columns[1].HeaderText = "左a2";
            this.dataGridView1.Columns[2].HeaderText = "左a3";
            this.dataGridView1.Columns[3].HeaderText = "右a1";
            this.dataGridView1.Columns[4].HeaderText = "右a2";
            this.dataGridView1.Columns[5].HeaderText = "右a3";
            for (int i = 0; i < 6; i++)
            {
                this.dataGridView1.Columns[i].Width = 48;
            }
            //选择整行显示数据
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //选择是否只读
            this.dataGridView1.ReadOnly = true;
            //选择是否添加一行空白行
            this.dataGridView1.AllowUserToAddRows = false;
        }

        /// <summary>
        /// 高速对应按钮1    水速6.4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hight_one_Click(object sender, EventArgs e)
        {
            gongkuang = 4;
            if (MainFrom.Data_pictureBox01 != 1)
            {
                MessageBox.Show("请切换到程控状态！！！", "提示");
                return;
            }
            Show_DataToPanel();
            MessageBox.Show("高速，速度6.4m/s,采集成功！！！","提示");
            
        }

        /// <summary>
        /// 高速对应按钮2    水速6.8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void higth_two_Click(object sender, EventArgs e)
        {
            gongkuang = 5;
            if (MainFrom.Data_pictureBox01 != 1)
            {
                MessageBox.Show("请切换到程控状态！！！", "提示");
                return;
            }
            Show_DataToPanel();
            MessageBox.Show("高速，速度6.8m/s,采集成功！！！", "提示");
            
             
        }

        /// <summary>
        /// 高速对应按钮3    水速7.2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hight_three_Click(object sender, EventArgs e)
        {
            gongkuang = 6;
            if (MainFrom.Data_pictureBox01 != 1)
            {
                MessageBox.Show("请切换到程控状态！！！", "提示");
                return;
            }
            Show_DataToPanel();
            MessageBox.Show("高速，速度7.2m/s,采集成功！！！", "提示");
        }
        /// <summary>
        /// 重做实验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            lists.Clear();
            sy5_Lists.Clear();
            this.dataGridView1.DataSource = new BindingList<Sy5_List>(sy5_Lists);
            Export_All_SY.DBDel(tb);
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            String sqldata;
                for (int i= 0;i<sy5_Lists.Count;i++)
                {
                    DateTime timer = DateTime.Now;
                    sqldata = "insert into [SY5_Fdck](Gk_no,Z_cgq1,Z_cgq2,Z_cgq3,Y_cgq1,Y_cgq2,Y_cgq3,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                              " values(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12)";
                    SqlHelper.ExecuteNonQuery(sqldata, 
                        new SqlParameter("@a1", lists[i]),
                        new SqlParameter("@a2", sy5_Lists[i].Left_a1),
                        new SqlParameter("@a3", sy5_Lists[i].Left_a2),
                        new SqlParameter("@a4", sy5_Lists[i].Left_a3),
                        new SqlParameter("@a5", sy5_Lists[i].Right_a1),
                        new SqlParameter("@a6", sy5_Lists[i].Right_a2),
                        new SqlParameter("@a7", sy5_Lists[i].Right_a3),
                        new SqlParameter("@a8", timer), 
                        new SqlParameter("@a9", MainFrom.Uname),
                        new SqlParameter("@a10", MainFrom.comBoxData_Form), 
                        new SqlParameter("@a11", MainFrom.textB_HbData_Form),
                        new SqlParameter("@a12", MainFrom.textB_CgqData_Form));
                }
            lists.Clear();
            sy5_Lists.Clear();
            this.dataGridView1.DataSource = new BindingList<Sy5_List>(sy5_Lists);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Export_One_SY.Exp_SY5();
        }
    }
    /// <summary>
    /// 数据列表
    /// </summary>
    class Sy5_List
    {
        float left_a1, left_a2, left_a3;
        float right_a1, right_a2, right_a3;
        public float Left_a1 { get => left_a1; set => left_a1 = value; }
        public float Left_a2 { get => left_a2; set => left_a2 = value; }
        public float Left_a3 { get => left_a3; set => left_a3 = value; }
        public float Right_a1 { get => right_a1; set => right_a1 = value; }
        public float Right_a2 { get => right_a2; set => right_a2 = value; }
        public float Right_a3 { get => right_a3; set => right_a3 = value; }
    }
}
