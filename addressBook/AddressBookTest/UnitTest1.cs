using addressBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AddressBookTest
{
    [TestClass]
    public class UnitTest1
    {
        Dictionary<string, List<ContactClass>> addressDict = new Dictionary<string, List<ContactClass>>();//store address books
        List<ContactClass> contactClassList = new List<ContactClass>();
        [TestInitialize]
        public void InitializeTestVariables()
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
            contactClassList.Add(varContactClass);

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
            contactClassList.Add(varContactClass2);

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
            contactClassList.Add(varContactClass3);

            addressDict.Add("Address_Book_1", contactClassList);
        }

        [TestMethod]
        public void TestStoreInDataBaseMethodofHandleDataBasePassADictionaryAndReceiveTheNumberOfRowsInserted()
        {
            int result = HandleDatabase.StoreInDataBase(addressDict);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void TestFindOfAddressBookClassPassADictionaryAndGetTrue()
        {
            bool actual = AddressBookClass.Find("City2",addressDict);
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TestCountOfAddressBookClassPassADictionaryAndAPlaceAndGetTheNumberOfPersons()
        {
            int actual = AddressBookClass.Count("City2", addressDict);
            Assert.AreEqual(actual, 1);
        }

        [TestMethod]
        public void TestContactsAddedInAParticularPeriodOfAddressBookClassPassAddressBookDictionaryAlongWithStartDateAndEndDateAndReceieveTheCountOfContactsInTheDateRange()
        {
            DateTime startDate = Convert.ToDateTime("01/01/2020");
            DateTime endDate = Convert.ToDateTime("01/02/2020");
            int noOfContacts=AddressBookClass.ContactsAddedInAParticularPeriod(addressDict, startDate, endDate);
            Assert.AreEqual(2, noOfContacts);
        }

    }
}
