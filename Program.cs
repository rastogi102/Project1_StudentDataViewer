using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDataViewer
{
    public class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public int Grade { get; set; }
    }

    class Program
    {
        static List<Student> ReadStudentDataFromFile(string filename)
        {
            List<Student> students = new List<Student>();
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string headerLine = sr.ReadLine(); // Skip header line
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        if (values.Length == 3 && int.TryParse(values[0], out int studentID) && int.TryParse(values[2], out int grade))
                        {
                            Student student = new Student
                            {
                                StudentID = studentID,
                                Name = values[1],
                                Grade = grade
                            };
                            students.Add(student);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading the file: " + ex.Message);
            }
            return students;
        }

        static void DisplayStudentData(List<Student> students)
        {
            Console.WriteLine("Student Data:");
            Console.WriteLine("ID\tName\t\tGrade");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.StudentID}\t{student.Name}\t\t{student.Grade}");
            }
        }
      
        static void Main(string[] args)
        {
            string filename = "student_data.txt";
            List<Student> students = ReadStudentDataFromFile(filename);
            DisplayStudentData(students);
            Console.ReadKey();
        }
    }
}
