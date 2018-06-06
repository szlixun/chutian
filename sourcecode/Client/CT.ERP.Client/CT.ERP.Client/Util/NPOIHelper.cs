using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.IO;
using System.Text;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;

using CT.ERP.Client.Entity;

namespace CT.ERP.Client.Util
{
    /// <summary>
    /// Excel导入，导出操作类
    /// lcl add 2015-02-02
    /// </summary>
    public class NPOIHelper
    {
        #region Excel导出方法 ExportByWeb(dtSource,strHeaderText,strFileName)
        /// <summary>
        /// Excel导出方法 ExportByWeb()
        /// </summary>
        /// <param name="dtSource">DataTable数据源</param>
        /// <param name="strHeaderText">Excel表头文本（例如：车辆列表）</param>
        /// <param name="strFileName">Excel文件名（例如：车辆列表.xls）</param>
        //public static void ExportByWeb(DataTable dtSource, string strHeaderText, string strFileName)
        //{
        //    HttpContext curContext = HttpContext.Current;
        //    // 设置编码和附件格式
        //    curContext.Response.ContentType = "application/ms-excel";
        //    curContext.Response.ContentEncoding = Encoding.UTF8;
        //    curContext.Response.Charset = "";
        //    curContext.Response.AppendHeader("Content-Disposition",
        //        "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));
        //    //调用导出具体方法Export()
        //    curContext.Response.BinaryWrite(Export(dtSource, strHeaderText).GetBuffer());
        //    curContext.Response.End();
        //}
        #endregion

