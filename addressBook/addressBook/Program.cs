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

            InitializeDefaultValues.SetUpDefaultValues();//Store Deafult Values In The Address Book Dictionary

            //Adding the infoemation in .json file
            //string addressBookName = "Address Book 1.json";

            //HandleJson.JsonHandler(addressBookName, ContactClassList);

            //Testing
            HandleDatabase.DeleteAllTables();//delete all the tables already present tables in the database
            HandleDatabase.StoreInDataBase(addressDict);//store the newly defined tables in the databse

            //HandleJson.PopulateDictionary(addressDict);//Populate the address book dictionary from the present .csv files

            HandleDatabase.RetrieveFromDataBase(addressDict);//Get all the data stored in the database

            OperateOnAddressBook.AddressBookOperations();//Perform all the operations on the address book
        }   
    }
}