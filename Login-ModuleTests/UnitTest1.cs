using Login_Module;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Login_ModuleTests
{
    [TestClass]
    public class FeaturesTest
    {
        [TestMethod]
        public void Test_Add_Function_In_BST()
        {
            string username = "MinhNguyen";
            string password = "Hello";

            Staff_Login temp = new Staff_Login();

            temp.setUserName(username);
            temp.setPassword(password);

            BinaryTree.Add(Staff_Login temp)
        }
    }
}