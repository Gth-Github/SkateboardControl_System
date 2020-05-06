using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using testsjk;

namespace SkateboardControl_System
{
    class Export_One_SY
    {
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
        public static void Exp_SY1_SY2()
        {
            PdfReader pdfReader; PdfStamper pdfStamper; AcroFields pdfFormFields;
            BaseFont bf1 = BaseFont.CreateFont("C:\\Windows\\Fonts\\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //文件保存的真正路径
            String tempname = getfilename();
            if (tempname == "") return;
            string path = AppDomain.CurrentDomain.BaseDirectory;
            int flag = 1;
            path = System.IO.Directory.GetParent(path).Parent.Parent.FullName + "\\SY1_SY2_MB.pdf";
            //MessageBox.Show(path);
            pdfReader = new PdfReader(path);//模板的路径
            pdfStamper = new PdfStamper(pdfReader, new FileStream(tempname, FileMode.OpenOrCreate));
            pdfFormFields = pdfStamper.AcroFields;
            pdfStamper.FormFlattening = true;
            pdfFormFields.AddSubstitutionFont(bf1);
            dtb = Export_All_SY.DBQuery("Sy1_Fdsk");
            Export_All_SY.DBDel("Sy1_Fdsk");
            Export_Table_Design.SY1_Table(pdfFormFields, ref flag, dtb);
            flag = 1;
            dtb = Export_All_SY.DBQuery("Sy2_Gzzd");
            Export_All_SY.DBDel("Sy2_Gzzd");
            Export_Table_Design.SY2_Table(pdfFormFields, ref flag, dtb);
            pdfStamper.Close();
            pdfReader.Close();
        }
        public static void Exp_SY3_SY4()
        {
            String tempname = getfilename();
            if (tempname == "") return;
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(tempname, FileMode.Create));
            document.Open();
            table = new PdfPTable(3);//  设计试验3的表格总共有3列
            table.WidthPercentage = 82;
            table.SetTotalWidth(new float[] { 100f, 200f, 250f });
            ///表頭設計
            //添加段落，第二个参数指定使用fonttitle格式的字体，写入中文必须指定字体否则无法显示中文
            Paragraph Title = new Paragraph("3. 滑板控制装置首板收板时间试验记录", fonttitle);
            Title.SetAlignment("center"); //设置居中 
            document.Add(Title); //将标题段加入PDF文档中
                                 //空一行 
            nullb = new Paragraph(" ", fonttitle2);
            nullb.Leading = 10;  //此数值用于调整空白大小0
            document.Add(nullb);
            DataTable dtb = Export_All_SY.DBQuery("SY3_Timer");//表格3设计单独做一个函数
            table = Export_Table_Design.SY3_Table(table, dtb);
            Export_All_SY.DBDel("SY3_Timer");
            document.Add(table);
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
            dtb = Export_All_SY.DBQuery("SY4_Lxsb");//表格4设计单独做一个函数
            table = Export_Table_Design.SY4_Table(table, dtb);
            Export_All_SY.DBDel("SY4_Lxsb");
            document.Add(table);//將table放到pdf文件中
            Title = new Paragraph("         注：试验中“√”表示动作正常，“×”表示动作异常", fonttitle3);
            document.Add(Title); //将标题段加入PDF文档中
            document.Close();
        }
        public static void Exp_SY5()
        {
            String tempname = getfilename();
            if (tempname == "") return;
            Document document = new Document();
            document.SetPageSize(PageSize.A4.Rotate());  //  设置文档为横向
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(tempname, FileMode.Create));
            document.Open();
            document.NewPage();
            table = new PdfPTable(18);//  设计试验5的表格总共有18列
            table.WidthPercentage = 100;
            Paragraph Title = new Paragraph("5. 滑板控制装置分档程控控制及程控精度试验记录", fonttitle);
            Title.SetAlignment("center");
            document.Add(Title);
            nullb = new Paragraph(" ", fonttitle2);
            nullb.Leading = 10;  //此数值用于调整空白大小0
            document.Add(nullb);
            dtb=Export_All_SY.DBQuery("SY5_Fdck");
            table = Export_Table_Design.SY5_Table(table, dtb);
            Export_All_SY.DBDel("SY5_Fdck");
            document.Add(table);//將table放到pdf文件中
            Title = new Paragraph(" 注：表中θ表示尾翼板角度，β表示首下板角度，φ表示首上板角度", fonttitle3);
            document.Add(Title); //将标题段加入PDF文档中
            document.Close();
        }
        public static void Exp_SY6()
        {
            String tempname = getfilename();
            if (tempname == "") return;
            Document document = new Document();
            document.SetPageSize(PageSize.A4.Rotate());  //  设置文档为横向
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(tempname, FileMode.Create));
            document.Open();
            document.NewPage();
            table = new PdfPTable(21);//  设计试验6的表格总共有21列
            table.WidthPercentage = 100;
            Paragraph Title = new Paragraph("6. 滑板控制装置系统调试精度试验记录", fonttitle);
            Title.SetAlignment("center");
            document.Add(Title);
            nullb = new Paragraph(" ", fonttitle2);
            nullb.Leading = 10;  //此数值用于调整空白大小0
            document.Add(nullb);
            dtb =Export_All_SY.DBQuery("SY6_Xtts");
            table = Export_Table_Design.SY6_Table(table, dtb);
            Export_All_SY.DBDel("SY6_Xtts");
            document.Add(table);//將table放到pdf文件中
            Title = new Paragraph("注：表中θ表示尾翼板角度，β表示首下板角度，φ表示首上板角度", fonttitle3);
            Title.SetAlignment("center");
            document.Add(Title); //将标题段加入PDF文档中
            document.Close();
        }
        public static void Exp_HPSY()
        {
            String tempname = getfilename();
            if (tempname == "") return;
            Document document = new Document();
            document.SetPageSize(PageSize.A4.Rotate());  //  设置文档为横向
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(tempname, FileMode.Create));
            document.Open();
            document.NewPage();
            table = new PdfPTable(13);//  设计试验8的表格总共有13列
            table.WidthPercentage = 100;
            Paragraph Title1 = new Paragraph("7. 滑板控制装置(I型车)火炮联锁试验记录", fonttitle);
            Title1.SetAlignment("center");
            document.Add(Title1);
            nullb = new Paragraph(" ", fonttitle2);
            nullb.Leading = 10;  //此数值用于调整空白大小0
            document.Add(nullb);
            dtb = Export_All_SY.DBQuery("SY8_Hplx");
            table = Export_Table_Design.SY8_Table(table, dtb);
            Export_All_SY.DBDel("SY8_Hplx");
            document.Add(table);//將table放到pdf文件中
            Title1 = new Paragraph("注：试验中“√”表示动作正常，“×”表示动作异常", fonttitle3);
            document.Add(Title1); //将标题段加入PDF文档中
            document.Close();
        }
        public static void Exp_SY7()
        {
            Paragraph Title;
            String tempname = getfilename();
            if (tempname == "") return;
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(tempname, FileMode.Create));
            document.Open();
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
            dtb = Export_All_SY.DBQuery("SY7_Lxsb_Ck");
            table1 = Export_Table_Design.SY7_Table1(table1, ref flag, dtb);//表格7设计单独做一个函数
            table2 = Export_Table_Design.SY7_Table2(table2, ref flag, dtb);//表格7设计单独做一个函数
            Export_All_SY.DBDel("SY7_Lxsb_Ck");
            cell1 = new PdfPCell(table1);
            cell1.Padding = 0;
            table.AddCell(cell1);
            cell1 = new PdfPCell(table2);
            cell1.Padding = 0.5f;
            table.AddCell(cell1);
            document.Add(table);//將table放到pdf文件中
            Title = new Paragraph("  注：试验中“√”表示动作正常，“×”表示动作异常", fonttitle3);
            document.Add(Title); //将标题段加入PDF文档中
            document.Close();
        }
        static string getfilename()
        {
            String str = " ";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text documents (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Text文件到";
            DateTime now = DateTime.Now;
            string[] com = MainFrom.comBoxData_Form.Split('/');
            string pno = "";
            for (int k = 0; k < com.Length; k++)
                pno = pno + com[k];
            saveFileDialog.FileName = pno + "_" +
            MainFrom.textB_HbData_Form + "_" +
            MainFrom.Uname + "_" + MainFrom.textB_CgqData_Form + "_" +
            now.Year.ToString().PadLeft(2) + "" +
            now.Month.ToString().PadLeft(2, '0') + "" +
            now.Day.ToString().PadLeft(2, '0') + "-" +
            now.Hour.ToString().PadLeft(2, '0') + "" +
            now.Minute.ToString().PadLeft(2, '0') + "" +
            now.Second.ToString().PadLeft(2, '0');
            DialogResult result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                //文件保存的真正路径
                string tempFilePath = saveFileDialog.FileName;               
                return tempFilePath;
            }
            return "";
        }
    }
}
