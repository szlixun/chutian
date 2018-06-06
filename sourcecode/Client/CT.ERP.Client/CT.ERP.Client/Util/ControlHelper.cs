using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CT.ERP.Client.Entity;

namespace CT.ERP.Client.Util
{
    public class ControlHelper
    {
        public static bool isNumeric(string sValue)
        {
            int iValue=0;
            return int.TryParse(sValue,out iValue);
        }

        public static string Object2String(object obj)
        {
            string sRet = "";
            if (obj != null)
            {
                sRet = obj.ToString().Trim();
            }
            return sRet;
        }

        public static int Object2Int(object obj)
        {
            int iRet = 0;
            if (obj != null)
            {
                int iConvert;
                if (int.TryParse(obj.ToString(), out iConvert))
                {
                    iRet = iConvert;
                }
            }
            return iRet;
        }

        public static double Object2Double(object obj)
        {
            double dRet = 0;
            if (obj != null)
            {
                double dConvert;
                if (double.TryParse(obj.ToString(), out dConvert))
                {
                    dRet = dConvert;
                }
            }
            return dRet;
        }

        public static DataTable ConvertList2DataTable(List<DeliveryNote> listValue)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("noteid", typeof(Int32)));
            dt.Columns.Add(new DataColumn("deliverid", typeof(string)));
            dt.Columns.Add(new DataColumn("customer", typeof(string)));
            dt.Columns.Add(new DataColumn("model", typeof(string)));
            dt.Columns.Add(new DataColumn("deliverdate", typeof(string)));
            dt.Columns.Add(new DataColumn("goodname", typeof(string)));
            dt.Columns.Add(new DataColumn("batch", typeof(string)));
            dt.Columns.Add(new DataColumn("description", typeof(string)));
            dt.Columns.Add(new DataColumn("loginid", typeof(string)));
            dt.Columns.Add(new DataColumn("description1", typeof(string)));

            foreach (DeliveryNote entity in listValue)
            {
                DataRow dr = dt.NewRow();
                dr["noteid"] = entity.noteid;
                dr["deliverid"] = entity.deliverid.ToString().PadLeft(3,'0');
                dr["customer"] = entity.customer;
                dr["model"] = entity.model;
                dr["deliverdate"] = entity.deliverdate.ToLongDateString() ;
                dr["goodname"] = entity.goodname;
                dr["batch"] = entity.batch;
                dr["description"] = entity.description;
                dr["loginid"] = entity.loginid;
                dr["description1"] = entity.description1;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static DataTable ConvertList2DataTable(List<QualityTrackingEntity> listValue)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("id", typeof(Int32)));
            dt.Columns.Add(new DataColumn("qtid", typeof(Int32)));
            dt.Columns.Add(new DataColumn("qtdate", typeof(string)));
            dt.Columns.Add(new DataColumn("category", typeof(string)));
            dt.Columns.Add(new DataColumn("batch", typeof(string)));
            dt.Columns.Add(new DataColumn("specifications", typeof(string)));
            dt.Columns.Add(new DataColumn("volume", typeof(string)));
            dt.Columns.Add(new DataColumn("stripping1", typeof(string)));
            dt.Columns.Add(new DataColumn("stripping2", typeof(string)));
            dt.Columns.Add(new DataColumn("sample11", typeof(string)));
            dt.Columns.Add(new DataColumn("sample12", typeof(string)));
            dt.Columns.Add(new DataColumn("sample13", typeof(string)));
            dt.Columns.Add(new DataColumn("sample21", typeof(string)));
            dt.Columns.Add(new DataColumn("sample22", typeof(string)));
            dt.Columns.Add(new DataColumn("sample23", typeof(string)));
            dt.Columns.Add(new DataColumn("baseheight", typeof(string)));
            dt.Columns.Add(new DataColumn("measuredheight", typeof(string)));
            dt.Columns.Add(new DataColumn("compositeheight", typeof(string)));
            dt.Columns.Add(new DataColumn("cutheight", typeof(string)));
            dt.Columns.Add(new DataColumn("bubblewater1", typeof(string)));
            dt.Columns.Add(new DataColumn("bubblewater2", typeof(string)));
            dt.Columns.Add(new DataColumn("bubbleoil", typeof(string)));
            dt.Columns.Add(new DataColumn("descrtiption", typeof(string)));
            dt.Columns.Add(new DataColumn("loginid", typeof(string)));
            dt.Columns.Add(new DataColumn("target", typeof(string)));
            dt.Columns.Add(new DataColumn("elongation", typeof(string)));
            dt.Columns.Add(new DataColumn("elongation1", typeof(string)));
            dt.Columns.Add(new DataColumn("type", typeof(string)));
            dt.Columns.Add(new DataColumn("decision", typeof(string)));

