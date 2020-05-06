using iTextSharp.text;
using iTextSharp.text.pdf;
using SkateboardControl_System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace testsjk
{
    class Export_All_SY
    {
        public static int count;
        static DataTable dtb;
        static PdfPTable table;//声明表格对象
        static PdfPCell cell1;//声明单元格对象
        static Paragraph nullb;//声明空行对象
        //SIMSUN.TTC：宋体和新宋体   SIMKAI.TTF：楷体   SIMHEI.TTF：黑体   SIMFANG.TTF：仿宋体
        static BaseFont bftitle = BaseFont.CreateFont("C:\\Windows\\Fonts\\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        static iTextSharp.text.Font fonttitle = new iTextSharp.text.Font(bftitle, 16); //表頭字体，大小16 
        static BaseFont bf1 = BaseFont.CreateFont("C:\\Windows\\Fonts\\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        static iTextSharp.text.Font fonttitle2 = new iTextSharp.text.Font(bf1, 12); //表格內容字体，大小15
        static iTextSharp.text.Font fonttitle3 = new iTextSharp.text.Font(bf1, 10); //表格內容字体，大小15
        public static void getCount(SY_Data data)
        {
            count = 1;
            DataTable dt = SY_Data.DBQueryFinal("SY1_Fdsk_Final",data);
            if (dt.Rows.Count != 0&&dt.Rows[0][0]!= DBNull.Value&&count<=Convert.ToInt32(dt.Rows[0][0].ToString()))
            {
                count = Convert.ToInt32(dt.Rows[0][0].ToString())+1;
            }
            dt = SY_Data.DBQueryFinal("Sy2_Gzzd_Final",data);
            if (dt.Rows.Count != 0 && dt.Rows[0][0] != DBNull.Value && count <= Convert.ToInt32(dt.Rows[0][0].ToString()))
            {
                count = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
            }
            dt = SY_Data.DBQueryFinal("SY3_Timer_Final",data);
            if (dt.Rows.Count != 0 && dt.Rows[0][0] != DBNull.Value && count <= Convert.ToInt32(dt.Rows[0][0].ToString()))
            {
                count = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
            }
            dt = SY_Data.DBQueryFinal("SY4_Lxsb_Final",data);
            if (dt.Rows.Count != 0 && dt.Rows[0][0] != DBNull.Value && count <= Convert.ToInt32(dt.Rows[0][0].ToString()))
            {
                count = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
            }
            dt = SY_Data.DBQueryFinal("SY5_Fdck_Final",data);
            if (dt.Rows.Count != 0 && dt.Rows[0][0] != DBNull.Value && count <= Convert.ToInt32(dt.Rows[0][0].ToString()))
            {
                count = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
            }
            dt = SY_Data.DBQueryFinal("SY6_Xtts_Final",data);
            if (dt.Rows.Count != 0 && dt.Rows[0][0] != DBNull.Value && count <= Convert.ToInt32(dt.Rows[0][0].ToString()))
            {
                count = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
            }
            dt = SY_Data.DBQueryFinal("SY7_Lxsb_Ck_Final",data);
            if (dt.Rows.Count != 0 && dt.Rows[0][0] != DBNull.Value && count <= Convert.ToInt32(dt.Rows[0][0].ToString()))
            {
                count = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
            }
            dt = SY_Data.DBQueryFinal("SY8_Hplx_Final",data);
            if (dt.Rows.Count != 0 && dt.Rows[0][0] != DBNull.Value && count <= Convert.ToInt32(dt.Rows[0][0].ToString()))
            {
                count = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
            }
        }
        /// <summary>
        /// 加载设计的pdf模板，并对需要赋值的地方赋值
        /// </summary>
        public static void Exp_PDF(int status)
        {
            String str = " ";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text documents (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Text文件到";
            DateTime now = DateTime.Now;
            if (status == 0)
            {
                string date = now.Year.ToString().PadLeft(2) + "-" +
                now.Month.ToString().PadLeft(2, '0') + "-" +
                now.Day.ToString().PadLeft(2, '0');
                SY_Data data = new SY_Data(MainFrom.comBoxData_Form,
                    MainFrom.textB_HbData_Form,MainFrom.textB_CgqData_Form,
                    MainFrom.Uname,date);
                getCount(data);
                string[] com = MainFrom.comBoxData_Form.Split('/');
                string pno = "";
                for (int k = 0; k < com.Length; k++)
                    pno = pno + com[k];
                saveFileDialog.FileName = count + "_" + pno + "_" +
                MainFrom.textB_HbData_Form + "_" +
                MainFrom.Uname + "_" + MainFrom.textB_CgqData_Form + "_" +
                now.Year.ToString().PadLeft(2) + "" +
                now.Month.ToString().PadLeft(2, '0') + "" +
                now.Day.ToString().PadLeft(2, '0') + "-" +
                now.Hour.ToString().PadLeft(2, '0') + "" +
                now.Minute.ToString().PadLeft(2, '0') + "" +
                now.Second.ToString().PadLeft(2, '0');
            }
            else
            {
                string[] com =History.Cp_no.Split('/');
                string p_no = "";
                for (int k = 0; k < com.Length; k++)
                    p_no = p_no + com[k];
                saveFileDialog.FileName = History.Sycs + "_" + p_no + "_" +
                History.Kzx_no + "_" +
                History.Czy + "_" + History.Cgq_no + "_"+
                History.Sy_date + "-" +
                now.Hour.ToString().PadLeft(2, '0') + "" +
                now.Minute.ToString().PadLeft(2, '0') + "" +
                now.Second.ToString().PadLeft(2, '0');
            }
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                PdfReader pdfReader; PdfStamper pdfStamper; AcroFields pdfFormFields;
                BaseFont bf1 = BaseFont.CreateFont("C:\\Windows\\Fonts\\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                //文件保存的真正路径
                string tempFilePath = saveFileDialog.FileName;
                string path = AppDomain.CurrentDomain.BaseDirectory, sql;
                int flag = 1;
                if (MainFrom.comBoxData_Form == "HBK-II/1")
                {
                    path = System.IO.Directory.GetParent(path).Parent.Parent.FullName + "\\II型车.pdf";
                    pdfReader = new PdfReader(path);//模板的路径
                    pdfStamper = new PdfStamper(pdfReader, new FileStream(tempFilePath, FileMode.OpenOrCreate));
                    pdfFormFields = pdfStamper.AcroFields;
                    pdfStamper.FormFlattening = true;
                    pdfFormFields.AddSubstitutionFont(bf1);
                    if (status == 0)
                    {
                        pdfFormFields.SetField("CPXH", MainFrom.comBoxData_Form);
                        pdfFormFields.SetField("HBKZXBH", MainFrom.textB_HbData_Form);
                        pdfFormFields.SetField("JDCGQBH", MainFrom.textB_CgqData_Form);
                        pdfFormFields.SetField("SYY", MainFrom.Uname);
                    }
                    else
                    {
                        pdfFormFields.SetField("CPXH", History.Cp_no);
                        pdfFormFields.SetField("HBKZXBH", History.Kzx_no);
                        pdfFormFields.SetField("JDCGQBH", History.Cgq_no);
                        pdfFormFields.SetField("SYY", History.Czy);
                    }
                    //试验1的报表
                    if (status == 0)
                        dtb = SY1_DB();
                    else
                        dtb = History.Final_Pdf("SY1_Fdsk_Final");
                    Export_Table_Design.SY1_Table(pdfFormFields, ref flag, dtb);
                    flag = 1;
                    if (status == 0)
                        dtb = SY2_DB();
                    else
                        dtb = History.Final_Pdf("SY2_Gzzd_Final");
                    Export_Table_Design.SY2_Table(pdfFormFields, ref flag, dtb);
                    pdfStamper.Close();
                    pdfReader.Close();
                }
                else
                {
                    path = System.IO.Directory.GetParent(path).Parent.Parent.FullName + "\\I型车.pdf";
                    pdfReader = new PdfReader(path);//模板的路径
                    pdfStamper = new PdfStamper(pdfReader, new FileStream(tempFilePath, FileMode.OpenOrCreate));
                    pdfFormFields = pdfStamper.AcroFields;
                    pdfStamper.FormFlattening = true;
                    pdfFormFields.AddSubstitutionFont(bf1);
                    if (status == 0)
                    {
                        pdfFormFields.SetField("CPXH", MainFrom.comBoxData_Form);
                        pdfFormFields.SetField("HBKZXBH", MainFrom.textB_HbData_Form);
                        pdfFormFields.SetField("JDCGQBH", MainFrom.textB_CgqData_Form);
                        pdfFormFields.SetField("SYY", MainFrom.Uname);
                    }
                    else
                    {
                        pdfFormFields.SetField("CPXH", History.Cp_no);
                        pdfFormFields.SetField("HBKZXBH", History.Kzx_no);
                        pdfFormFields.SetField("JDCGQBH", History.Cgq_no);
                        pdfFormFields.SetField("SYY", History.Czy);
                    }
                    //试验1的报表
                    if (status == 0)
                        dtb = SY1_DB();
                    else
                        dtb = History.Final_Pdf("SY1_Fdsk_Final");
                    Export_Table_Design.SY1_Table(pdfFormFields, ref flag, dtb);
                    flag = 1;
                    if (status == 0)
                        dtb = SY2_DB();
                    else
                        dtb = History.Final_Pdf("SY2_Gzzd_Final");
                    Export_Table_Design.SY2_Table(pdfFormFields, ref flag, dtb);
                    pdfStamper.Close();
                    pdfReader.Close();
                }

                Exp_PDF1(saveFileDialog.FileName,status);
            }
        }
        /// <summary>
        /// 设计试验3到试验6的打印报表
        /// </summary>
        /// <param name="filepath"></param>
        public static void Exp_PDF1(string filepath, int status)
        {
            PdfReader pdfReader = null;
            PdfImportedPage pageTemplate = null;
            int i, j, k;
            Document document = null;
            Paragraph Title;
            //============================================加载模板并在模板后添加新表格==================================================
            string tempPath = Path.GetDirectoryName(filepath) + Path.GetFileNameWithoutExtension(filepath) + "_temp.pdf";
            try
            {
                pdfReader = new PdfReader(filepath);
                Rectangle pageSize = pdfReader.GetPageSize(1);
                document = new Document(pageSize);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(tempPath, FileMode.Create));
                document.Open();
                int total = pdfReader.NumberOfPages + 1;
                PdfContentByte cbUnder = writer.DirectContentUnder;
                for (i = 1; i < total; i++)
                {
                    pageTemplate = writer.GetImportedPage(pdfReader, i);
                    cbUnder.AddTemplate(pageTemplate, 0, 0);
                    document.NewPage();
                }
                //============================================设计试验3的表格==================================================
                table = new PdfPTable(3);//  设计试验3的表格总共有3列
                table.WidthPercentage = 82;
                table.SetTotalWidth(new float[] { 100f, 200f, 250f });
                ///表頭設計
                //添加段落，第二个参数指定使用fonttitle格式的字体，写入中文必须指定字体否则无法显示中文
                Title = new Paragraph("3. 滑板控制装置首板收板时间试验记录", fonttitle);
                Title.SetAlignment("center"); //设置居中 
                document.Add(Title); //将标题段加入PDF文档中
                //空一行 
                nullb = new Paragraph(" ", fonttitle2);
                nullb.Leading = 10;  //此数值用于调整空白大小0
                document.Add(nullb);
                if (status == 0)
                    dtb = SY3_DB();//表格3设计单独做一个函数
                else
                    dtb = History.Final_Pdf("SY3_Timer_Final");
                table = Export_Table_Design.SY3_Table(table, dtb);
                document.Add(table);//將table放到pdf文件中
                //===================================================设计试验4的表格=================================
                //空一行 
                nullb = new Paragraph(" ", fonttitle2);
                nullb.Leading = 60;  //此数值用于调整空白大小0
                document.Add(nullb);
                table = new PdfPTable(5);//  设计试验4的表格总共有5列
                table.WidthPercentage = 82;
                table.SetTotalWidth(new float[] { 100f, 200f, 200f, 200f, 200f });
                Title = new Paragraph("4. 滑板控制装置连续收板（手动20次）试验记录", fonttitle);
                Title.SetAlignment("center");
                document.Add(Title);
                nullb = new Paragraph(" ", fonttitle2);
                nullb.Leading = 10;  //此数值用于调整空白大小0
                document.Add(nullb);
                if (status == 0)
                    dtb = SY4_DB();//表格4设计单独做一个函数
                else
                    dtb = History.Final_Pdf("SY4_Lxsb_Final");
                table = Export_Table_Design.SY4_Table(table, dtb);
                document.Add(table);//將table放到pdf文件中
                Title = new Paragraph("         注：试验中“√”表示动作正常，“×”表示动作异常", fonttitle3);
                document.Add(Title); //将标题段加入PDF文档中
                //=============================================添加新的一页，设计试验5的表格=========================
                document.SetPageSize(PageSize.A4.Rotate());  //  重新设置文档为横向
                document.NewPage();
                table = new PdfPTable(18);//  设计试验5的表格总共有18列
                table.WidthPercentage = 100;
                Title = new Paragraph("5. 滑板控制装置分档程控控制及程控精度试验记录", fonttitle);
                Title.SetAlignment("center");
                document.Add(Title);
                nullb = new Paragraph(" ", fonttitle2);
                nullb.Leading = 10;  //此数值用于调整空白大小0
                document.Add(nullb);
                if (status == 0)
                    dtb = SY5_DB();//表格5设计单独做一个函数
                else
                    dtb = History.Final_Pdf("SY5_Fdck_Final");
                table = Export_Table_Design.SY5_Table(table, dtb);
                document.Add(table);//將table放到pdf文件中
                Title = new Paragraph(" 注：表中θ表示尾翼板角度，β表示首下板角度，φ表示首上板角度", fonttitle3);
                document.Add(Title); //将标题段加入PDF文档中
                //=============================================添加新的一页，设计试验6的表格=========================
                document.SetPageSize(PageSize.A4.Rotate());  //  重新设置文档为横向
                document.NewPage();
                table = new PdfPTable(21);//  设计试验6的表格总共有21列
                table.WidthPercentage = 100;
                Title = new Paragraph("6. 滑板控制装置系统调试精度试验记录", fonttitle);
                Title.SetAlignment("center");
                document.Add(Title);
                nullb = new Paragraph(" ", fonttitle2);
                nullb.Leading = 10;  //此数值用于调整空白大小0
                document.Add(nullb);
                if (status == 0)
                    dtb = SY6_DB();//表格3设计单独做一个函数
                else
                    dtb = History.Final_Pdf("SY6_Xtts_Final");
                table = Export_Table_Design.SY6_Table(table, dtb);
                document.Add(table);//將table放到pdf文件中
                Title = new Paragraph("注：表中θ表示尾翼板角度，β表示首下板角度，φ表示首上板角度", fonttitle3);
                Title.SetAlignment("center");
                document.Add(Title); //将标题段加入PDF文档中
                //=============================================添加新的一页，设计火炮试验的表格=========================
                if (MainFrom.comBoxData_Form != "HBK-II/1")
                {
                    document.SetPageSize(PageSize.A4.Rotate());  //  重新设置文档为横向
                    document.NewPage();
                    table = new PdfPTable(13);//  设计试验8的表格总共有13列
                    table.WidthPercentage = 100;
                    Paragraph Title1 = new Paragraph("7. 滑板控制装置(I型车)火炮联锁试验记录", fonttitle);
                    Title1.SetAlignment("center");
                    document.Add(Title1);
                    nullb = new Paragraph(" ", fonttitle2);
                    nullb.Leading = 10;  //此数值用于调整空白大小0
                    document.Add(nullb);
                    if (status == 0)
                        dtb = SY8_DB();//表格火炮联锁试验设计单独做一个函数
                    else
                        dtb = History.Final_Pdf("SY8_Hplx_Final");
                    table = Export_Table_Design.SY8_Table(table, dtb);
                    document.Add(table);//將table放到pdf文件中
                    Title1 = new Paragraph("注：试验中“√”表示动作正常，“×”表示动作异常", fonttitle3);
                    document.Add(Title1); //将标题段加入PDF文档中
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                document.Close();
                if (pdfReader != null)
                    pdfReader.Close();
                File.Copy(tempPath, filepath, true);
                File.Delete(tempPath);
                Exp_PDF2(filepath,status);
            }
        }
        /// <summary>
        /// 设计项目试验7的打印报表
        /// </summary>
        /// <param name="filepath"></param>
        public static void Exp_PDF2(string filepath, int status)
        {
            PdfReader pdfReader = null;
            PdfImportedPage pageTemplate = null;
            int i, j, k;
            Document document = null;
            Paragraph Title;
            //============================================加载模板并在模板后添加新表格==================================================
            string tempPath = Path.GetDirectoryName(filepath) + Path.GetFileNameWithoutExtension(filepath) + "_temp.pdf";
            try
            {
                pdfReader = new PdfReader(filepath);
                Rectangle pageSize = pdfReader.GetPageSize(1);
                document = new Document(pageSize);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(tempPath, FileMode.Create));
                document.Open();
                int total = pdfReader.NumberOfPages + 1;
                PdfContentByte cbUnder = writer.DirectContentUnder;
                for (i = 1; i < total; i++)
                {
                    pageTemplate = writer.GetImportedPage(pdfReader, i);
                    cbUnder.AddTemplate(pageTemplate, 0, 0);
                    document.NewPage();
                }
                //============================================设计试验7的表格==================================================
                table = new PdfPTable(2);//  设计试验7的表格总共有6列
                table.WidthPercentage = 100;
                if (MainFrom.comBoxData_Form == "HBK-II/1")
                    Title = new Paragraph("7. 滑板控制装置连续收板（程控20次）试验记录", fonttitle);
                else Title = new Paragraph("8. 滑板控制装置连续收板（程控20次）试验记录", fonttitle);
                Title.SetAlignment("center");
                document.Add(Title);
                nullb = new Paragraph(" ", fonttitle2);
                nullb.Leading = 10;  //此数值用于调整空白大小0
                document.Add(nullb);
                int flag = 1;
                PdfPTable table1 = new PdfPTable(6);
                PdfPTable table2 = new PdfPTable(6);
                if (status == 0)
                    dtb = SY7_DB();
                else
                    dtb = History.Final_Pdf("SY7_Lxsb_Ck_Final");
                table1 = Export_Table_Design.SY7_Table1(table1, ref flag,dtb);//表格7设计单独做一个函数
                table2 = Export_Table_Design.SY7_Table2(table2, ref flag,dtb);//表格7设计单独做一个函数
                cell1 = new PdfPCell(table1);
                cell1.Padding = 0;
                table.AddCell(cell1);
                cell1 = new PdfPCell(table2);
                cell1.Padding = 0.5f;
                table.AddCell(cell1);
                document.Add(table);//將table放到pdf文件中
                Title = new Paragraph("  注：试验中“√”表示动作正常，“×”表示动作异常", fonttitle3);
                document.Add(Title); //将标题段加入PDF文档中
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                document.Close();
                if (pdfReader != null)
                    pdfReader.Close();
                File.Copy(tempPath, filepath, true);
                File.Delete(tempPath);
            }
        }    
        public static DataTable DBQuery(string table)
        {
            String sqlQuery = "select * from "+table+" where Sy_user=@SYY and C_no=@cno and P_no=@pno and Angle_no=@ano";
            DataSet ds = SqlHelper.ExecuteDataSet(sqlQuery, new SqlParameter("@SYY", MainFrom.Uname),
                new SqlParameter("@cno", MainFrom.textB_HbData_Form),
                new SqlParameter("@pno", MainFrom.comBoxData_Form),
                new SqlParameter("@ano", MainFrom.textB_CgqData_Form));
            DataTable dt = ds.Tables[0];
            return dt;
        }
        public static void DBDel(string table)
        {
            String sqlQuery = "Delete from " + table + " where Sy_user=@SYY and C_no=@cno and P_no=@pno and Angle_no=@ano";
            SqlHelper.ExecuteNonQuery(sqlQuery, new SqlParameter("@SYY", MainFrom.Uname),
                new SqlParameter("@cno", MainFrom.textB_HbData_Form),
                new SqlParameter("@pno", MainFrom.comBoxData_Form),
                new SqlParameter("@ano", MainFrom.textB_CgqData_Form));
        }
        public static DataTable SY1_DB()
        {
            String sql;
            string tb = "Sy1_Fdsk";
            DataTable dt = DBQuery(tb);
            if (dt.Rows.Count == 1)
            {
                sql = "insert into SY1_Fdsk_Final(Ss_s,Ss_f,Sx_s,Sx_f,Wy_s,Wy_f,Insert_timer,Sy_user,P_no,C_no,Angle_no) " +
                    "select Ss_s,Ss_f,Sx_s,Sx_f,Wy_s,Wy_f,Insert_timer,Sy_user,P_no,C_no,Angle_no" +
                    " from SY1_Fdsk where Sy_user=@SYY and C_no=@cno and P_no=@pno and Angle_no=@ano";
                SqlHelper.ExecuteNonQuery(sql, 
                    new SqlParameter("@SYY", MainFrom.Uname), 
                    new SqlParameter("@cno", MainFrom.textB_HbData_Form),
                    new SqlParameter("@pno", MainFrom.comBoxData_Form),
                    new SqlParameter("@ano", MainFrom.textB_CgqData_Form));
                sql = "update " + tb + "_Final set count=@cnt where count is null";
                SqlHelper.ExecuteNonQuery(sql,
                    new SqlParameter("@cnt", count));
            }
            DBDel(tb);
            return dt;
        }
        private static DataTable SY2_DB()
        {
            String sql;
            string tb = "Sy2_Gzzd";
            DataTable dt = DBQuery(tb);
            if (dt.Rows.Count == 1)
            {
                sql = "insert into Sy2_Gzzd_Final(XH_G,YC_G,DD_D,DD_Z,DD_G,DL_D,DL_Z,DL_G,JT_D,JT_Z,DY_D,DY_Z,DY_G,DL_CK,KL_CK,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                    " select XH_G,YC_G,DD_D,DD_Z,DD_G,DL_D,DL_Z,DL_G,JT_D,JT_Z,DY_D,DY_Z,DY_G,DL_CK,KL_CK,Insert_timer,Sy_user,P_no,C_no,Angle_no from Sy2_Gzzd " +
                    "where Sy_user=@SYY and C_no=@cno and P_no=@pno and Angle_no=@ano";
                SqlHelper.ExecuteNonQuery(sql, 
                    new SqlParameter("@SYY", MainFrom.Uname), 
                    new SqlParameter("@cno", MainFrom.textB_HbData_Form), 
                    new SqlParameter("@pno", MainFrom.comBoxData_Form),
                    new SqlParameter("@ano", MainFrom.textB_CgqData_Form));
                sql = "update "+tb+ "_Final set count=@cnt where count is null";
                SqlHelper.ExecuteNonQuery(sql,
                    new SqlParameter("@cnt", count));
            }
            DBDel(tb);
            return dt;
        }
        public static DataTable SY3_DB()
        {
            String sqlQuery;
            string tb = "SY3_Timer";
            DataTable dt = DBQuery(tb);
            if (dt.Rows.Count == 3)
            {
                sqlQuery = "insert into SY3_Timer_Final(timer,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                    " select timer,Insert_timer,Sy_user,P_no,C_no,Angle_no from SY3_Timer " +
                    "where Sy_user=@SYY and C_no=@cno and P_no=@pno and Angle_no=@ano";
                SqlHelper.ExecuteNonQuery(sqlQuery,
                    new SqlParameter("@SYY", MainFrom.Uname),
                    new SqlParameter("@cno", MainFrom.textB_HbData_Form),
                    new SqlParameter("@pno", MainFrom.comBoxData_Form),
                    new SqlParameter("@ano", MainFrom.textB_CgqData_Form));
                sqlQuery = "update " + tb + "_Final set count=@cnt where count is null";
                SqlHelper.ExecuteNonQuery(sqlQuery,
                    new SqlParameter("@cnt", count));
            }
                DBDel(tb);
            return dt;
        }
        public static DataTable SY4_DB()
        {
            String sqlQuery;
            string tb = "SY4_Lxsb";
            DataTable dt = DBQuery(tb);
            if (dt.Rows.Count == 20)
            {
                sqlQuery = "insert into SY4_Lxsb_Final(SS_data,SX_data,WY_data,Beizhu,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                    " select SS_data,SX_data,WY_data,Beizhu,Insert_timer,Sy_user,P_no,C_no,Angle_no from SY4_Lxsb " +
                    "where Sy_user=@SYY and C_no=@cno and P_no=@pno and Angle_no=@ano";
                SqlHelper.ExecuteNonQuery(sqlQuery,
                    new SqlParameter("@SYY", MainFrom.Uname),
                    new SqlParameter("@cno", MainFrom.textB_HbData_Form),
                    new SqlParameter("@pno", MainFrom.comBoxData_Form),
                    new SqlParameter("@ano", MainFrom.textB_CgqData_Form));
                sqlQuery = "update " + tb + "_Final set count=@cnt where count is null";
                SqlHelper.ExecuteNonQuery(sqlQuery,
                    new SqlParameter("@cnt", count));
            }
            DBDel(tb);
            return dt;
        }
        public static DataTable SY5_DB()
        {
            String sqlQuery;
            string tb = "SY5_Fdck";
            DataTable dt = DBQuery(tb);
            if (dt.Rows.Count == 19)
            {
                sqlQuery = "insert into SY5_Fdck_Final(Gk_no,Z_cgq1,Z_cgq2,Z_cgq3,Y_cgq1,Y_cgq2,Y_cgq3,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                    " select Gk_no,Z_cgq1,Z_cgq2,Z_cgq3,Y_cgq1,Y_cgq2,Y_cgq3,Insert_timer,Sy_user,P_no,C_no,Angle_no from SY5_Fdck " +
                    "where Sy_user=@SYY and C_no=@cno and P_no=@pno and Angle_no=@ano";
                SqlHelper.ExecuteNonQuery(sqlQuery,
                    new SqlParameter("@SYY", MainFrom.Uname),
                    new SqlParameter("@cno", MainFrom.textB_HbData_Form),
                    new SqlParameter("@pno", MainFrom.comBoxData_Form),
                    new SqlParameter("@ano", MainFrom.textB_CgqData_Form));
                sqlQuery = "update " + tb + "_Final set count=@cnt where count is null";
                SqlHelper.ExecuteNonQuery(sqlQuery,
                    new SqlParameter("@cnt", count));
            }
                DBDel(tb);
            return dt;
        }
        public static DataTable SY6_DB()
        {
            String sqlQuery;
            string tb = "SY6_Xtts";
            DataTable dt = DBQuery(tb);
            if (dt.Rows.Count == 18)
            {
                sqlQuery = "insert into SY6_Xtts_Final(ZC1,ZC2,ZC3,ZX1,ZX2,ZX3,YC1,YC2,YC3,YX1,YX2,YX3,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                    " select ZC1,ZC2,ZC3,ZX1,ZX2,ZX3,YC1,YC2,YC3,YX1,YX2,YX3,Insert_timer,Sy_user,P_no,C_no,Angle_no from SY6_Xtts " +
                    "where Sy_user=@SYY and C_no=@cno and P_no=@pno and Angle_no=@ano";
                SqlHelper.ExecuteNonQuery(sqlQuery,
                    new SqlParameter("@SYY", MainFrom.Uname),
                    new SqlParameter("@cno", MainFrom.textB_HbData_Form),
                    new SqlParameter("@pno", MainFrom.comBoxData_Form),
                    new SqlParameter("@ano", MainFrom.textB_CgqData_Form));
                sqlQuery = "update " + tb + "_Final set count=@cnt where count is null";
                SqlHelper.ExecuteNonQuery(sqlQuery,
                    new SqlParameter("@cnt", count));
            }
            DBDel(tb);
            return dt;
        }
        /// <summary>
        /// 试验7对数据库的操作
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataTable SY7_DB()
        {
            String sqlQuery;
            string tb = "SY7_Lxsb_Ck";
            DataTable dt = DBQuery(tb);
            if (dt.Rows.Count == 81)
            {
                sqlQuery = "insert into SY7_Lxsb_Ck_Final(Gk,Ss_b,Sx_b,Wy_b,Beizhu,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                    " select Gk,Ss_b,Sx_b,Wy_b,Beizhu,Insert_timer,Sy_user,P_no,C_no,Angle_no from SY7_Lxsb_Ck " +
                    "where Sy_user=@SYY and C_no=@cno and P_no=@pno and Angle_no=@ano";
                SqlHelper.ExecuteNonQuery(sqlQuery,new SqlParameter("@SYY", MainFrom.Uname),
                    new SqlParameter("@cno", MainFrom.textB_HbData_Form),
                    new SqlParameter("@pno", MainFrom.comBoxData_Form),
                    new SqlParameter("@ano", MainFrom.textB_CgqData_Form));
                sqlQuery = "update " + tb + "_Final set count=@cnt where count is null";
                SqlHelper.ExecuteNonQuery(sqlQuery,new SqlParameter("@cnt", count));
            }
            DBDel(tb);
            return dt;
        }
        public static DataTable SY8_DB()
        {
            String sqlQuery;
            string tb = "SY8_Hplx";
            DataTable dt = DBQuery(tb);
            if (dt.Rows.Count == 24)
            {
                sqlQuery = "insert into SY8_Hplx_Final(CS_no,K_yang,K_zuo,K_you,Hbsq,Hpyxd,Z_cgq1," +
                "Z_cgq2,Z_cgq3,Y_cgq1,Y_cgq2,Y_cgq3,Insert_timer,Sy_user,P_no,C_no,Angle_no)" +
                    " select CS_no,K_yang,K_zuo,K_you,Hbsq,Hpyxd,Z_cgq1," +
                "Z_cgq2,Z_cgq3,Y_cgq1,Y_cgq2,Y_cgq3,Insert_timer,Sy_user,P_no,C_no,Angle_no from SY8_Hplx " +
                    "where Sy_user=@SYY and C_no=@cno and P_no=@pno and Angle_no=@ano";
                SqlHelper.ExecuteNonQuery(sqlQuery,new SqlParameter("@SYY", MainFrom.Uname),
                    new SqlParameter("@cno", MainFrom.textB_HbData_Form),
                    new SqlParameter("@pno", MainFrom.comBoxData_Form),
                    new SqlParameter("@ano", MainFrom.textB_CgqData_Form));
                sqlQuery = "update " + tb + "_Final set count=@cnt where count is null";
                SqlHelper.ExecuteNonQuery(sqlQuery,new SqlParameter("@cnt", count));
            }
            DBDel(tb);
            return dt;
        }
    }
}