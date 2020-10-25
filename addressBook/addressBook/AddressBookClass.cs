using System;
using System.Collections.Generic;
using System.Text;

namespace addressBook
{
    class AddressBookClass
    {
        public static void select(List<contact> list, Dictionary<string, string> personCity, Dictionary<string, string> personState)
        {
            while (true)
            {
                if (list.Count == 0)
                    Console.WriteLine("There are no contacts in the address book");
                else
                {
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("The contacts are: ");
                    Console.WriteLine("-----------------------------");
                    foreach (var i in list)
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

                Console.WriteLine("Enter\n1 to Add a contact\n2 to Edit an existing contact\n3 to delete a contact\nAny other number to Exit");
                int choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1)
                    AddContact(list, personCity, personState);
                else if (choice == 2)
                    EditContact(list);
                else if (choice == 3)
                    DeleteContact(list);
                else
                    break;
            }
        }

        public static void AddContact(List<contact> list, Dictionary<string, string> personCity, Dictionary<string, string> personState)
        {
            //List<contact> list = new List<contact>();
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

                if (list.Contains(varContact))
                {
                    Console.WriteLine("List already has this contact template. Try again.");
                    Console.WriteLine("-------------------------");
                    continue;
                }

                list.Add(varContact);
                personCity.Add(first + " " + last, city);
                personState.Add(first + " " + last, state);
                Console.WriteLine("-------------------------");
                Console.WriteLine("The contact has been added succesfully");
                Console.WriteLine("-------------------------");

                Console.WriteLine("Enter 1 to add one more and 0 to exit");
                int check = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-------------------------");
                if (check == 0)
                    break;
            }
        }

        public static void EditContact(List<contact> list)
        {
            Console.WriteLine("Enter the first name of the person whose contact is to be edited");
            string firstName = Console.ReadLine();

            foreach (var i in list)
            {
                if (i.FirstName == firstName)
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

        public static void DeleteContact(List<contact> list)
        {
            Console.WriteLine("Enter the first name of the contact to be deleted.");
            string name = Console.ReadLine();

            foreach (var i in list)
            {
                if (i.FirstName == name)
                {
                    list.Remove(i);
                    break;
                }
            }
            Console.WriteLine("-----------------------");
            Console.WriteLine("Deleted Successfully");
            Console.WriteLine("-----------------------");
        }

        public static void find(string place, Dictionary<string, List<contact>> addressDict)
        {
            Console.WriteLine("Results:");
            Console.WriteLine("-----------------------");
            int count = 0;
            foreach (KeyValuePair<string, List<contact>> kvp in addressDict)
            {
                List<contact> list = kvp.Value;
                foreach (var i in list)
                {
                    if (i.City == place || i.State == place)
                    {
                        Console.WriteLine(i.FirstName + " " + i.LastName + " has been found in " + kvp.Key + " address book.");
                        count++;
                    }
                }
            }
            if (count == 0)
                Console.WriteLine("No results found");
            Console.WriteLine("-----------------------");
        }

        public static void count(string place, Dictionary<string, List<contact>> addressDict)
        {
            int count = 0;
            foreach (KeyValuePair<string, List<contact>> kvp in addressDict)
            {
                List<contact> list = kvp.Value;
                foreach (var i in list)
                {
                    if (i.City == place || i.State == place)
                        count++;
                }
            }
            if (count == 0)
                Console.WriteLine("No results found");
            else
                Console.WriteLine("The number of contact persons are: " + count);
            Console.WriteLine("-----------------------");
        }
    }
}