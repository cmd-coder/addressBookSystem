using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace addressBook
{
    public class Program
    {
        //C:\Users\dell\source\repos\addressBookSystem\addressBook\addressBook\bin\Debug\netcoreapp3.1
        static List<ContactClass> ContactClassList = new List<ContactClass>();//store contacts in an address book
        static Dictionary<string, List<ContactClass>> addressDict = new Dictionary<string, List<ContactClass>>();//store address books
        static Dictionary<string, string> personCity = new Dictionary<string, string>();//maintain a dictionary of a person and his city
        static Dictionary<string, string> personState = new Dictionary<string, string>();//maintain a dictionary of a person and his state

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Address Book Program!");

            SetUpDefaultValues();

            //Adding the infoemation in .json file
            //string addressBookName = "Address Book 1.json";

            //HandleJson.JsonHandler(addressBookName, ContactClassList);

            //Testing
            HandleDatabase.DeleteAllTables();
            HandleDatabase.StoreInDataBase(addressDict);

            //HandleJson.PopulateDictionary(addressDict);//Populate the address book dictionary from the present .csv files

            HandleDatabase.RetrieveFromDataBase(addressDict);

            AddressBookOperations();
        }

        public static void SetUpDefaultValues()
        {
            ContactClass varContactClass = new ContactClass()//Default data that will be present in the address book at the start of the program
            {
                First = "First",
                Last = "Last",
                Address = "Address",
                City = "City",
                State = "State",
                Phone = "phone",
                Email = "email",
                Zip = 10066,
                DateAdded = Convert.ToDateTime("01/01/2020")
            };
            ContactClassList.Add(varContactClass);
            personCity.Add("First Last", "City");
            personState.Add("First Last", "State");

            ContactClass varContactClass2 = new ContactClass()//Default data that will be present in the address book at the start of the program
            {
                First = "First2",
                Last = "Last2",
                Address = "Address2",
                City = "City2",
                State = "State2",
                Phone = "phone2",
                Email = "email2",
                Zip = 100667,
                DateAdded = Convert.ToDateTime("01/02/2020")
            };
            ContactClassList.Add(varContactClass2);
            personCity.Add("First2 Last2", "City2");
            personState.Add("First2 Last2", "State2");

            ContactClass varContactClass3 = new ContactClass()//Default data that will be present in the address book at the start of the program
            {
                First = "First3",
                Last = "Last3",
                Address = "Address3",
                City = "City3",
                State = "State3",
                Phone = "phone3",
                Email = "email3",
                Zip = 100668,
                DateAdded = Convert.ToDateTime("01/03/2020")
            };
            ContactClassList.Add(varContactClass3);
            personCity.Add("First3 Last3", "City3");
            personState.Add("First3 Last3", "State3");

            addressDict.Add("Address_Book_1", ContactClassList);

        }

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

                else if(option==5)
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