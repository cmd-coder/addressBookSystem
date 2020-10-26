using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace addressBook
{
    class AddressBookClass
    {

        //This function selects the concerned address book for further operations.
        public static void Select(List<Contact> list, Dictionary<string, string> personCity, Dictionary<string, string> personState)
        {
            while (true)
            {
                if (list.Count == 0)
                    Console.WriteLine("There are no Contacts in the address book");
                else
                {
                    list.Sort();//Sorting the list using overridden ToCompare methiod
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("The Contacts are: ");
                    Console.WriteLine("-----------------------------");
                    foreach (var i in list)
                    {
                        Console.WriteLine(i);//Using overridden ToString() method present in Contact class
                        Console.WriteLine("---------------------------");
                    }
                }

                Console.WriteLine("Enter\n1 to Add a Contact\n2 to Edit an existing Contact\n3 to delete a Contact\nAny other number to Exit");
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

        //This function is used to add contacts in an address book.
        public static void AddContact(List<Contact> list, Dictionary<string, string> personCity, Dictionary<string, string> personState)
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

                var varContact = new Contact(first, last, address, city, state, phone, email, zip);

                if (list.Contains(varContact))
                {
                    Console.WriteLine("List already has this Contact template. Try again.");
                    Console.WriteLine("-------------------------");
                    continue;
                }

                list.Add(varContact);

                personCity.Add(first + " " + last, city);
                personState.Add(first + " " + last, state);
                Console.WriteLine("-------------------------");
                Console.WriteLine("The Contact has been added succesfully");
                Console.WriteLine("-------------------------");

                Console.WriteLine("Enter 1 to add one more and 0 to exit");
                int check = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("-------------------------");
                if (check == 0)
                    break;
            }
        }

        //This function is used to edit a contact
        public static void EditContact(List<Contact> list)
        {
            Console.WriteLine("Enter the first name of the person whose Contact is to be edited");
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
        
        //This function is used to write everything present in the addressDict dictionary into the respective files.
        internal static void WriteIntoFile(Dictionary<string, List<Contact>> addressDict)
        {
            foreach (KeyValuePair<string, List<Contact>> kvp in addressDict)
            {
                string addressBookName = kvp.Key+".txt";
                List<Contact> list = kvp.Value;
                using (StreamWriter sw = new StreamWriter(addressBookName))
                {
                    foreach (var i in list)
                        sw.WriteLine(i);
                }
            }
        }

        //This function is used to delete a particular contact from an address book
        public static void DeleteContact(List<Contact> list)
        {
            Console.WriteLine("Enter the first name of the Contact to be deleted.");
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

        //This function is used to find a particular person present in a given city or state
        public static void Find(string place, Dictionary<string, List<Contact>> addressDict)
        {
            Console.WriteLine("Results:");
            Console.WriteLine("-----------------------");
            int count = 0;
            foreach (KeyValuePair<string, List<Contact>> kvp in addressDict)
            {
                List<Contact> list = kvp.Value;
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

        //This function is used to count the number of persons in a given city or state
        public static void Count(string place, Dictionary<string, List<Contact>> addressDict)
        {
            int count = 0;
            foreach (KeyValuePair<string, List<Contact>> kvp in addressDict)
            {
                List<Contact> list = kvp.Value;
                foreach (var i in list)
                {
                    if (i.City == place || i.State == place)
                        count++;
                }
            }
            if (count == 0)
                Console.WriteLine("No results found");
            else
                Console.WriteLine("The number of Contact persons are: " + count);
            Console.WriteLine("-----------------------");
        }

        //This function is used to populate the addressDict dictionary from the values present in different .txt files.
        public static void PopulateDictionary(Dictionary<string, List<Contact>> addressDict)
        {
            string path = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(path, "*.txt");
            foreach (var i in files)
            {
                List<Contact> tempList = new List<Contact>();
                using (StreamReader sr = new StreamReader(i))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        string first = s;
                        string last = sr.ReadLine();
                        string address = sr.ReadLine();
                        string city = sr.ReadLine();
                        string state = sr.ReadLine();
                        int zip = Convert.ToInt32(sr.ReadLine());
                        string phone = sr.ReadLine();
                        string email = sr.ReadLine();
                        var contact = new Contact(first, last, address, city, state, phone, email, zip);
                        tempList.Add(contact);
                    }
                }
                string fileName = i.Replace(path + "\\", "");
                fileName = fileName.Replace(".txt", "");
                if (!addressDict.ContainsKey(fileName))
                    addressDict.Add(fileName, tempList);
            }
        }
    }
}