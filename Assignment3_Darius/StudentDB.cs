using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_Darius
{
    internal class StudentDB
    {
        // create the list
        private static List<Students> students = new List<Students>();
        private const string Path = @"C:\Users\Taken\Desktop\App Dev C#.Net\students.txt";
        private const char Delimiter = '/';


        public static List<Students> GetStudents()
        {

            List<Students> students = new List<Students>();

            // Read data from the text file
            if (File.Exists(Path))
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(Path);

                // Parse each line to create Student objects
                foreach (string line in lines)
                {
                    string[] columns = line.Split(Delimiter);
                    if (columns.Length == 7)
                    {
                        Students newStudent = new Students()
                        {
                            ID = Convert.ToInt32(columns[0]),
                            FirstName = columns[1],
                            LastName = columns[2],
                            Age = Convert.ToInt32(columns[3]),
                            Gender = columns[4],
                            ClassName = columns[5],
                            Grade = Convert.ToDouble(columns[6])
                        };
                        students.Add(newStudent);
                    }
                }
            }

            return students;
        }


        public static void SaveStudents(List<Students> students)
        {
            // Convert each student to a string and write to the file
            List<string> lines = students.Select(student =>
                $"{student.ID}{Delimiter}{student.FirstName}{Delimiter}{student.LastName}{Delimiter}{student.Age}{Delimiter}{student.Gender}{Delimiter}{student.ClassName}{Delimiter}{student.Grade}")
                .ToList();

            // Write to the file
            File.WriteAllLines(Path, lines);
        }
        // method to delete the student from the listbox
        public static void RemoveStudent(Students student)
        {
            students.Remove(student);
        }

        public static void UpdateStudent(Students updatedstudent)
        {
            Students existstudent=students.FirstOrDefault(student=>student.ID == updatedstudent.ID);
            if (existstudent != null)
            {
                existstudent.ID=updatedstudent.ID;
                existstudent.FirstName=updatedstudent.FirstName;
                existstudent.LastName = updatedstudent.LastName;
                existstudent.Age = updatedstudent.Age;
                existstudent.Gender= updatedstudent.Gender;
                existstudent.ClassName=updatedstudent.ClassName;
                existstudent.Grade= updatedstudent.Grade;

            }
        }
    }
}
