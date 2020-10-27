using System;
using System.Collections.Generic;
using System.Text;

namespace addressBook
{
    public class ContactClass : IComparable<ContactClass>, IEquatable<ContactClass>
    {
        public string first
        { get ; set; }

        public string last
        { get; set; }

        public string address
        { get; set; }

        public string city
        { get; set; }

        public string state
        { get;set; }

        public int zip
        { get; set;}

        public string phone
        { get;set; }

        public string email
        { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ContactClass);
        }

        public bool Equals(ContactClass other)
        {
            return other != null &&
                   first == other.first &&
                   last == other.last;
        }

        public int CompareTo(ContactClass person)
        {
            if (person == null)
                return 1;
            if (this.first.CompareTo(person.first) != 0)
                return this.first.CompareTo(person.first);
            else if (this.last.CompareTo(person.last) != 0)
                return this.last.CompareTo(person.last);
            else if (this.city.CompareTo(person.city) != 0)
                return this.city.CompareTo(person.city);
            else if (this.state.CompareTo(person.state) != 0)
                return this.state.CompareTo(person.state);
            return this.zip.CompareTo(person.zip);
        }

        public override string ToString()
        {
            return first
                + "\n" + last
                + "\n" + address
                + "\n" + city
                + "\n" + state
                + "\n" + zip
                + "\n" + phone
                + "\n" + email;
        }
    }
}
