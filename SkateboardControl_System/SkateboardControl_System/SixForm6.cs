using Automation.BDaq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using testsjk;

namespace SkateboardControl_System
{
    public partial class SixForm6 : Form
    {
     
        #region  //模拟信号采集的相关定义
        private const int CHANEL_COUNT_MAX = 16;//最大通道数16
        //定义一个数组，存放各个通道的模拟量
        public double[] m_dataScaled = new double[CHANEL_COUNT_MAX];
        //设置开始的通道（从0开始，因为采集板提供调用函数用）
        private int comboBox_chanStart = 0;
        //设置使用的通道数（根据连接的通道数进行设置） 
        private int chanCountSet = 4;
        #endregion
        #region  //用于陀螺仪通信的定义
        Thread t1;
        string djip = "192.168.0.232";//主机IP
        int djport = 20001;  //对应主机端口
        Socket socket2;
        EndPoint clientEnd2;
        IPEndPoint ipEnd2;
        byte[] recvData2 = new byte[1024];
        int recvLen2;
        double data1_Temp;  //临时接收的数据，分配给各个对应的传感器
        double[] data1_Nomal = new double[6];//六个传感器的数据
        Byte[] askMsg = new byte[5]; //发送询问帧给相应的6个陀螺仪
        #endregion
        List<Sy6_List> sy6_Lists = new List<Sy6_List>();
        Sy6_List sy6_List;
        DataTable dt = null;
        string tb = "[SY6_Xtts]";
        /// <summary>
        /// 无参构造
        /// </summary>
        public SixForm6()
        {
            InitializeComponent();


            this.dataGridView1.DataSource = new BindingList<Sy6_List>(sy6_Lists);  //把dattable绑定datagridview
            this.dataGridView1.Columns[0].HeaderText = "左测1";
            this.dataGridView1.Columns[1].HeaderText = "左测2";
            this.dataGridView1.Columns[2].HeaderText = "左测3";
            this.dataGridView1.Columns[3].HeaderText = "左显1";
            this.dataGridView1.Columns[4].HeaderText = "左显2";
            this.dataGridView1.Columns[5].HeaderText = "左显3";
            this.dataGridView1.Columns[6].HeaderText = "右测1";
            this.dataGridView1.Columns[7].HeaderText = "右测2";
            this.dataGridView1.Columns[8].HeaderText = "右测3";
            this.dataGridView1.Columns[9].HeaderText = "右显1";
            this.dataGridView1.Columns[10].HeaderText = "右显2";
            this.dataGridView1.Columns[11].HeaderText = "右显3";

           
            //选择整行显示数据
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //选择是否只读
            this.dataGridView1.ReadOnly = true;
            //选择是否添加一行空白行
            this.dataGridView1.AllowUserToAddRows = false;
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
        /// 有参构造
        /// </summary>
        /// <param name="deviceNumber"></param>
        public SixForm6(int deviceNumber)
        {
            InitializeComponent();
            instantAiCtrl1.SelectedDevice = new DeviceInformation(deviceNumber);
        }
        /// <summary>
        /// 采集板加载
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
        }
        /// <summary>
        /// 加载采集卡失败执行函数
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
        /// 采集卡采集模拟信号定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Six_Show_Tick(object sender, EventArgs e)
        {
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
            }
            performanceCounter.Stop();
            //模拟数据角度显示
            this.label1.Text = "  " + Math.Round(Convert.ToDecimal(m_dataScaled[0]), 2, MidpointRounding.AwayFromZero); //左上
            this.label2.Text = "  " + Math.Round(Convert.ToDecimal(m_dataScaled[1]), 2, MidpointRounding.AwayFromZero); //左下
            this.label3.Text = "  " + Math.Round(Convert.ToDecimal(m_dataScaled[2]), 2, MidpointRounding.AwayFromZero); //左尾翼
            this.label4.Text = "  " + Math.Round(Convert.ToDecimal(m_dataScaled[3]), 2, MidpointRounding.AwayFromZero); //右上
            this.label15.Text = "  " + Math.Round(Convert.ToDecimal(m_dataScaled[4]), 2, MidpointRounding.AwayFromZero); //右下
            this.label16.Text = "  " + Math.Round(Convert.ToDecimal(m_dataScaled[5]), 2, MidpointRounding.AwayFromZero); //右尾翼
            //陀螺仪数据显示
            this.label13.Text =   data1_Nomal[0]+"度0号 ";   //陀螺仪0号
            this.label14.Text =   data1_Nomal[1]+"度1号";   //陀螺仪1号
            this.label17.Text =   data1_Nomal[2]+"度2号";   //陀螺仪2号
            this.label18.Text = data1_Nomal[3] + "度3号";   //陀螺仪3号
            this.label19.Text = data1_Nomal[4] + "度4号";   //陀螺仪4号
            this.label20.Text = data1_Nomal[5] + "度5号";   //陀螺仪5号

        }
        /// <summary>
        /// 陀螺仪的接收线程
        /// </summary>
        /// <param name="obj"></param>
        void dj_AskMsg(object obj)
        {
            InitSocket(djip, djport);
            clientEnd2 = new IPEndPoint(IPAddress.Parse("192.168.0.233"), 10001); //已知客户端的情况下
            recvData2 = new byte[20];
            int i = 0;
            while (true)
            {
                if (i == 0)
                {
                    //77 04 00 01 06
                    askMsg[0] = 0x77;
                    askMsg[1] = 0x04;
                    askMsg[2] = 0x00;
                    askMsg[3] = 0x01;
                    askMsg[4] = 0x05;
                }
                else if (i == 1)
                {
                    //77 04 01 01 06
                    askMsg[0] = 0x77;
                    askMsg[1] = 0x04;
                    askMsg[2] = 0x01;
                    askMsg[3] = 0x01;
                    askMsg[4] = 0x06;
                }else if (i == 2)
                {
                    //77 04 02 01 06
                    askMsg[0] = 0x77;
                    askMsg[1] = 0x04;
                    askMsg[2] = 0x02;
                    askMsg[3] = 0x01;
                    askMsg[4] = 0x06;
                }else if (i == 3)
                {
                    //77 04 03 01 06
                    askMsg[0] = 0x77;
                    askMsg[1] = 0x04;
                    askMsg[2] = 0x03;
                    askMsg[3] = 0x01;
                    askMsg[4] = 0x06;
                }else if (i == 4)
                {
                    //77 04 04 01 06
                    askMsg[0] = 0x77;
                    askMsg[1] = 0x04;
                    askMsg[2] = 0x04;
                    askMsg[3] = 0x01;
                    askMsg[4] = 0x06;
                }else if (i == 5)
                {
                    //77 04 05 01 06
                    askMsg[0] = 0x77;
                    askMsg[1] = 0x04;
                    askMsg[2] = 0x05;
                    askMsg[3] = 0x01;
                    askMsg[4] = 0x06;
                }
                if (socket2==null)
                {
                    break;
                }
                socket2.SendTo(askMsg, askMsg.Length, SocketFlags.None, clientEnd2);//当有客户端时应该打开
                recvLen2 = socket2.ReceiveFrom(recvData2, ref clientEnd2);
                data1_Temp = (recvData2[4] & 0x0F) * 100 + ((recvData2[5] & 0xF0) >> 4) * 10 +
                (recvData2[5] & 0x0F) + ((recvData2[6] & 0xF0) >> 4) * 0.1 + (recvData2[6] & 0x0F) * 0.01;
                if ((recvData2[4] & 0xF0) != 0)
                {
                    if (i == 0)
                    {
                        data1_Nomal[0] = data1_Temp * (-1);
                    }
                    if (i == 1)
                    {
                        data1_Nomal[1] = data1_Temp * (-1);
                    }
                    if (i == 2)
                    {
                        data1_Nomal[2] = data1_Temp * (-1);
                    }
                    if (i == 3)
                    {
                        data1_Nomal[3] = data1_Temp * (-1);
                    }
                    if (i == 4)
                    {
                        data1_Nomal[4] = data1_Temp * (-1);
                    }
                    if (i == 5)
                    {
                        data1_Nomal[5] = data1_Temp * (-1);
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        data1_Nomal[0] = data1_Temp;
                    }
                    if (i == 1)
                    {
                        data1_Nomal[1] = data1_Temp;
                    }
                    if (i == 2)
                    {
                        data1_Nomal[2] = data1_Temp;
                    }
                    if (i == 3)
                    {
                        data1_Nomal[3] = data1_Temp;
                    }
                    if (i == 4)
                    {
                        data1_Nomal[4] = data1_Temp;
                    }
                    if (i == 5)
                    {
                        data1_Nomal[5] = data1_Temp;
                    }
                }
                i++;
                if (i >= 6)  //用于判断询问帧的输入
                {
                    i = 0;
                }
            }
        }
        /// <summary>
        /// 初始化套节字 陀螺仪的
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="ConnectPort"></param>
        public void InitSocket(String ipAddress, int ConnectPort)
        {
            if (ipEnd2 == null)
            {
                return;
            }
            ipEnd2 = new IPEndPoint(IPAddress.Parse(ipAddress), ConnectPort);
            socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket2.Bind(ipEnd2);
            //定义客户端 （将客户端信息进行存储）
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            clientEnd2 = (EndPoint)sender;
            Console.WriteLine("客户端" + clientEnd2.ToString() + "已连接");
        }
        /// <summary>
        /// 开启数据开关
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
                t1 = new Thread(dj_AskMsg);//陀螺仪接收数据线程
                timer_Six_Show.Start();
                t1.Start();
                this.button_open.BackColor = Color.SandyBrown;
                this.button_open.Text = "关";
            }
            else if (this.button_open.Text == "关")
            {
                timer_Six_Show.Enabled = false;
                this.button_open.BackColor = Color.FloralWhite;
                if (t1.IsAlive)
                {
                    t1.Abort();
                }
                if (socket2!=null)
                {
                    socket2.Close();
                }
                this.label1.Text = "" + 0;
                this.label2.Text = "" + 0;
                this.label3.Text = "" + 0;
                this.label4.Text = "" + 0;
                this.label15.Text = "" + 0;
                this.label16.Text = "" + 0;

                this.label13.Text = "" + 0;
                this.label14.Text = "" + 0;
                this.button_open.Text = "开";
            }
        }
        /// <summary>
        /// 采集数据(用List接收)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_StartData_Click(object sender, EventArgs e)
        {
            if (this.button_open.Text == "开")
            {
                MessageBox.Show("请先打开接收数据的开关！！！", "提示");
            }
            else
            {
                if (sy6_Lists.Count == 18)
                {
                    MessageBox.Show("试验次数已达到18条，请提交数据试验或者重新试验,及时打印！！！");
                    //button_open.Text = "开";
                    return;
                }
                /*
                    在此填写数据的插入功能
                 */

                sy6_List = new Sy6_List();
                sy6_List.Left_a1 = (float)m_dataScaled[0];
                sy6_List.Left_a2 = (float)m_dataScaled[1]; sy6_List.Left_a3 = (float)m_dataScaled[2];
                sy6_List.Left_tly1 = (float)data1_Nomal[0];
                sy6_List.Left_tly2 = (float)data1_Nomal[1]; sy6_List.Left_tly3 = (float)data1_Nomal[2];
                sy6_List.Right_a1 = (float)m_dataScaled[3];
                sy6_List.Right_a2 = (float)m_dataScaled[4]; sy6_List.Right_a3 = (float)m_dataScaled[5];
                sy6_List.Right_tly1 = (float)data1_Nomal[3];
                sy6_List.Right_tly2 = (float)data1_Nomal[4]; sy6_List.Right_tly3 = (float)data1_Nomal[5];
                sy6_Lists.Add(sy6_List);
                /////////////////////////////
                //////////////////////////
                this.dataGridView1.DataSource = new BindingList<Sy6_List>(sy6_Lists);  //把dattable绑定datagridview
                this.dataGridView1.Columns[0].HeaderText = "左测1";
                this.dataGridView1.Columns[1].HeaderText = "左测2";
                this.dataGridView1.Columns[2].HeaderText = "左测3";
                this.dataGridView1.Columns[3].HeaderText = "左显1";
                this.dataGridView1.Columns[4].HeaderText = "左显2";
                this.dataGridView1.Columns[5].HeaderText = "左显3";
                this.dataGridView1.Columns[6].HeaderText = "右测1";
                this.dataGridView1.Columns[7].HeaderText = "右测2";
                this.dataGridView1.Columns[8].HeaderText = "右测3";
                this.dataGridView1.Columns[9].HeaderText = "右显1";
                this.dataGridView1.Columns[10].HeaderText = "右显2";
                this.dataGridView1.Columns[11].HeaderText = "右显3";

                for (int i = 0; i < 12; i++)
                {
                    this.dataGridView1.Columns[i].Width = 42;
                }
                //选择整行显示数据
                this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //选择是否只读
                this.dataGridView1.ReadOnly = true;
                //选择是否添加一行空白行
                this.dataGridView1.AllowUserToAddRows = false;

                MessageBox.Show("数据采集成功，请继续。。。", "提示");
            }
        }
        /// <summary>
        /// 重新实验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            sy6_Lists.Clear();
            this.dataGridView1.DataSource = new BindingList<Sy6_List>(sy6_Lists);
            Export_All_SY.DBDel(tb);
        }
        /// <summary>
        /// 保存数据(到数据库)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            String sqldata;
            for (int i = 0; i < sy6_Lists.Count; i++)
            {
                DateTime timer = DateTime.Now;
                sqldata = "insert into [SY6_Xtts](ZC1,ZC2,ZC3,ZX1,ZX2,ZX3,YC1,YC2,YC3,YX1,YX2,YX3,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                          " values(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17)";
                SqlHelper.ExecuteNonQuery(sqldata,
                    new SqlParameter("@a1", sy6_Lists[i].Left_a1),
                    new SqlParameter("@a2", sy6_Lists[i].Left_a2), new SqlParameter("@a3", sy6_Lists[i].Left_a3),
                    new SqlParameter("@a4", sy6_Lists[i].Left_tly1),
                    new SqlParameter("@a5", sy6_Lists[i].Left_tly2), new SqlParameter("@a6", sy6_Lists[i].Left_tly3),
                    new SqlParameter("@a7", sy6_Lists[i].Right_a1),
                    new SqlParameter("@a8", sy6_Lists[i].Right_a2), new SqlParameter("@a9", sy6_Lists[i].Right_a3),
                    new SqlParameter("@a10", sy6_Lists[i].Right_tly1),
                    new SqlParameter("@a11", sy6_Lists[i].Right_tly2), new SqlParameter("@a12", sy6_Lists[i].Right_tly3),
                    new SqlParameter("@a13", timer),
                    new SqlParameter("@a14", MainFrom.Uname),
                    new SqlParameter("@a15", MainFrom.comBoxData_Form),
                    new SqlParameter("@a16", MainFrom.textB_HbData_Form),
                    new SqlParameter("@a17", MainFrom.textB_CgqData_Form));
            }
            sy6_Lists.Clear();
            this.dataGridView1.DataSource = new BindingList<Sy6_List>(sy6_Lists);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Export_One_SY.Exp_SY6();
        }
    }


    class Sy6_List
    {
        float left_a1, left_a2, left_a3, left_tly1, left_tly2, left_tly3;
        float right_a1, right_a2, right_a3, right_tly1, right_tly2, right_tly3;

        public float Left_a1 { get => left_a1; set => left_a1 = value; }
        public float Left_a2 { get => left_a2; set => left_a2 = value; }
        public float Left_a3 { get => left_a3; set => left_a3 = value; }
        public float Left_tly1 { get => left_tly1; set => left_tly1 = value; }
        public float Left_tly2 { get => left_tly2; set => left_tly2 = value; }
        public float Left_tly3 { get => left_tly3; set => left_tly3 = value; }
        public float Right_a1 { get => right_a1; set => right_a1 = value; }
        public float Right_a2 { get => right_a2; set => right_a2 = value; }
        public float Right_a3 { get => right_a3; set => right_a3 = value; }
        public float Right_tly1 { get => right_tly1; set => right_tly1 = value; }
        public float Right_tly2 { get => right_tly2; set => right_tly2 = value; }
        public float Right_tly3 { get => right_tly3; set => right_tly3 = value; }
    }
}
