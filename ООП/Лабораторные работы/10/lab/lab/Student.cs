using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class Student : IComparable, IComparable<Student>
    {
        public string Name { get; set; }
        public int Group { get; set; }
        private int[] marks;
        public int[] Marks 
        { 
            get 
            {
                return marks;
            }
            set
            {
                marks = value;
                Avg = 0;
                foreach (int mark in marks)
                {
                    Avg += mark;
                }
                Avg /= 5;
            } 
        }
        public float Avg { get; set; }

        public Student()
        {
            Marks = new int[5];
        }

        public Student(string name, int group, int[] marks)
        {
            Name = name;
            Group = group;
            Marks = marks;
            Avg = 0;
            foreach (int mark in marks)
            {
                Avg += mark;
            }
            Avg /= 5;
        }

        public override bool Equals(object obj)
        {
            var a = (Student)obj;
            return Name.Equals(a.Name) && Group.Equals(a.Group) && Marks.Equals(a.Marks);
        }

        public int CompareTo(Student obj)
        {
            if (Group < obj.Group)
            {
                return 1;
            }
            else if (Group > obj.Group)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public override string ToString()
        {
            return String.Format("Имя: {0}\tГруппа: {1}\tОценки: {2} {3} {4} {5} {6}",
                                    Name, Group, Marks[0], Marks[1], Marks[2], Marks[3], Marks[4]);
        }

        int IComparable.CompareTo(object obj)
        {
            return CompareTo((Student)obj);
        }
    }
}
