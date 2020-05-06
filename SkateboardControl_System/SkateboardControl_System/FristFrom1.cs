using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testsjk;

namespace SkateboardControl_System
{
    public partial class FristFrom1 : Form
    {
        int[] checkData = new int[6] {0,0,0,0,0,0};
        DataTable dt = null;
        public FristFrom1()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;     //设置窗体为无边框样式
        }
        string tb = "[SY1_Fdsk]";
        private void button1_Click(object sender, EventArgs e)
        {
            dt = Export_All_SY.DBQuery(tb);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("数据库中已有数据，请进行下一个实验或点击重新实验");
                return;
            }
            if (chB1.Checked == true)
            {
                checkData[0] = 1;
            }
            if (chB2.Checked == true)
            {
                checkData[1] = 1;
            }
            if (chB3.Checked == true)
            {
                checkData[2] = 1;
            }
            if (chB4.Checked == true)
            {
                checkData[3] = 1;
            }
            if (chB5.Checked == true)
            {
                checkData[4] = 1;
            }
            if (chB6.Checked == true)
            {
                checkData[5] = 1;
            }
            DateTime timer = DateTime.Now;
            String sqldata = "insert into [SY1_Fdsk](Ss_s,Ss_f,Sx_s,Sx_f,Wy_s,Wy_f,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                " values(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11)";
            SqlHelper.ExecuteNonQuery(sqldata, new SqlParameter("@a1", checkData[0]), new SqlParameter("@a2", checkData[1]),
                new SqlParameter("@a3", checkData[2]), new SqlParameter("@a4", checkData[3]), new SqlParameter("@a5", checkData[4]),
                new SqlParameter("@a6", checkData[5]), new SqlParameter("@a7", timer), new SqlParameter("@a8", MainFrom.Uname),
                new SqlParameter("@a9", MainFrom.comBoxData_Form), new SqlParameter("@a10", MainFrom.textB_HbData_Form)
                , new SqlParameter("@a11", MainFrom.textB_CgqData_Form));
            MessageBox.Show("数据提交成功，记得及时打印！！！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Export_All_SY.DBDel(tb);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Export_One_SY.Exp_SY1_SY2();
        }
    }
}
