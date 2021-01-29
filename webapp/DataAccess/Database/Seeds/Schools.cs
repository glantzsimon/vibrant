﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using K9.DataAccess.Extensions;
using K9.DataAccess.Models;

namespace K9.DataAccess.Database.Seeds
{
	public static class SchoolSeeder
	{
		public static void SeedSchool(DbContext context)
		{
			{
				var students = new List<Student>
				{
					new Student {FirstMidName = "Carson", LastName = "Alexander", EnrollmentDate = DateTime.Parse("2005-09-01")},
					new Student {FirstMidName = "Meredith", LastName = "Alonso", EnrollmentDate = DateTime.Parse("2002-09-01")},
					new Student {FirstMidName = "Arturo", LastName = "Anand", EnrollmentDate = DateTime.Parse("2003-09-01")},
					new Student {FirstMidName = "Gytis", LastName = "Barzdukas", EnrollmentDate = DateTime.Parse("2002-09-01")},
					new Student {FirstMidName = "Yan", LastName = "Li", EnrollmentDate = DateTime.Parse("2002-09-01")},
					new Student {FirstMidName = "Peggy", LastName = "Justice", EnrollmentDate = DateTime.Parse("2001-09-01")},
					new Student {FirstMidName = "Laura", LastName = "Norman", EnrollmentDate = DateTime.Parse("2003-09-01")},
					new Student {FirstMidName = "Nino", LastName = "Olivetto", EnrollmentDate = DateTime.Parse("2005-09-01")}
				};
				students.ForEach(s =>
				{
					if (!context.Exists<Student>(st => st.FirstMidName == s.FirstMidName))
					{
						s.UpdateName();
						context.Set<Student>().Add(s);
					}
				});
				context.SaveChanges();

				var courses = new List<Course>
				{
					new Course {Id = 1050, Name = "Chemistry", Credits = 3},
					new Course {Id = 4022, Name = "Microeconomics", Credits = 3},
					new Course {Id = 4041, Name = "Macroeconomics", Credits = 3},
					new Course {Id = 1045, Name = "Calculus", Credits = 4},
					new Course {Id = 3141, Name = "Trigonometry", Credits = 4},
					new Course {Id = 2021, Name = "Composition", Credits = 3},
					new Course {Id = 2042, Name = "Literature", Credits = 4}
				};
				courses.ForEach(s =>
				{
					if (!context.Exists<Course>(s.Id))
					{
						context.Set<Course>().Add(s);
					}
				});
				context.SaveChanges();

				var enrollments = new List<Enrollment>
				{
					new Enrollment {StudentId = 1, CourseId = 1050, Grade = Grade.A, Name = Guid.NewGuid().ToString()},
					new Enrollment {StudentId = 1, CourseId = 4022, Grade = Grade.C, Name = Guid.NewGuid().ToString()},
					new Enrollment {StudentId = 1, CourseId = 4041, Grade = Grade.B, Name = Guid.NewGuid().ToString()},
					new Enrollment {StudentId = 2, CourseId = 1045, Grade = Grade.B, Name = Guid.NewGuid().ToString()},
					new Enrollment {StudentId = 2, CourseId = 3141, Grade = Grade.F, Name = Guid.NewGuid().ToString()},
					new Enrollment {StudentId = 2, CourseId = 2021, Grade = Grade.F, Name = Guid.NewGuid().ToString()},
					new Enrollment {StudentId = 3, CourseId = 1050, Name = Guid.NewGuid().ToString()},
					new Enrollment {StudentId = 4, CourseId = 1050, Name = Guid.NewGuid().ToString()},
					new Enrollment {StudentId = 4, CourseId = 4022, Grade = Grade.F, Name = Guid.NewGuid().ToString()},
					new Enrollment {StudentId = 5, CourseId = 4041, Grade = Grade.C, Name = Guid.NewGuid().ToString()},
					new Enrollment {StudentId = 6, CourseId = 1045, Name = Guid.NewGuid().ToString()},
					new Enrollment {StudentId = 7, CourseId = 3141, Grade = Grade.A, Name = Guid.NewGuid().ToString()}
				};
				enrollments.ForEach(s =>
				{
					if (!context.Exists<Enrollment>(e => e.StudentId == s.StudentId && e.CourseId == s.CourseId))
					{
						context.Set<Enrollment>().Add(s);
					}
				});
				context.SaveChanges();
			}

		}
	}
}
