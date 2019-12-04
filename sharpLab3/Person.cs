using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpLab3
{
    [Serializable]
    class Person 
    {
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            Person person = (Person)obj;
            return this.Name == person.Name &
                this.Surname == person.Surname &
                this.Data == person.Data;
        }
        public static bool operator ==(Person c1, Person c2)
        {
            return c1.Name == c2.Name &
               c1.Surname == c2.Surname &
                c1.Data == c2.Data;
        }
        public static bool operator !=(Person c1, Person c2)
        {
            return !(c1.Name == c2.Name &
               c1.Surname == c2.Surname &
                c1.Data == c2.Data);
        }
        public override int GetHashCode()
        {
            string str = Name + Surname + Data;
            return str.GetHashCode();
        }
        public object DeepCopy()
        {
            Person test = new Person();
            test.Name = this.Name;
            test.Surname = this.Surname;
            test.Data = this.Data;
            return test;

        }
        protected string name;
        protected string surname;
        protected System.DateTime data;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }
        public System.DateTime Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        public int Year
        {
            get
            {
                return data.Year;
            }
            set
            {
                data = new DateTime(value, this.data.Month, this.data.Day);
            }
        }
        public Person(string name, string surname, DateTime data)
        {
            this.name = name;
            this.surname = surname;
            this.data = data;
        }

        public Person()
        {
            name = "Unknown";
            surname = "Unknown";
            data = new DateTime();
        }
        public override string ToString()
        {
            string s = "Name: " + name + ". Surname: " + surname + ". " + "Date: " + data + ".";
            return s;
        }
        public string ToShortString()
        {
            string s = "Name: " + name + ". Surname: " + surname + ".";
            return s;
        }
    }
}