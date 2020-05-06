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
    public partial class SeventhForm7 : Form
    {
        int C_no=1;//C_no表示试验次数，T_no表示试验条数
        /// <summary>
        /// 采集数据按钮，第一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        DataTable dt = null;
        string tb = "[SY7_Lxsb_Ck]";
        private void button1_Click(object sender, EventArgs e)
        {
            dt = Export_All_SY.DBQuery(tb);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("数据库中已有数据，请进行下一个实验或点击重新实验");
                return;
            }
            string[] data_Beizhu = new string[8];
            for (int i =0;i<data_Beizhu.Length;i++) {
                data_Beizhu[i] = "";
            }
            data_Beizhu[0] = textBox1.Text.Trim(); data_Beizhu[1] = textBox2.Text.Trim();
            data_Beizhu[2] = textBox3.Text.Trim(); data_Beizhu[3] = textBox4.Text.Trim();
            data_Beizhu[4] = textBox5.Text.Trim(); data_Beizhu[5] = textBox6.Text.Trim();
            data_Beizhu[6] = textBox7.Text.Trim(); data_Beizhu[7] = textBox8.Text.Trim();
            int[] data_one = new int[24];
            for (int i = 0; i<data_one.Length;i++)
            {
                data_one[i] = 0;

            }
            if (checkBox1.Checked == true) { data_one[0] = 1; }
            if (checkBox2.Checked == true) { data_one[1] = 1; }
            if (checkBox3.Checked == true) { data_one[2] = 1; }
            if (checkBox4.Checked == true) { data_one[3] = 1; }
            if (checkBox5.Checked == true) { data_one[4] = 1; }
            if (checkBox6.Checked == true) { data_one[5] = 1; }
            if (checkBox7.Checked == true) { data_one[6] = 1; }
            if (checkBox8.Checked == true) { data_one[7] = 1; }
            if (checkBox9.Checked == true) { data_one[8] = 1; }
            if (checkBox10.Checked == true) { data_one[9] = 1; }
            if (checkBox11.Checked == true) { data_one[10] = 1; }
            if (checkBox12.Checked == true) { data_one[11] = 1; }
            if (checkBox13.Checked == true) { data_one[12] = 1; }
            if (checkBox14.Checked == true) { data_one[13] = 1; }
            if (checkBox15.Checked == true) { data_one[14] = 1; }
            if (checkBox16.Checked == true) { data_one[15] = 1; }
            if (checkBox17.Checked == true) { data_one[16] = 1; }
            if (checkBox18.Checked == true) { data_one[17] = 1; }
            if (checkBox19.Checked == true) { data_one[18] = 1; }
            if (checkBox20.Checked == true) { data_one[19] = 1; }
            if (checkBox21.Checked == true) { data_one[20] = 1; }
            if (checkBox22.Checked == true) { data_one[21] = 1; }
            if (checkBox23.Checked == true) { data_one[22] = 1; }
            if (checkBox24.Checked == true) { data_one[23] = 1; }
            C_no++;
            if (C_no==2)
            {
                this.button1.Enabled = false;
                this.button2.Enabled = true; this.button3.Enabled = false;
            }
            string[] Gk = new string[8];
            Gk[0] = "陆上"; Gk[1] = "低速";
            Gk[2] = "中速"; Gk[3] = "高速";
            Gk[4] = "应急"; Gk[5] = "中速";
            Gk[6] = "低速"; Gk[7] = "陆上";
            //插入数据库
            String sqldata;
            for (int i = 0; i < 8; i++)
            {
                DateTime timer = DateTime.Now;
                sqldata = "insert into [SY7_Lxsb_Ck](Gk,Ss_b,Sx_b,Wy_b,Beizhu,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                    " values(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)";
                SqlHelper.ExecuteNonQuery(sqldata,
                    new SqlParameter("@a1",Gk[i] ),
                    new SqlParameter("@a2",data_one[3*i]),
                    new SqlParameter("@a3", data_one[3 * i+1]),
                    new SqlParameter("@a4", data_one[3 * i+2]),
                    new SqlParameter("@a5",data_Beizhu[i]),
                    new SqlParameter("@a6",timer),
                    new SqlParameter("@a7", MainFrom.Uname),
                    new SqlParameter("@a8", MainFrom.comBoxData_Form),
                    new SqlParameter("@a9", MainFrom.textB_HbData_Form),
                    new SqlParameter("@a10", MainFrom.textB_CgqData_Form));
            }
        }
        /// <summary>
        /// 采集数据按钮，第二个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string[] data_Beizhu = new string[7];
            for (int i = 0; i < data_Beizhu.Length; i++)
            {
                data_Beizhu[i] = "";
            }
            data_Beizhu[0] = textBox9.Text.Trim(); data_Beizhu[1] = textBox10.Text.Trim();
            data_Beizhu[2] = textBox11.Text.Trim(); data_Beizhu[3] = textBox12.Text.Trim();
            data_Beizhu[4] = textBox13.Text.Trim(); data_Beizhu[5] = textBox14.Text.Trim();
            data_Beizhu[6] = textBox15.Text.Trim();
            int[] data_one = new int[21];
            for (int i = 0; i < data_one.Length; i++)
            {
                data_one[i] = 0;
            }
            if (checkBox25.Checked == true) { data_one[0] = 1; }
            if (checkBox26.Checked == true) { data_one[1] = 1; }
            if (checkBox27.Checked == true) { data_one[2] = 1; }
            if (checkBox28.Checked == true) { data_one[3] = 1; }
            if (checkBox29.Checked == true) { data_one[4] = 1; }
            if (checkBox30.Checked == true) { data_one[5] = 1; }
            if (checkBox31.Checked == true) { data_one[6] = 1; }
            if (checkBox32.Checked == true) { data_one[7] = 1; }
            if (checkBox33.Checked == true) { data_one[8] = 1; }
            if (checkBox34.Checked == true) { data_one[9] = 1; }
            if (checkBox35.Checked == true) { data_one[10] = 1; }
            if (checkBox36.Checked == true) { data_one[11] = 1; }
            if (checkBox37.Checked == true) { data_one[12] = 1; }
            if (checkBox38.Checked == true) { data_one[13] = 1; }
            if (checkBox39.Checked == true) { data_one[14] = 1; }
            if (checkBox40.Checked == true) { data_one[15] = 1; }
            if (checkBox41.Checked == true) { data_one[16] = 1; }
            if (checkBox42.Checked == true) { data_one[17] = 1; }
            if (checkBox43.Checked == true) { data_one[18] = 1; }
            if (checkBox44.Checked == true) { data_one[19] = 1; }
            if (checkBox45.Checked == true) { data_one[20] = 1; }
            C_no++;
            if (C_no>5)
            {
                this.button1.Enabled = false;
                this.button2.Enabled = false; this.button3.Enabled = true;
            }
            string[] Gk = new string[7];
            Gk[0] = "低速";
            Gk[1] = "中速"; Gk[2] = "高速";
            Gk[3] = "应急"; Gk[4] = "中速";
            Gk[5] = "低速"; Gk[6] = "陆上";
            //插入数据库
            String sqldata;
            for (int i = 0; i < 7; i++)
            {
                DateTime timer = DateTime.Now;
                sqldata = "insert into [SY7_Lxsb_Ck](Gk,Ss_b,Sx_b,Wy_b,Beizhu,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                    " values(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)";
                SqlHelper.ExecuteNonQuery(sqldata,
                    new SqlParameter("@a1", Gk[i]),
                    new SqlParameter("@a2", data_one[3 * i]),
                    new SqlParameter("@a3", data_one[3 * i + 1]),
                    new SqlParameter("@a4", data_one[3 * i + 2]),
                    new SqlParameter("@a5", data_Beizhu[i]),
                    new SqlParameter("@a6", timer),
                    new SqlParameter("@a7", MainFrom.Uname),
                    new SqlParameter("@a8", MainFrom.comBoxData_Form),
                    new SqlParameter("@a9", MainFrom.textB_HbData_Form),
                    new SqlParameter("@a10", MainFrom.textB_CgqData_Form));
            }
        }
        /// <summary>
        /// 采集数据按钮，第三个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            string[] data_Beizhu = new string[3];
            for (int i = 0; i < data_Beizhu.Length; i++)
            {
                data_Beizhu[i] = "";
            }
            data_Beizhu[0] = textBox16.Text.Trim(); data_Beizhu[1] = textBox17.Text.Trim();
            data_Beizhu[2] = textBox18.Text.Trim();
            int[] data_one = new int[9];
            for (int i = 0; i < data_one.Length; i++)
            {
                data_one[i] = 0;
            }
            if (checkBox46.Checked == true) { data_one[0] = 1; }
            if (checkBox47.Checked == true) { data_one[1] = 1; }
            if (checkBox48.Checked == true) { data_one[2] = 1; }
            if (checkBox49.Checked == true) { data_one[3] = 1; }
            if (checkBox50.Checked == true) { data_one[4] = 1; }
            if (checkBox51.Checked == true) { data_one[5] = 1; }
            if (checkBox52.Checked == true) { data_one[6] = 1; }
            if (checkBox53.Checked == true) { data_one[7] = 1; }
            if (checkBox54.Checked == true) { data_one[8] = 1; }
            C_no++;
            if (C_no==21)
            {
                this.button1.Enabled = false;
                this.button2.Enabled = false; this.button3.Enabled = false;
                MessageBox.Show("本次试验已完成，请进入下一项试验或者重新试验","提示");
            }
            string[] Gk = new string[3];
            Gk[0] = "高速";
            Gk[1] = "应急"; Gk[2] = "陆上";
            //插入数据库
            String sqldata;
            for (int i = 0; i < 3; i++)
            {
                DateTime timer = DateTime.Now;
                sqldata = "insert into [SY7_Lxsb_Ck](Gk,Ss_b,Sx_b,Wy_b,Beizhu,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                    " values(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)";
                SqlHelper.ExecuteNonQuery(sqldata,
                    new SqlParameter("@a1", Gk[i]),
                    new SqlParameter("@a2", data_one[3 * i]),
                    new SqlParameter("@a3", data_one[3 * i + 1]),
                    new SqlParameter("@a4", data_one[3 * i + 2]),
                    new SqlParameter("@a5", data_Beizhu[i]),
                    new SqlParameter("@a6", timer),
                    new SqlParameter("@a7", MainFrom.Uname),
                    new SqlParameter("@a8", MainFrom.comBoxData_Form),
                    new SqlParameter("@a9", MainFrom.textB_HbData_Form),
                    new SqlParameter("@a10", MainFrom.textB_CgqData_Form));
            }
        }

        public SeventhForm7()
        {
            InitializeComponent();
            if (C_no ==1)
            {
                this.button1.Enabled = true;
                this.button2.Enabled = false; this.button3.Enabled = false;
            }
        }
        /// <summary>
        /// 重新试验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            C_no = 1;
            //数据库
            Export_All_SY.DBDel(tb);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Export_One_SY.Exp_SY7();
        }
    }
}
