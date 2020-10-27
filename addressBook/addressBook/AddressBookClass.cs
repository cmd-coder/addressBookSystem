using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

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

                if (choice == 1)//Add a contact
                    AddContact(list, personcity, personState);
                else if (choice == 2)//Edit an existing contact
                    EditContact(list);
                else if (choice == 3)//Delete an existing contact
                    DeleteContact(list);
                else//Move out of a particular address book
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

                //Create a new contact template
                var varContact = new ContactClass() { First = first, Last = last, Address = address, City = city, State = state, Zip = zip, Phone = phone, Email = email };

                if (list.Contains(varContact))//If address book already has this contact, then discard it and ask the user to enter again
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
                if (check == 0)//If user does not want to add  another contact
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
                if (i.First == first)
                {
                    Console.WriteLine("The previous Address was: " + i.Address + "  ->Enter new Address");
                    i.Address = Console.ReadLine();

                    Console.WriteLine("The previous city was: " + i.City + "  ->Enter new city");
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
        internal static void WriteIntoFile(Dictionary<string, List<ContactClass>> addressDict)
        {
            foreach (KeyValuePair<string, List<ContactClass>> kvp in addressDict)
            {
                string addressBookName = kvp.Key + ".json";
                List<ContactClass> list = kvp.Value;
                JsonSerializer jsonSerializer = new JsonSerializer();
                using (StreamWriter sw = new StreamWriter(addressBookName))//Create new or replace existing Address Book 1 file
                using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                {
                    jsonSerializer.Serialize(jsonWriter, list);
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
                if (i.First == name)
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
                    if (i.City == place || i.State == place)
                    {
                        Console.WriteLine(i.First + " " + i.Last + " has been found in " + kvp.Key + " address book.");
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
        public static void PopulateDictionary(Dictionary<string, List<ContactClass>> addressDict)
        {
            string path = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(path, "*.json");
            foreach (var i in files)
            {
                if (i.Contains(" "))
                {
                    List<ContactClass> tempList = new List<ContactClass>();
                    IList<ContactClass> contactData = JsonConvert.DeserializeObject<IList<ContactClass>>(File.ReadAllText(i));
                    using (StreamReader sr = new StreamReader(i))
                        foreach (ContactClass items in contactData)
                        {
                            string first = items.First;
                            string last = items.Last;
                            string address = items.Address;
                            string city = items.City;
                            string state = items.State;
                            string phone = items.Phone;
                            string email = items.Email;
                            int zip = items.Zip;
                            ContactClass contact = new ContactClass()
                            {
                                First = first,
                                Last = last,
                                Address = address,
                                City = city,
                                State = state,
                                Phone = phone,
                                Email = email,
                                Zip = zip
                            };
                            tempList.Add(contact);
                        }
                    string fileName = i.Replace(path + "\\", "");
                    fileName = fileName.Replace(".json", "");
                    if (!addressDict.ContainsKey(fileName))
                        addressDict.Add(fileName, tempList);
                }
            }
        }
    }
}