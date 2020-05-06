using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using testsjk;

namespace SkateboardControl_System
{
    public partial class ShouyeForm : Form
    {
        static string comBoxData,textB_HbData, textB_CgqData;
        FristFrom1 fristFrom1 = null;
        /// <summary>
        /// 生成打印报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (MainFrom.textB_CgqData_Form == null || MainFrom.textB_HbData_Form == null || MainFrom.comBoxData_Form == null)
            {
                MessageBox.Show("请先保存设备属性值，再进行打印操作");
                return;
            }
            Export_All_SY.Exp_PDF(0);
        }
        /// <summary>
        /// 历史数据查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            History history = new History();
            history.ShowDialog();
        }

        public ShouyeForm()
        {
            InitializeComponent();
            if (MainFrom.textB_CgqData_Form!="" && MainFrom.textB_HbData_Form !="" && MainFrom.comBoxData_Form!="")
            {
                this.comboBox1.Text = MainFrom.comBoxData_Form;
                this.textB_Hbbh.Text = MainFrom.textB_HbData_Form;
                this.textB_Cgqbh.Text = MainFrom.textB_CgqData_Form;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            comBoxData = this.comboBox1.Text;
            textB_HbData = this.textB_Hbbh.Text;
            textB_CgqData = this.textB_Cgqbh.Text;
            if (comBoxData == "" || textB_CgqData == "" || textB_HbData == "")
            {
                MessageBox.Show("请先设置设备属性值，再进行实验项目");
                return;
            }
            else
            {
                //保存到主界面
                MainFrom.comBoxData_Form = comBoxData;   //产品编号
                MainFrom.textB_HbData_Form = textB_HbData;
                MainFrom.textB_CgqData_Form = textB_CgqData;
            }
            MessageBox.Show("用户"+MainFrom.Uname+"\n" + "产品编号"+comBoxData+"\n"+ 
                "控制箱编号"+textB_HbData+"\n"+"角度传感器编号"+textB_CgqData, "提示");
            //MainFrom mainFrom = new MainFrom();
            //MainFrom.Load_First();
                    }
    }
}
