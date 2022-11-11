using Login_Module;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Login_Module
{
    [TestClass]
     public class FeaturesTest
    {
        [TestMethod]
        public void Test_Add_Function_In_BST()
        {
            string username = "MinhNguyen";
            string password = "Hello";
            //BinaryTree var_temp = new BinaryTree();
            Staff_Login temp = new Staff_Login();

            temp.setUserName(username);
            temp.setPassword(password);

            //bool flag_test = var_temp.Add(temp);

            //Assert.IsTrue(flag_test);
        }
    }
}