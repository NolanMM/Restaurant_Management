using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble___Restaurant_System
{
    public class Staff_Login
    {
        private string _username;
        private string _password;

        private string First_Name;
        private string Last_Name;
        private string Role;
        private float Salary;
        private string _recovery_email;

        
        public void setUserName(string Username) { _username = Username; }
        public string getUserName() { return _username; }
        public void setPassword(string Password) { _password = Password; }
        public string getPassword() { return _password; }
        public void setRecovery(string Recovery) { _recovery_email = Recovery; }
        public string getRecovery() { return _recovery_email; }
        public void set_First_Name(string firstname) { First_Name = firstname; }
        public string get_First_Name() { return First_Name; }
        public void set_Last_Name(string lastname) { Last_Name = lastname; }
        public string get_Last_Name() { return Last_Name; }
        public void set_Role(string role) { Role = role; }
        public string get_Role() { return Role; }
        public void set_Salary(float salary) { Salary = salary; }
        public float get_Salary() { return Salary; }
    }
}
