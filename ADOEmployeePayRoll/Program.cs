namespace ADOEmployeePayRoll
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Payroll Service program using ADO.NET \n");
            EmployeeADO emp = new EmployeeADO();
           
            List<Employee> list = new List<Employee>() { new Employee { CompanyName = "Amazon",EmpName = "Tarak Mehta",
               Gender = "M",PhoneNumber=5151558451,EmpAddress="fjksgf fjksf",StartDate=DateTime.Now,Department="Sales",
               BasicPay=120200,Deductions=3400,IncomeTax=5600},
               new Employee { CompanyName = "Amazon",EmpName = "Priyanka Butko",
               Gender = "F",PhoneNumber=4121185,EmpAddress="sdhdvh ffdw",StartDate=DateTime.Now,Department="Marketing",
               BasicPay=990000,Deductions=7800,IncomeTax=5600} };
            OptionsDisplay();

            void OptionsDisplay()
            {
                Console.Write("\nSelect option :\n1.Get Payroll Table Details\n2.Add Emloyee Details\n3.Update Pay details\n4. Delete Employee details\n5. Get Employee details by Date Range\n6. Add Multiple Employee\n");
                //Retrieve all employee payroll table details
                int option = int.Parse(Console.ReadLine());
                Console.WriteLine();
                switch (option)
                {
                    case 1:
                        emp.GetEmployeeDetails();
                        OptionsDisplay();
                        break;
                    case 2:
                        emp.AddEmpDetails();
                        OptionsDisplay();
                        break;
                    case 3:
                        emp.UpdateBasicPay();
                        OptionsDisplay();
                        break;
                    case 4:
                        emp.DeleteEmployeeRecord();
                        OptionsDisplay();
                        break;
                    case 5:
                        emp.GetRowsByDateRange();
                        OptionsDisplay();
                        break;
                    case 6:
                        emp.AddMultipleEmployees(list);
                        emp.AddMultipleEmployeesUsingThreads(list);
                        break;
                    default:
                        Console.WriteLine("Enter correct option");
                        break;
                }
            }
        }
    }
}