using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using testsjk;

namespace SkateboardControl_System
{
    public partial class History : Form
    {
        /// <summary>
        /// 声明各个变量
        /// </summary>
        //Cp_no产品编号,Kzx_no控制箱编号，Czy操作员，Cgq_no传感器编号，Sy_date试验日期,Sycs要查询的实验次数
        public static string Cp_no, Kzx_no, Czy, Cgq_no, Sy_date, Sycs,date;
        int flag = 0;
        static int state=0;
        //要打开的文件名
        string[] FileName=new string[6];
        //路径
        
        private void button3_Click(object sender, EventArgs e)
        {
            ////得到产品型号
            //Cp_no = this.comboBox1.Text;
            //Kzx_no = this.textBox1.Text;//滑板控制箱编号
            //Cgq_no = this.textBox2.Text;//角度传感器编号
            //Czy = this.textBox3.Text;//操作员
            //Sy_date = this.comboBox2.Text + this.comboBox3.Text + this.comboBox4.Text;
            //date = this.comboBox2.Text + "-" + this.comboBox3.Text + "-" + this.comboBox4.Text;
            //Sycs = this.textBox4.Text;
            ////判断以哪种方式进行查询
            //if (this.radioButton1.Checked)
            //{
            //    flag = 1;//表示以文档形式查询
            //}
            //if (this.radioButton2.Checked)
            //{
            //    flag = 2;//表示以数据库查询
            //}
            ////if (Cp_no == "" || Kzx_no == "" || Czy == "" || Cgq_no == "" || Sy_date == "" || Sycs == "" || flag == 0)
            ////{
            ////    MessageBox.Show("请完整填写查询属性，再保存参数！！！");
            ////    return;
            ////}
            //if (flag == 2)
            //{
            //    if (Cp_no == "" || Kzx_no == "" || Czy == "" || Cgq_no == "" || Sy_date == "" || flag == 0)
            //    {
            //        MessageBox.Show("请完整填写查询属性,可以不填实验次数，再保存参数！！！");
            //        return;
            //    }
            //    SY_Data data = new SY_Data(Cp_no, Kzx_no, Cgq_no, Czy, date);
            //    Export_All_SY.getCount(data);
            //    if (Export_All_SY.count == 1) this.label11.Text = "您在" + date + "没有做任何实验，请选择查询其它日期";
            //    else this.label11.Text = "您在" + date + "一共做了" + (Export_All_SY.count - 1) + "次实验,请在上方文本框中输入您要查询哪一次实验";
            //}
            //else
            //{
            //    this.label11.Text = "";
            //}
        }
        //表示是否有文档被查询到
        public History()
        {
            InitializeComponent();
            this.comboBox1.Text = "HBK-I";
            //this.textBox3.Text = "admin";
            //this.comboBox2.Text = "2020";
            //this.comboBox3.Text = "03";
            //this.comboBox4.Text = "20";
            this.textBox4.Text = "1";
        }
        public static DataTable DBQuery(string table)
        {      
            String sqlQuery = "select * from " + table + " where Sy_user=@SYY and C_no=@cno " +
                "and P_no=@pno and Angle_no=@ano and convert(date,Insert_timer)=@date and count=@num";
            DataSet ds = SqlHelper.ExecuteDataSet(sqlQuery, new SqlParameter("@SYY", MainFrom.Uname),
                new SqlParameter("@cno", Kzx_no),
                new SqlParameter("@pno", Cp_no),
                new SqlParameter("@ano", Cgq_no),
                new SqlParameter("@date", date),
                new SqlParameter("@num", Sycs));
            DataTable dt = ds.Tables[0];
            return dt;
        }
        const string StrPath = @"f:\";
        private void button1_Click(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////////////////////////
            //得到产品型号
            Cp_no = this.comboBox1.Text;
            Kzx_no = this.textBox1.Text;//滑板控制箱编号
            Cgq_no = this.textBox2.Text;//角度传感器编号
            Czy = this.textBox3.Text;//操作员
            Sy_date = this.comboBox2.Text + this.comboBox3.Text + this.comboBox4.Text;
            date = this.comboBox2.Text + "-" + this.comboBox3.Text + "-" + this.comboBox4.Text;
            Sycs = this.textBox4.Text;
            //判断以哪种方式进行查询
            if (this.radioButton1.Checked)
            {
                flag = 1;//表示以文档形式查询
            }
            if (this.radioButton2.Checked)
            {
                flag = 2;//表示以数据库查询
            }
            //if (Cp_no == "" || Kzx_no == "" || Czy == "" || Cgq_no == "" || Sy_date == "" || Sycs == "" || flag == 0)
            //{
            //    MessageBox.Show("请完整填写查询属性，再保存参数！！！");
            //    return;
            //}
            if (flag == 2)
            {
                if (Cp_no == "" || Kzx_no == "" || Czy == "" || Cgq_no == "" || Sy_date == "" || flag == 0)
                {
                    MessageBox.Show("请完整填写查询属性,可以不填实验次数，再保存参数！！！");
                    return;
                }
                SY_Data data = new SY_Data(Cp_no, Kzx_no, Cgq_no, Czy, date);
                Export_All_SY.getCount(data);
                if (Export_All_SY.count == 1) this.label11.Text = "您在" + date + "没有做任何实验，请选择查询其它日期";
                else this.label11.Text = "您在" + date + "一共做了" + (Export_All_SY.count - 1) + "次实验,请在上方文本框中输入您要查询哪一次实验";
            }
            else
            {
                this.label11.Text = "";
            }
            ////////////////////////////////////////////////////////////////////////////////
            int f=0;
            //判断以哪种方式进行查询
            if (this.radioButton1.Checked)
            {
                f = 1;//表示以文档形式查询
            }
            if (this.radioButton2.Checked)
            {
                f = 2;//表示以数据库查询
            }
            
            if(Cp_no!= this.comboBox1.Text||Kzx_no!=this.textBox1.Text
                || Cgq_no != this.textBox2.Text|| Czy != this.textBox3.Text||
                Sy_date != (this.comboBox2.Text + this.comboBox3.Text + this.comboBox4.Text)
                || Sycs != this.textBox4.Text||flag!=f)
            {
                MessageBox.Show("您修改了参数但没有保存，请先保存参数，再进行查询！！！");
                return;
            }
            //FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            if (flag == 1)
            {
                Boolean isFile = false;
                string[] com = Cp_no.Split('/');
                string p_no = "";
                for (int k = 0; k < com.Length; k++)
                    p_no = p_no + com[k];
                //获得要查询的文件名
                FileName[0] = Sycs;
                FileName[1] = p_no;
                FileName[2] = Kzx_no;
                FileName[3] = Czy;
                FileName[4] = Cgq_no;
                FileName[5] = Sy_date;
                //DialogResult result = openFileDialog.ShowDialog();
                string hz = "pdf";
                //文件查询
                //if (result == DialogResult.OK)
                //{
                //DirectoryInfo dir = new DirectoryInfo(openFileDialog.SelectedPath);
                    DirectoryInfo dir = new DirectoryInfo(StrPath);
                    FileInfo[] fileInfo = dir.GetFiles();
                    foreach (FileInfo item in fileInfo)
                    {
                        int flag = 1;
                        string fileName = item.Name;
                        if (fileName.Substring(fileName.LastIndexOf(".") + 1) == hz)
                        {
                            string[] fname = fileName.Split('_');
                            if (fname.Length != 6) continue;
                            for (int h = 0; h < 5; h++)
                            {
                                if (FileName[h] != "")
                                {
                                    if(FileName[h] != fname[h])
                                        flag = 0;
                                }
                            }
                            if (FileName[5] != "")
                            {
                                if (!fname[5].Contains(FileName[5]))
                                    flag = 0;
                            }                                
                            if (flag==1)
                            {
                                //加判断条件
                                if (MessageBox.Show("确定打开？" + StrPath + "\\" + item.Name, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    MessageBox.Show("正在打开" + StrPath + "\\" + item.Name);
                                    Process.Start("explorer", StrPath + item.Name);
                                }
                                //MessageBox.Show("正在打开" + StrPath + "\\" + item.Name);
                                //Process.Start("explorer", StrPath + item.Name);
                                isFile = true;  //判断是否查询到文件
                            }
                        }
                    }
                    if (!isFile)
                    {
                        MessageBox.Show("未查询到文件，重新试验或进一步查询！！！");
                    }
                //}
            }
            else
            {
                if (Cp_no == "" || Kzx_no == "" || Czy == "" || Cgq_no == "" || Sy_date == "" || Sycs == "" || flag == 0)
                {
                    MessageBox.Show("请完整填写查询属性，再进行查询！！！");
                    return;
                }
                state = 0;
                getState();
                if (state == 0)
                {
                    MessageBox.Show("没有查到任何数据");
                    return;
                }
                //数据库查询，并生成文件
                Export_All_SY.Exp_PDF(1);
            }
        }
        static void getState()
        {
            DataTable dt = DBQuery("SY1_Fdsk_Final");
            if (dt.Rows.Count > 0) state = 1;
            dt = DBQuery("Sy2_Gzzd_Final");
            if (dt.Rows.Count > 0) state = 1;
            dt = DBQuery("SY3_Timer_Final");
            if (dt.Rows.Count > 0) state = 1;
            dt = DBQuery("SY4_Lxsb_Final");
            if (dt.Rows.Count > 0) state = 1;
            dt = DBQuery("SY5_Fdck_Final");
            if (dt.Rows.Count > 0) state = 1;
            dt = DBQuery("SY6_Xtts_Final");
            if (dt.Rows.Count > 0) state = 1;
            dt = DBQuery("SY7_Lxsb_Ck_Final");
            if (dt.Rows.Count > 0) state = 1;
            dt = DBQuery("SY8_Hplx_Final");
            if (dt.Rows.Count > 0) state = 1;
        }       
        public static DataTable Final_Pdf(string tb)
        {
            DataTable dt = DBQuery(tb);
            return dt;            
        }
    }
}