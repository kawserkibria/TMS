
using Dutility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace gobjVoucherName
{
    public class VoucherName
    {

        private static string mstrReceiptVoucher { get; set; }
        private static string mstrPaymentVoucher { get; set; }
        private static string mstrJournalVoucher { get; set; }
        private static string mstrContraVoucher { get; set; }
        private static string mstrQuotation { get; set; }
        private static string mstrSalesOrder { get; set; }
        private static string mstrDeliveryOrder { get; set; }
        private static string mstrSalesChallan { get; set; }
        private static string mstrSalesInvoice { get; set; }
        private static string mstrSalesInvoicePOS { get; set; }
        private static string mstrSalesReturn { get; set; }
        private static string mstrPurchaseOrder { get; set; }
        private static string mstrPurchaseReceive { get; set; }
        private static string mstrPurchaseInvoice { get; set; }
        private static string mstrPurchaseReturn { get; set; }
        private static string mstrStockInward { get; set; }
        private static string mstrStockOutward { get; set; }
        private static string mstrStockDamge { get; set; }
        private static string mstrStockPhysical { get; set; }
        private static string mstrStockTransfer { get; set; }
        private static string mstrStockReturn { get; set; }
        private static string mstrMFGConsumption { get; set; }
        private static string mstrMFGFinishedGoods { get; set; }
        private static string mstrMFGVoucher { get; set; }
        private static string mstrSTrequisition { get; set; }
        private static string mstrHWProductRv { get; set; }
        private static string mstrHWProductReplace { get; set; }
        private static string mstrHWPReceiptSupplier { get; set; }
        private static string mstrHWReturnSupplier { get; set; }
        private static string mstrStockTrasferIN { get; set; }
        public static string GetVoucherString(long vType)
        {
            //mGetInitialize();
            switch (vType)
            {
                case ((long)Utility.VOUCHER_TYPE.vtSALES_QUOTATION):
                    return Utility.vtSALES_QUOTATION_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSALES_ORDER):
                    return Utility.vtSALES_ORDER_STR;
                case ((long)Utility.VOUCHER_TYPE.vtDELIVERY_ORDER):
                    return Utility.vtSALES_DELIVERY_ORDER_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSALES_CHALLAN):
                    return Utility.vtSALES_CHALLAN_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSALES_INVOICE):
                    return Utility.vtSALES_INVOICE_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSALES_INVOICE_POS):
                    return Utility.vtSALES_INVOICE_POS_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSALES_RETURN):
                    return Utility.vtSALES_RETURN_STR;
                case ((long)Utility.VOUCHER_TYPE.vtPURCHASE_ORDER):
                    return Utility.vtPURCHASE_ORDER_STR;
                case ((long)Utility.VOUCHER_TYPE.vtPURCHASE_RECEIVE):
                    return Utility.vtPURCHASE_RECEIVE_STR;
                case ((long)Utility.VOUCHER_TYPE.vtPURCHASE_INVOICE):
                    return Utility.vtPURCHASE_INVOICE_STR;
                case ((long)Utility.VOUCHER_TYPE.vtPURCHASE_RETURN):
                    return Utility.vtPURCHASE_RETURN_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_INWARD):
                    return Utility.vtSTOCK_INWARD_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_OUTWARD):
                    return Utility.vtSTOCK_OUTWARD_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_DAMAGE):
                    return Utility.vtSTOCK_DAMAGE_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_PHYSICAL):
                    return Utility.vtSTOCK_PHYSICAL_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_TRANSFER):
                    return Utility.vtSTOCK_TRANSFER_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_RETURN):
                    return Utility.vtSTOCK_RETURN_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_MFG_CONSUMPTION):
                    return Utility.vtSTOCK_MFG_CONSUMPTION_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_MFG_FINISHED_GOODS):
                    return Utility.vtSTOCK_MFG_FINISHEDGOODS_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_MFG_VOUCHER):
                    return Utility.vtSTOCK_MFG_VOUCHER_STR;
                case ((long)Utility.VOUCHER_TYPE.vtPAYMENT_VOUCHER):
                    return Utility.vtPAYMENT_VOUCHER_STR;
                case ((long)Utility.VOUCHER_TYPE.vtRECEIPT_VOUCHER):
                    return Utility.vtRECEIPT_VOUCHER_STR;
                case ((long)Utility.VOUCHER_TYPE.vtJOURNAL_VOUCHER):
                    return Utility.vtJOURNAL_VOUCHER_STR;
                case ((long)Utility.VOUCHER_TYPE.vtCONTRA_VOUCHER):
                    return Utility.vtCONTRA_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_REQUISITION):
                    return Utility.vtSTOCKREQUISITION;
                case ((long)Utility.VOUCHER_TYPE.vt_SALESSAMPLE):
                    return Utility.vtSTOCK_SAMPLE_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_MFG_PRODUCTION):
                    return Utility.vtSTOCK_PRODUCTION_STR;
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_TRANFERIN):
                    return Utility.vtSTOCK_TRANSFERIN;
                default:
                    return "";


            }
        }
        public static string GetVoucherName(long vType)
        {
            
            switch (vType)
            {
                case ((long)Utility.VOUCHER_TYPE.vtSALES_QUOTATION):
                    return "";
                case ((long)Utility.VOUCHER_TYPE.vtSALES_ORDER):
                    return "Sales Order";
                case ((long)Utility.VOUCHER_TYPE.vtDELIVERY_ORDER):
                    return "Delivery Order";
                case ((long)Utility.VOUCHER_TYPE.vtSALES_CHALLAN):
                    return "Sales Challan";
                case ((long)Utility.VOUCHER_TYPE.vtSALES_INVOICE):
                    return "Sales Invoice";
                case ((long)Utility.VOUCHER_TYPE.vtSALES_INVOICE_POS):
                    return mstrSalesInvoicePOS ;
                case ((long)Utility.VOUCHER_TYPE.vtSALES_RETURN):
                    return "Sales Return";
                case ((long)Utility.VOUCHER_TYPE.vtPURCHASE_ORDER):
                    return "Purchase Order";
                case ((long)Utility.VOUCHER_TYPE.vtPURCHASE_RECEIVE):
                    return mstrPurchaseReceive;
                case ((long)Utility.VOUCHER_TYPE.vtPURCHASE_INVOICE):
                    return "Purchase Invoice";
                case ((long)Utility.VOUCHER_TYPE.vtPURCHASE_RETURN):
                    return "Purchase Return";
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_INWARD):
                    return mstrStockInward;
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_OUTWARD):
                    return mstrStockOutward;
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_DAMAGE):
                    return "Stock Damage";
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_PHYSICAL):
                    return mstrStockPhysical;
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_TRANSFER):
                    return "Stock Transfer";
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_RETURN):
                    return "Stock Return";
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_MFG_CONSUMPTION):
                    return "Stock Absorved";
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_MFG_FINISHED_GOODS):
                    return "Finished Goods List";
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_MFG_VOUCHER):
                    return mstrMFGVoucher;
                case ((long)Utility.VOUCHER_TYPE.vtPAYMENT_VOUCHER):
                    return "Payment";
                case ((long)Utility.VOUCHER_TYPE.vtRECEIPT_VOUCHER):
                    return "Receipt" ;
                case ((long)Utility.VOUCHER_TYPE.vtJOURNAL_VOUCHER):
                    return "Journal";
                case ((long)Utility.VOUCHER_TYPE.vtCONTRA_VOUCHER):
                    return "Contra";
                case ((long)Utility.VOUCHER_TYPE.vtSTOCK_REQUISITION):
                    return "Stock Requisition";
                case ((long)Utility.VOUCHER_TYPE.vt_SAMPLE_CLASS):
                    return "Sample Class";
                default:
                    return "";


            }
        }

        public static long GetVoucherType(string vstrRefNo)
        {

            switch (vstrRefNo)
            {
                case "Sales Order":
                    return (long)Utility.VOUCHER_TYPE.vtSALES_ORDER;
                case "Purchase Order":
                    return (long)Utility.VOUCHER_TYPE.vtPURCHASE_ORDER;
                case "Sales Challan":
                    return (long)Utility.VOUCHER_TYPE.vtSALES_CHALLAN;
                case "Sales Invoice":
                    return (long)Utility.VOUCHER_TYPE.vtSALES_INVOICE;
                case "Purchase Invoice":
                    return (long)Utility.VOUCHER_TYPE.vtPURCHASE_INVOICE;
                case "Sample Class":
                    return (long)Utility.VOUCHER_TYPE.vt_SAMPLE_CLASS;
                default:
                    return 0;


            }
        }
        private static void mGetInitialize(long lngvtype)
        {
            long lngVoucherType;
            string strSQL;
            string conDb;
            SqlDataReader dr;
            conDb = Utility.SQLConnstring();
            strSQL = "SELECT VOUCHER_TYPE_NAME,VOUCHER_TYPE_VALUE FROM ACC_VOUCHER_TYPE ";
            strSQL = " WHERE VOUCHER_TYPE_VALUE= " + lngvtype + " ";
            using (SqlConnection gcnMain = new SqlConnection(conDb))
            {
                if (gcnMain.State == System.Data.ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lngVoucherType = Convert.ToInt64(dr["VOUCHER_TYPE_VALUE"].ToString());
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtRECEIPT_VOUCHER)
                    {
                        mstrReceiptVoucher = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtPAYMENT_VOUCHER)
                    {
                        mstrPaymentVoucher = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtJOURNAL_VOUCHER)
                    {
                        mstrJournalVoucher = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtCONTRA_VOUCHER)
                    {
                        mstrContraVoucher  = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtSALES_QUOTATION)
                    {
                        mstrQuotation = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtDELIVERY_ORDER)
                    {
                        mstrDeliveryOrder  = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtSALES_INVOICE)
                    {
                        mstrSalesInvoice  = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtSALES_INVOICE_POS)
                    {
                        mstrSalesInvoicePOS  = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtPURCHASE_RECEIVE)
                    {
                        mstrPurchaseReceive  = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtPURCHASE_INVOICE)
                    {
                        mstrPurchaseInvoice = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtSALES_ORDER)
                    {
                        mstrSalesOrder = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtSALES_RETURN)
                    {
                        mstrSalesReturn  = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtPURCHASE_ORDER)
                    {
                        mstrPurchaseOrder  = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtPURCHASE_RECEIVE)
                    {
                        mstrPurchaseReceive  = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtPURCHASE_INVOICE)
                    {
                        mstrPurchaseInvoice = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtPURCHASE_RETURN)
                    {
                        mstrPurchaseReturn = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtSTOCK_INWARD)
                    {
                        mstrStockInward = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtSTOCK_OUTWARD)
                    {
                        mstrStockOutward = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtSTOCK_DAMAGE)
                    {
                        mstrStockDamge = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType  == (long)Utility.VOUCHER_TYPE.vtSTOCK_PHYSICAL)
                    {
                        mstrStockPhysical = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtSTOCK_TRANSFER)
                    {
                        mstrStockTransfer = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtSTOCK_RETURN)
                    {
                        mstrStockReturn = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtSTOCK_MFG_CONSUMPTION)
                    {
                        mstrMFGConsumption = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtSTOCK_MFG_FINISHED_GOODS)
                    {
                        mstrMFGFinishedGoods = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtSTOCK_MFG_VOUCHER)
                    {
                        mstrMFGVoucher = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtHW_PRODUCT_REPLACEMENT)
                    {
                        mstrHWProductReplace = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtHW_RECEIVE_SUPPLIER)
                    {
                        mstrHWPReceiptSupplier = dr["VOUCHER_TYPE_NAME"].ToString();
                    }
                    if (lngVoucherType == (long)Utility.VOUCHER_TYPE.vtHW_RETURN_SUPPLIER)
                    {
                        mstrHWReturnSupplier = dr["VOUCHER_TYPE_NAME"].ToString();
                    }

                }

                dr.Close();
                gcnMain.Close();
                //return strBranchName;


            }

        }


    }
}
