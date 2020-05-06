using Automation.BDaq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testsjk;

namespace SkateboardControl_System
{
    public partial class EighthForm8 : Form
    {
        DataTable dt = null;
        string tb = "[SY8_Hplx]";
        #region
        private const int CHANEL_COUNT_MAX = 16;//最大通道数16
        //定义一个数组，存放各个通道的模拟量
        public double[] m_dataScaled = new double[CHANEL_COUNT_MAX];
        //设置开始的通道（从0开始，因为采集板提供调用函数用）
        private int comboBox_chanStart = 0;
        //设置使用的通道数（根据连接的通道数进行设置） 
        private int chanCountSet = 4;
        List<SY8_List> sY8_Lists = new List<SY8_List>();
        public static float[] weiZhi = new float[3]{10,20,30 };
        int num = 0;
        int[] a = new int[5];
        #endregion
        public EighthForm8()
        {
            InitializeComponent();

            this.dataGridView1.DataSource = new BindingList<SY8_List>(sY8_Lists);  //把dattable绑定datagridview
            this.dataGridView1.Columns[0].HeaderText = "左尾板";
            this.dataGridView1.Columns[1].HeaderText = "左首下板";
            this.dataGridView1.Columns[2].HeaderText = "左首上板";
            this.dataGridView1.Columns[3].HeaderText = "右尾板";
            this.dataGridView1.Columns[4].HeaderText = "右首下板";
            this.dataGridView1.Columns[5].HeaderText = "右首上板";
            this.dataGridView1.Columns[6].HeaderText = "位置δ";
            this.dataGridView1.Columns[7].HeaderText = "位置β";
            this.dataGridView1.Columns[8].HeaderText = "位置Φ";
            for (int i = 0; i < 9; i++)
            {
                this.dataGridView1.Columns[i].Width = 35;
            }
            //选择整行显示数据
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //选择是否只读
            this.dataGridView1.ReadOnly = true;
            //选择是否添加一行空白行
            this.dataGridView1.AllowUserToAddRows = false;

            //样式设置
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
        /// 开关按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            dt = Export_All_SY.DBQuery(tb);
            if (this.button1.Text == "开")
            {
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("数据库中已有数据，请进行下一个实验或点击重新实验");
                    return;
                }
                timer1.Start();
                this.button1.Text = "关";
                this.button1.BackColor = Color.SandyBrown;
            } else if (this.button1.Text == "关")
            {
                timer1.Enabled = false;
                this.button1.Text = "开";
                this.button1.BackColor = Color.FloralWhite;
                this.pictureBox1.Image = Properties.Resources.Start;
                this.pictureBox2.Image = Properties.Resources.Start;
                this.pictureBox3.Image = Properties.Resources.Start;
                this.pictureBox4.Image = Properties.Resources.Start;
                this.pictureBox5.Image = Properties.Resources.Start;
                this.pictureBox6.Image = Properties.Resources.Start;
                this.pictureBox7.Image = Properties.Resources.Start;
                this.pictureBox8.Image = Properties.Resources.Start;
            }
        }
        /// <summary>
        /// 采集数据按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.button1.Text == "开")
            {
                MessageBox.Show("请打开开关", "提示");
            }
            if (sY8_Lists.Count==23)
            {
                this.button2.Enabled = false;
            }
            num++;
            if (num == 1 && MainFrom.Data_pictureBox12 != 1)
            {
                num = 0;
                MessageBox.Show("请设置初始状态为“陆上”状态", "提示");
                return;
            }
            Load_Gongbu();

            SY8_List sY8_List = new SY8_List();
            sY8_List.Left_a1 = (float)m_dataScaled[0];
            sY8_List.Left_a2 = (float)m_dataScaled[1]; sY8_List.Left_a3 = (float)m_dataScaled[2];
            sY8_List.Right_a1 = (float)m_dataScaled[3];
            sY8_List.Right_a2 = (float)m_dataScaled[4]; sY8_List.Right_a3 = (float)m_dataScaled[5];
            sY8_List.WeiZhi_a1 = weiZhi[0];
            sY8_List.WeiZhi_a2 = weiZhi[1]; sY8_List.WeiZhi_a3 = weiZhi[2];
            sY8_Lists.Add(sY8_List);
            this.dataGridView1.DataSource = new BindingList<SY8_List>(sY8_Lists);  //把dattable绑定datagridview
            this.dataGridView1.Columns[0].HeaderText = "左尾翼板";
            this.dataGridView1.Columns[1].HeaderText = "左首下板";
            this.dataGridView1.Columns[2].HeaderText = "左首上板";
            this.dataGridView1.Columns[3].HeaderText = "右尾翼板";
            this.dataGridView1.Columns[4].HeaderText = "右首下板";
            this.dataGridView1.Columns[5].HeaderText = "右首上板";
            this.dataGridView1.Columns[6].HeaderText = "位置δ";
            this.dataGridView1.Columns[7].HeaderText = "位置β";
            this.dataGridView1.Columns[8].HeaderText = "位置Φ";
            for (int i = 0; i < 9; i++)
            {
                this.dataGridView1.Columns[i].Width = 35;
            }
            //选择整行显示数据
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //选择是否只读
            this.dataGridView1.ReadOnly = true;
            //选择是否添加一行空白行
            this.dataGridView1.AllowUserToAddRows = false;

