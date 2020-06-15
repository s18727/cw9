using LinqCwiczenia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqCwiczenia
{
    public partial class Form1 : Form
    {
        public IEnumerable<Emp> Emps { get; set; }
        public IEnumerable<Dept> Depts { get; set; }

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts
            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;
            #endregion

            #region Load emps
            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;
            ResultsDataGridView.DataSource = Emps;

            #endregion

        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            LoadData();
        }

     

        
        private void Przyklad1Button_Click(object sender, EventArgs e)
        {
          
            var result = Emps.Where(emp => emp.Job == "Backend programmer").ToList();

            ResultsDataGridView.DataSource = result;
        }

        private void Przyklad2Button_Click(object sender, EventArgs e)
        {
            var res = (from emp in Emps
                       join dept in Depts on emp.Deptno equals dept.Deptno
                       where emp.Job == "Frontend programmer" && emp.Salary > 1000
                       orderby emp.Ename descending
                       select emp).ToList();
            
         
            var result = Emps
                .Where(emp => emp.Job == "Frontend programmer" && emp.Salary > 1000)
                .OrderByDescending(emp => emp.Ename)
                .ToList();
            
            ResultsDataGridView.DataSource = result;
        }

       
        private void Przyklad3Button_Click(object sender, EventArgs e)
        {
           
            
            var result = Emps.Max(emp => emp.Salary);
            
            WynikTextBox.Text = result + "";
        }

      
        private void Przyklad4Button_Click(object sender, EventArgs e)
        {
        
            var result = Emps
                .Where(emp => emp.Salary ==
                              Emps.Max(emp2 => emp2.Salary))
                .ToList();
            
            ResultsDataGridView.DataSource = result;
        }

       
        private void Przyklad5Button_Click(object sender, EventArgs e)
        {
            var result = Emps
                .Select(emp => new
                {
                    Nazwisko = emp.Ename,
                    Praca = emp.Job
                })
                .ToList();
            
            ResultsDataGridView.DataSource = result;
        }

    
        private void Przyklad6Button_Click(object sender, EventArgs e)
        {
            var result = Emps
                .Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (emp, dept) => new
                {
                    emp.Ename,
                    emp.Job,
                    dept.Dname
                })
                .ToList();
            
            ResultsDataGridView.DataSource = result;
        }

        private void Przyklad7Button_Click(object sender, EventArgs e)
        {
            var result = Emps
                .GroupBy(emp => emp.Job)
                .Select(emp => new
                {
                    Praca = emp.Key,
                    LiczbaPracownikow = emp.Count()
                })
                .ToList();
            
            ResultsDataGridView.DataSource = result;
        }

    
        private void Przyklad8Button_Click(object sender, EventArgs e)
        {
            
            if (Emps.Any(emp => emp.Job == "Backend programmer"))
            {
                WynikTextBox.Text = "Backend programmer istnieje";
            }
        }

   
        private void Przyklad9Button_Click(object sender, EventArgs e)
        {

            var result = Emps
                .Where(emp => emp.Job == "Frontend programmer")
                .OrderByDescending(emp => emp.HireDate)
                .FirstOrDefault();
            
            ResultsDataGridView.DataSource = new List<Emp> {result};
        }

     
        private void Przyklad10Button_Click(object sender, EventArgs e)
        {
            var result = Emps
                .Select(emp => new
                {
                    emp.Ename,
                    emp.Job,
                    emp.HireDate
                })
                .Union(Emps
                    .Select(temp => new
                    {
                        Ename = "Brak wartości",
                        Job = (string) null,
                        HireDate = (DateTime?) null
                    }))
                    .ToList();
            
            ResultsDataGridView.DataSource = result;
        }
        
     
        private void Przyklad11Button_Click(object sender, EventArgs e)
        {
            var result = Emps
                .Aggregate((emp, next) => emp.Salary > next.Salary ? emp : next);
            
            ResultsDataGridView.DataSource = new List<Emp> {result};
        }
     
        private void Przyklad12Button_Click(object sender, EventArgs e)
        {
            var result = Emps
                .SelectMany(emp => Depts, (emp, dept) => new
                {
                    emp,
                    dept
                })
                .ToList();
            ResultsDataGridView.DataSource = result;
        }
    }
}
