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
        public void AddEmpDetails()
        {
            SqlConnection sqlConnect = new SqlConnection(connectionString);
            try
            {
                using (sqlConnect)
                {
                    sqlConnect.Open();
                    SqlCommand cmd = new SqlCommand("AddEmployeeDetails", sqlConnect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    Console.WriteLine("Enter Company Name"); emp.CompanyName = Console.ReadLine();
                    Console.Write("Enter EmpName : "); emp.EmpName = Console.ReadLine();
                    Console.Write("Enter Gender : "); emp.Gender = Console.ReadLine();
                    Console.Write("Enter Phone : "); emp.PhoneNumber = Int64.Parse(Console.ReadLine());
                    Console.Write("Enter EmpAddress : "); emp.EmpAddress = Console.ReadLine();
                    Console.Write("Enter Department : "); emp.Department = Console.ReadLine();
                    Console.Write("Enter StartDate yyyy-mm-dd : "); emp.StartDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Enter BasicPay : "); emp.BasicPay = int.Parse(Console.ReadLine());
                    Console.Write("Enter Deductions : "); emp.Deductions = int.Parse(Console.ReadLine());
                    Console.Write("Enter IncomeTax : "); emp.IncomeTax = int.Parse(Console.ReadLine());
                    emp.TaxablePay = emp.BasicPay - emp.Deductions;
                    emp.NetPay = emp.TaxablePay - emp.IncomeTax;

                    cmd.Parameters.AddWithValue("@company", emp.CompanyName);
                    cmd.Parameters.AddWithValue("@FullName", emp.EmpName);
                    cmd.Parameters.AddWithValue("@gender", emp.Gender);
                    cmd.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", emp.EmpAddress);
                    cmd.Parameters.AddWithValue("@Date", emp.StartDate);
                    cmd.Parameters.AddWithValue("@Department", emp.Department);
                    cmd.Parameters.AddWithValue("@basicPay", emp.BasicPay);
                    cmd.Parameters.AddWithValue("@Taxablepay", emp.TaxablePay);
                    cmd.Parameters.AddWithValue("@deductions", emp.Deductions);
                    cmd.Parameters.AddWithValue("@IncomeTax", emp.IncomeTax);
                    cmd.Parameters.AddWithValue("@netPay", emp.NetPay);

                    int affRows = cmd.ExecuteNonQuery();
                    if (affRows >= 1)
                    {
                        Console.WriteLine("Employee added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Employee not added..");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnect.Close();
            }
        }
        public void UpdateBasicPay()
        {
            Console.Write("Enter Employee Name to update: ");
            string empName = Console.ReadLine();
            Console.Write("Enter Employee basic salary to update: ");
            float basic = float.Parse(Console.ReadLine());
            Console.Write("Enter Employee deductions to update: ");
            float deductions = float.Parse(Console.ReadLine());
            Console.Write("Enter Employee incometax  to update: ");
            float incometax = float.Parse(Console.ReadLine());

            SqlConnection sqlConnect = new SqlConnection(connectionString);
            try
            {
                using (sqlConnect)
                {
                    sqlConnect.Open();
                    SqlCommand cmd = new SqlCommand("updateEmployee", sqlConnect);

                    cmd.CommandType = CommandType.StoredProcedure;
                    emp.EmpName = empName;
                    emp.BasicPay = basic;
                    emp.Deductions = deductions;
                    emp.IncomeTax = incometax;
                    cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                    cmd.Parameters.AddWithValue("@BasicPay", emp.BasicPay);
                    cmd.Parameters.AddWithValue("@deductions", emp.Deductions);
                    cmd.Parameters.AddWithValue("@incometax", emp.IncomeTax);


                    int affRows = cmd.ExecuteNonQuery();

                    if (affRows >= 1)
                    { Console.WriteLine(" Employee pay  details Updated.."); }
                    else
                    { Console.WriteLine(" Employee pay not Updated..."); }
                }
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }

            finally
            {
                sqlConnect.Close();
            }
        }
        public void DeleteEmployeeRecord()
        {
            SqlConnection sqlConnect = new SqlConnection(connectionString);
            try
            {
                using (sqlConnect)
                {
                    sqlConnect.Open();
                    SqlCommand cmd = new SqlCommand("DeleteEmployeeDetails", sqlConnect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    Console.Write("Enter employee full name to delete record");
                    emp.EmpName = Console.ReadLine();
                    cmd.Parameters.AddWithValue("@FullName", emp.EmpName);
                    int affRows = cmd.ExecuteNonQuery();

                    if (affRows >= 1)
                    { Console.WriteLine("Employee details Removed successfully."); }
                    else
                    { Console.WriteLine("Employee not Removed.."); }
                }
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
            finally
            {
                sqlConnect.Close();
            }
        }
    }
}
