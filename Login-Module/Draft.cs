//static public bool Checking_Password(Staff_Login temp_login, LinkedList<Staff_Login> temp)
//{
//    Staff_Login current = new Staff_Login();
//    if (temp_login.getUserName == null){
//        return false;
//    }
//    else{
//        current = temp.Find(temp_login);
//    }
//    return true;
//}
//static public void Encrypted()
//{
//    BinaryTree temp = new BinaryTree();
//    AesManaged aes = new AesManaged();
//    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
//    MemoryStream ms = new MemoryStream();

//    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
//    {
//        // Create StreamWriter and write data to a stream    
//        using (StreamWriter sw = new StreamWriter(cs))
//            sw.Write(plainText);
//        encrypted = ms.ToArray();
//    }

//}

//static public LinkedList<Staff_Login> Decrypted_To_Linked_List()
//{

//    Staff_Login temp = new Staff_Login();
//    LinkedList<Staff_Login> list_Account = new LinkedList<Staff_Login>();

//    //var lines = File.ReadLines("Login.txt");
//    //foreach (var line in lines)
//    {
//        string[] Split_List_Item = line.Split('-');

//        // Convert string to the byte[] to decrypted it
//        byte[] byteArray_Username = Encoding.ASCII.GetBytes(Split_List_Item[0]);
//        byte[] byteArray_Password = Encoding.ASCII.GetBytes(Split_List_Item[1]);
//        byte[] byteArray_Key_Decrypted = Encoding.ASCII.GetBytes(Split_List_Item[2]);
//        byte[] byteArray_IV_Decrypted = Encoding.ASCII.GetBytes(Split_List_Item[3]);

//        //string username = Enccypted_Login.Decrypt(byteArray_Username, byteArray_Key_Decrypted, byteArray_IV_Decrypted);
//        //string password = Enccypted_Login.Decrypt(byteArray_Password, byteArray_Key_Decrypted, byteArray_IV_Decrypted);


//        //temp.setUserName(username);
//        //temp.setPassword(password);
//        //list_Account.AddFirst(temp);

//    }
//    return list_Account;
//}