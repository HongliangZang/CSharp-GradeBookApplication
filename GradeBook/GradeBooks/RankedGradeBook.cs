using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public int NoToDropAGrade {
            get
            {
                return Students.Count / 5;
            }
        }

        public List<Student> SortedStudents {
            get
            { return Students.OrderByDescending(x => x.AverageGrade).ToList(); }
        }

        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            if (averageGrade >= SortedStudents[NoToDropAGrade-1].AverageGrade)
            { return 'A'; }
            else if (averageGrade >= SortedStudents[2 * NoToDropAGrade -1].AverageGrade)
            { return 'B'; }
            else if (averageGrade >= SortedStudents[3 * NoToDropAGrade -1].AverageGrade)
            { return 'C'; }
            else if (averageGrade >= SortedStudents[4 * NoToDropAGrade - 1].AverageGrade)
            { return 'D'; }
            else            
                return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }

    }
}
