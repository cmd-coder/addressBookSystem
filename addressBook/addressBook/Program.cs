using System;
using System.Runtime.ExceptionServices;

namespace addressBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Address Book Program!");

            var varContact = new contact("First", "Last", "Address", "City", "State", "Phone", "Email", 100050);
            Console.WriteLine(varContact.FirstName + "--" + varContact.LastName + "--" + varContact.Address + "--" + varContact.City + "--" + varContact.State + "--" + varContact.Zip + "--" + varContact.Phone + "--" + varContact.Email);
        }
    }
}
