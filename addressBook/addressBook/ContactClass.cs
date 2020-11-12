using System;
using System.Collections.Generic;
using System.Text;

namespace addressBook
{
    public class ContactClass : IComparable<ContactClass>, IEquatable<ContactClass>
    {
        public string First
        { get; set; }

        public string Last
        { get; set; }

        public string Address
        { get; set; }

        public string City
        { get; set; }

        public string State
        { get; set; }

        public int Zip
        { get; set; }

        public string Phone
        { get; set; }

        public string Email
        { get; set; }

        public DateTime DateAdded
        { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ContactClass);
        }

        public bool Equals(ContactClass other)
        {
            return other != null &&
                   First == other.First &&
                   Last == other.Last;
        }

        public int CompareTo(ContactClass person)
        {
            if (person == null)
                return 1;
            if (this.First.CompareTo(person.First) != 0)
                return this.First.CompareTo(person.First);
            else if (this.Last.CompareTo(person.Last) != 0)
                return this.Last.CompareTo(person.Last);
            else if (this.City.CompareTo(person.City) != 0)
                return this.City.CompareTo(person.City);
            else if (this.State.CompareTo(person.State) != 0)
                return this.State.CompareTo(person.State);
            return this.Zip.CompareTo(person.Zip);
        }

        public override string ToString()
        {
            return First
                + "\n" + Last
                + "\n" + Address
                + "\n" + City
                + "\n" + State
                + "\n" + Zip
                + "\n" + Phone
                + "\n" + Email
                + "\n" + DateAdded;
        }
    }
}