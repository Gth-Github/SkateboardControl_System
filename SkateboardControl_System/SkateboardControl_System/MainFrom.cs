using Automation.BDaq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsApplication1;

namespace SkateboardControl_System
{
    public partial class MainFrom : Form
    {
        //数字信号采集
        #region
        private Label[] m_portNum;
        private Label[] m_portHex;
        private PictureBox[,] m_pictrueBox;
        private const int m_startPort = 0; 
        private const int m_portCountShow = 2;
        public static int Data_pictureBox00, Data_pictureBox01, Data_pictureBox02, Data_pictureBox03, Data_pictureBox04,
            Data_pictureBox05, Data_pictureBox06, Data_pictureBox07, Data_pictureBox10, Data_pictureBox11, Data_pictureBox12,
            Data_pictureBox13, Data_pictureBox14, Data_pictureBox15, Data_pictureBox16, Data_pictureBox17;
        #endregion
        //模拟信号采集
        #region
        private const int CHANEL_COUNT_MAX = 16;//最大通道数16
        //定义一个数组，存放各个通道的模拟量
        public double[] m_dataScaled = new double[CHANEL_COUNT_MAX];
        //设置开始的通道（从0开始，因为采集板提供调用函数用）
        private int comboBox_chanStart = 0;
        //设置使用的通道数（根据连接的通道数进行设置） 
        private int chanCountSet = 4;
        #endregion
        //窗体的相关量
        #region
        public static string Uname;
        FristFrom1 fristFrom1 = null;
        SecondFrom2 secondFrom = null;
        ThirdForm3 thirdForm = null;
        EighthForm8 eighthForm = null;
        FourthForm4 fourthForm = null;
        FifthForm5 fifthForm = null;
        SixForm6 sixForm = null;
        //模拟信号的采集函数（定时器实现）
        FifthForm5 fifth = new FifthForm5(); //用于传递数据
        SeventhForm7 seventhForm = null;
        #endregion
        #region
        public static string comBoxData_Form, textB_HbData_Form, textB_CgqData_Form;
        #endregion
        
