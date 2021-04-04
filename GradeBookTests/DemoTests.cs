using GradeBook.Enums;
using GradeBook.GradeBooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace GradeBookTests
{
    public class DemoTests
    {
        [Fact(DisplayName = "Demo Test 1")]
        public void DemoTest1()
        {
            var RankedType = TestHelpers.GetUserType("GradeBook.GradeBooks.RankedGradeBook");
            //assert if type exists
            Assert.True(RankedType != null, "`GradeBook.GradeBooks.RankedGradeBook` doesn't exist.");

            //assert is public
            Assert.True(RankedType.IsPublic, "`GradeBook.GradeBooks.RankedGradeBook` doesn't set to Public.");

            //Assert is base type 
            Assert.True(RankedType.BaseType == typeof(BaseGradeBook), "`GradeBook.GradeBooks.StandardGradeBook` doesn't inherit `BaseGradeBook`");

            //assert ctor
            var ctor = RankedType.GetConstructors().FirstOrDefault();
            Assert.True(ctor != null, "No constructor found for GradeBook.GradeBooks.StandardGradeBook.");

            var paras = ctor.GetParameters();
            Assert.True(paras.Count() == 2 && paras[0].ParameterType == typeof(string) && paras[1].ParameterType == typeof(bool), "`GradeBook.GradeBooks.BaseGradeBook`'s constructor doesn't have the correct parameters. It should be a `string` and a `bool`.");
            object gradeBook = null;
            gradeBook = Activator.CreateInstance(RankedType, "rankedGradeBook", true);

            MethodInfo method = RankedType.GetMethod("GetGPA");
            Assert.True(method != null, "GetGPA doesn't exist.");

            //test weighted grades
            Assert.True((double)method.Invoke(gradeBook, new object[] { 'A', StudentType.Standard }) == 4, "`GradeBook.GradeBooks.BaseGradeBook`'s `GetGPA` method weighted a student's grade even when they weren't an Honors or Duel Enrolled student.");
            Assert.True((double)method.Invoke(gradeBook, new object[] { 'A', StudentType.Honors }) == 5, "`GradeBook.GradeBooks.BaseGradeBook`'s `GetGPA` method doesn't weighted a student's grade when they were Honors student.");

            //test not weighted grades
            gradeBook.GetType().GetProperty("IsWeighted").SetValue(gradeBook, false);
            Assert.True((double)method.Invoke(gradeBook, new object[] { 'A', StudentType.Standard }) == 4, "`GradeBook.GradeBooks.BaseGradeBook`'s `GetGPA` method doesn't weighted a student's grade even when they weren't an Honors or Duel Enrolled student.");
            Assert.True((double)method.Invoke(gradeBook, new object[] { 'A', StudentType.Honors }) == 4, "`GradeBook.GradeBooks.BaseGradeBook`'s `GetGPA` method weighted a student's grade when they were Honors student.");

        }
    }
}
