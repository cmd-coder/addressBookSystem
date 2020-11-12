/// Including the requried assemblies in to the program
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

        /// <summary>
        /// The function is written to call the overloaded Equals function and passing the type casted object
        /// </summary>
        /// <param name="obj">the object that is to be converted to an object of type ContactClass</param>
        /// <returns>true or false dependinng on whether the objects are equal or not repectively</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as ContactClass);
        }

        /// <summary>
        /// The function is written to check equality of two objects
        /// </summary>
        /// <param name="other">receives ContactClass object to be compared</param
        /// <returns>true or false depending on whether the objects are equal or not repectively</returns>
        public bool Equals(ContactClass other)
        {
            return other != null &&
                   First == other.First &&
                   Last == other.Last;
        }

        /// <summary>
        /// The function is written to override the CompareTo function declared in the class.
        /// </summary>
        /// <param name="person">the objects that needs to be compared</param>
        /// <returns>1 or -1 depending on whether first object should come before or after respectively</returns>
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

        /// <summary>
        /// The function is written to override the ToString function declared in the Object class
        /// </summary>
        /// <returns>concatenated string containing all the details of a contact</returns>
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