        /// <summary>
        /// 无参构造
        /// </summary>
        public MainFrom()
        {
            InitializeComponent();
            LoadShouye();
            this.WindowState = FormWindowState.Maximized;    //最大化窗体
        }
        /// <summary>
        /// 打开采集卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //The default device of project is demo device, users can choose other devices according to their needs. 
            //默认选择Demo的设配，用户通过设置选择自己的设配
            if (!instantDiCtrl1.Initialized)
            {
                MessageBox.Show("No device be selected or device open failed!", "StaticDI");
                //this.Close();
                return;
            }
            this.Text = "Static DI(" + instantDiCtrl1.SelectedDevice.Description + ")";
            m_portNum = new Label[m_portCountShow] { PortNum0, PortNum1 };
            m_portHex = new Label[m_portCountShow] { PortHex0, PortHex1 };
            m_pictrueBox = new PictureBox[m_portCountShow, 8]{
             {pictureBox00, pictureBox01, pictureBox02, pictureBox03, pictureBox04, pictureBox05,pictureBox06, pictureBox07},
             {pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15,pictureBox16, pictureBox17} };
            this.timer1.Enabled = true;//组件加载后，立即开启数字信号采集的定时器
            //timer_getDataAI.Enabled = true;// 关闭模拟信号的采集
            this.label15.Text = Uname;//主界面加载登陆人员信息
        }
        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="deviceNumber"></param>
        public MainFrom(int deviceNumber)  
        {
            InitializeComponent();
            LoadShouye();
            this.WindowState = FormWindowState.Maximized;    //最大化窗体
            instantDiCtrl1.SelectedDevice = new DeviceInformation(deviceNumber);
        }
        /// <summary>
        /// 模拟信号的采集函数（定时器实现）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_getDataAI_Tick(object sender, EventArgs e)
        {
            PerformanceCounter performanceCounter = new PerformanceCounter();
            ErrorCode err;
            performanceCounter.Start();
            err = instantAiCtrl1.Read(comboBox_chanStart, chanCountSet, m_dataScaled);
            if (err != ErrorCode.Success)
            {
                HandleError(err);
                timer_getDataAI.Stop();
            }
            //fifth.getAiData(m_dataScaled);//将模拟数据的值传到FifthForm页面使用
            performanceCounter.Stop();
        }
        /// <summary>
        /// 和数字信号的显示部分相关
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstantDiForm_Load(object sender, EventArgs e)
        {
            //The default device of project is demo device, users can choose other devices according to their needs. 
            //默认选择Demo的设配，用户通过设置选择自己的设配
            if (!instantDiCtrl1.Initialized)
            {
                MessageBox.Show("No device be selected or device open failed!", "StaticDI");
                //this.Close();
                return;
            }
            this.Text = "Static DI(" + instantDiCtrl1.SelectedDevice.Description + ")";
            m_portNum = new Label[m_portCountShow] { PortNum0, PortNum1 };
            m_portHex = new Label[m_portCountShow] { PortHex0, PortHex1 };
            m_pictrueBox = new PictureBox[m_portCountShow, 8]{
             {pictureBox00, pictureBox01, pictureBox02, pictureBox03, pictureBox04, pictureBox05,pictureBox06, pictureBox07},
             {pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15,pictureBox16, pictureBox17} };
            this.timer1.Enabled = true;//组件加载后，立即开启数字信号采集的定时器
            //timer_getDataAI.Enabled = true;// 关闭模拟信号的采集
            this.label15.Text = Uname;//主界面加载登陆人员信息
        }
        /// <summary>
        /// 数字信号的采集函数（定时器实现）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            byte portData = 0;     
            ErrorCode err = ErrorCode.Success;
            for (int i = 0; (i + m_startPort) < instantDiCtrl1.Features.PortCount && i < m_portCountShow; ++i)
            {
                err = instantDiCtrl1.Read(i + m_startPort, out portData);
                if (err != ErrorCode.Success)
                {
                    timer1.Enabled = false;
                    HandleError(err);
                    return;
                }
                m_portNum[i].Text = (i + m_startPort).ToString();
                m_portHex[i].Text = portData.ToString("X2");
                /********************************************************************************/
                if (i == 0)
                {
                    //红表示得到的数值为0，绿表示得到的数值为1
                    Data_pictureBox00 = (~portData >> 0) & 0x1;  //00手动
                    Data_pictureBox01 = (~portData >> 1) & 0x1;  //01程控
                    Data_pictureBox02 = (~portData >> 2) & 0x1;  //02应急 
                    Data_pictureBox03 = (~portData >> 3) & 0x1;                   //03预留
                    Data_pictureBox04 = (~portData >> 4) & 0x1;  //04首上板——收
                    Data_pictureBox05 = (~portData >> 5) & 0x1;  //05首上板——放
                    Data_pictureBox06 = (~portData >> 6) & 0x1;  //06首下板——收
                    Data_pictureBox07 = (~portData >> 7) & 0x1;  //07首下板——放
                }
                else
                {
                    Data_pictureBox10 = (~portData >> 0) & 0x1;  //10尾翼板——收
                    Data_pictureBox11 = (~portData >> 1) & 0x1;  //11尾翼板——放
                    Data_pictureBox12 = (~portData >> 2) & 0x1;  //12陆上
                    Data_pictureBox13 = (~portData >> 3) & 0x1;  //13低速
                    Data_pictureBox14 = (~portData >> 4) & 0x1;  //14中速
                    Data_pictureBox15 = (~portData >> 5) & 0x1;  //15高速
                    Data_pictureBox16 = (~portData >> 6) & 0x1;                     //16预留
                    Data_pictureBox17 = (~portData >> 7) & 0x1;                     //17预留
                }
            //    FifthForm.getDi_Data( Data_pictureBox00,  Data_pictureBox01,  Data_pictureBox02,  Data_pictureBox03,  Data_pictureBox04,
            //Data_pictureBox05,  Data_pictureBox06,  Data_pictureBox07,  Data_pictureBox10,  Data_pictureBox11,  Data_pictureBox12,
            //Data_pictureBox13,  Data_pictureBox14,  Data_pictureBox15,  Data_pictureBox16,  Data_pictureBox17);
                for (int j = 0; j < 8; ++j)
                {
                    m_pictrueBox[i, j].Image = imageList1.Images[(portData >> j) & 0x1];
                    m_pictrueBox[i, j].Invalidate();
                }
            }
        }
        /// <summary>
        /// 采集器加载失败处理函数
        /// </summary>
        /// <param name="err"></param>
        private void HandleError(ErrorCode err)
        {
            if ((err >= ErrorCode.ErrorHandleNotValid) && (err != ErrorCode.Success))
            {
                MessageBox.Show("Sorry ! Some errors happened, the error code is: " + err.ToString(), "Static DI");
            }
        }
        
        /// <summary>
        /// 加载首页按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fanhuishouye_Click(object sender, EventArgs e)
        {
            LoadShouye();
        }
        /// <summary>
        /// 用于加载设备信息设置界面，保证进行了数据的设置
        /// </summary>
        /// <returns></returns>
        private Boolean ShouyeDataChecked()
        {
            if (MainFrom.textB_CgqData_Form == null || MainFrom.textB_HbData_Form == null || MainFrom.comBoxData_Form == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 加载首页对应的方法
        /// </summary>
        private void LoadShouye()
        {
            
            ShouyeForm shouyeForm = new ShouyeForm();
            shouyeForm.TopLevel = false;
            shouyeForm.Dock = DockStyle.Fill;
            shouyeForm.FormBorderStyle = FormBorderStyle.None;
            if (PanelRightMain.Controls != null)
            {
                PanelRightMain.Controls.Clear();
            }
            PanelRightMain.Controls.Add(shouyeForm);
            shouyeForm.Show();
            //设置按钮颜色，点中变色，以增强实用性。
            this.fanhuishouye.BackColor = Color.Red;
            this.FristFrom_button.BackColor = Color.White;
            this.SecondFrombutton.BackColor = Color.White;
            this.ThrifFrom_button.BackColor = Color.White;
            this.FourthFrom_button.BackColor = Color.White;
            this.FifthFrom_button.BackColor = Color.White;
            this.SixFrom_button.BackColor = Color.White;
            this.SeventhFrom_button.BackColor = Color.White;
            this.EighthFrom_button.BackColor = Color.White;
        }
        /// <summary>
        /// 第一个实验 ：加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FristFrom_button_Click(object sender, EventArgs e)
        {
            Load_First();
            
        }

        public void Load_First()
        {
            if (!ShouyeDataChecked())
            {
                MessageBox.Show("首页参数设置不完整，试验无法进行！！！");
                return;
            }
            //MessageBox.Show(textB_CgqData_Form+comBoxData_Form+textB_HbData_Form);
            fristFrom1 = new FristFrom1();  //创建新窗体
            fristFrom1.TopLevel = false;               //Form.TopLevel 获取或设置一个值，该值指示是否将窗体显示为顶级窗口。
            fristFrom1.Dock = DockStyle.Fill;          //把子窗体设置为控件 
            fristFrom1.FormBorderStyle = FormBorderStyle.None;
            if (PanelRightMain.Controls != null)   //先判断窗体是否包含其他内容，情况载加载其他内容
            {
                PanelRightMain.Controls.Clear();
            }
            PanelRightMain.Controls.Add(fristFrom1);
            fristFrom1.Show();
            //设置按钮颜色，点中变色，以增强实用性。
            this.fanhuishouye.BackColor = Color.White;
            this.FristFrom_button.BackColor = Color.Red;
            this.SecondFrombutton.BackColor = Color.White;
            this.ThrifFrom_button.BackColor = Color.White;
            this.FourthFrom_button.BackColor = Color.White;
            this.FifthFrom_button.BackColor = Color.White;
            this.SixFrom_button.BackColor = Color.White;
            this.SeventhFrom_button.BackColor = Color.White;
            this.EighthFrom_button.BackColor = Color.White;
        }
        /// <summary>
        /// 第二个实验 ：加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondFrombutton_Click(object sender, EventArgs e)
        {
            if (!ShouyeDataChecked())
            {
                MessageBox.Show("首页参数设置不完整，试验无法进行！！！");
                return;
            }
            secondFrom = new SecondFrom2();
            secondFrom.TopLevel = false;
            secondFrom.Dock = DockStyle.Fill;
            secondFrom.FormBorderStyle = FormBorderStyle.None;
            if (PanelRightMain.Controls != null)  
            {
                PanelRightMain.Controls.Clear();
            }
            PanelRightMain.Controls.Add(secondFrom);
            secondFrom.Show();
            //设置按钮颜色，点中变色，以增强实用性。
            this.fanhuishouye.BackColor = Color.White;
            this.FristFrom_button.BackColor = Color.White;
            this.SecondFrombutton.BackColor = Color.Red;
            this.ThrifFrom_button.BackColor = Color.White;
            this.FourthFrom_button.BackColor = Color.White;
            this.FifthFrom_button.BackColor = Color.White;
            this.SixFrom_button.BackColor = Color.White;
            this.SeventhFrom_button.BackColor = Color.White;
            this.EighthFrom_button.BackColor = Color.White;
        }
        /// <summary>
        /// 第三个实验 ：加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThrifFrom_button_Click(object sender, EventArgs e)
        {
            if (!ShouyeDataChecked())
            {
                MessageBox.Show("首页参数设置不完整，试验无法进行！！！");
                return;
            }
            thirdForm = new ThirdForm3();
            thirdForm.TopLevel = false;
            thirdForm.Dock = DockStyle.Fill;
            thirdForm.FormBorderStyle = FormBorderStyle.None;
            if (PanelRightMain.Controls != null)
            {
                PanelRightMain.Controls.Clear();
            }
            PanelRightMain.Controls.Add(thirdForm);
            thirdForm.Show();
            //设置按钮颜色，点中变色，以增强实用性。
            this.fanhuishouye.BackColor = Color.White;
            this.FristFrom_button.BackColor = Color.White;
            this.SecondFrombutton.BackColor = Color.White;
            this.ThrifFrom_button.BackColor = Color.Red;
            this.FourthFrom_button.BackColor = Color.White;
            this.FifthFrom_button.BackColor = Color.White;
            this.SixFrom_button.BackColor = Color.White;
            this.SeventhFrom_button.BackColor = Color.White;
            this.EighthFrom_button.BackColor = Color.White;
        }
        /// <summary>
        /// 第八试验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EighthFrom_button_Click(object sender, EventArgs e)
        {
            if (!ShouyeDataChecked())
            {
                MessageBox.Show("首页参数设置不完整，试验无法进行！！！");
                return;
            }
            if (MainFrom.comBoxData_Form != "HBK-I")
            {
                MessageBox.Show("II型车无火炮试验，试验无法进行！！！");
                return;
            }
            eighthForm = new EighthForm8();
            eighthForm.TopLevel = false;
            eighthForm.Dock = DockStyle.Fill;
            eighthForm.FormBorderStyle = FormBorderStyle.None;
            if (PanelRightMain.Controls != null)
            {
                PanelRightMain.Controls.Clear();
            }
            PanelRightMain.Controls.Add(eighthForm);
            eighthForm.Show();
            //设置按钮颜色，点中变色，以增强实用性。
            this.fanhuishouye.BackColor = Color.White;
            this.FristFrom_button.BackColor = Color.White;
            this.SecondFrombutton.BackColor = Color.White;
            this.ThrifFrom_button.BackColor = Color.White;
            this.FourthFrom_button.BackColor = Color.White;
            this.FifthFrom_button.BackColor = Color.White;
            this.SixFrom_button.BackColor = Color.White;
            this.SeventhFrom_button.BackColor = Color.White;
            this.EighthFrom_button.BackColor = Color.Red;
        }
        /// <summary>
        /// 第四个试验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FourthFrom_button_Click(object sender, EventArgs e)
        {
            if (!ShouyeDataChecked())
            {
                MessageBox.Show("首页参数设置不完整，试验无法进行！！！");
                return;
            }
            fourthForm = new FourthForm4();
            fourthForm.TopLevel = false;
            fourthForm.Dock = DockStyle.Fill;
            fourthForm.FormBorderStyle = FormBorderStyle.None;
            if (PanelRightMain.Controls != null)
            {
                PanelRightMain.Controls.Clear();
            }
            PanelRightMain.Controls.Add(fourthForm);
            fourthForm.Show();
            //设置按钮颜色，点中变色，以增强实用性。
            this.fanhuishouye.BackColor = Color.White;
            this.FristFrom_button.BackColor = Color.White;
            this.SecondFrombutton.BackColor = Color.White;
            this.ThrifFrom_button.BackColor = Color.White;
            this.FourthFrom_button.BackColor = Color.Red;
            this.FifthFrom_button.BackColor = Color.White;
            this.SixFrom_button.BackColor = Color.White;
            this.SeventhFrom_button.BackColor = Color.White;
            this.EighthFrom_button.BackColor = Color.White;
        }
        /// <summary>
        /// 第五个试验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FifthFrom_button_Click(object sender, EventArgs e)
        {
            if (!ShouyeDataChecked())
            {
                MessageBox.Show("首页参数设置不完整，试验无法进行！！！");
                return;
            }
            fifthForm = new FifthForm5();
            fifthForm.TopLevel = false;
            fifthForm.Dock = DockStyle.Fill;
            fifthForm.FormBorderStyle = FormBorderStyle.None;
            if (PanelRightMain.Controls != null)
            {
                PanelRightMain.Controls.Clear();
            }
            PanelRightMain.Controls.Add(fifthForm);
            fifthForm.Show();
            //设置按钮颜色，点中变色，以增强实用性。
            this.fanhuishouye.BackColor = Color.White;
            this.FristFrom_button.BackColor = Color.White;
            this.SecondFrombutton.BackColor = Color.White;
            this.ThrifFrom_button.BackColor = Color.White;
            this.FourthFrom_button.BackColor = Color.White;
            this.FifthFrom_button.BackColor = Color.Red;
            this.SixFrom_button.BackColor = Color.White;
            this.SeventhFrom_button.BackColor = Color.White;
            this.EighthFrom_button.BackColor = Color.White;
        }
        /// <summary>
        /// 第六个试验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SixFrom_button_Click(object sender, EventArgs e)
        {
            if (!ShouyeDataChecked())
            {
                MessageBox.Show("首页参数设置不完整，试验无法进行！！！");
                return;
            }
            sixForm = new SixForm6();
            sixForm.TopLevel = false;
            sixForm.Dock = DockStyle.Fill;
            sixForm.FormBorderStyle = FormBorderStyle.None;
            if (PanelRightMain.Controls != null)
            {
                PanelRightMain.Controls.Clear();
            }
            PanelRightMain.Controls.Add(sixForm);
            sixForm.Show();
            //设置按钮颜色，点中变色，以增强实用性。
            this.fanhuishouye.BackColor = Color.White;
            this.FristFrom_button.BackColor = Color.White;
            this.SecondFrombutton.BackColor = Color.White;
            this.ThrifFrom_button.BackColor = Color.White;
            this.FourthFrom_button.BackColor = Color.White;
            this.FifthFrom_button.BackColor = Color.White;
            this.SixFrom_button.BackColor = Color.Red;
            this.SeventhFrom_button.BackColor = Color.White;
            this.EighthFrom_button.BackColor = Color.White;
        }
        /// <summary>
        /// 第七试验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeventhFrom_button_Click(object sender, EventArgs e)
        {
            if (!ShouyeDataChecked())
            {
                MessageBox.Show("首页参数设置不完整，试验无法进行！！！");
                return;
            }
            seventhForm = new SeventhForm7();
            seventhForm.TopLevel = false;
            seventhForm.Dock = DockStyle.Fill;
            seventhForm.FormBorderStyle = FormBorderStyle.None;
            if (PanelRightMain.Controls != null)
            {
                PanelRightMain.Controls.Clear();    
            }    
            PanelRightMain.Controls.Add(seventhForm);
            seventhForm.Show();
            //设置按钮颜色，点中变色，以增强实用性。
            this.fanhuishouye.BackColor = Color.White;
            this.FristFrom_button.BackColor = Color.White;
            this.SecondFrombutton.BackColor = Color.White;
            this.ThrifFrom_button.BackColor = Color.White;
            this.FourthFrom_button.BackColor = Color.White;
            this.FifthFrom_button.BackColor = Color.White;
            this.SixFrom_button.BackColor = Color.White;
            this.SeventhFrom_button.BackColor = Color.Red;
            this.EighthFrom_button.BackColor = Color.White;
        }
    }
}
