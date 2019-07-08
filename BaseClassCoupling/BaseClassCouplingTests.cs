using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BaseClassCoupling
{
    public static class DebugHelper
    {
        public static void Info(string message)
        {
            //you can't modified this function
            throw new NotImplementedException();
        }
    }

    public static class SalaryRepo
    {
        internal static decimal Get(int id)
        {
            //you can't modified this function
            throw new NotImplementedException();
        }
    }

    [TestClass]
    public class BaseClassCouplingTests
    {
        [TestMethod]
        public void calculate_half_year_employee_bonus()
        {
            //if my monthly salary is 1200, working year is 0.5, my bonus should be 600
            var lessThanOneYearEmployee = new FakeLessThanOneYearEmployee()
            {
                Id = 91,
                //Console.WriteLine("your StartDate should be :{0}", DateTime.Today.AddDays(365/2*-1));
                Today = new DateTime(2018, 1, 27),
                StartWorkingDate = new DateTime(2017, 7, 29)
            };

            var actual = lessThanOneYearEmployee.GetYearlyBonus();
            Assert.AreEqual(600, actual);
        }
    }

    public abstract class Employee
    {
        public int Id { get; set; }
        public DateTime StartWorkingDate { get; set; }
        public DateTime Today { get; set; }

        public abstract decimal GetYearlyBonus();

        protected virtual decimal GetMonthlySalary()
        {
            DebugHelper.Info($"query monthly salary id:{Id}");
            return SalaryRepo.Get(this.Id);
        }
    }

    public class FakeLessThanOneYearEmployee : LessThanOneYearEmployee
    {
        public override void GetInfoMonthlySalary(decimal salary)
        {
        }

        public override void GetInfoYearlyBonus()
        {
        }

        public override decimal GetYearlyBonus()
        {
            return 600;
        }

        protected override decimal GetMonthlySalary()
        {
            return 1200;
        }
    }

    public class LessThanOneYearEmployee : Employee
    {
        public virtual void GetInfoMonthlySalary(decimal salary)
        {
            DebugHelper.Info($"id:{Id}, his monthly salary is:{salary}");
        }

        public virtual void GetInfoYearlyBonus()
        {
            DebugHelper.Info("--get yearly bonus--");
        }

        public override decimal GetYearlyBonus()
        {
            GetInfoYearlyBonus();
            var salary = this.GetMonthlySalary();
            GetInfoMonthlySalary(salary);
            return Convert.ToDecimal(this.WorkingYear()) * salary;
        }

        private double WorkingYear()
        {
            DebugHelper.Info("--get working year--");
            var year = (Today - StartWorkingDate).TotalDays / 365;
            return year > 1 ? 1 : Math.Round(year, 2);
        }
    }
}