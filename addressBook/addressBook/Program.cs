using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace addressBook
{
    class Program
    {
        //C:\Users\dell\source\repos\addressBookSystem\addressBook\addressBook\bin\Debug\netcoreapp3.1
        static List<ContactClass> ContactClassList = new List<ContactClass>();
        static Dictionary<string, List<ContactClass>> addressDict = new Dictionary<string, List<ContactClass>>();
        static Dictionary<string, string> personCity = new Dictionary<string, string>();
        static Dictionary<string, string> personState = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Address Book Program!");

            ContactClass varContactClass = new ContactClass()
            {
                first = "First",
                last = "Last",
                address="Address",
                city="City",
                state="State",
                phone="phone",
                email="email",
                zip=10066
            };
            ContactClassList.Add(varContactClass);
            personCity.Add("First Last", "City");
            personState.Add("First Last", "State");

            ContactClass varContactClass2 = new ContactClass()
            {
                first = "First2",
                last = "Last2",
                address = "Address2",
                city = "City2",
                state = "State2",
                phone = "phone2",
                email = "email2",
                zip = 100667
            };
            ContactClassList.Add(varContactClass2);
            personCity.Add("First2 Last2", "City2");
            personState.Add("First2 Last2", "State2");

            ContactClass varContactClass3 = new ContactClass()
            {
                first = "First3",
                last = "Last3",
                address = "Address3",
                city = "City3",
                state = "State3",
                phone = "phone3",
                email = "email3",
                zip = 100668
            };
            ContactClassList.Add(varContactClass3);
            personCity.Add("First3 Last3", "City3");
            personState.Add("First3 Last3", "State3");

            /*var varContactClass2 = new ContactClass("First2", "Last2", "Address2", "City2", "State2", "Phone2", "Email2", 200050);
            ContactClassList.Add(varContactClass2);
            personCity.Add("First2 Last2", "City2");
            personState.Add("First2 Last2", "State2");

            var varContactClass3 = new ContactClass("First3", "Last3", "Address3", "City3", "State3", "Phone3", "Email3", 300050);
            ContactClassList.Add(varContactClass3);
            personCity.Add("First3 Last3", "City3");
            personState.Add("First3 Last3", "State3");*/

            addressDict.Add("Address Book 1", ContactClassList);

            //Adding the infoemation in .txt file
            string addressBookName = "Address Book 1";

            using (StreamWriter sw = new StreamWriter(addressBookName))//Create new or replace existing Address Book 1 file
            using (var csv = new CsvWriter(sw, CultureInfo.InvariantCulture))
            {
                //csv.Configuration.HasHeaderRecord = false;
                csv.WriteRecords(ContactClassList);
            }

            AddressBookClass.PopulateDictionary(addressDict);//Populate the address book dictionary from the present .txt files
            
            while (true)
            {
                Console.WriteLine("The address books in the system are:");
                foreach(KeyValuePair<string, List<ContactClass>> j in addressDict)
                {
                    Console.WriteLine(j.Key);
                }

                Console.WriteLine("---------------------------");
                Console.WriteLine("Enter:\n1 for adding a new address book\n2 for selecting an existing one\n3 to find a person residing in a city or a state\n4 to find the number of ContactClass persons residing in a city or state\n5 to exit");
                int option = Convert.ToInt32(Console.ReadLine());
                if (option == 1)
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

                else if (option == 2)
                {
                    Console.WriteLine("Enter the name of the address book to be selected");
                    string book = Console.ReadLine();
                    Console.WriteLine("-------------------");
                    AddressBookClass.PopulateDictionary(addressDict);
                    AddressBookClass.Select(addressDict[book], personCity, personState);
                }

                else if(option == 3)
                {
                    Console.WriteLine("Enter the name of a city or a state");
                    string place = Console.ReadLine();
                    Console.WriteLine("-------------------");
                    AddressBookClass.Find(place, addressDict);
                }

                else if(option==4)
                {
                    Console.WriteLine("Enter the name of a city or a state");
                    string place = Console.ReadLine();
                    Console.WriteLine("-------------------");
                    AddressBookClass.Count(place, addressDict);
                }

                else
                {
                    AddressBookClass.WriteIntoFile(addressDict);
                    break;
                }
            }

        }

    }
}