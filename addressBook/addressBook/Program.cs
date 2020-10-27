﻿using System;
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
    class Program
    {
        //C:\Users\dell\source\repos\addressBookSystem\addressBook\addressBook\bin\Debug\netcoreapp3.1
        static List<ContactClass> ContactClassList = new List<ContactClass>();//store contacts in an address book
        static Dictionary<string, List<ContactClass>> addressDict = new Dictionary<string, List<ContactClass>>();//store address books
        static Dictionary<string, string> personCity = new Dictionary<string, string>();//maintain a dictionary of a person and his city
        static Dictionary<string, string> personState = new Dictionary<string, string>();//maintain a dictionary of a person and his state

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Address Book Program!");

            ContactClass varContactClass = new ContactClass()//Default data that will be present in the address book at the start of the program
            {
                First = "First",
                Last = "Last",
                Address="Address",
                City="City",
                State="State",
                Phone="phone",
                Email="email",
                Zip=10066
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
                Zip = 100667
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
                Zip = 100668
            };
            ContactClassList.Add(varContactClass3);
            personCity.Add("First3 Last3", "City3");
            personState.Add("First3 Last3", "State3");

            addressDict.Add("Address Book 1", ContactClassList);

            //Adding the infoemation in .txt file
            string addressBookName = "Address Book 1.json";

            JsonSerializer jsonSerializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(addressBookName))//Create new or replace existing Address Book 1 file
            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonSerializer.Serialize(jsonWriter,ContactClassList);
            }

            AddressBookClass.PopulateDictionary(addressDict);//Populate the address book dictionary from the present .csv files
            
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
                    AddressBookClass.PopulateDictionary(addressDict);
                    AddressBookClass.Select(addressDict[book], personCity, personState);
                }

                else if(option == 3)//For searching the persons residing in a city or state
                {
                    Console.WriteLine("Enter the name of a city or a state");
                    string place = Console.ReadLine();
                    Console.WriteLine("-------------------");
                    AddressBookClass.Find(place, addressDict);
                }

                else if(option==4)//For counting the number of persons residing in a city or state
                {
                    Console.WriteLine("Enter the name of a city or a state");
                    string place = Console.ReadLine();
                    Console.WriteLine("-------------------");
                    AddressBookClass.Count(place, addressDict);
                }

                else//End the program and store everything present in addressDict dictionary into the respective files
                {
                    AddressBookClass.WriteIntoFile(addressDict);
                    break;
                }
            }

        }

    }
}