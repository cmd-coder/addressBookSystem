using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace addressBook
{
    class Program
    {

        static List<contact> contactList = new List<contact>();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Address Book Program!");

            var varContact = new contact("First", "Last", "Address", "City", "State", "Phone", "Email", 100050);
            contactList.Add(varContact);

            var varContact2 = new contact("First2", "Last2", "Address2", "City2", "State2", "Phone2", "Email2", 200050);
            contactList.Add(varContact2);

            var varContact3 = new contact("First3", "Last3", "Address3", "City3", "State3", "Phone3", "Email3", 300050);
            contactList.Add(varContact3);
            //Console.WriteLine(varContact.FirstName + "--" + varContact.LastName + "--" + varContact.Address + "--" + varContact.City + "--" + varContact.State + "--" + varContact.Zip + "--" + varContact.Phone + "--" + varContact.Email);

            while (true)
            {
                if (contactList.Count == 0)
                    Console.WriteLine("There are no contacts in the address book");
                else
                {
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("The contacts are: ");
                    Console.WriteLine("-----------------------------");
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

                Console.WriteLine("Enter\n1 to Add a contact\n2 to Edit an existing contact\nAny other number to Exit");
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1)
                    AddContact();
                else if (choice == 2)
                    EditContact();
                else
                    break;
            }
        }

        static void AddContact()
        {
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
                Console.WriteLine("------------------------");
                Console.WriteLine("The contact has been added succesfully");
                Console.WriteLine("------------------------");

                Console.WriteLine("Enter 1 to add one more and 0 to exit");
                int check = Convert.ToInt32(Console.ReadLine());
                if (check == 0)
                    break;
            }
        }

        static void EditContact()
        {
            Console.WriteLine("Enter the first name of the person whose contact is to be edited");
            string firstName = Console.ReadLine();
            
            foreach(var i in contactList)
            {
                if (i.FirstName==firstName)
                {
                    Console.WriteLine("The previous Address was: " + i.Address + "  ->Enter new Address");
                    i.Address = Console.ReadLine();

                    Console.WriteLine("The previous City was: " + i.City + "  ->Enter new City");
                    i.City = Console.ReadLine();

                    Console.WriteLine("The previous State was: " + i.State + "  ->Enter new State");
                    i.State = Console.ReadLine();

                    Console.WriteLine("The previous Zip was: " + i.Zip + "  ->Enter new Zip");
                    i.Zip = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("The previous Phone was: " + i.Phone + "  ->Enter new Phone");
                    i.Phone = Console.ReadLine();

                    Console.WriteLine("The previous Email was: " + i.Email + "  ->Enter new Email");
                    i.Email = Console.ReadLine();

                    break;
                }
            }
            Console.WriteLine("-----------------------");
            Console.WriteLine("Edited Successfully");
            Console.WriteLine("-----------------------");
        }
     }
}
