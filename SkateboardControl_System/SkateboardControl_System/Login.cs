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

namespace SkateboardControl_System
{
    public partial class Login_ : Form
    {
        public Login_()
        {
            InitializeComponent();
        }
        //登陆按钮   
        private void button1_Click(object sender, EventArgs e)
        {
            string UserName = Login_Uname.Text.Trim();//用户名
            string Password = Login_Upsd.Text.Trim();//密码
            if("".Equals(UserName))
            {
                MessageBox.Show("用户名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }else if ("".Equals(Password))
            {
                MessageBox.Show("密码为空，请输入密码", "提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                string str = "select * from [User] where Uname=@name and Pwd = @psd";
                DataSet ds = SqlHelper.ExecuteDataSet(str, new SqlParameter("@name", UserName), new SqlParameter("@psd", Password));
                
                if (ds.Tables[0].Rows.Count != 0)
                {
                    MainFrom mainFrom = new MainFrom();
                    MainFrom.Uname = UserName;
                    this.Hide();
                    mainFrom.ShowDialog();
                    Application.ExitThread();
                }
                else
                {
                    MessageBox.Show("非合法用户！！！", "注意");
                }
            }
        }
        //退出按钮
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定退出？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }
    }
}
