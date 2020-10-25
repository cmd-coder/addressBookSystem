using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace addressBook
{
    public class contact : IEquatable<contact>, IComparable<contact>
    {
        string first = "", last = "", add = "", city = "", state = "", phone = "", email = "";
        int zip = 0;

        public contact(string first, string last, string add, string city, string state, string phone, string email, int zip)
        {
            this.first = first;
            this.last = last;
            this.add = add;
            this.city = city;
            this.state = state;
            this.phone = phone;
            this.email = email;
            this.zip = zip;
        }

        public string FirstName
        { get=>first; set=>first=value; }

        public string LastName
        { get=>last; set=>last=value; }

        public string Address
        { get=>add; set=>add=value; }

        public string City
        { get=>city; set=>city=value; }

        public string State
        { get=>state; set=>state=value; }

        public int Zip
        { get=>zip; set=>zip=value; }

        public string Phone
        { get=>phone; set=>phone=value; }

        public string Email
        { get=>email; set=>email=value; }

        public override bool Equals(object obj)
        {
            return Equals(obj as contact);
        }

        public bool Equals(contact other)
        {
            return other != null &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName;
        }

        public  int CompareTo(contact person)
        {
            if (person == null)
                return 1;
            if (this.FirstName.CompareTo(person.FirstName) != 0)
                return this.FirstName.CompareTo(person.FirstName);
            else if (this.LastName.CompareTo(person.LastName) != 0)
                return this.LastName.CompareTo(person.LastName);
            else if (this.City.CompareTo(person.City) != 0)
                return this.City.CompareTo(person.City);
            else if (this.State.CompareTo(person.State) != 0)
                return this.State.CompareTo(person.State);
            return this.Zip.CompareTo(person.Zip);
        }

        public override string ToString()
        {
            return "First name: " + FirstName
                + "\nLast name: " + LastName
                + "\nAddress: " + Address
                + "\nCity: "+City
                +"\nState: "+State
                +"\nZip: "+Zip
                +"\nPhone: "+Phone
                +"\nEmail: "+Email;
        }
    }
}
