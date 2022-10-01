using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOEmployeePayRoll
{
    internal class EmployeeADO
    {
        public static string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=EmployeePayRollServices;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        Employee emp = new Employee();

        public void GetEmployeeDetails()
        {
            SqlConnection sqlConnect = new SqlConnection(connectionString);
            try
            {
                using (sqlConnect)
                {
                    sqlConnect.Open();
                    SqlCommand cmd = new SqlCommand("GetAllDetails", sqlConnect);
                    cmd.CommandType = CommandType.StoredProcedure;


                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        Console.WriteLine("EmployeePayroll Table Contents : \n");
                        Console.WriteLine();
                        while (dr.Read())
                        {
                            emp.CompID = dr.GetInt32(0);
                            emp.CompanyName = dr.GetString(1);
                            emp.EmpId = dr.GetInt32(2);
                            emp.EmpName = dr.GetString(3);
                            emp.EmpAddress = dr.GetString(4);
                            emp.PhoneNumber = dr.GetInt64(5);
                            emp.StartDate = dr.GetDateTime(6);
                            emp.Gender = dr.GetString(7);
                            emp.BasicPay = dr.GetDouble(8);
                            emp.Deductions = dr.GetDouble(9);
                            emp.TaxablePay = dr.GetDouble(10);
                            emp.IncomeTax = dr.GetDouble(11);
                            emp.NetPay = dr.GetDouble(12);
                            emp.Department = dr.GetString(13);

                            Console.WriteLine("{0}  {1}   {2}   {3}   {4}    {5}   {6}   {7}   {8}   {9}   {10}   {11}   {12}   {13}", emp.CompID, emp.CompanyName, emp.EmpId, emp.EmpName, emp.EmpAddress, emp.PhoneNumber, emp.StartDate, emp.Gender, emp.BasicPay, emp.Deductions, emp.TaxablePay, emp.IncomeTax, emp.NetPay, emp.Department);
                        }
                    }
                    dr.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sqlConnect.Close();
            }
        }
    }
}
