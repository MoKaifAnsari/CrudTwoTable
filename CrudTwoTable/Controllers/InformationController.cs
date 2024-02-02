using CrudTwoTable.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CrudTwoTable.Controllers
{
    public class InformationController : Controller
    {
        // GET: Information
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InsertUpdateStudentDetail
            (string sid, string fname, string lname, string username, string cid, string city, string state, string zip)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            dic["Focus"] = "";
            dic["Status"] = "";
            try
            {
                if (fname.Trim() == "")
                {
                    dic["Message"] = "Please Enter Your First Name";
                    dic["Foucs"] = "txtfname";
                }
                else if (lname.Trim() == "")
                {
                    dic["Message"] = "Please Enter Your Second Name";
                    dic["Foucs"] = "txtlname";
                }
                else if (username.Trim() == "")
                {
                    dic["Message"] = "Please Enter Your UserName";
                    dic["Foucs"] = "txtusername";
                }
                else if (city.Trim() == "")
                {
                    dic["Message"] = "Please Enter Your City Name";
                    dic["Foucs"] = "txtcity";
                }
                else if (state.Trim() == "")
                {
                    dic["Message"] = "Please Enter Your State Name";
                    dic["Foucs"] = "txtstate";
                }
                else if (zip.Trim() == "")
                {
                    dic["Message"] = "Please Enter Your Zip Code";
                    dic["Foucs"] = "txtzip";
                }
                else
                {
                    string[,] param = new string[,]
                    {
                        {"@sid",sid },
                        {"@fname",fname },
                        {"@lname",lname },
                        {"@username",username },
                        {"@city",city },
                        {"@state",state },
                        {"@zip",zip }
                    };
                    DataTable dt = DBManager.ExcuteProcedure("sp_InsertUpdateStuDetail", param);
                    if (dt.Rows.Count > 0)
                    {
                        dic["Message"] = dt.Rows[0]["Msg"].ToString();
                        dic["Focus"] = dt.Rows[0]["Focus"].ToString();
                        dic["Status"] = dt.Rows[0]["Status"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                dic["Message"] = ex.Message;
            }
            return Json(dic);
        }
        public ActionResult ShowStudet()
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            dic["Grid"] = "";
            try
            {
                string[,] param = new string[,]
                {
                    {"@sid","0" },
                    {"@cid","0" },
                    {"@Type","S" }
                };
                DataTable dt = DBManager.ExcuteProcedure("sp_ShowStudentDetain", param);
                if (dt.Rows.Count > 0)
                {
                    sb.Append("<table style='padding:20px;text-align:center;'border='1' id='tbl' class='table-bordered table table-striped table-responsive'><tr>");
                    sb.Append("<th class='table-dark'>Action</th>");
                    sb.Append("<th class='table-dark'>Full-Name</th>");
                    sb.Append("<th class='table-dark'>User-Name</th>");
                    sb.Append("<th class='table-dark'>City</th>");
                    sb.Append("<th class='table-dark'>State</th>");
                    sb.Append("<th class='table-dark'>Zip</th></tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr><td><button type='button' onclick='DeleteStuDetail(" + dt.Rows[i]["sid"] + ")' class='btn btn-danger'><b>Delete</b></button>" + " " + "<button type='button' onclick='EditStuDetail(" + dt.Rows[i]["sid"] + ")'  class='btn btn-success'><b>Edit</b></button></td>");
                        sb.Append("<td>" + dt.Rows[i]["fullname"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["username"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["city"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["state"].ToString() + "</td>");
                        sb.Append("<td>" + dt.Rows[i]["zip"].ToString() + "</td></tr>");
                    }
                    sb.Append("</table>");
                    dic["Grid"] = sb.ToString();
                }
            }
            catch (Exception ex)
            {
                dic["Message"] = ex.Message;
            }
            return Json(dic);
        }
        public ActionResult EditStudentDetail(string sid)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            dic["Status"] = "";
            try
            {
                string[,] param = new string[,]
                {
                    {"@sid",sid.Trim() },
                    {"@Type","E" }
                };
                DataTable dt = DBManager.ExcuteProcedure("sp_ShowStudentDetain", param);
                if (dt.Rows.Count > 0)
                {
                    dic["sid"] = dt.Rows[0]["sid"].ToString();
                    dic["fname"] = dt.Rows[0]["fname"].ToString();
                    dic["lname"] = dt.Rows[0]["lname"].ToString();
                    dic["username"] = dt.Rows[0]["username"].ToString();
                    dic["cid"] = dt.Rows[0]["cid"].ToString();
                    dic["city"] = dt.Rows[0]["city"].ToString();
                    dic["state"] = dt.Rows[0]["state"].ToString();
                    dic["zip"] = dt.Rows[0]["zip"].ToString();
                    dic["Status"] = "1";
                }
            }
            catch (Exception ex)
            {
                dic["Message"] = ex.Message;
            }
            return Json(dic);
        }
        public ActionResult DeleteStudent(string sid)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            try
            {
                string[,] param = new string[,]
                 {
                    {"@sid", sid.ToString()},
                 };
                DataTable dt = DBManager.ExcuteProcedure("sp_DeleteStuDetail", param);
                if (dt.Rows.Count > 0)
                {
                    dic["Message"] = dt.Rows[0]["Msg"].ToString();
                }
            }
            catch (Exception ex)
            {
                dic["Message"] = ex.Message;
            }

            return Json(dic);
        }
    }
}