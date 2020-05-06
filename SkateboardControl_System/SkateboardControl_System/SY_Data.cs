using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SkateboardControl_System
{
    class SY_Data
    {
        string Cp_no;//产品型号
        string Kzx_no;//控制箱编号
        string Cgq_no;//角度传感器编号
        string Czy;//操作员
        string date;//实验日期
        public SY_Data(string cp_no, string kzx_no, string cgq_no, string czy, string date)
        {
            Cp_no = cp_no;
            Kzx_no = kzx_no;
            Cgq_no = cgq_no;
            Czy = czy;
            this.date = date;
        }
        public string Cp_no1 { get => Cp_no; set => Cp_no = value; }
        public string Kzx_no1 { get => Kzx_no; set => Kzx_no = value; }
        public string Cgq_no1 { get => Cgq_no; set => Cgq_no = value; }
        public string Czy1 { get => Czy; set => Czy = value; }
        public string Date { get => date; set => date = value; }
        public static DataTable DBQueryFinal(string table,SY_Data data)
        {
            String sqlQuery = "select Max(count) from " + table + " where Sy_user=@SYY and C_no=@cno " +
                "and P_no=@pno and Angle_no=@ano and convert(date,Insert_timer)=@date";
            DataSet ds = SqlHelper.ExecuteDataSet(sqlQuery, new SqlParameter("@SYY", data.Czy1),
                new SqlParameter("@cno", data.Kzx_no1),
                new SqlParameter("@pno", data.Cp_no1),
                new SqlParameter("@ano", data.Cgq_no1),
                new SqlParameter("@date", data.Date));
            DataTable dt = ds.Tables[0];
            return dt;
        }
    }
}
