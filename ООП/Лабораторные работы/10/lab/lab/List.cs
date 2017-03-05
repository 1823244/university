using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace lab
{
    class ListElement
    {
        public Student Data { get; set; }
        public ListElement Next { get; set; }

        public ListElement()
        {
            Data = null;
            Next = null;
        }
    }

    class List : IList, IList<Student>
    {
        ListElement Start;
        ListElement Current;
        public int Count { get; private set; }

        public List()
        {
            Start = Current = new ListElement();
            this.Count = 0;
        }

        public void Search(double avg)
        {
            try
            {
                int counter = 0;
                for (ListElement a = Start.Next; a != null; a = a.Next)
                {
                    if (a.Data.Avg > avg)
                    {
                        Console.WriteLine(a.Data.ToString());
                        counter++;
                    }
                }
                if (counter != 0)
                {
                    return;
                }
                throw new Exception("Искомые студенты не найдены.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public int SearchIndex(Student item)
        {
            int i = 0;
            for (; i < Count; i++)
            {
                if (this[i].CompareTo(item) == -1)
                {
                    return i;
                }
            }
            return i;
        }

        public int IndexOf(Student item)
        {
            int i = 0;
            ListElement pos;

            for (pos = Start.Next; (pos != null) && pos.Data.Equals(item); pos = pos.Next)
            {
                i++;
            }
            
            if (pos == null)
            {
                return -1;
            }
            else
            {
                return i;
            }
        }

        int IList.IndexOf(object value)
        {
            return IndexOf((Student)value);
        }

        public void Insert(int index, Student item)
        {
            if (this.IsReadOnly) throw new NotSupportedException();
            if ((index > -1) && (index <= Count))
            {
                ListElement t = Current;
                Current = Start;
                for (int i = 0; i <= (index - 1); i++)
                {
                    Current = Current.Next;
                }
                this.Add(item);
                Current = t;
                return;
            }
            throw new ArgumentOutOfRangeException();
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (Student)value);
        }

        public void RemoveAt(int index)
        {
            if (this.IsReadOnly) throw new NotSupportedException();
            if (index > -1 && index < Count)
            {
                ListElement t = this.Start;
                for (int i = 0; i < index - 1; i++)
                {
                    t = t.Next;
                }
                t.Next = t.Next.Next;
                Count--;
            }
            throw new ArgumentOutOfRangeException();
        }

        void IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        public Student this[int index]
        {
            get
            {
                if ((index > -1) && (index < Count))
                {
                    ListElement t = Start.Next;
                    for (int i = 0; i < index; i++)
                    {
                        t = t.Next;
                    }
                    return t.Data;
                }
                throw new ArgumentOutOfRangeException();
            }
            set
            {
                if ((index > 0) && (index < Count))
                {
                    Insert(index, value);
                }
                throw new ArgumentOutOfRangeException();
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (Student)value;
            }
        }

        public void Add(Student item)
        {
            ListElement newListElement = new ListElement();
            newListElement.Next = this.Current.Next;
            this.Current.Next = newListElement;
            newListElement.Data = item;
            Count++;
        }

        int IList.Add(object value)
        {
            this.Add((Student)value);
            return IndexOf((Student)value);
        }

        public void Clear()
        {
            for (ListElement t = this.Start; t != null; t = t.Next)
            {
                t = null;
            }
        }

        void IList.Clear()
        {
            this.Clear();
        }

        public bool Contains(Student item)
        {
            return IndexOf(item) != -1;
        }

        bool IList.Contains(object value)
        {
            return this.Contains((Student)value);
        }

        public void CopyTo(Student[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (this.Count + arrayIndex > array.Length)
            {
                throw new ArgumentException();
            }

            int i = 0;
            while (i < this.Count)
            {
                array[arrayIndex++] = this[i++];
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.CopyTo((Student[])array, index);
        }

        int ICollection.Count
        {
            get { return this.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        bool IList.IsReadOnly
        {
            get { return this.IsReadOnly; }
        }

        public bool Remove(Student item)
        {
            ListElement pos;
            for (pos = this.Start; pos.Next != null && pos.Next.Data.Equals(item); pos = pos.Next) ;
            if (pos != null)
            {
                pos.Next = pos.Next.Next;
                return true;
            }
            else
            {
                return false;
            }
        }

        void IList.Remove(object value)
        {
            this.Remove((Student)value);
        }

        public IEnumerator<Student> GetEnumerator()
        {
            ListElement startPos = this.Start;

            while (startPos.Next != null)
            {
                yield return startPos.Next.Data;
                startPos = startPos.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return this[0]; }
        }
    }
}
