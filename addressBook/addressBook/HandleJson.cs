/// Including the requried assemblies in to the program
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace addressBook
{
    class HandleJson
    {
        /// <summary>
        /// The function is written to populate the dictionary from the data stores in the json files
        /// </summary>
        /// <param name="addressDict">the address book to be operated</param>
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

        /// <summary>
        /// The main function is written to test PrimeChecker class
        /// </summary>
        /// <param name="addressBookName">the name of the address book to write data into</param>
        /// <param name="ContactClassList">the list of contacts that will be stored in the files</param>
        public static void JsonHandler(string addressBookName, List<ContactClass> ContactClassList)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(addressBookName))//Create new or replace existing Address Book 1 file
            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonSerializer.Serialize(jsonWriter, ContactClassList);
            }
        }
    }
}
