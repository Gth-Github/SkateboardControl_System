using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SkateboardControl_System
{
    class Export_Table_Design
    {
        static PdfPTable table;//声明表格对象

        static PdfPCell cell1;//声明单元格对象
        static Paragraph nullb;//声明空行对象
                               //SIMSUN.TTC：宋体和新宋体   SIMKAI.TTF：楷体   SIMHEI.TTF：黑体   SIMFANG.TTF：仿宋体
        static BaseFont bftitle = BaseFont.CreateFont("C:\\Windows\\Fonts\\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        static iTextSharp.text.Font fonttitle = new iTextSharp.text.Font(bftitle, 16); //表頭字体，大小16 
                                                                                       //用系统中的字体文件SimSun.ttc创建文件字体 
        static BaseFont bf1 = BaseFont.CreateFont("C:\\Windows\\Fonts\\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        static iTextSharp.text.Font fonttitle2 = new iTextSharp.text.Font(bf1, 12); //表格內容字体，大小15
        static iTextSharp.text.Font fonttitle3 = new iTextSharp.text.Font(bf1, 10); //表格內容字体，大小15
        static string[] gongk = new string[5];
        private static string change_IntToString(int changenum)
        {
            if (changenum == 0) return "×";
            return "√";
        }
        private static void change_Gongkuang(int gk)
        {
            if (gk == 1)
            {
                gongk[0] = "陆上"; gongk[1] = "-"; gongk[2] = "90";
                gongk[3] = "29"; gongk[4] = "33";
            }
            if (gk == 2)
            {
                gongk[0] = "低速"; gongk[1] = "-"; gongk[2] = "0";
                gongk[3] = "22"; gongk[4] = "180";
            }
            if (gk == 3)
            {
                gongk[0] = "中速"; gongk[1] = "-"; gongk[2] = "-3.5";
                gongk[3] = "18"; gongk[4] = "180";
            }
            if (gk == 4)
            {
                gongk[0] = "高速"; gongk[1] = "6.4"; gongk[2] = "-3.5";
                gongk[3] = "18"; gongk[4] = "180";
            }
            if (gk == 5)
            {
                gongk[0] = "高速"; gongk[1] = "6.8"; gongk[2] = "-3.5";
                gongk[3] = "14"; gongk[4] = "180";
            }
            if (gk == 6)
            {
                gongk[0] = "高速"; gongk[1] = "7.2"; gongk[2] = "-3.5";
                gongk[3] = "10"; gongk[4] = "180";
            }
            if (gk == 7)
            {
                gongk[0] = "应急"; gongk[1] = "-"; gongk[2] = "0";
                gongk[3] = "22"; gongk[4] = "180";
            }
        }
        private static string change_CSno(int cs)
        {
            if (cs == 1) return "陆上";
            if (cs == 2) return "低速";
            if (cs == 3) return "中速";
            if (cs == 4) return "高速";
            if (cs == 5) return "高速";
            if (cs == 6) return "中速";
            if (cs == 7) return "低速";
            if (cs == 8) return "陆上";
            return " ";
        }
        public static void SY1_Table(AcroFields pdfFormFields, ref int flag,DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                    if ((int)dt.Rows[0][1] == 0)
                    {
                        flag = 0;
                        pdfFormFields.SetField("Ss_s", "×");
                    }
                    else pdfFormFields.SetField("Ss_s", "√");
                    if ((int)dt.Rows[0][2] == 0)
                    {
                        flag = 0;
                        pdfFormFields.SetField("Ss_f", "×");
                    }
                    else pdfFormFields.SetField("Ss_f", "√");
                    if ((int)dt.Rows[0][3] == 0)
                    {
                        flag = 0;
                        pdfFormFields.SetField("Sx_s", "×");
                    }
                    else pdfFormFields.SetField("Sx_s", "√");
                    if ((int)dt.Rows[0][4] == 0)
                    {
                        flag = 0;
                        pdfFormFields.SetField("Sx_f", "×");
                    }
                    else pdfFormFields.SetField("Sx_f", "√");
                    if ((int)dt.Rows[0][5] == 0)
                    {
                        flag = 0;
                        pdfFormFields.SetField("Wy_s", "×");
                    }
                    else pdfFormFields.SetField("Wy_s", "√");
                    if ((int)dt.Rows[0][6] == 0)
                    {
                        flag = 0;
                        pdfFormFields.SetField("Wy_f", "×");
                    }
                    else pdfFormFields.SetField("Wy_f", "√");

                    if (flag == 0) pdfFormFields.SetField("Sy1_JL", "试验未通过");
                    if (flag == 1) pdfFormFields.SetField("Sy1_JL", "试验通过");
            }
        }
        public static void SY2_Table(AcroFields pdfFormFields, ref int flag, DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                if ((int)dt.Rows[0][1] == 0)
                {
                    flag = 0;
                    pdfFormFields.SetField("XH_G", "×");
                }
                else pdfFormFields.SetField("XH_G", "√");
                if ((int)dt.Rows[0][2] == 0)
                {
                    flag = 0;
                    pdfFormFields.SetField("YC_G", "×");
                }
                else pdfFormFields.SetField("YC_G", "√");
                if ((int)dt.Rows[0][3] == 0)
                {
                    flag = 0;
                    pdfFormFields.SetField("DD_D", "×");
                }
                else pdfFormFields.SetField("DD_D", "√");
                if ((int)dt.Rows[0][4] == 0)
                {
                    flag = 0;
                    pdfFormFields.SetField("DD_Z", "×");
                }
                else pdfFormFields.SetField("DD_Z", "√");
                if ((int)dt.Rows[0][5] == 0)
                {
                    flag = 0;
                    pdfFormFields.SetField("DD_G", "×");
                }
                else pdfFormFields.SetField("DD_G", "√");
                if ((int)dt.Rows[0][6] == 0)
                {
                    flag = 0;
                    pdfFormFields.SetField("DL_D", "×");
                }
                else pdfFormFields.SetField("DL_D", "√");
                if ((int)dt.Rows[0][7] == 0)
                {
                    flag = 0;
                    pdfFormFields.SetField("DL_Z", "×");
                }
                else pdfFormFields.SetField("DL_Z", "√");
                if ((int)dt.Rows[0][8] == 0)
                {
                    flag = 0;
                    pdfFormFields.SetField("DL_G", "×");
                }
                else pdfFormFields.SetField("DL_G", "√");
                if ((int)dt.Rows[0][9] == 0)
                {
                    flag = 0;
                    pdfFormFields.SetField("JT_D", "×");
                }
                else pdfFormFields.SetField("JT_D", "√");
                if ((int)dt.Rows[0][10] == 0)
                {
                    flag = 0;
                    pdfFormFields.SetField("JT_Z", "×");
                }
                else pdfFormFields.SetField("JT_Z", "√");
                if ((int)dt.Rows[0][11] == 0)
                {
                    flag = 0;
                    pdfFormFields.SetField("DY_D", "×");
                }
                else pdfFormFields.SetField("DY_D", "√");
                if ((int)dt.Rows[0][12] == 0)
                {
                    flag = 0;
                    pdfFormFields.SetField("DY_Z", "×");
                }
                else pdfFormFields.SetField("DY_Z", "√");
                if ((int)dt.Rows[0][13] == 0)
                {
                    flag = 0;
                    pdfFormFields.SetField("DY_G", "×");
                }
                else pdfFormFields.SetField("DY_G", "√");
                if ((int)dt.Rows[0][14] == 0)
                {
                    flag = 0;
                    pdfFormFields.SetField("DL_CK", "×");
                }
                else pdfFormFields.SetField("DL_CK", "√");
                if ((int)dt.Rows[0][15] == 0)
                {
                    flag = 0;
                    pdfFormFields.SetField("KL_CK", "×");
                }
                else pdfFormFields.SetField("KL_CK", "√");
                if (flag == 0) pdfFormFields.SetField("Sy2_JL", "试验未通过");
                if (flag == 1) pdfFormFields.SetField("Sy2_JL", "试验通过");
            }
        }
        public static PdfPTable SY3_Table(PdfPTable table,DataTable dt)
        {
            int i, j, k, flag = 1;
            cell1 = new PdfPCell(new Phrase("次 数", fonttitle2));
            cell1.MinimumHeight = 40; //设置单元格高度
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("首板收起时间（S)", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("合格判据", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);

            for (i = 0; i < 3; i++)
            {
                String str = "" + (i + 1);
                cell1 = new PdfPCell(new Phrase(str, fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                cell1.MinimumHeight = 40; //设置单元格高度
                table.AddCell(cell1);
                if (dt.Rows.Count == 0) { cell1 = new PdfPCell(new Phrase(" ", fonttitle2)); }
                else if(i < dt.Rows.Count)
                {
                    if ((int)dt.Rows[i]["timer"] > 20) flag = 0;
                    cell1 = new PdfPCell(new Phrase(dt.Rows[i]["timer"].ToString(), fonttitle2));
                }
                else
                {
                    flag = 0;
                    cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
                }
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                if (i == 0)
                {
                    cell1 = new PdfPCell(new Phrase("≤20S", fonttitle2));
                    cell1.Rowspan = 3;
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                    cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                    table.AddCell(cell1);
                }
            }
            cell1 = new PdfPCell(new Phrase("结论", fonttitle2));
            cell1.MinimumHeight = 40; //设置单元格高度
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            if (dt.Rows.Count != 0)
            {
                if (flag == 0) cell1 = new PdfPCell(new Phrase("试验未通过", fonttitle2));
                else cell1 = new PdfPCell(new Phrase("试验通过", fonttitle2));
            }
            else cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
            table.AddCell(cell1);
            return table;
        }
        public static PdfPTable SY4_Table(PdfPTable table, DataTable dt)
        {
            int i, j, k, flag = 1;
            cell1 = new PdfPCell(new Phrase("次数", fonttitle2));
            cell1.MinimumHeight = 20; //设置单元格高度
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("首上板动作", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("首下板动作", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("尾翼板动作", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("备注", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            for (j = 0; j < 20; j++)
            {
                String str = "" + (j + 1);
                cell1 = new PdfPCell(new Phrase(str, fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                if (dt.Rows.Count == 0)
                {
                    for (k = 0; k < 4; k++)
                    {
                        cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                }
                else if (dt.Rows.Count > 0 && dt.Rows.Count < 20)
                {
                    if (j < dt.Rows.Count)
                    {
                        for (k = 0; k < 3; k++)
                        {
                            cell1 = new PdfPCell(new Phrase(change_IntToString((int)dt.Rows[j][k + 1]), fonttitle2));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                            table.AddCell(cell1);
                            if ((int)dt.Rows[j][k + 1] == 0) flag = 0;
                        }
                        cell1 = new PdfPCell(new Phrase(dt.Rows[j][4].ToString(), fonttitle2));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                    else
                    {
                        flag = 0;
                        for (k = 0; k < 4; k++)
                        {
                            cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                            table.AddCell(cell1);
                        }
                    }
                }
                else if(dt.Rows.Count >= 20)
                {
                    for (k = 0; k < 3; k++)
                    {
                        cell1 = new PdfPCell(new Phrase(change_IntToString((int)dt.Rows[j][k + 1]), fonttitle2));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                        if ((int)dt.Rows[j][k + 1] == 0) flag = 0;
                    }
                    cell1 = new PdfPCell(new Phrase(dt.Rows[j][4].ToString(), fonttitle2));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                    cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                    table.AddCell(cell1);
                }
            }
            cell1 = new PdfPCell(new Phrase("结论", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            if (dt.Rows.Count > 0)
            {
                if (flag == 0) cell1 = new PdfPCell(new Phrase("试验未通过", fonttitle2));
                else cell1 = new PdfPCell(new Phrase("试验通过", fonttitle2));
            }
            else cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
            cell1.Colspan = 4;
            table.AddCell(cell1);
            return table;
        }
        public static PdfPTable SY5_Table(PdfPTable table,DataTable dt)
        {
            int i, j, k, flag = 1;
            float θ1, β1, φ1;
            cell1 = new PdfPCell(new Phrase("工况", fonttitle2));
            cell1.Rowspan = 3;

            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("速度（m/s)", fonttitle2));
            cell1.Rowspan = 3;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("设定值（d1)", fonttitle2));
            cell1.Colspan = 3;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("左传感器控制值（d2)", fonttitle2));
            cell1.Colspan = 3;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("右传感器控制值（d2)", fonttitle2));
            cell1.Colspan = 3;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("差值（d2-d1)", fonttitle2));
            cell1.Colspan = 6;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("合格判据", fonttitle2));
            cell1.Rowspan = 3;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("θ°", fonttitle2));
            cell1.Rowspan = 2;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("β°", fonttitle2));
            cell1.Rowspan = 2;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("φ°", fonttitle2));
            cell1.Rowspan = 2;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            for (k = 0; k < 2; k++)
            {
                cell1 = new PdfPCell(new Phrase("θ°", fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("β°", fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("φ°", fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
            }
            cell1 = new PdfPCell(new Phrase("△θ°", fonttitle2));
            cell1.Colspan = 2;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("△β°", fonttitle2));
            cell1.Colspan = 2;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("△φ°", fonttitle2));
            cell1.Colspan = 2;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            for (k = 0; k < 3; k++)
            {
                cell1 = new PdfPCell(new Phrase("左", fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
            }
            for (k = 0; k < 3; k++)
            {
                cell1 = new PdfPCell(new Phrase("右", fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
            }
            for (k = 0; k < 3; k++)
            {
                cell1 = new PdfPCell(new Phrase("左", fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("右", fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
            }
            
            float[] a = new float[6];
            for (i = 0; i < 19; i++)
            {
                if (i < dt.Rows.Count)
                {
                    change_Gongkuang((int)dt.Rows[i][1]);
                    θ1 = Convert.ToSingle(gongk[2]);
                    β1 = Convert.ToSingle(gongk[3]);
                    φ1 = Convert.ToSingle(gongk[4]);
                    a[0] = (float)dt.Rows[i][2] - θ1;
                    a[1] = (float)dt.Rows[i][5] - θ1;
                    a[2] = (float)dt.Rows[i][3] - β1;
                    a[3] = (float)dt.Rows[i][6] - β1;
                    a[4] = (float)dt.Rows[i][4] - φ1;
                    a[5] = (float)dt.Rows[i][7] - φ1;
                    if (a[0] < -1 || a[0] > 1) flag = 0;
                    if (a[1] < -1 || a[1] > 1) flag = 0;
                    if (a[2] < -1 || a[2] > 1) flag = 0;
                    if (a[3] < -1 || a[3] > 1) flag = 0;
                    if (a[4] < -1 || a[4] > 1) flag = 0;
                    if (a[5] < -1 || a[5] > 1) flag = 0;
                    for (j = 0; j < 5; j++)
                    {
                        cell1 = new PdfPCell(new Phrase(gongk[j], fonttitle3));
                        cell1.MinimumHeight = 20;
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                    for (j = 0; j < 6; j++)
                    {
                        cell1 = new PdfPCell(new Phrase(dt.Rows[i][j + 2].ToString(), fonttitle3));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                    for (j = 0; j < 6; j++)
                    {
                        cell1 = new PdfPCell(new Phrase(a[j].ToString("f2"), fonttitle3));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                }
                else
                {
                    flag = 0;
                    for (j = 0; j < 17; j++)
                    {
                        cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
                        cell1.MinimumHeight = 20;
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                }

                if (i == 0)
                {
                    cell1 = new PdfPCell(new Phrase("±1°", fonttitle2));
                    cell1.Rowspan = 19;
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                    cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                    table.AddCell(cell1);
                }
            }
            cell1 = new PdfPCell(new Phrase("结 论", fonttitle2));
            cell1.Colspan = 2;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            if (dt.Rows.Count <= 0)
            {
                cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
            }
            else
            {
                if (flag == 1) cell1 = new PdfPCell(new Phrase("试验通过", fonttitle2));
                if (flag == 0) cell1 = new PdfPCell(new Phrase("试验未通过", fonttitle2));
            }
            cell1.Colspan = 16;
            table.AddCell(cell1);
            return table;
        }
        public static PdfPTable SY6_Table(PdfPTable table, DataTable dt)
        {
            int i, j, k, flag = 1;
            cell1 = new PdfPCell(new Phrase("左传感器", fonttitle2));
            cell1.Colspan = 6;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("右传感器", fonttitle2));
            cell1.Colspan = 6;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("差值(d2-d1)", fonttitle2));
            cell1.Colspan = 6;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("合格判据", fonttitle2));
            cell1.Colspan = 3;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            for (i = 0; i < 2; i++)
            {
                cell1 = new PdfPCell(new Phrase("测量值d1", fonttitle2));
                cell1.Colspan = 3;
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("显示值d2", fonttitle2));
                cell1.Colspan = 3;
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
            }
            cell1 = new PdfPCell(new Phrase("△θ°", fonttitle2));
            cell1.Colspan = 2;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("△β°", fonttitle2));
            cell1.Colspan = 2;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("△φ°", fonttitle2));
            cell1.Colspan = 2;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("△θ°", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("△β°", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("△φ°", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            for (i = 0; i < 4; i++)
            {
                cell1 = new PdfPCell(new Phrase("θ°", fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("β°", fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("φ°", fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
            }
            for (k = 0; k < 3; k++)
            {
                cell1 = new PdfPCell(new Phrase("左", fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("右", fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
            }
            for (k = 0; k < 2; k++)
            {
                cell1 = new PdfPCell(new Phrase("±1.5°", fonttitle2));
                cell1.Rowspan = 19;
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
            }
            cell1 = new PdfPCell(new Phrase("±3°", fonttitle2));
            cell1.Rowspan = 19;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            float[] a = new float[6];
            for (j = 0; j < 18; j++)
            {
                if (j < dt.Rows.Count)
                {
                    a[0] = (float)dt.Rows[j][1] - (float)dt.Rows[j][4];
                    a[1] = (float)dt.Rows[j][7] - (float)dt.Rows[j][10];
                    a[2] = (float)dt.Rows[j][2] - (float)dt.Rows[j][5];
                    a[3] = (float)dt.Rows[j][8] - (float)dt.Rows[j][11];
                    a[4] = (float)dt.Rows[j][3] - (float)dt.Rows[j][6];
                    a[5] = (float)dt.Rows[j][9] - (float)dt.Rows[j][12];
                    if (a[0] < -1.5 || a[0] > 1.5) flag = 0;
                    if (a[1] < -1.5 || a[1] > 1.5) flag = 0;
                    if (a[2] < -1.5 || a[2] > 1.5) flag = 0;
                    if (a[3] < -1.5 || a[3] > 1.5) flag = 0;
                    if (a[4] < -3 || a[4] > 3) flag = 0;
                    if (a[5] < -3 || a[5] > 3) flag = 0;
                    for (k = 0; k < 12; k++)
                    {
                        cell1 = new PdfPCell(new Phrase(dt.Rows[j][k + 1].ToString(), fonttitle3));
                        cell1.MinimumHeight = 20; //设置单元格高度
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                    for (k = 0; k < 6; k++)
                    {
                        cell1 = new PdfPCell(new Phrase(a[k].ToString("f2"), fonttitle3));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                }
                else
                {
                    flag = 0;
                    for (k = 0; k < 18; k++)
                    {
                        cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
                        cell1.MinimumHeight = 20; //设置单元格高度
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                }

            }
            cell1 = new PdfPCell(new Phrase("结 论", fonttitle2));
            cell1.Colspan = 3;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            if (dt.Rows.Count > 0)
            {
                if (flag == 1) cell1 = new PdfPCell(new Phrase("试验通过", fonttitle2));
                else cell1 = new PdfPCell(new Phrase("试验未通过", fonttitle2));
            }
            else cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
            cell1.Colspan = 18;
            table.AddCell(cell1);
            return table;
        }
        public static PdfPTable SY7_Table1(PdfPTable table, ref int flag, DataTable dt)
        {
            int i, j, k;
            String str;
            cell1 = new PdfPCell(new Phrase("次数", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("首上板动作", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("首下板动作", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("尾翼板动作", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("备注", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("1", fonttitle2));
            cell1.Rowspan = 8;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            for (i = 0; i < 8; i++)
            {
                for (k = 0; k < 5; k++)
                {
                    if (dt.Rows.Count > i)
                    {
                        if (k == 1 || k == 2 || k == 3)
                        {
                            if ((int)dt.Rows[i][k + 1] == 0) flag = 0;
                            cell1 = new PdfPCell(new Phrase(change_IntToString((int)dt.Rows[i][k + 1]), fonttitle2));
                        }
                        else cell1 = new PdfPCell(new Phrase(dt.Rows[i][k + 1].ToString(), fonttitle2));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                    else
                    {
                        flag = 0;
                        cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                }
            }
            for (i = 0; i < 4; i++)
            {
                str = "" + (i + 2);
                cell1 = new PdfPCell(new Phrase(str, fonttitle2));
                cell1.Rowspan = 7;
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                for (j = 0; j < 7; j++)
                {
                    for (k = 0; k < 5; k++)
                    {
                        if (dt.Rows.Count > (i * 7 + j + 8))
                        {
                            if (k == 1 || k == 2 || k == 3)
                            {
                                if ((int)dt.Rows[i * 7 + j + 8][k + 1] == 0) flag = 0;
                                cell1 = new PdfPCell(new Phrase(change_IntToString((int)dt.Rows[i * 7 + j + 8][k + 1]), fonttitle2));
                            }
                            else cell1 = new PdfPCell(new Phrase(dt.Rows[i * 7 + j + 8][k + 1].ToString(), fonttitle2));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                            table.AddCell(cell1);
                        }
                        else
                        {
                            flag = 0;
                            cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                            table.AddCell(cell1);
                        }
                    }
                }
            }
            for (i = 0; i < 2; i++)
            {
                str = "" + (i + 6);
                cell1 = new PdfPCell(new Phrase(str, fonttitle2));
                cell1.Rowspan = 3;
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                for (j = 0; j < 3; j++)
                {
                    for (k = 0; k < 5; k++)
                    {
                        if (dt.Rows.Count > (i * 3 + j + 36))
                        {
                            if (k == 1 || k == 2 || k == 3)
                            {
                                if ((int)dt.Rows[i * 3 + j + 36][k + 1] == 0) flag = 0;
                                cell1 = new PdfPCell(new Phrase(change_IntToString((int)dt.Rows[i * 3 + j + 36][k + 1]), fonttitle2));
                            }
                            else cell1 = new PdfPCell(new Phrase(dt.Rows[i * 3 + j + 36][k + 1].ToString(), fonttitle2));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                            table.AddCell(cell1);
                        }
                        else
                        {
                            flag = 0;
                            cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                            table.AddCell(cell1);
                        }
                    }
                }
            }
            return table;
        }
        public static PdfPTable SY7_Table2(PdfPTable table, ref int flag, DataTable dt)
        {
            int i, j, k;
            String str;
            cell1 = new PdfPCell(new Phrase("次数", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("首上板动作", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("首下板动作", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("尾翼板动作", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("备注", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);            
            for (i = 0; i < 13; i++)
            {
                str = "" + (i + 8);
                cell1 = new PdfPCell(new Phrase(str, fonttitle2));
                cell1.Rowspan = 3;
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                for (j = 0; j < 3; j++)
                {
                    for (k = 0; k < 5; k++)
                    {
                        if (dt.Rows.Count > (i * 3 + j + 42))
                        {
                            if (k == 1 || k == 2 || k == 3)
                            {
                                if ((int)dt.Rows[i * 3 + j + 42][k + 1] == 0) flag = 0;
                                cell1 = new PdfPCell(new Phrase(change_IntToString((int)dt.Rows[i * 3 + j + 42][k + 1]), fonttitle2));
                            }
                            else cell1 = new PdfPCell(new Phrase(dt.Rows[i * 3 + j + 42][k + 1].ToString(), fonttitle2));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                            table.AddCell(cell1);
                        }
                        else
                        {
                            flag = 0;
                            cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                            table.AddCell(cell1);
                        }
                    }
                }
            }
            cell1 = new PdfPCell(new Phrase("结 论", fonttitle2));
            cell1.Rowspan = 3;
            cell1.Colspan = 2;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            if (dt.Rows.Count > 0)
            {
                if (flag == 1) cell1 = new PdfPCell(new Phrase("试验通过", fonttitle2));
                else cell1 = new PdfPCell(new Phrase("试验未通过", fonttitle2));
            }
            else cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
            cell1.Colspan = 4;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            return table;
        }
        public static PdfPTable SY8_Table(PdfPTable table, DataTable dt)
        {
            int i, j, k, flag = 1;
            float[] a = new float[3];
            for (i = 0; i < 3; i++)
            {
                a[i] = EighthForm8.weiZhi[i];
            }
            cell1 = new PdfPCell(new Phrase("序号", fonttitle2));
            cell1.Rowspan = 2;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("试验项目", fonttitle2));
            cell1.Rowspan = 2;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("试验内容", fonttitle2));
            cell1.Rowspan = 2;
            cell1.Colspan = 2;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("位置", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("开关", fonttitle2));
            cell1.Colspan = 3;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("试验结果", fonttitle2));
            cell1.Colspan = 5;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("θ/β/φ（°）", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("仰角", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("左侧", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("右侧", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("滑板申请", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("允许灯", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("尾板", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("首下板", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            cell1 = new PdfPCell(new Phrase("首上板", fonttitle2));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            for (i = 0; i < 3; i++)
            {
                cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
                cell1.Rowspan = 4;
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("全放", fonttitle2));
                cell1.Rowspan = 4;
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("初始", fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                if (dt.Rows.Count > (i*8))
                {
                    string str1 = change_CSno((int)dt.Rows[i * 8][1]);
                    cell1 = new PdfPCell(new Phrase(str1, fonttitle2));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                    cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                    table.AddCell(cell1);
                    str1 = "" + a[0] + "/" + a[1] + "/" + a[2];
                    cell1 = new PdfPCell(new Phrase(str1, fonttitle2));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                    cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                    table.AddCell(cell1);
                    for (j = 0; j < 5; j++)
                    {
                        if ((int)dt.Rows[i * 8][j + 2] == 0) flag = 0;
                        cell1 = new PdfPCell(new Phrase(change_IntToString((int)dt.Rows[i * 8][j + 2]), fonttitle2));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                    for (j = 0; j < 3; j++)
                    {
                        string str2;
                        if ((float)dt.Rows[i * 8][j + 7] - a[j] < -1 || (float)dt.Rows[i * 8][j + 7] - a[j] > 1) { str2 = "×"; flag = 0; }
                        else str2 = "√";
                        cell1 = new PdfPCell(new Phrase(str2, fonttitle2));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                }
                else
                {
                    flag = 0;
                    for (j = 0; j < 10; j++)
                    {
                        cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                }
                cell1 = new PdfPCell(new Phrase("目标", fonttitle2));
                cell1.Rowspan = 3;
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                
                for (k = 0; k < 3; k++)
                {
                    if (dt.Rows.Count > (i * 8 + 1 + k))
                    {
                        string str1 = change_CSno((int)dt.Rows[i * 8 + 1 + k][1]);
                        cell1 = new PdfPCell(new Phrase(str1, fonttitle2));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                        str1 = "" + a[0] + "/" + a[1] + "/" + a[2];
                        cell1 = new PdfPCell(new Phrase(str1, fonttitle2));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                        for (j = 0; j < 5; j++)
                        {
                            if ((int)dt.Rows[i * 8 + 1 + k][j + 2] == 0) flag = 0;
                            cell1 = new PdfPCell(new Phrase(change_IntToString((int)dt.Rows[i * 8 + 1 + k][j + 2]), fonttitle2));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                            table.AddCell(cell1);
                        }
                        for (j = 0; j < 3; j++)
                        {
                            string str2;
                            if ((float)dt.Rows[i * 8 + 1 + k][j + 7] - a[j] < -1 || (float)dt.Rows[i * 8 + 1 + k][j + 7] - a[j] > 1) { str2 = "×"; flag = 0; }
                            else str2 = "√";
                            cell1 = new PdfPCell(new Phrase(str2, fonttitle2));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                            table.AddCell(cell1);
                        }
                    }
                    else
                    {
                        flag = 0;
                        for (j = 0; j < 10; j++)
                        {
                            cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                            table.AddCell(cell1);
                        }
                    }
                }
                cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
                cell1.Rowspan = 4;
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("全收", fonttitle2));
                cell1.Rowspan = 4;
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase("初始", fonttitle2));
                cell1.Rowspan = 3;
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                for (k = 0; k < 3; k++)
                {
                    if (dt.Rows.Count > (i * 8 + 4 + k))
                    {
                        string str1 = change_CSno((int)dt.Rows[i * 8 + 4 + k][1]);
                        cell1 = new PdfPCell(new Phrase(str1, fonttitle2));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                        str1 = "" + a[0] + "/" + a[1] + "/" + a[2];
                        cell1 = new PdfPCell(new Phrase(str1, fonttitle2));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                        for (j = 0; j < 5; j++)
                        {
                            if ((int)dt.Rows[i * 8 + 4 + k][j + 2] == 0) flag = 0;
                            cell1 = new PdfPCell(new Phrase(change_IntToString((int)dt.Rows[i * 8 + 4 + k][j + 2]), fonttitle2));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                            table.AddCell(cell1);
                        }
                        for (j = 0; j < 3; j++)
                        {
                            string str2;
                            if ((float)dt.Rows[i * 8 + 4 + k][j + 7] - a[j] < -1 || (float)dt.Rows[i * 8 + 4 + k][j + 7] - a[j] > 1) { str2 = "×"; flag = 0; }
                            else str2 = "√";
                            cell1 = new PdfPCell(new Phrase(str2, fonttitle2));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                            table.AddCell(cell1);
                        }
                    }
                    else
                    {
                        flag = 0;
                        for (j = 0; j < 10; j++)
                        {
                            cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                            table.AddCell(cell1);
                        }
                    }
                }                       
                cell1 = new PdfPCell(new Phrase("目标", fonttitle2));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                table.AddCell(cell1);
                if (dt.Rows.Count > (i * 8 + 7))
                {
                    string str1 = change_CSno((int)dt.Rows[i * 8 + 7][1]);
                    cell1 = new PdfPCell(new Phrase(str1, fonttitle2));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                    cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                    table.AddCell(cell1);
                    str1 = "" + a[0] + "/" + a[1] + "/" + a[2];
                    cell1 = new PdfPCell(new Phrase(str1, fonttitle2));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                    cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                    table.AddCell(cell1);
                    for (j = 0; j < 5; j++)
                    {
                        if ((int)dt.Rows[i * 8 + 7][j + 2] == 0) flag = 0;
                        cell1 = new PdfPCell(new Phrase(change_IntToString((int)dt.Rows[i * 8 + 7][j + 2]), fonttitle2));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                    for (j = 0; j < 3; j++)
                    {
                        string str2;
                        if ((float)dt.Rows[i * 8 + 7][j + 7] - a[j] < -1 || (float)dt.Rows[i * 8 + 7][j + 7] - a[j] > 1) { str2 = "×"; flag = 0; }
                        else str2 = "√";
                        cell1 = new PdfPCell(new Phrase(str2, fonttitle2));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                }
                else
                {
                    flag = 0;
                    for (j = 0; j < 10; j++)
                    {
                        cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
                        cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
                        table.AddCell(cell1);
                    }
                }
            }
            cell1 = new PdfPCell(new Phrase("结论", fonttitle2));
            cell1.Colspan = 4;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            if (dt.Rows.Count > 0)
            {
                if (flag == 1) cell1 = new PdfPCell(new Phrase("试验通过", fonttitle2));
                else cell1 = new PdfPCell(new Phrase("试验未通过", fonttitle2));
            }
            else cell1 = new PdfPCell(new Phrase(" ", fonttitle2));
            cell1.Colspan = 9;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;//单元格水平居中
            cell1.VerticalAlignment = Element.ALIGN_MIDDLE;//单元格垂直居中
            table.AddCell(cell1);
            return table;
        }
    }
}
