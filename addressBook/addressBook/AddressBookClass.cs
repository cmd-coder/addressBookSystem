/// Including the requried assemblies in to the program
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
    public class AddressBookClass
    {
        /// <summary>
        /// The function is written to ask the user the actions which is to be performed on the address book
        /// </summary>
        /// <param name="list">the contact list that has been selected by the user</param>
        /// <param name="personcity">a dictionary to store the person name along with the city in which the person resides</param>
        /// <param name="personState">a dictionary to store the person name along with the state in which the person resides</param>
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

        /// <summary>
        /// The function is written to add contacts in the contact list
        /// </summary>
        /// <param name="list">the contact list that has been selected by the user</param>
        /// <param name="personcity">a dictionary to store the person name along with the city in which the person resides</param>
        /// <param name="personState">a dictionary to store the person name along with the state in which the person resides</param>
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

        /// <summary>
        /// The function is written to edit a contact
        /// </summary>
        /// <param name="list">the contact list in which editing is to be done</param>
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

        /// <summary>
        /// The function is written to write data into the json file
        /// </summary>
        /// <param name="addressDict">contains the contact lists that has to be stored</param>
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

        /// <summary>
        /// The function is written to delete a contact from the contact list
        /// </summary>
        /// <param name="list">the contat list from which a contact has to be deleted</param>
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

        /// <summary>
        /// The function is written to find a person int the address book
        /// </summary>
        /// <param name="addressDict">contains all the contacts details</param>
        public static bool Find(string place, Dictionary<string, List<ContactClass>> addressDict)
        {
            bool found = false;
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
                        found = true;
                    }
                }
            }
            if (count == 0)
                Console.WriteLine("No results found");
            Console.WriteLine("-----------------------");
            return found;
        }

        /// <summary>
        /// The function is written to count the number of persons residing in the given place
        /// </summary>
        /// <param name="place">the place which is to be searched</param>
        /// <param name="addressDict">contains the data that has to be searched</param>
        public static int Count(string place, Dictionary<string, List<ContactClass>> addressDict)
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
            return count;
        }

        /// <summary>
        /// The function is written to count the number of contaccts added in a particular period
        /// </summary>
        /// <param name="addressDict">contains the address book data that has to be operated upon</param>
        /// <param name="endDate">the date upto which the search has to be made</param>
        /// <param name="startDate">the date from which the search has to be started</param>
        public static int ContactsAddedInAParticularPeriod(Dictionary<string, List<ContactClass>> addressDict, DateTime startDate, DateTime endDate)
        {
            int count = 0;
            foreach (KeyValuePair<string, List<ContactClass>> kvp in addressDict)
            {
                List<ContactClass> list = kvp.Value;
                foreach (var i in list)
                {
                    if (i.DateAdded<=endDate && i.DateAdded >=startDate)
                    {
                        count++;
                        Console.WriteLine(i);
                    }
                }
            }
            if (count == 0)
                Console.WriteLine("No results found");
            else
                Console.WriteLine("The number of Contact persons are: " + count);
            Console.WriteLine("-----------------------");
            return count;
        }
        
    }
}