        #region DataTable导出到Excel文件 Export(dtSource,strHeaderText,strFileName)
        /// <summary>
        /// DataTable导出到Excel文件 Export()
        /// </summary>
        /// <param name="dtSource">DataTable数据源</param>
        /// <param name="strHeaderText">Excel表头文本（例如：车辆列表）</param>
        /// <param name="strFileName">保存位置</param>
        public static void Export(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        public static void ExportDelivery(string strFileName,DeliveryNote note)
        {
            using (MemoryStream ms = Export2Delivery(note))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }
        #endregion

        #region DataTable导出到Excel的MemoryStream Export(dtSource,strHeaderText)
        /// <summary>
        /// DataTable导出到Excel的MemoryStream Export()
        /// </summary>
        /// <param name="dtSource">DataTable数据源</param>
        /// <param name="strHeaderText">Excel表头文本（例如：车辆列表）</param>
        public static MemoryStream Export(DataTable dtSource, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            Sheet sheet = workbook.CreateSheet();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "NPOI";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "lixun"; //填加xls文件作者信息
                si.ApplicationName = "ExcelOrderHelper"; //填加xls文件创建程序信息
                si.LastAuthor = "lixun"; //填加xls文件最后保存者信息
                si.Comments = ""; //填加xls文件作者信息
                si.Title = ""; //填加xls文件标题信息
                si.Subject = "";//填加文件主题信息
                si.CreateDateTime = System.DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            CellStyle dateStyle = workbook.CreateCellStyle();
            DataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }

                    #region 表头及样式
                    {
                        if (strHeaderText.Length > 0)
                        {
                            Row headerRow = sheet.CreateRow(rowIndex);
                            headerRow.HeightInPoints = 25;
                            headerRow.CreateCell(0).SetCellValue(strHeaderText);

                            CellStyle headStyle = workbook.CreateCellStyle();
                            headStyle.Alignment = HorizontalAlignment.CENTER; // ------------------
                            Font font = workbook.CreateFont();
                            font.FontHeightInPoints = 20;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);
                            headerRow.GetCell(0).CellStyle = headStyle;
                            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1)); // ------------------
                            rowIndex++;
                        }

                    }
                    #endregion

                    #region 列头及样式
                    {
                        Row headerRow = sheet.CreateRow(rowIndex);
                        CellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.CENTER; // ------------------
                        headStyle.BorderTop = CellBorderType.THIN;
                        headStyle.BorderBottom = CellBorderType.THIN;
                        headStyle.BorderLeft = CellBorderType.THIN;
                        headStyle.BorderRight = CellBorderType.THIN;
                        Font font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            string sName = column.ColumnName;
                            if (sName.Contains("剥离"))
                            {
                                sName = "剥离";
                            }
                            else if (sName.Contains("样品1"))
                            {
                                sName = "热合样品1";
                            }
                            else if (sName.Contains("样品2"))
                            {
                                sName = "热合样品2";
                            }
                            else if (sName.Contains("泡水"))
                            {
                                sName = "泡水";
                            }
                            headerRow.CreateCell(column.Ordinal).SetCellValue(sName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 350);
                        }
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowIndex, rowIndex, 6, 7));
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowIndex, rowIndex, 8, 10));
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowIndex, rowIndex, 11, 13));
                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowIndex, rowIndex, 18, 19));
                        rowIndex++;
                    }
                    #endregion
                }
                #endregion

                #region 填充内容
                Row dataRow = sheet.CreateRow(rowIndex);
                CellStyle mainStyle = workbook.CreateCellStyle();
                mainStyle.BorderTop = CellBorderType.THIN;
                mainStyle.BorderBottom = CellBorderType.THIN;
                mainStyle.BorderLeft = CellBorderType.THIN;
                mainStyle.BorderRight = CellBorderType.THIN;
                foreach (DataColumn column in dtSource.Columns)
                {
                    Cell newCell = dataRow.CreateCell(column.Ordinal);
                    newCell.CellStyle = mainStyle;

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            System.DateTime dateV;
                            System.DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                }
                #endregion

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet.Dispose();
                return ms;
            }
        }
        #endregion

        #region 导出送货单
        private static MemoryStream Export2Delivery(DeliveryNote note)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            Sheet sheet = workbook.CreateSheet();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "NPOI";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "ChuTian"; //填加xls文件作者信息
                si.ApplicationName = "ExcelOrderHelper"; //填加xls文件创建程序信息
                si.LastAuthor = "ChuTian"; //填加xls文件最后保存者信息
                si.Comments = ""; //填加xls文件作者信息
                si.Title = ""; //填加xls文件标题信息
                si.Subject = "";//填加文件主题信息
                si.CreateDateTime = System.DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            //CellStyle dateStyle = workbook.CreateCellStyle();
            //DataFormat format = workbook.CreateDataFormat();
            //dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            int rowIndex = 1;
            sheet.SetColumnWidth(0, (int)((9.5 + 0.72) * 256));
            sheet.SetColumnWidth(1, (int)((18.5 + 0.72) * 256));
            sheet.SetColumnWidth(2, (int)((9.5 + 0.72) * 256));
            sheet.SetColumnWidth(3, (int)((12.5 + 0.72) * 256));
            sheet.SetColumnWidth(4, (int)((12.5 + 0.72) * 256));
            sheet.SetColumnWidth(5, (int)((15.5 + 0.72) * 256));
            sheet.SetColumnWidth(6, (int)((20 + 0.72) * 256));


            //所有的字体
            Font font18 = workbook.CreateFont();
            font18.FontName = "华文彩云";
            font18.FontHeightInPoints = 18;
            font18.IsItalic = true;

            Font font11Bold = workbook.CreateFont();
            font11Bold.FontHeightInPoints = 11;
            font11Bold.Boldweight = 700;

            Font font11Normal = workbook.CreateFont();
            font11Normal.FontHeightInPoints = 11;

            Font font20Bold = workbook.CreateFont();
            font20Bold.FontHeightInPoints = 20;
            font20Bold.Boldweight = 700;

            Font foot10Bold = workbook.CreateFont();
            foot10Bold.FontHeightInPoints = 10;
            foot10Bold.Boldweight = 700;

            CellStyle companyStyle = workbook.CreateCellStyle();
            companyStyle.Alignment = HorizontalAlignment.CENTER;
            companyStyle.SetFont(font18);

            Row companyRow = sheet.CreateRow(rowIndex);
            companyRow.HeightInPoints = 30;
            companyRow.CreateCell(0).SetCellValue("                                                                                                 湖北楚天通讯材料有限公司");
            companyRow.GetCell(0).CellStyle = companyStyle;
            rowIndex++;

            //创建表头
            CellStyle titleStyle = workbook.CreateCellStyle();
            titleStyle.Alignment = HorizontalAlignment.CENTER;
            titleStyle.SetFont(font20Bold);
            
            Row titleRow = sheet.CreateRow(rowIndex);
            titleRow.HeightInPoints = 29;
            titleRow.CreateCell(0).SetCellValue("                                                                          送    货    单");
            titleRow.GetCell(0).CellStyle = titleStyle;

            //创建Logo
            string strLogoPath = AppDomain.CurrentDomain.BaseDirectory + "ctlogo.png";
            byte[] bLogo = System.IO.File.ReadAllBytes(strLogoPath);
            int pictureIdx = workbook.AddPicture(bLogo, PictureType.PNG);
            var patriarch = sheet.CreateDrawingPatriarch();
            HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 82, 39, 0, 2, 0, 2);
            var pict=patriarch.CreatePicture(anchor, pictureIdx);
            pict.Resize();
            
            //sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));
            rowIndex++;
            rowIndex++;            

            //创建父表内容
            CellStyle headStyle = workbook.CreateCellStyle();
            headStyle.Alignment = HorizontalAlignment.LEFT;
            headStyle.SetFont(font11Bold);

            CellStyle headDescStyle = workbook.CreateCellStyle();
            headDescStyle.Alignment = HorizontalAlignment.RIGHT;
            headDescStyle.SetFont(font11Normal);

            Row parentRow1 = sheet.CreateRow(rowIndex);
            parentRow1.CreateCell(0).SetCellValue("客户:");
            parentRow1.CreateCell(1).SetCellValue(note.customer);
            parentRow1.CreateCell(4).SetCellValue("送货单号：");
            parentRow1.CreateCell(5).SetCellValue(note.deliverid.ToString().PadLeft(3,'0'));
            parentRow1.CreateCell(6).SetCellValue(note.description);
            parentRow1.GetCell(0).CellStyle = headStyle;
            parentRow1.GetCell(1).CellStyle = headStyle;
            parentRow1.GetCell(4).CellStyle = headStyle;
            parentRow1.GetCell(5).CellStyle = headStyle;
            parentRow1.GetCell(6).CellStyle = headDescStyle;
            
            rowIndex++;
            Row parentRow2 = sheet.CreateRow(rowIndex);
            parentRow2.CreateCell(0).SetCellValue("型号:");
            parentRow2.CreateCell(1).SetCellValue(note.model);
            parentRow2.CreateCell(4).SetCellValue("发货时间：");
            parentRow2.CreateCell(5).SetCellValue(note.deliverdate.ToString("yyyy.MM.dd"));
            parentRow2.GetCell(0).CellStyle = headStyle;
            parentRow2.GetCell(1).CellStyle = headStyle;
            parentRow2.GetCell(4).CellStyle = headStyle;
            parentRow2.GetCell(5).CellStyle = headStyle;

            rowIndex++;
            Row parentRow3 = sheet.CreateRow(rowIndex);
            parentRow3.CreateCell(0).SetCellValue("货物名称:");
            parentRow3.CreateCell(1).SetCellValue(note.goodname);
            parentRow3.CreateCell(4).SetCellValue("出厂批号：");
            parentRow3.CreateCell(5).SetCellValue(note.batch);
            parentRow3.GetCell(0).CellStyle = headStyle;
            parentRow3.GetCell(1).CellStyle = headStyle;
            parentRow3.GetCell(4).CellStyle = headStyle;
            parentRow3.GetCell(5).CellStyle = headStyle;

            rowIndex++;
            //表格样式
            CellStyle tableStyle = workbook.CreateCellStyle();
            CellStyle tableLeftStyle = workbook.CreateCellStyle();
            tableStyle.Alignment = HorizontalAlignment.CENTER;
            tableLeftStyle.Alignment = HorizontalAlignment.LEFT;
            tableStyle.SetFont(font11Normal);
            tableLeftStyle.SetFont(font11Normal);
            tableStyle.BorderTop = CellBorderType.THIN;
            tableStyle.BorderBottom = CellBorderType.THIN;
            tableStyle.BorderLeft = CellBorderType.THIN;
            tableStyle.BorderRight = CellBorderType.THIN;
            tableLeftStyle.BorderTop = CellBorderType.THIN;
            tableLeftStyle.BorderBottom = CellBorderType.THIN;
            tableLeftStyle.BorderLeft = CellBorderType.THIN;
            tableLeftStyle.BorderRight = CellBorderType.THIN;

            //小计和合计样式
            CellStyle tableSumStyle = workbook.CreateCellStyle();
            CellStyle tableSumLeftStyle = workbook.CreateCellStyle();
            tableSumStyle.Alignment = HorizontalAlignment.CENTER;
            tableSumLeftStyle.Alignment = HorizontalAlignment.LEFT;
            tableSumStyle.SetFont(font11Bold);
            tableSumLeftStyle.SetFont(font11Bold);
            tableSumStyle.BorderTop = CellBorderType.THIN;
            tableSumStyle.BorderBottom = CellBorderType.THIN;
            tableSumStyle.BorderLeft = CellBorderType.THIN;
            tableSumStyle.BorderRight = CellBorderType.THIN;
            tableSumLeftStyle.BorderTop = CellBorderType.THIN;
            tableSumLeftStyle.BorderBottom = CellBorderType.THIN;
            tableSumLeftStyle.BorderLeft = CellBorderType.THIN;
            tableSumLeftStyle.BorderRight = CellBorderType.THIN;


            Row colHeaderRow = sheet.CreateRow(rowIndex);
            colHeaderRow.CreateCell(0).SetCellValue("件号");
            colHeaderRow.CreateCell(1).SetCellValue("规格");
            colHeaderRow.CreateCell(2).SetCellValue("盘数");
            colHeaderRow.CreateCell(3).SetCellValue("净重（KG)");
            colHeaderRow.CreateCell(4).SetCellValue("单价");
            colHeaderRow.CreateCell(5).SetCellValue("金额");
            colHeaderRow.CreateCell(6).SetCellValue("合同号");
            colHeaderRow.CreateCell(7).SetCellValue("毛重");
            colHeaderRow.CreateCell(8).SetCellValue("管芯重量");
            colHeaderRow.GetCell(0).CellStyle = tableLeftStyle;
            colHeaderRow.GetCell(1).CellStyle = tableStyle;
            colHeaderRow.GetCell(2).CellStyle = tableStyle;
            colHeaderRow.GetCell(3).CellStyle = tableStyle;
            colHeaderRow.GetCell(4).CellStyle = tableStyle;
            colHeaderRow.GetCell(5).CellStyle = tableStyle;
            colHeaderRow.GetCell(6).CellStyle = tableStyle;
            colHeaderRow.GetCell(7).CellStyle = tableStyle;
            colHeaderRow.GetCell(8).CellStyle = tableStyle;

            rowIndex++;
            Dictionary<string, List<DeliveryItem>> dicDelivery = DeliveryItemGroup(note.items);
            int iDisoTotal = 0;
            double dWeightTotal = 0;
            double dPriceTotal = 0;
            foreach (string delivertyId in dicDelivery.Keys)
            {

                int iDisoSum = 0;
                double dWeightSum = 0;
                double dPriceSum = 0;
                List<DeliveryItem> noteItems = dicDelivery[delivertyId];
                foreach (DeliveryItem item in noteItems)
                {
                    iDisoSum = iDisoSum + item.discnum;
                    dWeightSum = dWeightSum + item.weight;
                    dPriceSum = dPriceSum + item.totalprice;

                    iDisoTotal = iDisoTotal + item.discnum;
                    dWeightTotal = dWeightTotal + item.weight;
                    dPriceTotal = dPriceTotal + item.totalprice;

                    Row colItemRow = sheet.CreateRow(rowIndex);
                    colItemRow.CreateCell(0).SetCellValue(item.jiannum);
                    colItemRow.GetCell(0).CellStyle = tableLeftStyle;
                    colItemRow.CreateCell(1).SetCellValue(item.specifications + "*" + item.lenght.ToString());
                    colItemRow.GetCell(1).CellStyle = tableStyle;
                    colItemRow.CreateCell(2).SetCellValue(item.discnum);
                    colItemRow.GetCell(2).CellStyle = tableStyle;
                    colItemRow.CreateCell(3).SetCellValue(item.weight.ToString("f2"));
                    colItemRow.GetCell(3).CellStyle = tableStyle;
                    colItemRow.CreateCell(4).SetCellValue(item.price.ToString("f5"));
                    colItemRow.GetCell(4).CellStyle = tableStyle;
                    colItemRow.CreateCell(5).SetCellValue(item.totalprice.ToString("f5"));
                    colItemRow.GetCell(5).CellStyle = tableStyle;
                    colItemRow.CreateCell(6).SetCellValue(item.contractno);
                    colItemRow.GetCell(6).CellStyle = tableStyle;
                    colItemRow.CreateCell(7).SetCellValue(item.netweight);
                    colItemRow.GetCell(7).CellStyle = tableStyle;
                    colItemRow.CreateCell(8).SetCellValue(item.coreweight);
                    colItemRow.GetCell(8).CellStyle = tableStyle;
                    rowIndex++;
                }
                if (noteItems.Count > 1 && dicDelivery.Count>1)
                {
                    Row colItemRow = sheet.CreateRow(rowIndex);
                    colItemRow.CreateCell(0).SetCellValue("小计");
                    colItemRow.GetCell(0).CellStyle = tableSumLeftStyle;
                    colItemRow.CreateCell(1).SetCellValue("");
                    colItemRow.GetCell(1).CellStyle = tableSumStyle;
                    colItemRow.CreateCell(2).SetCellValue(iDisoSum.ToString());
                    colItemRow.GetCell(2).CellStyle = tableSumStyle;
                    colItemRow.CreateCell(3).SetCellValue(dWeightSum.ToString("f2"));
                    colItemRow.GetCell(3).CellStyle = tableSumStyle;
                    colItemRow.CreateCell(4).SetCellValue("");
                    colItemRow.GetCell(4).CellStyle = tableSumStyle;
                    colItemRow.CreateCell(5).SetCellValue(dPriceSum.ToString("f2"));
                    colItemRow.GetCell(5).CellStyle = tableSumStyle;
                    colItemRow.CreateCell(6).SetCellValue("");
                    colItemRow.GetCell(6).CellStyle = tableSumStyle;
                    colItemRow.CreateCell(7).SetCellValue("");
                    colItemRow.GetCell(7).CellStyle = tableSumStyle;
                    colItemRow.CreateCell(8).SetCellValue("");
                    colItemRow.GetCell(8).CellStyle = tableSumStyle;
                    rowIndex++;
                }
                else 
                {
                    Row colItemRow = sheet.CreateRow(rowIndex);
                    colItemRow.CreateCell(0).SetCellValue("");
                    colItemRow.GetCell(0).CellStyle = tableSumLeftStyle;
                    colItemRow.CreateCell(1).SetCellValue("");
                    colItemRow.GetCell(1).CellStyle = tableSumStyle;
                    colItemRow.CreateCell(2).SetCellValue("");
                    colItemRow.GetCell(2).CellStyle = tableSumStyle;
                    colItemRow.CreateCell(3).SetCellValue("");
                    colItemRow.GetCell(3).CellStyle = tableSumStyle;
                    colItemRow.CreateCell(4).SetCellValue("");
                    colItemRow.GetCell(4).CellStyle = tableSumStyle;
                    colItemRow.CreateCell(5).SetCellValue("");
                    colItemRow.GetCell(5).CellStyle = tableSumStyle;
                    colItemRow.CreateCell(6).SetCellValue("");
                    colItemRow.GetCell(6).CellStyle = tableSumStyle;
                    colItemRow.CreateCell(7).SetCellValue("");
                    colItemRow.GetCell(7).CellStyle = tableSumStyle;
                    colItemRow.CreateCell(8).SetCellValue("");
                    colItemRow.GetCell(8).CellStyle = tableSumStyle;
                    rowIndex++;
                }
            }

            //空行
            Row colBlankRow = sheet.CreateRow(rowIndex);
            colBlankRow.CreateCell(0).SetCellValue("");
            colBlankRow.GetCell(0).CellStyle = tableLeftStyle;
            colBlankRow.CreateCell(1).SetCellValue("");
            colBlankRow.GetCell(1).CellStyle = tableStyle;
            colBlankRow.CreateCell(2).SetCellValue("");
            colBlankRow.GetCell(2).CellStyle = tableStyle;
            colBlankRow.CreateCell(3).SetCellValue("");
            colBlankRow.GetCell(3).CellStyle = tableStyle;
            colBlankRow.CreateCell(4).SetCellValue("");
            colBlankRow.GetCell(4).CellStyle = tableStyle;
            colBlankRow.CreateCell(5).SetCellValue("");
            colBlankRow.GetCell(5).CellStyle = tableStyle;
            colBlankRow.CreateCell(6).SetCellValue("");
            colBlankRow.GetCell(6).CellStyle = tableStyle;
            colBlankRow.CreateCell(7).SetCellValue("");
            colBlankRow.GetCell(7).CellStyle = tableStyle;
            colBlankRow.CreateCell(8).SetCellValue("");
            colBlankRow.GetCell(8).CellStyle = tableStyle;
            rowIndex++;

            //合计
            Row colTotalRow = sheet.CreateRow(rowIndex);
            colTotalRow.CreateCell(0).SetCellValue("合计");
            colTotalRow.GetCell(0).CellStyle = tableSumLeftStyle;
            colTotalRow.CreateCell(1).SetCellValue("");
            colTotalRow.GetCell(1).CellStyle = tableSumStyle;
            colTotalRow.CreateCell(2).SetCellValue(iDisoTotal.ToString());
            colTotalRow.GetCell(2).CellStyle = tableSumStyle;
            colTotalRow.CreateCell(3).SetCellValue(dWeightTotal.ToString("f2"));
            colTotalRow.GetCell(3).CellStyle = tableSumStyle;
            colTotalRow.CreateCell(4).SetCellValue("");
            colTotalRow.GetCell(4).CellStyle = tableSumStyle;
            colTotalRow.CreateCell(5).SetCellValue(dPriceTotal.ToString("f2"));
            colTotalRow.GetCell(5).CellStyle = tableSumStyle;
            colTotalRow.CreateCell(6).SetCellValue("");
            colTotalRow.GetCell(6).CellStyle = tableSumStyle;
            colTotalRow.CreateCell(7).SetCellValue("");
            colTotalRow.GetCell(7).CellStyle = tableSumStyle;
            colTotalRow.CreateCell(8).SetCellValue("");
            colTotalRow.GetCell(8).CellStyle = tableSumStyle;
            rowIndex++;

            CellStyle footStyle = workbook.CreateCellStyle();
            footStyle.Alignment = HorizontalAlignment.LEFT;
            footStyle.SetFont(font11Normal);

            CellStyle footBoldStyle = workbook.CreateCellStyle();
            footBoldStyle.Alignment = HorizontalAlignment.LEFT;
            footBoldStyle.SetFont(font11Bold);

            CellStyle footRightStyle = workbook.CreateCellStyle();
            footRightStyle.Alignment = HorizontalAlignment.RIGHT;
            footRightStyle.SetFont(font11Normal);

            Row descRow = sheet.CreateRow(rowIndex);
            descRow.CreateCell(0).SetCellValue("备注：");
            descRow.GetCell(0).CellStyle = footBoldStyle;
            descRow.CreateCell(1).SetCellValue(note.description1);
            descRow.GetCell(1).CellStyle = footBoldStyle;
            rowIndex++;

            Row footerRow = sheet.CreateRow(rowIndex);
            footerRow.CreateCell(0).SetCellValue("请按上列货验收");
            footerRow.GetCell(0).CellStyle = footStyle;
            rowIndex++;

            Row footerRow1 = sheet.CreateRow(rowIndex);
            footerRow1.CreateCell(0).SetCellValue("收货人:");
            footerRow1.GetCell(0).CellStyle = footStyle;
            footerRow1.CreateCell(1).SetCellValue("");
            footerRow1.GetCell(1).CellStyle = footStyle;
            footerRow1.CreateCell(2).SetCellValue("送货人:");
            footerRow1.GetCell(2).CellStyle = footStyle;
            footerRow1.CreateCell(3).SetCellValue("");
            footerRow1.GetCell(3).CellStyle = footRightStyle;
            footerRow1.CreateCell(4).SetCellValue("制单:");
            footerRow1.GetCell(4).CellStyle = footRightStyle;
            footerRow1.CreateCell(5).SetCellValue(note.loginid);
            footerRow1.GetCell(5).CellStyle = footStyle;
            footerRow1.CreateCell(6).SetCellValue("审核：");
            footerRow1.GetCell(6).CellStyle = footStyle;
            rowIndex++;

            CellStyle footStyle1 = workbook.CreateCellStyle();
            footStyle1.Alignment = HorizontalAlignment.LEFT;
            footStyle1.SetFont(foot10Bold);

            Row footerRow2 = sheet.CreateRow(rowIndex);
            footerRow2.CreateCell(0).SetCellValue("地址：湖北省汉川市马口工业园区楚天路");
            footerRow2.GetCell(0).CellStyle = footStyle1;
            footerRow2.CreateCell(1).SetCellValue("");
            footerRow2.GetCell(1).CellStyle = footStyle1;
            footerRow2.CreateCell(2).SetCellValue("");
            footerRow2.GetCell(2).CellStyle = footStyle1;
            footerRow2.CreateCell(3).SetCellValue("                    电话：0712-8521088                 传真：0712-8512311");
            footerRow2.GetCell(3).CellStyle = footStyle1;
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rowIndex, rowIndex, 0, 2));

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;                
                sheet.Dispose();
                workbook.Dispose();
                return ms;
            }
        }

        #endregion


        private static Dictionary<string, List<DeliveryItem>> DeliveryItemGroup(List<DeliveryItem> items)
        {
            Dictionary<string, List<DeliveryItem>> dicRet = new Dictionary<string, List<DeliveryItem>>();
            foreach (DeliveryItem item in items)
            {
                List<DeliveryItem> lstSepc;
                string sKey = item.specifications;
                if (!dicRet.ContainsKey(sKey))
                {
                    lstSepc = new List<DeliveryItem>();
                    lstSepc.Add(item);
                    dicRet.Add(sKey, lstSepc);
                }
                else
                {
                    lstSepc = dicRet[sKey];
                    lstSepc.Add(item);
                }
            }

            return dicRet;

        }


        #region 读取excel ,默认第一行为标头Import()
        /// <summary>
        /// 读取excel ,默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            Sheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            Row headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                Cell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                Row row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }
        #endregion
    }
}
