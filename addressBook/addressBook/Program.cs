using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace addressBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Address Book Program!");

            //var varContact = new contact("First", "Last", "Address", "City", "State", "Phone", "Email", 100050);
            //Console.WriteLine(varContact.FirstName + "--" + varContact.LastName + "--" + varContact.Address + "--" + varContact.City + "--" + varContact.State + "--" + varContact.Zip + "--" + varContact.Phone + "--" + varContact.Email);

            List<contact> contactList = new List<contact>();

            while (true)
            {
                Console.WriteLine("Enter first name, last name, address, city, state, zip, phone number and email in sequence");
                string first = Console.ReadLine();
                string last = Console.ReadLine();
                string address = Console.ReadLine();
                string city = Console.ReadLine();
                string state = Console.ReadLine();
                int zip = Convert.ToInt32(Console.ReadLine());
                string phone = Console.ReadLine();
                string email = Console.ReadLine();

                var varContact = new contact(first, last, address, city, state, phone, email, zip);
                contactList.Add(varContact);
                Console.WriteLine("The contact has been added succesfully");

                Console.WriteLine("Enter 1 to add one more and 0 to exit");
                int check= Convert.ToInt32(Console.ReadLine());
                if (check == 0)
                    break;
            }
            if (contactList.Count == 0)
                Console.WriteLine("There are no contacts in the address book");
            else
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine("The contacts are: ");
                foreach (var i in contactList)
                {
                    Console.WriteLine("First name: " + i.FirstName);
                    Console.WriteLine("Last name: " + i.LastName);
                    Console.WriteLine("Address: " + i.Address);
                    Console.WriteLine("City: " + i.City);
                    Console.WriteLine("State: " + i.State);
                    Console.WriteLine("Zip code: " + i.Zip);
                    Console.WriteLine("Phone Number: " + i.Phone);
                    Console.WriteLine("Email: " + i.Email);
                    Console.WriteLine("---------------------------");
                }
            }
        }
    }
}