            int iIndex = 1;
            foreach (QualityTrackingEntity entity in listValue)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = iIndex;
                dr["qtid"] = entity.qtid;
                dr["qtdate"] = entity.qtdate.ToShortDateString();
                dr["category"] = entity.category;
                dr["batch"] = entity.batch;
                dr["specifications"] = entity.specifications + "*" + entity.length;
                dr["volume"] = entity.volume;
                dr["stripping1"] = entity.stripping1;
                dr["stripping2"] = entity.stripping2;
                dr["sample11"] = entity.sample11;
                dr["sample12"] = entity.sample12;
                dr["sample13"] = entity.sample13;
                dr["sample21"] = entity.sample21;
                dr["sample22"] = entity.sample22;
                dr["sample23"] = entity.sample23;
                dr["baseheight"] = entity.baseheight;
                dr["measuredheight"] = entity.measuredheight;
                dr["compositeheight"] = entity.compositeheight;
                dr["cutheight"] = entity.cutheight;
                dr["bubblewater1"] = entity.bubblewater1;
                dr["bubblewater2"] = entity.bubblewater2;
                dr["bubbleoil"] = entity.bubbleoil;
                dr["descrtiption"] = entity.descrtiption;
                dr["loginid"] = entity.loginid;
                dr["target"] = entity.target;
                dr["elongation"] = entity.elongation;
                dr["elongation1"] = entity.elongation1;
                dr["type"] = entity.type;
                dr["decision"] = entity.decision;
                iIndex++;
                dt.Rows.Add(dr);  
            }

            return dt;
        }

        public static DataTable ConvertList2ExcelTable(List<QualityTrackingEntity> listValue)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("序号", typeof(Int32)));
            dt.Columns.Add(new DataColumn("时间", typeof(string)));
            dt.Columns.Add(new DataColumn("品种", typeof(string)));
            dt.Columns.Add(new DataColumn("批次", typeof(string)));
            dt.Columns.Add(new DataColumn("规格", typeof(string)));
            dt.Columns.Add(new DataColumn("卷号", typeof(string)));
            dt.Columns.Add(new DataColumn("剥离1", typeof(string)));
            dt.Columns.Add(new DataColumn("剥离2", typeof(string)));
            dt.Columns.Add(new DataColumn("样品1-1", typeof(string)));
            dt.Columns.Add(new DataColumn("样品1-2", typeof(string)));
            dt.Columns.Add(new DataColumn("样品1-3", typeof(string)));
            dt.Columns.Add(new DataColumn("样品2-1", typeof(string)));
            dt.Columns.Add(new DataColumn("样品2-2", typeof(string)));
            dt.Columns.Add(new DataColumn("样品2-3", typeof(string)));
            dt.Columns.Add(new DataColumn("基带厚度", typeof(string)));
            dt.Columns.Add(new DataColumn("实测厚度", typeof(string)));
            dt.Columns.Add(new DataColumn("复合厚度", typeof(string)));
            dt.Columns.Add(new DataColumn("分切厚度", typeof(string)));
            dt.Columns.Add(new DataColumn("泡水1", typeof(string)));
            dt.Columns.Add(new DataColumn("泡水2", typeof(string)));
            dt.Columns.Add(new DataColumn("泡油", typeof(string)));
            dt.Columns.Add(new DataColumn("备注", typeof(string)));
            dt.Columns.Add(new DataColumn("去向", typeof(string)));
            dt.Columns.Add(new DataColumn("原材料延伸率", typeof(string)));
            dt.Columns.Add(new DataColumn("成品延伸率", typeof(string)));
            dt.Columns.Add(new DataColumn("类型", typeof(string)));
            dt.Columns.Add(new DataColumn("品质判定", typeof(string)));

            int i = 1;
            foreach (QualityTrackingEntity entity in listValue)
            {
                DataRow dr = dt.NewRow();
                dr["序号"] = i;
                dr["时间"] = entity.qtdate.ToShortDateString();
                dr["品种"] = entity.category;
                dr["批次"] = entity.batch;
                dr["规格"] = entity.specifications + "*" + entity.length;
                dr["卷号"] = entity.volume;
                dr["剥离1"] = entity.stripping1;
                dr["剥离2"] = entity.stripping2;
                dr["样品1-1"] = entity.sample11;
                dr["样品1-2"] = entity.sample12;
                dr["样品1-3"] = entity.sample13;
                dr["样品2-1"] = entity.sample21;
                dr["样品2-2"] = entity.sample22;
                dr["样品2-3"] = entity.sample23;
                dr["基带厚度"] = entity.baseheight;
                dr["实测厚度"] = entity.measuredheight;
                dr["复合厚度"] = entity.compositeheight;
                dr["分切厚度"] = entity.cutheight;
                dr["泡水1"] = entity.bubblewater1;
                dr["泡水2"] = entity.bubblewater2;
                dr["泡油"] = entity.bubbleoil;
                dr["备注"] = entity.descrtiption;
                dr["去向"] = entity.target;
                dr["原材料延伸率"] = entity.elongation;
                dr["成品延伸率"] = entity.elongation1;
                dr["类型"] = entity.type;
                dr["品质判定"] = entity.decision;
                i++;
                dt.Rows.Add(dr);
            }

            return dt;
        }

    }
}
