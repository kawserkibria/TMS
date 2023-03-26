using Jagoron.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using TMS.Models;

namespace TMS.Repository
{
    public class RepTMS
    {

        string connectionString = WebConfigurationManager.ConnectionStrings["gcnmain"].ConnectionString;
        #region "Order"

        public List<OrderDList> mGetTextVal(int intid, string strorderNo)
        {
            string strSQL = null;
            SqlDataReader dr;


            List<OrderDList> ooCategory = new List<OrderDList>();
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                try
                {
                    gcnMain.Open();
                    strSQL = "select TEXT_VAL from ORDER_DRESS_TEXT_INFO WHERE (DRESS_ID = " + intid + ") AND  ORDER_NO=" + strorderNo + " order by TEXT_ID asc ";
                    SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        OrderDList oCat = new OrderDList();
                        oCat.dblDressRate = Convert.ToInt32(dr["TEXT_VAL"]);
                        ooCategory.Add(oCat);
                    }

                    dr.Close();
                    gcnMain.Close();
                    gcnMain.Dispose();
                    return ooCategory;
                }
                catch (Exception ex)
                {
                    OrderDList oCat = new OrderDList();
                    oCat.dblDressRate = 0;
                    ooCategory.Add(oCat);
                    return ooCategory;
                }
            }

        }

        public string mInsertOrder(OrderM obj)
        {
            string strSQL = "";
            string strstringNew = "", strOrderNO = "OR1";
            List<OrderDList> OrderDList = new List<OrderDList>();
            List<OrderDD> Mesurmentlist = new List<OrderDD>();
            List<OrderandFabrics> OrderandFabrics = new List<OrderandFabrics>();

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                try
                {
                    gcnMain.Open();

                    int intBranchID = 0;

                    SqlDataReader dr;
                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;



                    strSQL = "SELECT COUNT(SERIAL) as OrderNO from ORDER_MASTER_INFO";
                    cmdInsert.CommandText = strSQL;
                    dr = cmdInsert.ExecuteReader();
                    if (dr.Read())
                    {
                        strOrderNO = "";
                        strOrderNO = "OR" + Convert.ToInt32(dr["OrderNO"]);
                    }
                    dr.Close();

                    


                    OrderDList = obj.detailsList[0].OrderDList;
                    OrderandFabrics = obj.detailsList[0].OrderandFabrics;
                    Mesurmentlist = obj.detailsList[0].Mesurmentlist;
                    for (int i = 0; i < Mesurmentlist.Count; i++)
                    {
                        strSQL = "INSERT INTO ORDER_DRESS_TEXT_INFO";
                        strSQL = strSQL + "(ORDER_NO, DRESS_ID, TEXT_ID, TEXT_VAL)";
                        strSQL = strSQL + "VALUES(";
                        strSQL = strSQL + "'" + strOrderNO + "'," + Mesurmentlist[i].intDressId + ",'" + Mesurmentlist[i].inttxtid + "','" + Mesurmentlist[i].dbltxtVal + "' ";
                        strSQL = strSQL + ")";
                        cmdInsert.CommandText = strSQL;
                        cmdInsert.ExecuteNonQuery();
                    }

                    for (int i = 0; i < OrderandFabrics.Count; i++)
                    {
                        strSQL = "INSERT INTO ORDER_CHILD_PURPOSE_INFO";
                        strSQL = strSQL + "(ORDER_NO, DRESS_ID, PURPOSE, ORDER_QTY, ORDER_RATE, ORDER_VAL)";
                        strSQL = strSQL + "VALUES(";
                        strSQL = strSQL + "'" + strOrderNO + "'," + OrderandFabrics[i].intDressId + ",'" + OrderandFabrics[i].strFabricsName + "','" + OrderandFabrics[i].intQty + "','" + OrderandFabrics[i].intRate + "','" + OrderandFabrics[i].dbltotalVal + "' ";
                        strSQL = strSQL + ")";
                        cmdInsert.CommandText = strSQL;
                        cmdInsert.ExecuteNonQuery();
                    }
                    for (int i = 0; i < OrderDList.Count; i++)
                    {
                        strSQL = "INSERT INTO ORDER_CHILD_INFO";
                        strSQL = strSQL + "( ORDER_NO, DRESS_ID, ORDER_QTY, ORDER_VAL)";
                        strSQL = strSQL + "VALUES(";
                        strSQL = strSQL + "'" + strOrderNO + "'," + OrderDList[i].intDressId + "," + OrderDList[i].intIDressQty + "," + OrderDList[i].dblTotalAmount + " ";
                        strSQL = strSQL + ")";
                        cmdInsert.CommandText = strSQL;
                        cmdInsert.ExecuteNonQuery();

                        strstringNew = "";

                        strSQL = "SELECT * FROM DivCreates where CategoryId=" + OrderDList[i].intDressId + " ";
                        cmdInsert.CommandText = strSQL;
                        dr = cmdInsert.ExecuteReader();
                        while (dr.Read())
                        {                    //string strCategoryId = "";
                            //if (dr["CategoryId"].ToString() != "")
                            //{
                            //    strCategoryId = Convert.ToDateTime(dr["CategoryId"]).ToString("dd/MM/yyyy");
                            //}
                            //else
                            //{
                            //    strCategoryId = "";
                            //}
                 
        
                            strstringNew = strstringNew + dr["LabelTxt"].ToString() + "~";

                        }
                        dr.Close();


                        if (strstringNew != "")
                        {

                            string[] words = strstringNew.Split('~');
                            foreach (string ooassets in words)
                            {
                                string[] oAssets = ooassets.Split('|');
                                if (oAssets[0] != "")
                                {
                                    //strSQL = "UPDATE ACC_VOUCHER_PARENT SET ";
                                    //strSQL = strSQL + "CHEQUE_NO='" + oAssets[1].ToString() + "' ";

                                    strSQL = "INSERT INTO ORDER_DRESS_CATEGORY_INFO";
                                    strSQL = strSQL + "(ORDER_NO,INVOICE_NO,CUSTOMER_ID,DRESS_ID,LABEL_TEXT)";
                                    strSQL = strSQL + "VALUES(";
                                    strSQL = strSQL + "'" + strOrderNO + "','1'," + obj.strCustomerId + "," + OrderDList[i].intDressId + ",N'" + oAssets[0].ToString() + "' ";
                                    strSQL = strSQL + ")";
                                    cmdInsert.CommandText = strSQL;
                                    cmdInsert.ExecuteNonQuery();


                                }
                            }
                        }


                    }

                    strSQL = "INSERT INTO ORDER_MASTER_INFO";
                    strSQL = strSQL + "(ORDER_NO, CUSTOMER_ID, ORDER_VAL)";
                    strSQL = strSQL + "VALUES(";
                    strSQL = strSQL + "'" + strOrderNO + "'," + obj.strCustomerId + "," + obj.dblVal + " ";
                    strSQL = strSQL + ")";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();


                    //strSQL = "SELECT * FROM DivCreates where CategoryId=2 ";
                    //SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                    //dr = cmd.ExecuteReader();
                    //if (dr.Read())
                    //{
                    //    strYesNo = "Y";
                    //}





                    cmdInsert.Transaction.Commit();
                    gcnMain.Close();
                    return "OK";

                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }


            }

        }
        public List<OrderList> OrderList()
        {
            string strSQL = null;
            SqlDataReader dr;


            List<OrderList> ooCategory = new List<OrderList>();
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                try
                {
                    gcnMain.Open();
                    strSQL = "SELECT SERIAL, ORDER_NO, CUSTOMER_ID, ORDER_VAL, ORDER_DATE FROM ORDER_MASTER_INFO";
                    SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        OrderList oCat = new OrderList();
                        oCat.intSerialNo = Convert.ToInt32(dr["SERIAL"]);
                        oCat.strOrderNo = dr["ORDER_NO"].ToString();
                        oCat.intCustomer = Convert.ToInt32( dr["CUSTOMER_ID"]);
                        oCat.dblOrderValue = Convert.ToDouble(dr["ORDER_VAL"]);
                        oCat.strOrderDate = Convert.ToDateTime(dr["ORDER_DATE"]).ToString("dd-MM-yyyy");
                        ooCategory.Add(oCat);
                    }

                    dr.Close();
                    gcnMain.Close();
                    gcnMain.Dispose();
                    return ooCategory;
                }
                catch (Exception ex)
                {
                    OrderList oCat = new OrderList();
                    oCat.strOrderNo = "";
                    oCat.intCustomer = 0;
                    oCat.intSerialNo = 0;
                    ooCategory.Add(oCat);
                    return ooCategory;
                }
            }

        }
        #endregion
        #region "Companay Entry"
        public string mInsertEmployeeImage(string strDeComID, string strCompanyName, string strMobileNumber, int intStatus, byte[] vImage)
        {

            SqlDataReader dr;
            string strSQL = "";

            int intComID = 01;


            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd1, cmd2;
            con.Open();
            cmd1 = new SqlCommand("sp1", con);
            cmd2 = new SqlCommand("sp2", con);
            SqlTransaction trans = con.BeginTransaction();
            cmd1.Transaction = trans;
            cmd2.Transaction = trans;
            try
            {


                strSQL = "SELECT (case when  MAX(COMPANY_ID) is null then 0 else MAX(COMPANY_ID) end) +1 as COMPANY_ID FROM COMPANY_NAME ";
                cmd1.CommandText = strSQL;
                cmd1.ExecuteNonQuery();

                dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    intComID = Convert.ToInt32(dr["COMPANY_ID"].ToString());
                }
                dr.Close();
              
                cmd1.CommandText = "INSERT_COMPANY_NAME";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@CompanyID", SqlDbType.Int).Value = intComID;
                cmd1.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = strCompanyName;
                cmd1.Parameters.Add("@MobileNumber", SqlDbType.VarChar).Value = strMobileNumber;
                cmd1.Parameters.Add("@status", SqlDbType.SmallInt).Value = intStatus;
                cmd1.ExecuteNonQuery();

                cmd2.CommandText = "INSERT_COMPANY_ICON";
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@CompanyID", SqlDbType.Int).Value = intComID;
                cmd2.Parameters.Add("@img", SqlDbType.Image).Value = vImage;
                cmd2.ExecuteNonQuery();
          
                trans.Commit();
                return "OK";
            }
            catch (Exception)
            {
                trans.Rollback();
            }
            return "NO";
          
        }

        public string mUpdateEmployeeImage(string strDeComID, string strCompanyName, string strMobileNumber, int intStatus, byte[] vImage, int intID)
        {



     
            string strSQL = "";

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd1, cmd2;
            con.Open();
            cmd1 = new SqlCommand("sp1", con);
            cmd2 = new SqlCommand("sp2", con);
            SqlTransaction trans = con.BeginTransaction();
            cmd1.Transaction = trans;
            cmd2.Transaction = trans;
            try
            {


           
                if (vImage.Length > 0)
                {
                    strSQL = "DELETE FROM  COMPANY_ICON WHERE COMPANY_ID=" + intID + " ";
                    cmd1.CommandText = strSQL;
                    cmd1.ExecuteNonQuery();
                }


                cmd1.CommandText = "UPDATE_COMPANY_NAME";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@CompanyID", SqlDbType.Int).Value = intID;
                cmd1.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = strCompanyName;
                cmd1.Parameters.Add("@MobileNumber", SqlDbType.VarChar).Value = strMobileNumber;
                cmd1.Parameters.Add("@status", SqlDbType.SmallInt).Value = intStatus;
                cmd1.ExecuteNonQuery();
                if (vImage.Length > 0)
                {
                    cmd2.CommandText = "INSERT_COMPANY_ICON";
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add("@CompanyID", SqlDbType.Int).Value = intID;
                    cmd2.Parameters.Add("@img", SqlDbType.Image).Value = vImage;
                    cmd2.ExecuteNonQuery();
                }

                trans.Commit();
                return "OK";
            }
            catch (Exception)
            {
                trans.Rollback();
            }
            return "NO";

           
        }

        public List<CompanyEntry> mFillShowGrid(string strDeComID)
        {
            string strSQL = null;
            SqlDataReader dr;


            List<CompanyEntry> ooCategory = new List<CompanyEntry>();
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                try
                {
                    gcnMain.Open();
                    strSQL = "SELECT * from COMPANY_NAME,COMPANY_ICON  WHERE COMPANY_NAME.COMPANY_ID=COMPANY_ICON.COMPANY_ID";
                    SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        CompanyEntry oCat = new CompanyEntry();
                     
                            oCat.CompanayID = Convert.ToInt32(dr["COMPANY_ID"].ToString());
                            oCat.CompanayName = dr["COMPANY_NAME"].ToString();
                            oCat.MobileNumber = dr["MOBILE_NUMBER"].ToString();
                            oCat.inStatus = Convert.ToInt32(dr["COMPANY_STATUS"].ToString());
                            if (dr["COMPANY_IMAGE"] != null)
                            {
                                oCat.Data = Convert.ToBase64String((byte[])dr["COMPANY_IMAGE"]);
                            }
                            ooCategory.Add(oCat);
                    }

                    dr.Close();
                    gcnMain.Close();
                    gcnMain.Dispose();
                    return ooCategory;
                }
                catch (Exception ex)
                {
                    CompanyEntry oCat = new CompanyEntry();
                    oCat.CompanayName = "";
                    ooCategory.Add(oCat);
                    return ooCategory;
                }
            }

        }

        public List<CompanyEntry> GetImages()
        {
            string strSQL;
            SqlDataReader drGetGroup;
            List<CompanyEntry> oogrp = new List<CompanyEntry>();
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                try
                {
                    gcnMain.Open();
                    strSQL = "SELECT * FROM COMPANY_NAME";
                    SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                    drGetGroup = cmd.ExecuteReader();
                    while (drGetGroup.Read())
                    {

                        CompanyEntry ogrp = new CompanyEntry();
                        ogrp.Id = Convert.ToInt32(drGetGroup["SERIAL_NO"].ToString());
                        ogrp.CompanayName = drGetGroup["COMPANY_NAME"].ToString();
                        ogrp.strIamge = (byte[])drGetGroup["COMPANY_IMAGE"];

                        oogrp.Add(ogrp);
                    }
                    drGetGroup.Close();
                    gcnMain.Dispose();
                    return oogrp;

                }
                catch (Exception ex)
                {
                    CompanyEntry ogrp = new CompanyEntry();
                    //ogrp.strBranchName = ex.ToString();
                    oogrp.Add(ogrp);
                    return oogrp;
                }
            }

        }

        public string DeleteCompany(CompanyEntry obj)
        {
           string strsubFroup = "",strSQL="";
                using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }

                try
                {
                    gcnMain.Open();

                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    SqlDataReader dr;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;
                    strSQL = "DELETE FROM  COMPANY_NAME WHERE SERIAL_NO=" + obj.Id + " ";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();

                    cmdInsert.Transaction.Commit();


                    gcnMain.Dispose();
                    return "OK";

                }
                catch (Exception ex)
                {
                    return "ex";
                }
            }

        }
        #endregion
        #region "Branch Entry"
        public string mInsertBranch(BranchEntry obj)
        {
            string strSQL = "";
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }

                gcnMain.Open();

                int intBranchID = 0;

                SqlDataReader dr;
                SqlCommand cmdInsert = new SqlCommand();
                SqlTransaction myTrans;
                myTrans = gcnMain.BeginTransaction();
                cmdInsert.Connection = gcnMain;
                cmdInsert.Transaction = myTrans;

                strSQL = "SELECT (case when  MAX(BRANCH_ID) is null then 0 else MAX(BRANCH_ID) end) +1 as BRANCH_ID FROM BRANCH_NAME ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                dr = cmdInsert.ExecuteReader();
                if (dr.Read())
                {
                    intBranchID = Convert.ToInt32(dr["BRANCH_ID"].ToString());
                }
                dr.Close();


                strSQL = "INSERT INTO BRANCH_NAME";
                strSQL = strSQL + "(COMPANY_ID, BRANCH_ID, BRANCH_NAME,BRANCH_ADDRESS, MOBILE_NUMBER1, MOBILE_NUMBER2, MOBILE_NUMBER3, EMAIL_ADDRES, FACEBOOK_ID, BRANCH_STATUS)";
                strSQL = strSQL + "VALUES(";
                strSQL = strSQL + "" + obj.intCompanayID + "," + intBranchID + ",'" + obj.strBranchName + "','" + obj.strBranch_Address + "',";
                strSQL = strSQL + "'" + obj.strMOBILE_NUMBER1 + "','" + obj.strMOBILE_NUMBER2 + "','" +  obj.strMOBILE_NUMBER3 + "' ";
                strSQL = strSQL + ",'" + obj.strEmail + "','" + obj.strfacebookID + "' ";
                strSQL = strSQL + "," + obj.intBranchStatus + " ";
                strSQL = strSQL + ")";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                cmdInsert.Transaction.Commit();
                gcnMain.Close();
                return "OK";
               
            }




        }
        public string mUpdateBranch(BranchEntry obj)
        {
            string strSQL = "";
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }

                gcnMain.Open();

                int intBranchID = 0;

                SqlDataReader dr;
                SqlCommand cmdInsert = new SqlCommand();
                SqlTransaction myTrans;
                myTrans = gcnMain.BeginTransaction();
                cmdInsert.Connection = gcnMain;
                cmdInsert.Transaction = myTrans;

                strSQL = "UPDATE BRANCH_NAME SET BRANCH_NAME = '" + obj.strBranchName + "',BRANCH_ADDRESS = '" + obj.strBranch_Address + "' ,MOBILE_NUMBER1 = '" + obj.strMOBILE_NUMBER1 + "' ,MOBILE_NUMBER2 = '" + obj.strMOBILE_NUMBER2 + "',  ";
                strSQL = strSQL + "MOBILE_NUMBER3 = '" + obj.strMOBILE_NUMBER3 + "',EMAIL_ADDRES = '" + obj.strEmail + "', ";
                strSQL = strSQL + " FACEBOOK_ID = '" + obj.strfacebookID + "',BRANCH_STATUS = " + obj.intBranchStatus + "  ";
                strSQL = strSQL + "WHERE BRANCH_ID = " + obj.intBranchID + " AND COMPANY_ID= " + obj.intCompanayID + " ";
                cmdInsert.CommandText = strSQL;
                cmdInsert.ExecuteNonQuery();
                cmdInsert.Transaction.Commit();
                gcnMain.Close();
                return "OK";

            }




        }
        public List<BranchEntry> mFillShowBranchName(BranchEntry obj)
        {
            string strSQL = null;
            SqlDataReader dr;


            List<BranchEntry> ooCategory = new List<BranchEntry>();
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                try
                {
                    gcnMain.Open();
                    strSQL = "SELECT * from BRANCH_NAME";
                    SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        BranchEntry oCat = new BranchEntry();
                        oCat.intCompanayID = Convert.ToInt32(dr["COMPANY_ID"].ToString());
                        oCat.intBranchID = Convert.ToInt32(dr["BRANCH_ID"].ToString());
                        oCat.strBranchName = dr["BRANCH_NAME"].ToString();
                        oCat.strBranch_Address = dr["BRANCH_ADDRESS"].ToString();
                        oCat.strMOBILE_NUMBER1 = dr["MOBILE_NUMBER1"].ToString();
                        oCat.strMOBILE_NUMBER2 = dr["MOBILE_NUMBER2"].ToString();
                        oCat.strMOBILE_NUMBER3 = dr["MOBILE_NUMBER3"].ToString();
                        oCat.strEmail = dr["EMAIL_ADDRES"].ToString();
                        oCat.strfacebookID = dr["FACEBOOK_ID"].ToString();
                        oCat.intBranchStatus = Convert.ToInt32(dr["BRANCH_STATUS"].ToString());
                        ooCategory.Add(oCat);
                    }

                    dr.Close();
                    gcnMain.Close();
                    gcnMain.Dispose();
                    return ooCategory;
                }
                catch (Exception ex)
                {

                    return ooCategory;
                 
                 
                }
            }

        }
        public string DeleteBranch(BranchEntry obj)
        {
            string strsubFroup = "", strSQL = "";
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }

                try
                {
                    gcnMain.Open();

                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    SqlDataReader dr;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;
                    strSQL = "DELETE FROM  BRANCH_NAME WHERE BRANCH_ID = " + obj.intBranchID + " AND COMPANY_ID= " + obj.intCompanayID + " ";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();
                    cmdInsert.Transaction.Commit();


                    gcnMain.Dispose();
                    return "OK";

                }
                catch (Exception ex)
                {
                    return "ex";
                }
            }

        }
        #endregion
        #region "Button"
        public string mInsertButton(ButtonName obj)
        {

            SqlDataReader dr;
            string strSQL = "";

            int intButtonID = 01;



            var byts = new byte[0];
            if ((obj.Picture != null) && (obj.Picture.ContentLength > 0))
            {

                var pic = obj.Picture.InputStream;

                MemoryStream ms = new MemoryStream();
                pic.CopyTo(ms);
                byts = ms.ToArray();
                ms.Dispose();
            }



            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd1, cmd2;
            con.Open();
            cmd1 = new SqlCommand("sp1", con);
            cmd2 = new SqlCommand("sp2", con);
            SqlTransaction trans = con.BeginTransaction();
            cmd1.Transaction = trans;
            cmd2.Transaction = trans;
            try
            {


                strSQL = "SELECT (case when  MAX(BUTTON_ID) is null then 0 else MAX(BUTTON_ID) end) +1 as BUTTON_ID FROM BUTTON_NAME_INFO ";
                cmd1.CommandText = strSQL;
                cmd1.ExecuteNonQuery();

                dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    intButtonID = Convert.ToInt32(dr["BUTTON_ID"].ToString());
                }
                dr.Close();

                cmd1.CommandText = "INSERT_BUTTON_NAME_INFO";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@ButtonID", SqlDbType.Int).Value = intButtonID;
                cmd1.Parameters.Add("@ButtonName", SqlDbType.VarChar).Value = obj.strBUTTON_NAME;
                cmd1.Parameters.Add("@status", SqlDbType.SmallInt).Value = obj.inStatus;
                cmd1.ExecuteNonQuery();

                cmd2.CommandText = "INSERT_BUTTON_ICON";
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@ButtonID", SqlDbType.Int).Value = intButtonID;
                cmd2.Parameters.Add("@img", SqlDbType.Image).Value = byts;
                cmd2.ExecuteNonQuery();
          

                trans.Commit();
                return "OK";
            }
            catch (Exception)
            {
                trans.Rollback();
            }
            return "NO";

        }

        public string mUpdateButton(ButtonName obj)
        {




            var bytss = new byte[0];
            if ((obj.Picture != null) && (obj.Picture.ContentLength > 0))
            {

                var pic = obj.Picture.InputStream;

                MemoryStream msss = new MemoryStream();
                pic.CopyTo(msss);
                bytss = msss.ToArray();
                msss.Dispose();
            }

            string strSQL = "";

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd1, cmd2;
            con.Open();
            cmd1 = new SqlCommand("sp1", con);
            cmd2 = new SqlCommand("sp2", con);
            SqlTransaction trans = con.BeginTransaction();
            cmd1.Transaction = trans;
            cmd2.Transaction = trans;
            try
            {



                if (bytss.Length > 0)
                {
                    strSQL = "DELETE FROM  BUTTON_ICON WHERE BUTTON_ID=" + obj.intBUTTON_ID + " ";
                    cmd1.CommandText = strSQL;
                    cmd1.ExecuteNonQuery();
                }


                cmd1.CommandText = "UPDATE_BUTTON_NAME";
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@ButtonID", SqlDbType.Int).Value = obj.intBUTTON_ID;
                cmd1.Parameters.Add("@ButtonName", SqlDbType.VarChar).Value = obj.strBUTTON_NAME;
                cmd1.Parameters.Add("@status", SqlDbType.SmallInt).Value = obj.inStatus;
                cmd1.ExecuteNonQuery();
                if (bytss.Length > 0)
                {
                    cmd2.CommandText = "INSERT_BUTTON_ICON";
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add("@ButtonID", SqlDbType.Int).Value = obj.intBUTTON_ID;
                    cmd2.Parameters.Add("@img", SqlDbType.Image).Value = bytss;
                    cmd2.ExecuteNonQuery();
                }

                trans.Commit();
                return "OK";
            }
            catch (Exception)
            {
                trans.Rollback();
            }
            return "NO";


        }

        public List<ButtonName> mFillShowGridBrand(string strDeComID)
        {
            string strSQL = null;
            SqlDataReader dr;


            List<ButtonName> ooCategory = new List<ButtonName>();
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                try
                {
                    gcnMain.Open();
                    strSQL = "SELECT BUTTON_NAME_INFO.BUTTON_ID,BUTTON_NAME_INFO.BUTTON_NAME,BUTTON_NAME_INFO.BUTTON_STATUS,BUTTON_ICON.BUTTON_IMAGE from BUTTON_NAME_INFO,BUTTON_ICON  WHERE BUTTON_NAME_INFO.BUTTON_ID=BUTTON_ICON.BUTTON_ID";
                    SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ButtonName oCat = new ButtonName();

                        oCat.intBUTTON_ID = Convert.ToInt32(dr["BUTTON_ID"].ToString());
                        oCat.strBUTTON_NAME = dr["BUTTON_NAME"].ToString();
                        oCat.inStatus = Convert.ToInt32(dr["BUTTON_STATUS"].ToString());
                        if (dr["BUTTON_IMAGE"] != null)
                        {
                            oCat.Data = Convert.ToBase64String((byte[])dr["BUTTON_IMAGE"]);
                        }
                        ooCategory.Add(oCat);
                    }

                    dr.Close();
                    gcnMain.Close();
                    gcnMain.Dispose();
                    return ooCategory;
                }
                catch (Exception ex)
                {
                    ButtonName oCat = new ButtonName();
                    oCat.strBUTTON_NAME = "";
                    ooCategory.Add(oCat);
                    return ooCategory;
                }
            }

        }



        public string DeleteButton(ButtonName obj)
        {
            string strsubFroup = "", strSQL = "";
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }

                try
                {
                    gcnMain.Open();

                    SqlCommand cmdInsert = new SqlCommand();
                    SqlTransaction myTrans;
                    SqlDataReader dr;
                    myTrans = gcnMain.BeginTransaction();
                    cmdInsert.Connection = gcnMain;
                    cmdInsert.Transaction = myTrans;
                    strSQL = "DELETE FROM  BUTTON_ICON WHERE BUTTON_ID=" + obj.intBUTTON_ID + " ";
                    cmdInsert.CommandText = strSQL;
                    cmdInsert.ExecuteNonQuery();

                    cmdInsert.Transaction.Commit();


                    gcnMain.Dispose();
                    return "OK";

                }
                catch (Exception ex)
                {
                    return "ex";
                }
            }

        }
        #endregion
        public List<DressSetup> mFillMeserment(string strDeComID)
        {
            string strSQL = null;
            SqlDataReader dr;


            List<DressSetup> ooCategory = new List<DressSetup>();
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                try
                {
                    gcnMain.Open();
                    strSQL = "SELECT * FROM Categories ";
                    SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        DressSetup oCat = new DressSetup();

                        //oCat.strDressName = dr["DRESS_NAME"].ToString();
                        oCat.Name = dr["Name"].ToString();
                        oCat.Id = Convert.ToInt32(dr["Id"]);
                        ooCategory.Add(oCat);
                    }

                    dr.Close();
                    gcnMain.Close();
                    gcnMain.Dispose();
                    return ooCategory;
                }
                catch (Exception ex)
                {
                    DressSetup oCat = new DressSetup();
                    oCat.strDressName = "";
                    oCat.strMESURMENT_NAME = "";
                    oCat.intPOSITION = 0;
                    ooCategory.Add(oCat);
                    return ooCategory;
                }
            }

        }
        public List<CategoryViewModel> mFillDressMesermen(int intDid,int intAddmode,string strOrderNo)
        {
            string strSQL = null;
            SqlDataReader dr;
            int intDressid = 0;

            List<CategoryViewModel> ooCategory = new List<CategoryViewModel>();
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                try
                {
                    gcnMain.Open();


                    strSQL = "SELECT COUNT(DISTINCT ORDER_NO) ORDER_NO  from ORDER_DRESS_CATEGORY_INFO DC    WHERE (DC.DRESS_ID = " + intDid + ") AND  DC.ORDER_NO='" + strOrderNo + "'  ";
                    SqlCommand cmdd = new SqlCommand(strSQL, gcnMain);
                    dr = cmdd.ExecuteReader();
                    if (dr.Read())
                    {
                        intDressid = Convert.ToInt32(dr["ORDER_NO"]);
                    }
                    dr.Close();
                    if (intDressid > 0)
                    {
                            strSQL = "SELECT DC.DRESS_ID, C.Name, DC.LABEL_TEXT, 1 as Total ";
                            strSQL = strSQL + "FROM ORDER_DRESS_CATEGORY_INFO AS DC INNER JOIN  ";
                            strSQL = strSQL + "Categories AS C ON C.Id = DC.DRESS_ID AND C.IsDeleted = 0   ";
                            strSQL = strSQL + "WHERE (DC.DRESS_ID = " + intDid + ") AND DC.ORDER_NO='" + strOrderNo + "' ";
                    }
                    else
                    {
                        strSQL = "SELECT DC.CategoryId, C.Name, DC.LabelTxt, DC.Total ";
                        strSQL = strSQL + "FROM DivCreates AS DC INNER JOIN ";
                        strSQL = strSQL + "Categories AS C ON C.Id = DC.CategoryId AND C.IsDeleted = 0 ";
                        strSQL = strSQL + "WHERE (DC.CategoryId = " + intDid + ") AND (DC.IsDeleted = 0)";
                    }
                    SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        CategoryViewModel oCat = new CategoryViewModel();
                        if (intDressid == 0)
                        {
                            oCat.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                            oCat.Name = dr["Name"].ToString();
                            oCat.LabelTxt = dr["LabelTxt"].ToString();
                            oCat.Total = Convert.ToInt32(dr["Total"]);
                        }
                        else
                        {
                            oCat.CategoryId = Convert.ToInt32(dr["DRESS_ID"]);
                            oCat.Name = dr["Name"].ToString();
                            oCat.LabelTxt = dr["LABEL_TEXT"].ToString();
                            oCat.Total = Convert.ToInt32(dr["Total"]);
                        }
                        ooCategory.Add(oCat);
                    }

                    dr.Close();
                    gcnMain.Close();
                    gcnMain.Dispose();
                    return ooCategory;
                }
                catch (Exception ex)
                {
                    CategoryViewModel oCat = new CategoryViewModel();
                    oCat.CategoryId = 0;
                    oCat.Name = "";
                    oCat.LabelTxt = "";
                    oCat.Total = 0;
                    ooCategory.Add(oCat);
                    return ooCategory;
                }
            }

        }
    }
}