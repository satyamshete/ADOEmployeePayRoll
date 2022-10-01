namespace ADOEmployeePayRoll
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Payroll Service program using ADO.NET \n");
            EmployeeADO emp = new EmployeeADO();
            OptionsDisplay();

            void OptionsDisplay()
            {
                Console.Write("\nSelect option :\n1.Get Payroll Table Details\n2.Add Emloyee Details\n3.Update Pay details\n");
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
                    default:
                        Console.WriteLine("Enter correct option");
                        break;
                }
            }
        }
    }
}