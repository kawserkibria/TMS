using MVCTreeview.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace MVCTreeview.Controllers
{
    public class TreeController : Controller
    {
        //
        // GET: /Tree/
       
        string connectionString = WebConfigurationManager.ConnectionStrings["gcnmain"].ConnectionString;
        #region "StockTree"
        public ActionResult InventoryTree()
        {
            return View();
        }
        public JsonResult GetJsTree3Data()
        {
            List<Treenode> list = ShowTreeNode("");
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        public List<Treenode> mloadAddStockItem(string vstrRoot)
        {
            string strSQL;
            SqlDataReader drGetGroup;

            List<Treenode> oogrp = new List<Treenode>();
            strSQL = "SELECT STOCKITEM_NAME,STOCKITEM_ALIAS FROM INV_STOCKITEM ";
            strSQL = strSQL + "WHERE STOCKGROUP_NAME = '" + vstrRoot + "' ";

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                while (drGetGroup.Read())
                {
                    Treenode ogrp = new Treenode();
                    ogrp.id = drGetGroup["STOCKITEM_NAME"].ToString();
                    ogrp.text = drGetGroup["STOCKITEM_NAME"].ToString();
                    ogrp.type = "second";
                    oogrp.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                return oogrp;

            }
        }
        public List<Treenode> ShowTreeNode(String Parent)
        {

            List<Treenode> TreeList = new List<Treenode>();
            string strSQL;
            SqlDataReader drGetGroup;
            List<Treenode> oogrp = new List<Treenode>();
            string connectionString = WebConfigurationManager.ConnectionStrings["gcnmain"].ConnectionString;
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                if (Parent == "")
                {
                    strSQL = "SELECT STOCKGROUP_NAME FROM INV_STOCKGROUP ";
                    strSQL = strSQL + "WHERE STOCKGROUP_LEVEL = 1";
                    strSQL = strSQL + "ORDER BY STOCKGROUP_SERIAL ";
                }
                else
                {
                    strSQL = "SELECT STOCKGROUP_NAME FROM INV_STOCKGROUP ";
                    strSQL = strSQL + " WHERE STOCKGROUP_PARENT = '" + Parent + "'";
                    strSQL = strSQL + " and STOCKGROUP_LEVEL <> 1 ";
                    strSQL = strSQL + " ORDER BY STOCKGROUP_SERIAL";

                }
                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                while (drGetGroup.Read())
                {
                    Treenode ogrp = new Treenode();
                    ogrp.id = drGetGroup["STOCKGROUP_NAME"].ToString();
                    ogrp.text = drGetGroup["STOCKGROUP_NAME"].ToString();
                    ogrp.type = "root";
                    TreeList.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                if (TreeList.Count > 0)
                {
                    for (int i = 0; i < TreeList.Count; i++)
                    {
                        TreeList[i].children = ShowTreeNode(TreeList[i].text);
                    }
                }

                else
                {
                    if (Parent != "")
                    {
                        TreeList = mloadAddStockItem(Parent);
                    }
                }

                return TreeList;

            }


        }
        #endregion
        #region Costcatagory
        public ActionResult CostcategoryTree()
        {
            return View();
        }
        public JsonResult GetJsTree3Datacategory()
        {
            List<Treenode> list = ShowCatagorynode("");
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        public List<Treenode> ShowCatagorynode(String Parent)
        {

            List<Treenode> TreeList = new List<Treenode>();
            string strSQL;
            SqlDataReader drGetGroup;
            List<Treenode> oogrp = new List<Treenode>();
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                //if (Parent == "")
                //{
                //    strSQL = "SELECT STOCKGROUP_NAME FROM INV_STOCKGROUP ";
                //    strSQL = strSQL + "WHERE STOCKGROUP_LEVEL = 1";
                //    strSQL = strSQL + "ORDER BY STOCKGROUP_SERIAL ";
                //}
                //else
                //{
                //    strSQL = "SELECT STOCKGROUP_NAME FROM INV_STOCKGROUP ";
                //    strSQL = strSQL + " WHERE STOCKGROUP_PARENT = '" + Parent + "'";
                //    strSQL = strSQL + " and STOCKGROUP_LEVEL <> 1 ";
                //    strSQL = strSQL + " ORDER BY STOCKGROUP_SERIAL";

                //}
                strSQL = "SELECT VECTOR_CATEGORY_SERIAL,VECTOR_CATEGORY_NAME FROM VECTOR_CATEGORY ORDER BY VECTOR_CATEGORY_NAME ";
                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                while (drGetGroup.Read())
                {
                    Treenode ogrp = new Treenode();
                    ogrp.id = drGetGroup["VECTOR_CATEGORY_SERIAL"].ToString();
                    ogrp.text = drGetGroup["VECTOR_CATEGORY_NAME"].ToString();
                    ogrp.type = "root";
                    TreeList.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                if (TreeList.Count > 0)
                {
                    for (int i = 0; i < TreeList.Count; i++)
                    {
                        //TreeList[i].children = ShowTreeNode(TreeList[i].text);
                        TreeList[i].children = mAddItemCostCenter(TreeList[i].text);
                    }
                }

                else
                {
                    if (Parent != "")
                    {
                        TreeList = mAddItemCostCenter(Parent);
                    }
                }

                return TreeList;

            }


        }

        public List<Treenode> mAddItemCostCenter(string vstrRoot)
        {
            string strSQL;
            SqlDataReader drGetGroup;

            List<Treenode> oogrp = new List<Treenode>();
            string connectionString = WebConfigurationManager.ConnectionStrings["gcnmain"].ConnectionString;
            strSQL = "SELECT VMASTER_NAME FROM VECTOR_MASTER ";
            strSQL = strSQL + "WHERE VECTOR_CATEGORY_NAME = '" + vstrRoot + "' ";

            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();

                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                while (drGetGroup.Read())
                {
                    Treenode ogrp = new Treenode();
                    //ogrp.id = drGetGroup["STOCKITEM_NAME"].ToString();
                    ogrp.text = drGetGroup["VMASTER_NAME"].ToString();
                    ogrp.type = "second";
                    oogrp.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                return oogrp;

            }
        }
        #endregion
        #region "Accounts"
        public ActionResult AccountsTree()
        {
           
            return View();
        }
        public JsonResult GetJsTree3AccountsData()
        {
          
            List<Treenode> list = ShowTreeNodeAccounts("");
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        public List<Treenode> ShowTreeNodeAccounts(String Parent)
        {

            List<Treenode> TreeList = new List<Treenode>();
          
    
            string strSQL;
            
            SqlDataReader drGetGroup;
            List<Treenode> oogrp = new List<Treenode>();
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                gcnMain.Open();
                if (Parent == "")
                {
                    strSQL = "SELECT 'Assets' GR_NAME ";
                    strSQL = strSQL + "union all ";
                    strSQL = strSQL + "SELECT 'Liabilities' GR_NAME ";
                    strSQL = strSQL + "union all ";
                    strSQL = strSQL + "SELECT 'Income' GR_NAME ";
                    strSQL = strSQL + "union all ";
                    strSQL = strSQL + "SELECT 'Expenses'  GR_NAME ";
                }
                else
                {
                    strSQL = "SELECT GR_NAME FROM ACC_LEDGERGROUP ";
                    strSQL = strSQL + " WHERE GR_PARENT = '" + Parent + "'";
                    strSQL = strSQL + "ORDER BY GR_LEVEL,GR_SERIAL ";

                }
                SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                drGetGroup = cmd.ExecuteReader();
                while (drGetGroup.Read())
                {
                    Treenode ogrp = new Treenode();
                    ogrp.id = drGetGroup["GR_NAME"].ToString();
                    ogrp.text = drGetGroup["GR_NAME"].ToString();
                    ogrp.type = "root";
                    TreeList.Add(ogrp);
                }
                drGetGroup.Close();
                gcnMain.Dispose();
                if (TreeList.Count > 0)
                {
                   
                    for (int i = 0; i < TreeList.Count; i++)
                    {
                        TreeList[i].children = ShowTreeNodeAccounts(TreeList[i].text);
                        
                    }
                }

                else
                {
                    if (Parent != "")
                    {
                        TreeList = mLoadLedgerTreeNode(Parent);
                    }
                }
                return TreeList;

            }


        }
      
        public List<Treenode> mLoadLedgerTreeNode(string vstrRoot)
        {
            string strSQL = "";
            SqlDataReader rsget;
            List<Treenode> ooLedger = new List<Treenode>();
            string connectionString = WebConfigurationManager.ConnectionStrings["gcnmain"].ConnectionString;
            using (SqlConnection gcnMain = new SqlConnection(connectionString))
            {
                if (gcnMain.State == System.Data.ConnectionState.Open)
                {
                    gcnMain.Close();
                }
                try
                {
                    gcnMain.Open();
                    strSQL = "SELECT LEDGER_NAME FROM ACC_LEDGER ";
                    strSQL = strSQL + "WHERE LEDGER_PARENT_GROUP = '" + vstrRoot.Replace("'", "''") + "' ";
                    strSQL = strSQL + "AND LEDGER_STATUS = 0 ";
                    strSQL = strSQL + "AND LEDGER_GROUP <> 204 ";
                    SqlCommand cmd = new SqlCommand(strSQL, gcnMain);
                    rsget = cmd.ExecuteReader();
                    while (rsget.Read())
                    {
                        Treenode oLedger = new Treenode();
                        oLedger.id = rsget["LEDGER_NAME"].ToString();
                        oLedger.text = rsget["LEDGER_NAME"].ToString();
                        oLedger.type = "second";
                        ooLedger.Add(oLedger);

                    }
                    rsget.Close();
                    gcnMain.Dispose();
                    return ooLedger;
                }
                catch (Exception ex)
                {
                    return ooLedger;
                }
            }
        }
        #endregion


        public JsonResult ExportsExcelAccounts()
        {
           
            #region "Export"
            int introw = 1;
            string strname4 = "", strname3 = "", strname5 = "", strname6 = "", strname7 = "", strname8 = "", strname9 = "",
                   strname12 = "", strname13 = "", strname14 = "";

            DataTable dt = new DataTable();
            DataRow dr;
            for (int intcol = 1; intcol < 100; intcol++)
            {
                dt.Columns.Add(intcol.ToString());
            }
            var list = ShowTreeNodeAccounts("");

            for (int introw1 = 0; introw1 < list.Count; introw1++)
            {
                string strname = list[introw1].id.ToString();
                dr = dt.NewRow();
                dr[introw] = strname;
                dt.Rows.Add(dr);
                for (int introw2 = 0; introw2 < list[introw1].children.Count; introw2++)
                {
                    string strname1 = list[introw1].children[introw2].id.ToString();
                    dr = dt.NewRow();
                    dr[introw + 1] = strname1;
                    dt.Rows.Add(dr);
                    try
                    {
                        for (int introw3 = 0; introw3 <= list[introw2].children[introw3].children.Count; introw3++)
                        {
                            try
                            {
                                string strname2 = list[introw1].children[introw2].children[introw3].id.ToString();
                                dr = dt.NewRow();
                                dr[introw + 2] = strname2;
                                dt.Rows.Add(dr);
                                for (int introw4 = 0; introw4 <= list[introw1].children[introw2].children[introw3].children.Count; introw4++)
                                {
                                    if (strname3 != list[introw1].children[introw2].children[introw3].children[introw4].id.ToString())
                                    {
                                        strname3 = list[introw1].children[introw2].children[introw3].children[introw4].id.ToString();
                                        dr = dt.NewRow();
                                        dr[introw + 3] = strname3;
                                        dt.Rows.Add(dr);
                                    }
                                    try
                                    {
                                        for (int introw5 = 0; introw5 <= list[introw1].children[introw2].children[introw3].children[introw5].children.Count; introw5++)
                                        {
                                            try
                                            {

                                                if (strname4 != list[introw1].children[introw2].children[introw3].children[introw4].id.ToString())
                                                {
                                                    strname4 = list[introw1].children[introw2].children[introw3].children[introw4].id.ToString();
                                                    dr = dt.NewRow();
                                                    dr[introw + 3] = strname4;
                                                    dt.Rows.Add(dr);
                                                }

                                                for (int introw6 = 0; introw6 <= list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children.Count; introw6++)
                                                {

                                                    if (strname5 != list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].id.ToString())
                                                    {
                                                        strname5 = list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].id.ToString();
                                                        dr = dt.NewRow();
                                                        dr[introw + 4] = strname5;
                                                        dt.Rows.Add(dr);
                                                    }
                                                    for (int introw7 = 0; introw7 <= list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children.Count; introw7++)
                                                    {

                                                        if (strname6 != list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].id.ToString())
                                                        {
                                                            strname6 = list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].id.ToString();
                                                            dr = dt.NewRow();
                                                            dr[introw + 5] = strname6;
                                                            dt.Rows.Add(dr);
                                                        }
                                                        for (int introw8 = 0; introw8 <= list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children.Count; introw8++)
                                                        {

                                                            if (strname7 != list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].id.ToString())
                                                            {
                                                                strname7 = list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].id.ToString();
                                                                dr = dt.NewRow();
                                                                dr[introw + 6] = strname7;
                                                                dt.Rows.Add(dr);
                                                            }
                                                            for (int introw9 = 0; introw9 <= list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children[introw8].children.Count; introw9++)
                                                            {

                                                                if (strname8 != list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children[introw8].id.ToString())
                                                                {
                                                                    strname8 = list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children[introw8].id.ToString();
                                                                    dr = dt.NewRow();
                                                                    dr[introw + 7] = strname8;
                                                                    dt.Rows.Add(dr);
                                                                }
                                                                for (int introw10 = 0; introw10 <= list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children[introw8].children[introw9].children.Count; introw9++)
                                                                {

                                                                    if (strname9 != list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children[introw8].children[introw9].id.ToString())
                                                                    {
                                                                        strname9 = list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children[introw8].children[introw9].id.ToString();
                                                                        dr = dt.NewRow();
                                                                        dr[introw + 8] = strname9;
                                                                        dt.Rows.Add(dr);
                                                                    }
                                                                    for (int introw11 = 0; introw11 <= list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children[introw8].children[introw9].children[introw10].children.Count; introw11++)
                                                                    {

                                                                        if (strname12 != list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children[introw8].children[introw9].children[introw10].id.ToString())
                                                                        {
                                                                            strname12 = list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children[introw8].children[introw9].children[introw10].id.ToString();
                                                                            dr = dt.NewRow();
                                                                            dr[introw + 9] = strname12;
                                                                            dt.Rows.Add(dr);
                                                                        }
                                                                        for (int introw12 = 0; introw12 <= list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children[introw8].children[introw9].children[introw10].children[introw11].children.Count; introw12++)
                                                                        {

                                                                            if (strname13 != list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children[introw8].children[introw9].children[introw10].children[introw11].id.ToString())
                                                                            {
                                                                                strname13 = list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children[introw8].children[introw9].children[introw10].children[introw11].id.ToString();
                                                                                dr = dt.NewRow();
                                                                                dr[introw + 10] = strname13;
                                                                                dt.Rows.Add(dr);
                                                                            }
                                                                            for (int introw13 = 0; introw13 <= list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children[introw8].children[introw9].children[introw10].children[introw11].children[introw12].children.Count; introw13++)
                                                                            {

                                                                                if (strname14 != list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children[introw8].children[introw9].children[introw10].children[introw11].children[introw12].id.ToString())
                                                                                {
                                                                                    strname14 = list[introw1].children[introw2].children[introw3].children[introw4].children[introw5].children[introw6].children[introw7].children[introw8].children[introw9].children[introw10].children[introw11].children[introw12].id.ToString();
                                                                                    dr = dt.NewRow();
                                                                                    dr[introw + 11] = strname14;
                                                                                    dt.Rows.Add(dr);
                                                                                }

                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }

                                                }

                                            }
                                            catch (Exception)
                                            {
                                                continue;
                                            }

                                        }
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }

                                }
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }

                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }

            DataTable dt1 = dt;

            Excel.ExcelUtlity objexcel = new Excel.ExcelUtlity();
            ////string path = Environment.SpecialFolder.UserProfile + @"\Downloads\ChartofAccounts.xlsx";
            objexcel.WriteDataTableToExcel(dt, "Chart of Accounts", "D:\\ChartofAccounts.xlsx", "Details");
            //objexcel.WriteDataTableToExcel(dt, "Person Details", path, "Details");
            #endregion

            return new JsonResult() { Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
           
        }

      
        public ActionResult NewPage()
        {
            return View();
        }
       
    }
}