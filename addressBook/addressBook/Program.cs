using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;

namespace addressBook
{
    class Program
    {

        static List<contact> contactList = new List<contact>();
        static Dictionary<string, List<contact>> addressDict = new Dictionary<string, List<contact>>();
        static Dictionary<string, string> personCity = new Dictionary<string, string>();
        static Dictionary<string, string> personState = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Address Book Program!");

            var varContact = new contact("First5", "Last", "Address", "City", "State", "Phone", "Email", 100050);
            contactList.Add(varContact);
            personCity.Add("First Last", "City");
            personState.Add("First Last", "State");

            var varContact2 = new contact("First2", "Last2", "Address2", "City2", "State2", "Phone2", "Email2", 200050);
            contactList.Add(varContact2);
            personCity.Add("First2 Last2", "City2");
            personState.Add("First2 Last2", "State2");

            var varContact3 = new contact("First3", "Last3", "Address3", "City3", "State3", "Phone3", "Email3", 300050);
            contactList.Add(varContact3);
            personCity.Add("First3 Last3", "City3");
            personState.Add("First3 Last3", "State3");
            //Console.WriteLine(varContact.FirstName + "--" + varContact.LastName + "--" + varContact.Address + "--" + varContact.City + "--" + varContact.State + "--" + varContact.Zip + "--" + varContact.Phone + "--" + varContact.Email);

            addressDict.Add("Address Book 1", contactList);

            while (true)
            {
                Console.WriteLine("The address books in the system are:");
                foreach(KeyValuePair<string, List<contact>> j in addressDict)
                {
                    Console.WriteLine(j.Key);
                }

                Console.WriteLine("---------------------------");
                Console.WriteLine("Enter:\n1 for adding a new address book\n2 for selecting an existing one\n3 to find a person residing in a city or a state\n4 to find the number of contact persons residing in a city or state\n5 to exit");
                int option = Convert.ToInt32(Console.ReadLine());
                if (option == 1)
                {
                    Console.WriteLine("Enter the name of the address book");
                    string name = Console.ReadLine();
                    List<contact> list = new List<contact>();
                    //Console.WriteLine("Enter the contacts");
                    //list = AddContact();
                    addressDict.Add(name, list);
                    Console.WriteLine("Address book created");
                    Console.WriteLine("--------------------");
                }

                else if (option == 2)
                {
                    Console.WriteLine("Enter the name of the address book to be selected");
                    string book = Console.ReadLine();
                    Console.WriteLine("-------------------");
                    AddressBookClass.select(addressDict[book], personCity, personState);
                }

                else if(option == 3)
                {
                    Console.WriteLine("Enter the name of a city or a state");
                    string place = Console.ReadLine();
                    Console.WriteLine("-------------------");
                    AddressBookClass.find(place, addressDict);
                }

                else if(option==4)
                {
                    Console.WriteLine("Enter the name of a city or a state");
                    string place = Console.ReadLine();
                    Console.WriteLine("-------------------");
                    AddressBookClass.count(place, addressDict);
                }

                else
                    break;
            }

        }

    }
}