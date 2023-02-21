using Demo20230215.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo20230215.Services
{
    public class EmpMethod
    {
        /// <summary>
        /// 取得員工列表
        /// </summary>
        /// <returns>List of employees</returns>
        public List<EmployeeModel> GetEmployeeModels()
        {
            List<EmployeeModel> employeeModels = new List<EmployeeModel>();
            employeeModels.Add(new EmployeeModel() { EmpNo = "FG4453", Name = "范家華", Ext = 0000 });
            employeeModels.Add(new EmployeeModel() { EmpNo = "FG4503", Name = "姚柏宏", Ext = 7891 });
            employeeModels.Add(new EmployeeModel() { EmpNo = "FG4487", Name = "王有盡", Ext = 1234 });
            

            return employeeModels;
        }
    }
}