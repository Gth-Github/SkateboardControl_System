using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkateboardControl_System
{
    class SqlHelper
    {
        //通过在Main函数中动态得到数据库的相对地址，将其交给变量DataDirectory，直接利用相对变量
        public static string ConnectString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                    "AttachDbFilename=|DataDirectory|\\Skateboard_Datasets.mdf;Integrated Security=true";
        /*第一：
        public static string ConnectString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                    "AttachDbFilename=C:\\Users\\Administrator\\Desktop\\708项目\\数据采集\\testsjk\\testsjk\\User.mdf;Integrated Security=true";* /
        /*第二：要更改（根据不同数据库版本）
        public static string ConnectString = "Data Source=(localdb)\\ProjectsV13;" +
                    "AttachDbFilename=|DataDirectory|\\testdj3.mdf;Integrated Security=true";*/

        //执行增、删、改的方法
        public static int ExecuteNonQuery(String sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        //封装一个执行返回单个对象的方法：ExecuteScalar()
        public static object ExecuteScalar(String sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        //执行查询操作，只用来查询结果比较少的sql
        public static DataSet ExecuteDataSet(String sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            }
        }
    }
}
