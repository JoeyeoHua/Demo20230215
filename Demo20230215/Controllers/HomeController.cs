using Demo20230215.Models;
using Demo20230215.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Demo20230215.Controllers
{
    public class HomeController : Controller
    {
        public PartialViewResult _EmpData(string EmpNo = "")
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            if (Session["EmpData"] == null)
            {
                EmpMethod empMethod = new EmpMethod();
                employees = empMethod.GetEmployeeModels();
                Session["EmpData"] = employees;
            }
            else
            {
                employees = Session["EmpData"] as List<EmployeeModel>;
                //or
                //employees = (List<EmployeeModel>)Session["EmpData"] ;


            }

            //如果是空的 就全部顯示
            if (string.IsNullOrEmpty(EmpNo))
            {
                return PartialView(employees);
            }

            //如果有東西 則過濾並回傳 相當於跑foreach if 
            //用法近似於SQL語法
            return PartialView(employees.Where(x => x.EmpNo == EmpNo).ToList());
            //even有底下這種寫法 (標準linQ語法)
            //return (from x in employees
            // where x.EmpNo==EmpNo
            //select x).ToList();
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Edit(string EmpNo)
        {
            //MessageInfo messageInfo = new MessageInfo() { IsSuccess=true,Message=""};
            if (string.IsNullOrEmpty(EmpNo))
            {
                return View(new EmployeeModel());
            }
            if (Session["EmpData"] == null)
            {
                return View(new EmployeeModel());

            }
            EmployeeModel result =
                ((List<EmployeeModel>)Session["EmpData"]).Where(x => x.EmpNo == EmpNo).FirstOrDefault();
            return View(result);
        }

        [HttpPost]
        public JsonResult EditPost(EmployeeModel empData)
        {

            MessageInfo messageInfo = new MessageInfo() { IsSuccess = true, Message = "" };
            //TODO data verify
            if (Session["EmpData"] == null)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Message = "編號為空 重新輸入";
                return Json(messageInfo);
            }
            List<EmployeeModel> result = ((List<EmployeeModel>)Session["EmpData"]);

            //以下這行是防止查詢與修改為不同的emp number
            if (result.Where(x => x.EmpNo == empData.EmpNo).Count() == 0)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Message = "編號為空 重新輸入";
                return Json(messageInfo);
            }

            var emp = result.Where(x => x.EmpNo == empData.EmpNo).FirstOrDefault();
            emp.Name = empData.Name;
            emp.Ext = empData.Ext;
            Session["EmpData"] = result;
            return Json(messageInfo);

        }


        [HttpPost]
        public JsonResult DeletePost(string EmpNo)
        {
            MessageInfo messageInfo = new MessageInfo() { IsSuccess = true, Message = "" };
            if (Session["EmpData"] == null)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Message = "編號為空 重新輸入";
                return Json(messageInfo);
            }
            List<EmployeeModel> empDatas = ((List<EmployeeModel>)Session["EmpData"]);
            //以下是檢查資料庫有無此筆資料
            if (empDatas.Where(x => x.EmpNo == EmpNo).Count() == 0)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Message = "編號為空 重新輸入";
                return Json(messageInfo);
            }

            empDatas.Remove(empDatas.Where(x => x.EmpNo == EmpNo).FirstOrDefault());
            Session["EmpData"] = empDatas;
            return Json(messageInfo);

        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddPost(EmployeeModel employee)
        {
            MessageInfo messageInfo = new MessageInfo() { IsSuccess = true, Message = "" };

            //TODO 先驗證資料是否符合格式

            //驗證是否在資料庫內 
            List<EmployeeModel> result = ((List<EmployeeModel>)Session["EmpData"]);
            if (result.Where(x => x.EmpNo == employee.EmpNo).Count() != 0)
            {
                messageInfo.IsSuccess = false;
                messageInfo.Message = "資料庫內已有資料";
                return Json(messageInfo);
            }

            result.Add(employee);
            Session["EmpData"] = result;
            return Json(messageInfo);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}