using System;
using System.Collections.Generic;
using static Assemble___Restaurant_System.Inventory_Stub_Class;

namespace Assemble___Restaurant_System
{
    public static class Inventory_Functions
    {
        static public void Checking_Inventory()
        {
            LinkedList<Inventory_Food> list_food = Read_The_Inventory_From_File();
            Console.WriteLine("The Inventory\n");
            foreach (Inventory_Food food in list_food)
            {
                Console.WriteLine("@Name: " + food.getNameFood() + " " + "@Portions:" + food.getPortionFood());
                Console.WriteLine("\n");
            }
        }
        static public LinkedList<Inventory_Food> Read_The_Inventory_From_File()
        {
            LinkedList<Inventory_Food> temp_List_In_Inventory = new LinkedList<Inventory_Food>();
            Inventory_Food inventory_Food_node = new Inventory_Food();

            /* @ Loop through all the line in the file...................................*/
            var lines = File.ReadLines("Inventory.txt");
            foreach (var line in lines)
            {
                /* Store the Name of the food in the Split_List_Item_Food[0] (String)....*/
                /* Store the Portion of the food in the Split_List_Item_Food[1] (String).*/
                string[] Split_List_Item_Food = line.Split('-');

                /* Store information of the food into the node of the list of the food...*/
                inventory_Food_node.setNameFood(Split_List_Item_Food[0]);
                bool temp1 = int.TryParse(Split_List_Item_Food[1], out int Portions);
                if (temp1 == true)
                {
                    inventory_Food_node.setPortionFood(Portions);
                }
                /* | Add the node to the list of the food in the fridge..................*/
                temp_List_In_Inventory.AddFirst(inventory_Food_node);
            }
            return temp_List_In_Inventory;
        }

        static public bool Checking_Specific_Item_In_Inventory()
        {
            string name_item;
            LinkedList<Inventory_Food> list_food = Read_The_Inventory_From_File();
            int number_of_items = list_food.Count();
            bool flag = false;
            Inventory_Food find_item = new Inventory_Food();
            Console.WriteLine("Please enter the name of the item you want to check correctly: ");
            name_item = Console.ReadLine();
            foreach (Inventory_Food food in list_food)
            {
                if (food.getNameFood() == name_item)
                {
                    find_item = food;
                    flag = true;
                    break;
                }
                else
                {
                    flag = false;
                }
            }
            if (flag == true)
            {
                /* @ Using switch instead of else if statement for practice new method 
                /* @ The Code below equal with 
                /* @ if(find_item.getPortionFood() == 0 ){
                /* @    Console.WriteLine("The item: " +find_item.getNameFood() + " is empty inside the fridge\n"); 
                /* @ }
                /* @ else{ 
                /* @    Console.WriteLine("Found item in the fridge\n");
                /* @    Console.WriteLine("@Name: " + find_item.getNameFood() + " " + "@Portions:" + find_item.getPortionFood());
                /* @ }*/

                switch (find_item.getPortionFood())
                {
                    case 0:
                        Console.WriteLine("The item: " + find_item.getNameFood() + " is empty inside the fridge\n");
                        break;
                    default:
                        Console.WriteLine("Found item in the fridge\n");
                        Console.WriteLine("@Name: " + find_item.getNameFood() + " " + "@Portions:" + find_item.getPortionFood());
                        break;
                }
                
            }
            else
            {
                
                Console.WriteLine("The item not in the list\n");
            }
            return flag;
            
        }
        static public void Modify_The_Inventory()
        {
            /* @ First Take the List from file */
            LinkedList<Inventory_Food> list_food = Read_The_Inventory_From_File();

            /* @ Print the inventory list */
            Checking_Inventory();

            /*            |@ Modify the inventory list @|                  */
            /* @ 1. Take the name of the item(string) staff want to modify */
            /* @ 2. Take the node of the input item from the list of food  */
            /* @ 3. Take the portion(int) wants to modify                  */
            /* @ 4. Modify the node in the list                            */

            /* @ 1. Take the name of the item(string) staff want to modify */
            Console.WriteLine("Enter the name of the item\n");
            string name = Console.ReadLine();
            /* @ 1. End*/

            /* @ 2. Take the node of the input item from the list of food..*/
            Inventory_Food item_modify = Node_From_List_by_name_of_Food_in_Inventory_List(name, list_food);
            /* @ 2. End*/

            if (item_modify != null)
            {
                /* @ 3. Take the portion(int) wants to modify..............*/
                /* @ Just take the integer, Using while loop to check for  */
                /* @ the input from user                                   */
                bool isNumerical = false;
                int portions = item_modify.getPortionFood();
                while (isNumerical == false)
                {
                    Console.WriteLine("Enter the number of the items inside fridge: ");
                    string input = Console.ReadLine();


                    /* @ TryParse method will try to convert the string to a number (integer) and 
                     * @ if it succeeds it will return true and 
                     * @ place the corresponding number in myInt. 
                     * @ If it can't, it returns false.........................................*/
                    isNumerical = int.TryParse(input, out portions);

                    /* @ Send message when receive wrong input */
                    if (isNumerical == false) { Console.WriteLine("Wrong Input. Please enter integer\n"); }
                }
                /* @ 3. End*/

                /* @ 4. Modify the node in the list........................*/
                item_modify.setPortionFood(portions);
                /* @ 4. End*/
            }

            /* @ Store the new inventory list to the file */
            Store_The_Inventory_To_File(list_food);

            /* @ Send the message to notice about the change */
            Console.WriteLine("Modify the inventory successfully\n");

        }
        static public Inventory_Food Node_From_List_by_name_of_Food_in_Inventory_List(string name, LinkedList<Inventory_Food> list_food)
        {
            Inventory_Food found_item = new Inventory_Food();
            foreach (Inventory_Food item in list_food)
            {
                /* @ Compare string with the  name of the food in the node in the list of the food items */
                if (name.CompareTo(item.getNameFood()) == 0)
                {
                    found_item = item;
                }
            }
            /* @ If found the item the variable found_item has value = item(node) */
            /* @ Else the found_item variable will be null                        */
            return found_item;
        }
        static public void Store_The_Inventory_To_File(LinkedList<Inventory_Food> list_food)
        {
            string filename = "Inventory.txt";
            if (File.Exists(filename))
            {
                /* Delete the file if it exsist to update new information whenever write to file again */
                File.Delete(filename);
                File.Create(filename);
            }
            else if (!File.Exists(filename)) { File.Create(filename); }

            /* @ Loop thourgh all the list and write it into the file line by line */
            foreach (Inventory_Food item in list_food)
            {
                /* @ The inventory_line layout is "Name_Of_Food-Portion_Number" */
                string inventory_line = item.getNameFood() + "-" + item.getPortionFood().ToString();
                File.WriteAllText(filename, inventory_line);
            }
        }
    }
}