using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            }
            base.CalculateStudentStatistics(name);
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (this.Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            var grades = (from e in Students
                          orderby e.AverageGrade descending
                          select e.AverageGrade).ToList();

            var smallerGrades = (from e in Students
                                 orderby e.AverageGrade descending
                                 where averageGrade <= e.AverageGrade
                                 select e.AverageGrade).Count();

            var gradeArray = new char[] { 'A', 'B', 'C', 'D', 'F' };

            var intgrade = (int)Math.Ceiling(Students.Count / (double)gradeArray.Length);

            return gradeArray[smallerGrades * intgrade - 1];
        }
    }
}
