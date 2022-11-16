using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Module
{
    public class Staff_Login
    {
        private string _username; 
        private string _password;

        // The matrix to decrypted the account
        private byte[] IV = null;
        // The key to decrypted the account
        private byte[] Key = null;

        private string _recovery_email;
        public void setUserName(string Username){_username = Username;}
        public string getUserName(){return _username;}
        public void setPassword(string Password){_password = Password;}
        public string getPassword(){return _password;}
        public void setKey(byte[] temp_Key){Key = temp_Key;}
        public byte[] getKey(){return Key;}
        public void setIV(byte[] temp_IV){IV = temp_IV;}
        public byte[] getIV(){return IV;}
        public void setRecovery(string Recovery){_recovery_email = Recovery;}
        public string getRecovery(){return _recovery_email;}
    }
    public class Inventory_Food
    {
        private string _Name;
        private int Portions;
        public void setNameFood(string name){_Name = name;}
        public string getNameFood(){return _Name;}
        public void setPortionFood(int portions) { Portions = portions; }
        public int getPortionFood() { return Portions; }
    }
    public class Items
    {

    }


}
