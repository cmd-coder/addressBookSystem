using System;
using System.Collections.Generic;
using System.Text;

namespace addressBook
{
    public class contact
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

    }
}
