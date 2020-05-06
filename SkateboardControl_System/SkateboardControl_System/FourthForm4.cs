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
    public partial class FourthForm4 : Form
    {
        int num;
        string StringNum;
        int[] check = new int[3] { 0,0,0};
        string beizhu = "";
        int count;
        //添加显示内容
        List<Sy4_List> sy4_Lists = new List<Sy4_List>();
        public FourthForm4()
        {
            InitializeComponent();
            timer1.Start();
            //数据显示
            this.dataGridView1.DataSource = new BindingList<Sy4_List>(sy4_Lists);  //把dattable绑定datagridview
            this.dataGridView1.Columns[0].HeaderText = "首上板动作";
            this.dataGridView1.Columns[1].HeaderText = "首上板动作";
            this.dataGridView1.Columns[2].HeaderText = "尾翼板动作";
            this.dataGridView1.Columns[3].HeaderText = "备注";
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

        private void button1_Click(object sender, EventArgs e)
        {
            StringNum = cobB1.Text;
            beizhu = textB_beizhu.Text.Trim();
            if (!"".Equals(StringNum))
            {
                num = Convert.ToInt32(StringNum);
            }
            else
            {
                MessageBox.Show("请输入试验次数！！！");
                return;
            }
            String sqlsel = ("select count(*) count from [SY4_Lxsb] where Sy_user=@SYY and C_no=@cno and P_no=@pno and Angle_no=@ano");
            DataSet ds = SqlHelper.ExecuteDataSet(sqlsel, new SqlParameter("@SYY", MainFrom.Uname),
                new SqlParameter("@cno", MainFrom.textB_HbData_Form),
                new SqlParameter("@pno", MainFrom.comBoxData_Form),
                new SqlParameter("@ano", MainFrom.textB_CgqData_Form));
            count = (int)ds.Tables[0].Rows[0]["count"];
            ///以下代码为了更改下拉菜单内容
            this.cobB1.Items.Clear();
            switch (count)
            {
                case 0:
                    string[] items0 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };
                    this.cobB1.Items.AddRange(items0);
                    break;
                case 1:
                    string[] items1 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };
                    this.cobB1.Items.AddRange(items1);
                    break;
                case 2:
                    string[] items2 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18" };
                    this.cobB1.Items.AddRange(items2);
                    break;
                case 3:
                    string[] items3 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17" };
                    this.cobB1.Items.AddRange(items3);
                    break;
                case 4:
                    string[] items4 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };
                    this.cobB1.Items.AddRange(items4);
                    break;
                case 5:
                    string[] items5 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" };
                    this.cobB1.Items.AddRange(items5);
                    break;
                case 6:
                    string[] items6 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14" };
                    this.cobB1.Items.AddRange(items6);
                    break;
                case 7:
                    string[] items7 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" };
                    this.cobB1.Items.AddRange(items7);
                    break;
                case 8:
                    string[] items8 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
                    this.cobB1.Items.AddRange(items8);
                    break;
                case 9:
                    string[] items9 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };
                    this.cobB1.Items.AddRange(items9);
                    break;
                case 10:
                    string[] items10 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
                    this.cobB1.Items.AddRange(items10);
                    break;
                case 11:
                    string[] items11 = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                    this.cobB1.Items.AddRange(items11);
                    break;
                case 12:
                    string[] items12 = { "1", "2", "3", "4", "5", "6", "7", "8" };
                    this.cobB1.Items.AddRange(items12);
                    break;
                case 13:
                    string[] items13 = { "1", "2", "3", "4", "5", "6", "7" };
                    this.cobB1.Items.AddRange(items13);
                    break;
                case 14:
                    string[] items14 = { "1", "2", "3", "4", "5", "6" };
                    this.cobB1.Items.AddRange(items14);
                    break;
                case 15:
                    string[] items15 = { "1", "2", "3", "4", "5" };
                    this.cobB1.Items.AddRange(items15);
                    break;
                case 16:
                    string[] items16 = { "1", "2", "3", "4" };
                    this.cobB1.Items.AddRange(items16);
                    break;
                case 17:
                    string[] items17 = { "1", "2", "3" };
                    this.cobB1.Items.AddRange(items17);
                    break;
                case 18:
                    string[] items18 = { "1", "2" };
                    this.cobB1.Items.AddRange(items18);
                    break;
                case 19:
                    string[] items19 = { "1" };
                    this.cobB1.Items.AddRange(items19);
                    break;
                case 20:
                    string[] items20 = { "0" };
                    this.cobB1.Items.AddRange(items20);
                    this.cobB1.Text = "0";
                    break;
            }
            if (count < 20)
            {
                int subcount = 20 - count;
                if (chB1.Checked == true)
                {
                    check[0] = 1;
                }else
                {
                    check[0] = 0;
                }
                if (chB2.Checked == true)
                {
                    check[1] = 1;
                }else
                {
                    check[1] = 0;
                }
                if (chB3.Checked == true)
                {
                    check[2] = 1;
                }else
                {
                    check[2] = 0;
                }
                if (num > (20 - count))
                {
                    DialogResult dr = MessageBox.Show("当前已经做了" + count + "次试验，标准需做20次试验。是否插入前" + (20 - count) + "条数据",
                        "确定插入",  MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK) num = 20 - count;
                    else
                    {
                        MessageBox.Show("请重新输入试验次数，要求试验次数小于等于"+(20-count));
                        return;
                    }
                }
                String sqldata;
                Sy4_List sy4_List = new Sy4_List();
                for (int i = 0; i < num; i++)
                {
                    DateTime timer = DateTime.Now;  
                    sqldata = "insert into [SY4_Lxsb](SS_data,SX_data,WY_data,Beizhu,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                        " values(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)";
                    SqlHelper.ExecuteNonQuery(sqldata,
                        new SqlParameter("@a1", check[0]),
                        new SqlParameter("@a2", check[1]),
                        new SqlParameter("@a3", check[2]),
                        new SqlParameter("@a4", beizhu),
                        new SqlParameter("@a5", timer),
                        new SqlParameter("@a6", MainFrom.Uname),
                        new SqlParameter("@a7", MainFrom.comBoxData_Form),
                        new SqlParameter("@a8", MainFrom.textB_HbData_Form),
                        new SqlParameter("@a9", MainFrom.textB_CgqData_Form));
                    if (check[0] == 1)
                    {
                        sy4_List.Chek1 = "正常";
                    } else
                    {
                        sy4_List.Chek1 = "不正常";
                    }
                    if (check[1] == 1)
                    {
                        sy4_List.Chek2 = "正常";
                    }else
                    {
                        sy4_List.Chek2 = "不正常";
                    }
                    if (check[2] == 1)
                    {
                        sy4_List.Chek3 = "正常";
                    }else
                    {
                        sy4_List.Chek3 = "不正常";
                    }
                    sy4_List.Beizhu = beizhu;
                    sy4_Lists.Add(sy4_List);
                    this.dataGridView1.DataSource = new BindingList<Sy4_List>(sy4_Lists);  //把dattable绑定datagridview
                    this.dataGridView1.Columns[0].HeaderText = "首上板动作";
                    this.dataGridView1.Columns[1].HeaderText = "首上板动作";
                    this.dataGridView1.Columns[2].HeaderText = "尾翼板动作";
                    this.dataGridView1.Columns[3].HeaderText = "备注";
                    //选择整行显示数据
                    this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    //选择是否只读
                    this.dataGridView1.ReadOnly = true;
                    //选择是否添加一行空白行
                    this.dataGridView1.AllowUserToAddRows = false;
                }
                MessageBox.Show("数据提交成功，记得及时打印！！！");
            }
            else
            {
                MessageBox.Show("数据库中已有20条数据，请进行下一个实验或点击重新实验");
            }
            ///以下代码为了更改下拉菜单内容
            
            int count1 = getCount();
            this.cobB1.Items.Clear();
            switch (count)
            {
                case 0:
                    string[] items0 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };
                    this.cobB1.Items.AddRange(items0);
                    break;
                case 1:
                    string[] items1 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };
                    this.cobB1.Items.AddRange(items1);
                    break;
                case 2:
                    string[] items2 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18" };
                    this.cobB1.Items.AddRange(items2);
                    break;
                case 3:
                    string[] items3 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17" };
                    this.cobB1.Items.AddRange(items3);
                    break;
                case 4:
                    string[] items4 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };
                    this.cobB1.Items.AddRange(items4);
                    break;
                case 5:
                    string[] items5 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" };
                    this.cobB1.Items.AddRange(items5);
                    break;
                case 6:
                    string[] items6 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14" };
                    this.cobB1.Items.AddRange(items6);
                    break;
                case 7:
                    string[] items7 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" };
                    this.cobB1.Items.AddRange(items7);
                    break;
                case 8:
                    string[] items8 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
                    this.cobB1.Items.AddRange(items8);
                    break;
                case 9:
                    string[] items9 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };
                    this.cobB1.Items.AddRange(items9);
                    break;
                case 10:
                    string[] items10 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
                    this.cobB1.Items.AddRange(items10);
                    break;
                case 11:
                    string[] items11 = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                    this.cobB1.Items.AddRange(items11);
                    break;
                case 12:
                    string[] items12 = { "1", "2", "3", "4", "5", "6", "7", "8" };
                    this.cobB1.Items.AddRange(items12);
                    break;
                case 13:
                    string[] items13 = { "1", "2", "3", "4", "5", "6", "7" };
                    this.cobB1.Items.AddRange(items13);
                    break;
                case 14:
                    string[] items14 = { "1", "2", "3", "4", "5", "6" };
                    this.cobB1.Items.AddRange(items14);
                    break;
                case 15:
                    string[] items15 = { "1", "2", "3", "4", "5" };
                    this.cobB1.Items.AddRange(items15);
                    break;
                case 16:
                    string[] items16 = { "1", "2", "3", "4" };
                    this.cobB1.Items.AddRange(items16);
                    break;
                case 17:
                    string[] items17 = { "1", "2", "3" };
                    this.cobB1.Items.AddRange(items17);
                    break;
                case 18:
                    string[] items18 = { "1", "2" };
                    this.cobB1.Items.AddRange(items18);
                    break;
                case 19:
                    string[] items19 = { "1" };
                    this.cobB1.Items.AddRange(items19);
                    break;
                case 20:
                    string[] items20 = { "0" };
                    this.cobB1.Items.AddRange(items20);
                    this.cobB1.Text = "0";
                    break;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tb = "[SY4_Lxsb]";
            Export_All_SY.DBDel(tb);
            ////////////////////////////////////////////////////////
            ///以下代码为了更改下拉菜单内容
            count = getCount();
            this.cobB1.Items.Clear();
            switch (count)
            {
                case 0:
                    string[] items0 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };
                    this.cobB1.Items.AddRange(items0);
                    break;
                case 1:
                    string[] items1 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };
                    this.cobB1.Items.AddRange(items1);
                    break;
                case 2:
                    string[] items2 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18" };
                    this.cobB1.Items.AddRange(items2);
                    break;
                case 3:
                    string[] items3 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17" };
                    this.cobB1.Items.AddRange(items3);
                    break;
                case 4:
                    string[] items4 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };
                    this.cobB1.Items.AddRange(items4);
                    break;
                case 5:
                    string[] items5 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" };
                    this.cobB1.Items.AddRange(items5);
                    break;
                case 6:
                    string[] items6 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14" };
                    this.cobB1.Items.AddRange(items6);
                    break;
                case 7:
                    string[] items7 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" };
                    this.cobB1.Items.AddRange(items7);
                    break;
                case 8:
                    string[] items8 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
                    this.cobB1.Items.AddRange(items8);
                    break;
                case 9:
                    string[] items9 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };
                    this.cobB1.Items.AddRange(items9);
                    break;
                case 10:
                    string[] items10 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
                    this.cobB1.Items.AddRange(items10);
                    break;
                case 11:
                    string[] items11 = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                    this.cobB1.Items.AddRange(items11);
                    break;
                case 12:
                    string[] items12 = { "1", "2", "3", "4", "5", "6", "7", "8" };
                    this.cobB1.Items.AddRange(items12);
                    break;
                case 13:
                    string[] items13 = { "1", "2", "3", "4", "5", "6", "7" };
                    this.cobB1.Items.AddRange(items13);
                    break;
                case 14:
                    string[] items14 = { "1", "2", "3", "4", "5", "6" };
                    this.cobB1.Items.AddRange(items14);
                    break;
                case 15:
                    string[] items15 = { "1", "2", "3", "4", "5" };
                    this.cobB1.Items.AddRange(items15);
                    break;
                case 16:
                    string[] items16 = { "1", "2", "3", "4" };
                    this.cobB1.Items.AddRange(items16);
                    break;
                case 17:
                    string[] items17 = { "1", "2", "3" };
                    this.cobB1.Items.AddRange(items17);
                    break;
                case 18:
                    string[] items18 = { "1", "2" };
                    this.cobB1.Items.AddRange(items18);
                    break;
                case 19:
                    string[] items19 = { "1" };
                    this.cobB1.Items.AddRange(items19);
                    break;
                case 20:
                    string[] items20 = { };
                    this.cobB1.Items.AddRange(items20);
                    this.cobB1.Text = "0";
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int count = getCount();
            this.label3.Text = "" + count;
            if (count == 0)
            {
                sy4_Lists.Clear();
                this.dataGridView1.DataSource = new BindingList<Sy4_List>(sy4_Lists);  //把dattable绑定datagridview
                this.dataGridView1.Columns[0].HeaderText = "首上板动作";
                this.dataGridView1.Columns[1].HeaderText = "首上板动作";
                this.dataGridView1.Columns[2].HeaderText = "尾翼板动作";
                this.dataGridView1.Columns[3].HeaderText = "备注";
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Export_One_SY.Exp_SY3_SY4();
        }
        /// <summary>
        /// 为记录试验数据封装的LIST集合类
        /// </summary>
        class Sy4_List
        {
            string chek1;
            string chek2;
            string chek3;
            string beizhu;

            public string Chek1 { get => chek1; set => chek1 = value; }
            public string Chek2 { get => chek2; set => chek2 = value; }
            public string Chek3 { get => chek3; set => chek3 = value; }
            public string Beizhu { get => beizhu; set => beizhu = value; }
        }
       /// <summary>
       /// 窗体加载函数，里面内容用于加载下拉菜单内容
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void FourthForm4_Load(object sender, EventArgs e)
        {
            ///以下代码为了更改下拉菜单内容
            int count = getCount();
            this.cobB1.Items.Clear();
            switch (count)
            {
                
                case 0:
                    string[] items0 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };
                    
                    this.cobB1.Items.AddRange(items0);
                    break;
                case 1:
                    string[] items1 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19" };
                    
                    this.cobB1.Items.AddRange(items1);
                    break;
                case 2:
                    string[] items2 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18" };
                    this.cobB1.Items.AddRange(items2);
                    break;
                case 3:
                    string[] items3 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17" };
                    this.cobB1.Items.AddRange(items3);
                    break;
                case 4:
                    string[] items4 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };
                    this.cobB1.Items.AddRange(items4);
                    break;
                case 5:
                    string[] items5 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" };
                    this.cobB1.Items.AddRange(items5);
                    break;
                case 6:
                    string[] items6 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14" };
                    this.cobB1.Items.AddRange(items6);
                    break;
                case 7:
                    string[] items7 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" };
                    this.cobB1.Items.AddRange(items7);
                    break;
                case 8:
                    string[] items8 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
                    this.cobB1.Items.AddRange(items8);
                    break;
                case 9:
                    string[] items9 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };
                    this.cobB1.Items.AddRange(items9);
                    break;
                case 10:
                    string[] items10 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
                    this.cobB1.Items.AddRange(items10);
                    break;
                case 11:
                    string[] items11 = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                    this.cobB1.Items.AddRange(items11);
                    break;
                case 12:
                    string[] items12 = { "1", "2", "3", "4", "5", "6", "7", "8" };
                    this.cobB1.Items.AddRange(items12);
                    break;
                case 13:
                    string[] items13 = { "1", "2", "3", "4", "5", "6", "7" };
                    this.cobB1.Items.AddRange(items13);
                    break;
                case 14:
                    string[] items14 = { "1", "2", "3", "4", "5", "6" };
                    this.cobB1.Items.AddRange(items14);
                    break;
                case 15:
                    string[] items15 = { "1", "2", "3", "4", "5" };
                    this.cobB1.Items.AddRange(items15);
                    break;
                case 16:
                    string[] items16 = { "1", "2", "3", "4" };
                    this.cobB1.Items.AddRange(items16);
                    break;
                case 17:
                    string[] items17 = { "1", "2", "3" };
                    this.cobB1.Items.AddRange(items17);
                    break;
                case 18:
                    string[] items18 = { "1", "2" };
                    this.cobB1.Items.AddRange(items18);
                    break;
                case 19:
                    string[] items19 = { "1" };
                    this.cobB1.Items.AddRange(items19);
                    break;
                case 20:
                    string[] items20 = { "0" };
                    this.cobB1.Items.AddRange(items20);
                    this.cobB1.Text = "0";
                    break;
            }
        }
        public int getCount()
        {
            String sqlsel = ("select count(*) count from [SY4_Lxsb] where Sy_user=@SYY and C_no=@cno and P_no=@pno and Angle_no=@ano");
            DataSet ds = SqlHelper.ExecuteDataSet(sqlsel, new SqlParameter("@SYY", MainFrom.Uname),
                new SqlParameter("@cno", MainFrom.textB_HbData_Form),
                new SqlParameter("@pno", MainFrom.comBoxData_Form),
                new SqlParameter("@ano", MainFrom.textB_CgqData_Form));
            count = (int)ds.Tables[0].Rows[0]["count"];
            return count;
        }
    }
}
