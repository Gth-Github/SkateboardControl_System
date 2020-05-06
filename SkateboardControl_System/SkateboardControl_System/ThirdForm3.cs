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
    public partial class ThirdForm3 : Form
    {
        int num,count_num=0;
        List<Sy3_List> sy3_Lists = new List<Sy3_List>();
        public ThirdForm3()
        {
            InitializeComponent();
            //初始化时候加载 集合数据，设置表显示
            this.dataGridView1.DataSource = new BindingList<Sy3_List>(sy3_Lists);  //把dattable绑定datagridview
            this.dataGridView1.Columns[0].HeaderText = "试验次数";
            this.dataGridView1.Columns[1].HeaderText = "试验结果";
            //设置单元格前背景与前景色
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
            if (button1.Text == "开始计时")
            {
                num = 1;
                timer1.Enabled = true ;
                button1.Text = "结束计时";
            }
            else if (button1.Text == "结束计时")
            {
                count_num++;
                timer1.Enabled = false;
                num = num - 1;
                ////查询数据库数据条数，超过试验次数3次给与提醒
                String sqlQuery = "select * from [SY3_Timer] where Sy_user=@SYY and C_no=@cno and P_no=@pno and Angle_no=@ano";
                DataSet ds = SqlHelper.ExecuteDataSet(sqlQuery, new SqlParameter("@SYY", MainFrom.Uname),
                new SqlParameter("@cno", MainFrom.textB_HbData_Form),
                new SqlParameter("@pno", MainFrom.comBoxData_Form),
                new SqlParameter("@ano", MainFrom.textB_CgqData_Form));
                if (ds.Tables[0].Rows.Count >0)
                {
                    MessageBox.Show("数据库中已有数据，请进行下一个实验或点击重新实验");
                    return;
                }
                if (sy3_Lists.Count == 3)
                {
                    MessageBox.Show("试验次数已达到3条，请进行下次试验或者重新试验,及时打印！！！");
                    button1.Text = "开始计时";
                    this.label_time.Text = "" + 0;
                    return;
                }
                MessageBox.Show("本次收板所用时间为：" + num, "提示");
                Sy3_List sy3_List = new Sy3_List();
                sy3_List.Num_key = count_num;
                sy3_List.Data_value = num;
                sy3_Lists.Add(sy3_List);
                this.dataGridView1.DataSource = new BindingList<Sy3_List>(sy3_Lists);  //把dattable绑定datagridview
                this.dataGridView1.Columns[0].HeaderText = "试验次数";
                this.dataGridView1.Columns[1].HeaderText = "试验结果";
                //选择整行显示数据
                this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //选择是否只读
                this.dataGridView1.ReadOnly = true;
                //选择是否添加一行空白行
                this.dataGridView1.AllowUserToAddRows = false;
                button1.Text = "开始计时";
                this.label_time.Text = "" + 0;
            }
        }
        /// <summary>
        /// 重做试验按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            sy3_Lists.Clear();
            count_num = 0;
            this.dataGridView1.DataSource = new BindingList<Sy3_List>(sy3_Lists);
            string tb = "[SY3_Timer]";
            Export_All_SY.DBDel(tb);
        }
        /// <summary>
        /// 保存3次数据到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            String sqldata;
            if (sy3_Lists.Count < 3)
            {
                MessageBox.Show("试验未满3次，请继续完成试验", "提示");
                return;
            }
            else
            {
                for (int i = 0;i< sy3_Lists.Count; i++)
                {
                    DateTime timer = DateTime.Now;
                    sqldata = "insert into [SY3_Timer](timer,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                              " values(@a1,@a2,@a3,@a4,@a5,@a6)";
                    SqlHelper.ExecuteNonQuery(sqldata, new SqlParameter("@a1", sy3_Lists[i].Data_value),
                        new SqlParameter("@a2", timer), new SqlParameter("@a3",MainFrom.Uname),
                        new SqlParameter("@a4", MainFrom.comBoxData_Form), new SqlParameter("@a5", MainFrom.textB_HbData_Form)
                        , new SqlParameter("@a6", MainFrom.textB_CgqData_Form));
                }
            }
            sy3_Lists.Clear();
            count_num = 0;
            this.dataGridView1.DataSource = new BindingList<Sy3_List>(sy3_Lists);
            MessageBox.Show("数据保存成功，请打印或者重新试验", "提示");
        }
        /// <summary>
        /// 打印本实验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            Export_One_SY.Exp_SY3_SY4();
        }

        /// <summary>
        /// 首板收板计时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (MainFrom.Data_pictureBox04 == 1 || MainFrom.Data_pictureBox06 == 1)
            {
                label_time.Text = Convert.ToString(num++);
            }
        }
    }
    /// <summary>
    /// 为记录试验数据封装的LIST集合类
    /// </summary>
    class Sy3_List
    {
        int num_key;
        int data_value;
        public int Num_key { get => num_key; set => num_key = value; }
        public int Data_value { get => data_value; set => data_value = value; }
    }
}
