/// Including the requried assemblies in to the program
using System;
using System.Collections.Generic;
using System.Text;

namespace addressBook
{
    class InitializeDefaultValues
    {
        /// <summary>
        /// The function is written to store default values in the variabes which will be later used in the program
        /// </summary>
        public static void SetUpDefaultValues()
        {
            List<ContactClass> ContactClassList = new List<ContactClass>();//store contacts in an address book
            Dictionary<string, List<ContactClass>> addressDict = new Dictionary<string, List<ContactClass>>();//store address books
            Dictionary<string, string> personCity = new Dictionary<string, string>();//maintain a dictionary of a person and his city
            Dictionary<string, string> personState = new Dictionary<string, string>();//maintain a dictionary of a person and his state


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
    }
}
