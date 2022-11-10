using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Module
{
    public class Staff_Login
    {
        private string _username = "\0"; 
        private string _password = "\0";
        // The link to decrypted the account
        byte[] IV = null;
        // The key to decrypted the account
        byte[] Key = null;
        public void setUserName(string Username)
        {
            _username = Username;
        }
        public string getUserName()
        {
            return _username;
        }
        public void setPassword(string Password)
        {
            _password = Password;
        }
        public string getPassword()
        {
            return _password;
        }
        public void setKey(byte[] temp_Key)
        {
            Key = temp_Key;
        }
        public byte[] getKey()
        {
            return Key;
        }
        public void setIV(byte[] temp_IV)
        {
            IV = temp_IV;
        }
        public byte[] getIV()
        {
            return IV;
        }
    }
    class Node
    {
        public Node LeftNode { get; set; }
        public Node RightNode { get; set; }
        public Staff_Login Account;
    }
}
