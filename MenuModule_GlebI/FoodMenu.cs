//Menu module
//This module is responsible for keeping the menu of a restaurant. 
//It can read the menu from the .txt file, write to the .txt file, display and change the menu,
//and pass the menu to the Inventory Checking module.
//Gleb Ignatov

class Item
{
    string name;
    string description;
    int calories;
    float weight;
    float price;

    public Item() { }

    public Item(string name, string description, float weight, float price)
    {
        this.name = name;
        this.description = description;
        this.weight = weight;
        this.price = price;
    }
    public Item(string name, string description, int calories, float price)
    {
        this.name = name;
        this.description = description;
        this.calories = calories;
        this.price = price;
    }

    public Item(string name, string description, int calories, float weight, float price)
    {
        this.name = name;
        this.description = description;
        this.calories = calories;
        this.weight = weight;
        this.price = price;
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Description
    {
        get { return description; } 
        set { description = value; }
    }

    public float Price
    {
        get { return price; }
        set { price = value; }
    }

    public int Calories
    {
        get { return calories; }
        set { calories = value; }
    }

    public float Weight
    {
        get { return weight; }
        set { weight = value; }
    }

    private void displayFoodItem()
    {
        Console.WriteLine("@Name: " + this.Name + "\n");
        Console.WriteLine("@Description: " + this.Description + "\n");
        Console.WriteLine("@Calories: " + this.Calories + "\n");
        Console.WriteLine("@Weight: " + this.Weight + "\n");
        Console.WriteLine("@Price: " + this.Price + "\n");
    }

    public void DisplayAllFoodItems(LinkedList<Item> listFoodMenu)
    {
        Console.WriteLine("--------------------\n");
        foreach (Item item in listFoodMenu)
        {
            item.displayFoodItem();
            Console.WriteLine("--------------------");
        }
    }

    static public LinkedList<Item> ReadFoodMenuFromFile()
    {
        LinkedList<Item> tempListFoodMenu = new LinkedList<Item>();
        Item FoodMenuNode = new Item();

        /* @ Loop through all the line in the file...................................*/
        var lines = File.ReadLines("FoodMenu.txt");
        foreach (var line in lines)
        {
            /* Store the Name of the food in the SplitFoodMenuLine[0] (String)....*/
            /* Store the description of the food in the SplitFoodMenuLine[1] (String).*/
            string[] SplitFoodMenuLine = line.Split('-');

            /* Store information of the food into the node of the list of the food...*/
            FoodMenuNode.Name = SplitFoodMenuLine[0];
            FoodMenuNode.Description = SplitFoodMenuLine[1];

            /* Store the calories of the food in the SplitFoodMenuLine[2] (int).*/
            bool temp1 = int.TryParse(SplitFoodMenuLine[2], out int Calories);
            if (temp1 == true)
            {
                FoodMenuNode.Calories = Calories;
            }

            /* Store the weight of the food in the SplitFoodMenuLine[3] (float).*/
            temp1 = float.TryParse(SplitFoodMenuLine[3], out float Weight);
            if (temp1 == true)
            {
                FoodMenuNode.Weight = Weight;
            }

            /* Store the price of the food in the SplitFoodMenuLine[4] (float).*/
            temp1 = float.TryParse(SplitFoodMenuLine[4], out float Price);
            if (temp1 == true)
            {
                FoodMenuNode.Price = Price;
            }

            /* | Add the node to the list of the food in the fridge..................*/
            tempListFoodMenu.AddFirst(FoodMenuNode);
        }
        return tempListFoodMenu;
    }

    static public void StoreFoodMenuToFile(LinkedList<Item> listFoodMenu)
    {
        string filename = "FoodMenu.txt";
        if (File.Exists(filename))
        {
            /* Delete the file if it exsist to update new information whenever write to file again */
            File.Delete(filename);
            File.Create(filename);
        }
        else if (!File.Exists(filename)) { File.Create(filename); }

        /* @ Loop thourgh all the list and write it into the file line by line */
        foreach (Item item in listFoodMenu)
        {
            /* @ The inventory_line layout is "Name_Of_Food-Portion_Number" */
            string itemOfMenu_line = item.Name + "-" + item.Description + "-" + 
                item.Calories.ToString() + "-" + item.Weight.ToString() + "-" + item.Price.ToString();
            File.WriteAllText(filename, itemOfMenu_line);
        }
    }
};



