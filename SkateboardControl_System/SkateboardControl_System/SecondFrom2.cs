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
    public partial class SecondFrom2 : Form
    {
        DataTable dt = null;
        string tb = "[SY2_Gzzd]";
        public SecondFrom2()
        {
            InitializeComponent();
        }
        private void Second_Btn_Result_Click(object sender, EventArgs e)
        {
            dt = Export_All_SY.DBQuery(tb);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("数据库中已有数据，请进行下一个实验或点击重新实验");
                return;
            }
            int[] a = new int[15] ;
            for (int i = 0; i < 15; i++)
                a[i] = 0;
            if (chB01.Checked==true)
            {
                a[0] = 1;
            }
            if (chB02.Checked == true)
            {
                a[1] = 1;
            }
            if (chB03.Checked == true)
            {
                a[2] = 1;
            }
            if (chB04.Checked == true)
            {
                a[3] = 1;
            }
            if (chB05.Checked == true)
            {
                a[4] = 1;
            }
            if (chB06.Checked == true)
            {
                a[5] = 1;
            }
            if (chB07.Checked == true)
            {
                a[6] = 1;
            }
            if (chB08.Checked == true)
            {
                a[7] = 1;
            }
            if (chB09.Checked == true)
            {
                a[8] = 1;
            }
            if (chB10.Checked == true)
            {
                a[9] = 1;
            }
            if (chB11.Checked == true)
            {
                a[10] = 1;
            }
            if (chB12.Checked == true)
            {
                a[11] = 1;
            }
            if (chB13.Checked == true)
            {
                a[12] = 1;
            }
            if (chB14.Checked == true)
            {
                a[13] = 1;
            }
            if (chB15.Checked == true)
            {
                a[14] = 1;
            }
            DateTime timer = DateTime.Now;
            String sqldata = "insert into [SY2_Gzzd](XH_G,YC_G,DD_D,DD_Z,DD_G,DL_D,DL_Z,DL_G,JT_D,JT_Z,DY_D,DY_Z,DY_G,DL_CK,KL_CK,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                " values(@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10,@a11,@a12,@a13,@a14,@a15,@a16,@a17,@a18,@a19,@a20)";
            SqlHelper.ExecuteNonQuery(sqldata, new SqlParameter("@a1", a[0]), new SqlParameter("@a2", a[1]), 
                new SqlParameter("@a3", a[2]), new SqlParameter("@a4", a[3]), new SqlParameter("@a5", a[4]), 
                new SqlParameter("@a6", a[5]), new SqlParameter("@a7", a[6]), new SqlParameter("@a8", a[7]), 
                new SqlParameter("@a9", a[8]), new SqlParameter("@a10", a[9]), new SqlParameter("@a11", a[10]), 
                new SqlParameter("@a12", a[11]), new SqlParameter("@a13", a[12]), new SqlParameter("@a14", a[13]),
                new SqlParameter("@a15", a[14]), new SqlParameter("@a16", timer), new SqlParameter("@a17", MainFrom.Uname),
                new SqlParameter("@a18", MainFrom.comBoxData_Form), new SqlParameter("@a19", MainFrom.textB_HbData_Form), 
                new SqlParameter("@a20", MainFrom.textB_CgqData_Form));
            MessageBox.Show("数据提交成功，记得及时打印！！！");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Export_All_SY.DBDel(tb);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Export_One_SY.Exp_SY1_SY2();
        }
    }
}
