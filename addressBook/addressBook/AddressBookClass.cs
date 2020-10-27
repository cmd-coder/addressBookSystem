using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Linq;

namespace addressBook
{
    class AddressBookClass
    {

        //This function selects the concerned address book for further operations.
        public static void Select(List<ContactClass> list, Dictionary<string, string> personcity, Dictionary<string, string> personState)
        {
            while (true)
            {
                if (list.Count == 0)
                    Console.WriteLine("There are no ContactClass in the address book");
                else
                {
                    list.Sort();//Sorting the list using overridden ToCompare methiod
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("The Contact are: ");
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
                    AddContact(list, personcity, personState);
                else if (choice == 2)
                    EditContact(list);
                else if (choice == 3)
                    DeleteContact(list);
                else
                    break;
            }
        }

        //This function is used to add ContactClass in an address book.
        public static void AddContact(List<ContactClass> list, Dictionary<string, string> personcity, Dictionary<string, string> personState)
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

                //var varContact = new Contact(first, last, address, city, state, phone, email, zip);
                var varContact = new ContactClass() { first = first, last = last, address = address, city = city, state = state, zip = zip, phone = phone, email = email };

                if (list.Contains(varContact))
                {
                    Console.WriteLine("List already has this Contact template. Try again.");
                    Console.WriteLine("-------------------------");
                    continue;
                }

                list.Add(varContact);

                personcity.Add(first + " " + last, city);
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
        public static void EditContact(List<ContactClass> list)
        {
            Console.WriteLine("Enter the first name of the person whose Contact is to be edited");
            string first = Console.ReadLine();

            foreach (var i in list)
            {
                if (i.first == first)
                {
                    Console.WriteLine("The previous Address was: " + i.address + "  ->Enter new Address");
                    i.address = Console.ReadLine();

                    Console.WriteLine("The previous city was: " + i.city + "  ->Enter new city");
                    i.city = Console.ReadLine();

                    Console.WriteLine("The previous State was: " + i.state + "  ->Enter new State");
                    i.state = Console.ReadLine();

                    Console.WriteLine("The previous Zip was: " + i.zip + "  ->Enter new Zip");
                    i.zip = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("The previous Phone was: " + i.phone + "  ->Enter new Phone");
                    i.phone = Console.ReadLine();

                    Console.WriteLine("The previous Email was: " + i.email + "  ->Enter new Email");
                    i.email = Console.ReadLine();

                    break;
                }
            }
            Console.WriteLine("-----------------------");
            Console.WriteLine("Edited Successfully");
            Console.WriteLine("-----------------------");
        }
        
        //This function is used to write everything present in the addressDict dictionary into the respective files.
        internal static void WriteIntoFile(Dictionary<string, List<ContactClass>> addressDict)
        {
            foreach (KeyValuePair<string, List<ContactClass>> kvp in addressDict)
            {
                string addressBookName = kvp.Key+".csv";
                List<ContactClass> list = kvp.Value;
                using (StreamWriter sw = new StreamWriter(addressBookName))
                using (var writer = new CsvWriter(sw, CultureInfo.InvariantCulture))
                {
                    sw.WriteLine(list);
                }
            }
        }

        //This function is used to delete a particular contact from an address book
        public static void DeleteContact(List<ContactClass> list)
        {
            Console.WriteLine("Enter the first name of the Contact to be deleted.");
            string name = Console.ReadLine();

            foreach (var i in list)
            {
                if (i.first == name)
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
        public static void Find(string place, Dictionary<string, List<ContactClass>> addressDict)
        {
            Console.WriteLine("Results:");
            Console.WriteLine("-----------------------");
            int count = 0;
            foreach (KeyValuePair<string, List<ContactClass>> kvp in addressDict)
            {
                List<ContactClass> list = kvp.Value;
                foreach (var i in list)
                {
                    if (i.city == place || i.state == place)
                    {
                        Console.WriteLine(i.first + " " + i.last + " has been found in " + kvp.Key + " address book.");
                        count++;
                    }
                }
            }
            if (count == 0)
                Console.WriteLine("No results found");
            Console.WriteLine("-----------------------");
        }

        //This function is used to count the number of persons in a given city or state
        public static void Count(string place, Dictionary<string, List<ContactClass>> addressDict)
        {
            int count = 0;
            foreach (KeyValuePair<string, List<ContactClass>> kvp in addressDict)
            {
                List<ContactClass> list = kvp.Value;
                foreach (var i in list)
                {
                    if (i.city == place || i.state == place)
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
        public static void PopulateDictionary(Dictionary<string, List<ContactClass>> addressDict)
        {
            string path = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(path, "*.csv");
            foreach (var i in files)
            {
                List<ContactClass> tempList = new List<ContactClass>();
                using (StreamReader sr = new StreamReader(i))
                using(var csv=new CsvReader(sr, CultureInfo.InvariantCulture))
                {
                    //csv.Configuration.HasHeaderRecord = false;
                    var records = csv.GetRecords<ContactClass>().ToList();
                    foreach(ContactClass items in records)
                    {
                        string first1 = items.first;
                        string last1 = items.last;
                        string address1 =items.address;
                        string city1 = items.city;
                        string state1 = items.state;
                        string phone1 = items.phone;
                        string email1 = items.email;
                        int zip1 = items.zip;
                        //var ConatactClass = new Contact(first, last, address, city, state, phone, email, zip);
                        ContactClass contact = new ContactClass()
                        {
                            first = first1,
                            last = last1,
                            address = address1,
                            city = city1,
                            state = state1,
                            phone = phone1,
                            email = email1,
                            zip = 10066
                        };
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