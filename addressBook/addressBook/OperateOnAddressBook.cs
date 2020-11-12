using System;
using System.Collections.Generic;
using System.Text;

namespace addressBook
{
    class OperateOnAddressBook
    {
        static List<ContactClass> ContactClassList = new List<ContactClass>();//store contacts in an address book
        static Dictionary<string, List<ContactClass>> addressDict = new Dictionary<string, List<ContactClass>>();//store address books
        static Dictionary<string, string> personCity = new Dictionary<string, string>();//maintain a dictionary of a person and his city
        static Dictionary<string, string> personState = new Dictionary<string, string>();//maintain a dictionary of a person and his state
        public static void AddressBookOperations()
        {
            while (true)
            {
                Console.WriteLine("The address books in the system are:");
                foreach (KeyValuePair<string, List<ContactClass>> j in addressDict)
                {
                    Console.WriteLine(j.Key);
                }

                Console.WriteLine("---------------------------");
                Console.WriteLine("Enter:\n1 for adding a new address book\n" +
                    "2 for selecting an existing one\n" +
                    "3 to find a person residing in a city or a state\n" +
                    "4 to find the number of ContactClass persons residing in a city or state\n" +
                    "5 to to find the contacts that were added in a particular period\n" +
                    "6 to exit");
                int option = Convert.ToInt32(Console.ReadLine());
                if (option == 1)//For adding a new address book
                {
                    Console.WriteLine("Enter the name of the address book");
                    string name = Console.ReadLine();
                    if (!addressDict.ContainsKey(name))
                    {
                        List<ContactClass> list = new List<ContactClass>();
                        addressDict.Add(name, list);
                        Console.WriteLine("Address book created");
                        Console.WriteLine("--------------------");
                    }
                    else
                    {
                        Console.WriteLine("An address book with the same name already exists.");
                    }
                }

                else if (option == 2)//For selecting an existing address book
                {
                    Console.WriteLine("Enter the name of the address book to be selected");
                    string book = Console.ReadLine();
                    Console.WriteLine("-------------------");
                    //HandleJson.PopulateDictionary(addressDict);
                    HandleDatabase.RetrieveFromDataBase(addressDict);
                    AddressBookClass.Select(addressDict[book], personCity, personState);
                }

                else if (option == 3)//For searching the persons residing in a city or state
                {
                    Console.WriteLine("Enter the name of a city or a state");
                    string place = Console.ReadLine();
                    Console.WriteLine("-------------------");
                    AddressBookClass.Find(place, addressDict);
                }

                else if (option == 4)//For counting the number of persons residing in a city or state
                {
                    Console.WriteLine("Enter the name of a city or a state");
                    string place = Console.ReadLine();
                    Console.WriteLine("-------------------");
                    AddressBookClass.Count(place, addressDict);
                }

                else if (option == 5)
                {
                    Console.WriteLine("Enter the start date in mm/dd/yyyy format");
                    string startDateString = Console.ReadLine();
                    Console.WriteLine("Enter the end date in mm/dd/yyyy format");
                    string endDateString = Console.ReadLine();
                    DateTime startDate = Convert.ToDateTime(startDateString);
                    DateTime endDate = Convert.ToDateTime(endDateString);
                    AddressBookClass.ContactsAddedInAParticularPeriod(addressDict, startDate, endDate);
                }

                else//End the program and store everything present in addressDict dictionary into the respective files
                {
                    //AddressBookClass.WriteIntoFile(addressDict);
                    HandleDatabase.DeleteAllTables();
                    HandleDatabase.StoreInDataBase(addressDict);
                    break;
                }
            }
        }
    }
}
