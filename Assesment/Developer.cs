using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Assesment
{
    class Developer
    {
        public int Id { get; set; }
        public string DeveloperName { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Project_Assigned { get; set; }

        public Developer()
        {
            Console.Write("Id: ");
            Id = int.Parse(Console.ReadLine());

            Console.Write("Name: ");
            DeveloperName = Console.ReadLine();

            Console.Write("Joining Date (yyyy-mm-dd): ");
            JoiningDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Project Assigned: ");
            Project_Assigned = Console.ReadLine();
        }

        public virtual void DisplayDetails()
        {
            Console.WriteLine($"Developer Id: {Id}");
            Console.WriteLine($"name: {DeveloperName}");
            Console.WriteLine($"joining date: {JoiningDate}");
            Console.WriteLine($"project assigned: {Project_Assigned}");
        }
    }

    class OnContract : Developer
    {
        public int Duration { get; set; }
        public int ChargePerDay { get; set; }
        public int TotalSalary { get; set; }

        public OnContract() : base()
        {

            Console.Write("Duration (in days): ");
             Duration = int.Parse(Console.ReadLine());

            Console.Write("Charges Per Day: ");
             ChargePerDay = int.Parse(Console.ReadLine());

            CalculateContractTotalCharges();
        }
        public void CalculateContractTotalCharges()
        {
            TotalSalary = Duration * ChargePerDay;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Duration: {Duration} days");
            Console.WriteLine($"Charges Per Day: {ChargePerDay}");
            Console.WriteLine($"Total Salary: {CalculateContractTotalCharges}");
            Console.WriteLine("--------------");
        }

    }

    class OnPayroll : Developer
    {

        public string Dept { get; set; }
        public string Manager { get; set; }
        public int BaseSalary { get; set; }
        public float Exp { get; set; }

        int da { get; set; }
        int pf { get; set; }
        int lta { get; set; }
        int hra { get; set; }
        int totalSalary { get; set; }

        public OnPayroll() : base()
        {
            Console.Write("Department: ");
            Dept = Console.ReadLine();

            Console.Write("Manager: ");
            Manager = Console.ReadLine();

            Console.Write("Net Salary: ");
            BaseSalary = int.Parse(Console.ReadLine());

            Console.Write("Experience: ");
            Exp = float.Parse(Console.ReadLine());

            Totalsalary();
        }
        public void Totalsalary()
        {
            da = (int)(0.1 * BaseSalary);
            hra = (int)(0.2 * BaseSalary);
            lta = (int)(0.1 * BaseSalary);
            pf = (int)(0.12 * BaseSalary);
            totalSalary = BaseSalary + da + hra + lta - pf;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"Department: {this.Dept}");
            Console.WriteLine($"manager: {Manager}");
            Console.WriteLine($"net salary: {BaseSalary}");
            Console.WriteLine($"experience: {Exp} years");
            Console.WriteLine($"total salary: {Totalsalary}");
            Console.WriteLine("--------------");
        }
    }

    class Program
    {
        static List<Developer> developers = new List<Developer>();
        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("1. Create Developer");
                Console.WriteLine("2. Exit");

                Console.WriteLine("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateDeveloper();
                        break;
                    case "2":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void CreateDeveloper()
        {
            Console.Write("Type (1 - OnContract, 2 - OnPayroll): ");
            int type = int.Parse(Console.ReadLine());

            if (type == 1)
            {
                OnContract developer = new OnContract();
                developers.Add(developer);
            }

            else if (type == 2)
            {
                OnPayroll developer = new OnPayroll();
                developers.Add(developer);
            }
            else
            {
                Console.WriteLine("Invalid developer type. Please try again.");
            }

            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Display all records");
            Console.WriteLine("2. Display records where net salary is more than 20000");
            Console.WriteLine("3. Display records where DeveloperName contains 'D'");
            Console.WriteLine("4. Display records where JoiningDate is in between 01/01/2020 and 01/01/2022");
            Console.WriteLine("5. Display records where Joining Date was 12 Jan 2022");
            Console.WriteLine("6. Exit");

            Console.Write("\nEnter your choice (1-6): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllRecords();
                    break;
                case "2":
                    DisplayHighSalaryRecords();
                    break;
                case "3":
                    DisplayNameContainsDRecords();
                    break;
                case "4":
                    DisplayDateRangeRecords();
                    break;
                case "5":
                    DisplaySpecificDateRecords();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                    break;
            }

            static void DisplayAllRecords()
            {
                var onPayrollDevelopersList = developers.OfType<OnPayroll>().ToList();

                foreach(OnPayroll dev in onPayrollDevelopersList)
                {
                    dev.DisplayDetails();
                }

                //var devP1 = from emp in onPayrollDevelopersList
                //select emp;
                //foreach (var devep1 in devP1)
                //{
                //    devep1.DisplayDetails();
                //}

                var onContractDevelopersList = developers.OfType<OnContract>().ToList();

                foreach (OnContract dev in onContractDevelopersList)
                {
                    dev.DisplayDetails();
                }

                //var devC1 = from emp in onContractDevelopersList
                //select emp;
                //foreach (var devep1 in devC1)
                //{
                //    devep1.DisplayDetails();
                //}
            }

            static void DisplayHighSalaryRecords()
            {
                var onPayrollDevelopersList = developers.OfType<OnPayroll>().ToList();

                var devP2 = from dd in onPayrollDevelopersList
                           where dd.BaseSalary > 20000
                           select dd;
                foreach (var developer in devP2)
                {
                    developer.DisplayDetails();
                }


            }

            static void DisplayNameContainsDRecords()
            {
                var listContainiungNameD = developers.Where(x => x.DeveloperName.Contains('D')).ToList();
                if (listContainiungNameD.Count == 0)
                {
                    Console.WriteLine("No Name");
                }
                else
                    foreach (var item in listContainiungNameD)
                    {
                        item.DisplayDetails();
                    }
            }

            static void DisplayDateRangeRecords()
            {
                var onPayrollDevelopersList = developers.OfType<OnPayroll>().ToList();

                var devs3 = from record in onPayrollDevelopersList
                            where record.JoiningDate >= new DateTime(2020, 1, 1) && record.JoiningDate < new DateTime(2022, 1, 1)
                            select record;
                foreach (var dd4 in devs3)
                {
                    dd4.DisplayDetails();
                }
            }

            static void DisplaySpecificDateRecords()
            {
                var onContractDevelopersList = developers.OfType<OnContract>().ToList();

                var devs4 = from record1 in onContractDevelopersList
                             where record1.JoiningDate >= new DateTime(2020, 1, 1) && record1.JoiningDate < new DateTime(2022, 1, 1)
                             select record1;
                foreach (var dd4 in devs4)
                {
                    dd4.DisplayDetails();
                }
            }
        }
    }
}
