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
            { return Students.OrderBy(x => x.AverageGrade).ToList(); }
        }

        public RankedGradeBook(string name) : base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            if (averageGrade >= SortedStudents[NoToDropAGrade].AverageGrade)
            { return 'A'; }
            else if (averageGrade >= SortedStudents[2 * NoToDropAGrade].AverageGrade)
            { return 'B'; }
            else if (averageGrade >= SortedStudents[3 * NoToDropAGrade].AverageGrade)
            { return 'C'; }
            else if (averageGrade >= SortedStudents[4 * NoToDropAGrade].AverageGrade)
            { return 'D'; }
            else            
                return 'F';
        }
    }
}