            //插入数据库
            if (checkBox1.Checked == true) a[0] = 1;
            else a[0] = 0;
            if (checkBox2.Checked == true) a[1] = 1;
            else a[1] = 0;
            if (checkBox3.Checked == true) a[2] = 1;
            else a[2] = 0;
            if (checkBox4.Checked == true) a[3] = 1;
            else a[3] = 0;
            if (checkBox5.Checked == true) a[4] = 1;
            else a[4] = 0;
            DateTime timer = DateTime.Now;
            String sqldata = "insert into [SY8_Hplx](CS_no,K_yang,K_zuo,K_you,Hbsq,Hpyxd,Z_cgq1," +
                "Z_cgq2,Z_cgq3,Y_cgq1,Y_cgq2,Y_cgq3,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                      " values(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17)";
            SqlHelper.ExecuteNonQuery(sqldata,
                new SqlParameter("@a1", num),
                new SqlParameter("@a2", a[0]),
                new SqlParameter("@a3", a[1]),
                new SqlParameter("@a4", a[2]),
                new SqlParameter("@a5", a[3]),
                new SqlParameter("@a6", a[4]),
                new SqlParameter("@a7", sY8_List.Left_a1),
                new SqlParameter("@a8", sY8_List.Left_a2),
                new SqlParameter("@a9", sY8_List.Left_a3),
                new SqlParameter("@a10", sY8_List.Right_a1),
                new SqlParameter("@a11", sY8_List.Right_a2),
                new SqlParameter("@a12", sY8_List.Right_a3),
                new SqlParameter("@a13", timer),
                new SqlParameter("@a14", MainFrom.Uname),
                new SqlParameter("@a15", MainFrom.comBoxData_Form),
                new SqlParameter("@a16", MainFrom.textB_HbData_Form),
                new SqlParameter("@a17", MainFrom.textB_CgqData_Form));
            if (num == 8)
            {
                num = 0;
            }
        }
        /// <summary>
        /// 指示灯的显示
        /// </summary>
        private void Load_Gongbu()
        {
            if (num == 1 && MainFrom.Data_pictureBox12 == 1)
            {
                this.pictureBox1.Image = Properties.Resources.ledLow;
                this.pictureBox2.Image = Properties.Resources.ledHigh;
                this.pictureBox3.Image = Properties.Resources.ledHigh;
                this.pictureBox4.Image = Properties.Resources.ledHigh;
                this.pictureBox5.Image = Properties.Resources.ledHigh;
                this.pictureBox6.Image = Properties.Resources.ledHigh;
                this.pictureBox7.Image = Properties.Resources.ledHigh;
                this.pictureBox8.Image = Properties.Resources.ledHigh;
            }
            if (num == 2 && MainFrom.Data_pictureBox13 == 1)
            {
                this.pictureBox1.Image = Properties.Resources.ledHigh;

                this.pictureBox2.Image = Properties.Resources.ledLow;
                this.pictureBox3.Image = Properties.Resources.ledHigh;
                this.pictureBox4.Image = Properties.Resources.ledHigh;
                this.pictureBox5.Image = Properties.Resources.ledHigh;
                this.pictureBox6.Image = Properties.Resources.ledHigh;
                this.pictureBox7.Image = Properties.Resources.ledHigh;
                this.pictureBox8.Image = Properties.Resources.ledHigh;
            }
            if (num == 3 && MainFrom.Data_pictureBox14 == 1)
            {
                this.pictureBox1.Image = Properties.Resources.ledHigh;

                this.pictureBox2.Image = Properties.Resources.ledHigh;
                this.pictureBox3.Image = Properties.Resources.ledLow;
                this.pictureBox4.Image = Properties.Resources.ledHigh;
                this.pictureBox5.Image = Properties.Resources.ledHigh;
                this.pictureBox6.Image = Properties.Resources.ledHigh;
                this.pictureBox7.Image = Properties.Resources.ledHigh;
                this.pictureBox8.Image = Properties.Resources.ledHigh;
            }
            if (num == 4 && MainFrom.Data_pictureBox15 == 1)
            {
                this.pictureBox1.Image = Properties.Resources.ledHigh;

                this.pictureBox2.Image = Properties.Resources.ledHigh;
                this.pictureBox3.Image = Properties.Resources.ledHigh;
                this.pictureBox4.Image = Properties.Resources.ledLow;
                this.pictureBox5.Image = Properties.Resources.ledHigh;
                this.pictureBox6.Image = Properties.Resources.ledHigh;
                this.pictureBox7.Image = Properties.Resources.ledHigh;
                this.pictureBox8.Image = Properties.Resources.ledHigh;
            }
            if (num == 5 && MainFrom.Data_pictureBox15 == 1)
            {
                this.pictureBox1.Image = Properties.Resources.ledHigh;

                this.pictureBox2.Image = Properties.Resources.ledHigh;
                this.pictureBox3.Image = Properties.Resources.ledHigh;
                this.pictureBox4.Image = Properties.Resources.ledHigh;
                this.pictureBox5.Image = Properties.Resources.ledLow;
                this.pictureBox6.Image = Properties.Resources.ledHigh;
                this.pictureBox7.Image = Properties.Resources.ledHigh;
                this.pictureBox8.Image = Properties.Resources.ledHigh;
            }
            if (num == 6 && MainFrom.Data_pictureBox14 == 1)
            {
                this.pictureBox1.Image = Properties.Resources.ledHigh;

                this.pictureBox2.Image = Properties.Resources.ledHigh;
                this.pictureBox3.Image = Properties.Resources.ledHigh;
                this.pictureBox4.Image = Properties.Resources.ledHigh;
                this.pictureBox5.Image = Properties.Resources.ledHigh;
                this.pictureBox6.Image = Properties.Resources.ledLow;
                this.pictureBox7.Image = Properties.Resources.ledHigh;
                this.pictureBox8.Image = Properties.Resources.ledHigh;
            }
            if (num == 7 && MainFrom.Data_pictureBox13 == 1)
            {
                this.pictureBox1.Image = Properties.Resources.ledHigh;

                this.pictureBox2.Image = Properties.Resources.ledHigh;
                this.pictureBox3.Image = Properties.Resources.ledHigh;
                this.pictureBox4.Image = Properties.Resources.ledHigh;
                this.pictureBox5.Image = Properties.Resources.ledHigh;
                this.pictureBox6.Image = Properties.Resources.ledHigh;
                this.pictureBox7.Image = Properties.Resources.ledLow;
                this.pictureBox8.Image = Properties.Resources.ledHigh;
            }
            if (num == 8 && MainFrom.Data_pictureBox12 == 1)
            {
                this.pictureBox1.Image = Properties.Resources.ledHigh;

                this.pictureBox2.Image = Properties.Resources.ledHigh;
                this.pictureBox3.Image = Properties.Resources.ledHigh;
                this.pictureBox4.Image = Properties.Resources.ledHigh;
                this.pictureBox5.Image = Properties.Resources.ledHigh;
                this.pictureBox6.Image = Properties.Resources.ledHigh;
                this.pictureBox7.Image = Properties.Resources.ledHigh;
                this.pictureBox8.Image = Properties.Resources.ledLow;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (num!=8&&MainFrom.Data_pictureBox12 == 1)
            {
                this.pictureBox1.Image = Properties.Resources.ledLow;

                this.pictureBox2.Image = Properties.Resources.ledHigh;
                this.pictureBox3.Image = Properties.Resources.ledHigh;
                this.pictureBox4.Image = Properties.Resources.ledHigh;
                this.pictureBox5.Image = Properties.Resources.ledHigh;
                this.pictureBox6.Image = Properties.Resources.ledHigh;
                this.pictureBox7.Image = Properties.Resources.ledHigh;
                this.pictureBox8.Image = Properties.Resources.ledHigh;
            }
            PerformanceCounter performanceCounter = new PerformanceCounter();
            ErrorCode err;
            performanceCounter.Start();
            err = instantAiCtrl1.Read(comboBox_chanStart, chanCountSet, m_dataScaled);
            for (int i = 0; i < m_dataScaled.Length; i++)
            {
                m_dataScaled[i] = (double)Math.Round(Convert.ToDecimal(m_dataScaled[i]), 2, MidpointRounding.AwayFromZero)*36;
            }
            if (err != ErrorCode.Success)
            {
                HandleError(err);
                timer1.Stop();
            }
            performanceCounter.Stop();
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
        /// 重新试验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            sY8_Lists.Clear();
            this.dataGridView1.DataSource = new BindingList<SY8_List>(sY8_Lists);
            Export_All_SY.DBDel(tb);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Export_One_SY.Exp_HPSY();
        }
    }
    class SY8_List
    {
        float left_a1, left_a2, left_a3;
        float right_a1, right_a2, right_a3;
        float weiZhi_a1, weiZhi_a2, weiZhi_a3;

        public float Left_a1 { get => left_a1; set => left_a1 = value; }
        public float Left_a2 { get => left_a2; set => left_a2 = value; }
        public float Left_a3 { get => left_a3; set => left_a3 = value; }
        public float Right_a1 { get => right_a1; set => right_a1 = value; }
        public float Right_a2 { get => right_a2; set => right_a2 = value; }
        public float Right_a3 { get => right_a3; set => right_a3 = value; }
        public float WeiZhi_a1 { get => weiZhi_a1; set => weiZhi_a1 = value; }
        public float WeiZhi_a2 { get => weiZhi_a2; set => weiZhi_a2 = value; }
        public float WeiZhi_a3 { get => weiZhi_a3; set => weiZhi_a3 = value; }
    }
}
