//Financial Report module
//This module is responsible for keeping all orders of a restaurant. 
//It can read the list of orders from the .txt file representing a
//cashier machine or a kiosk and display the order info to the user.
//Gleb Ignatov

namespace GlebI_modules
{
    internal class Order
    {
        int orderNumber;
        int totalCost;
        List<string> orderItems;
        DateTime orderDateAndTime;     //To initialize: year -> month -> day -> hour -> minute -> second -> ms

        public Order() 
            { orderNumber = 0; totalCost = 0; orderItems = new List<string>(); orderDateAndTime = new DateTime(); }

        public int OrderNumber
            { get { return orderNumber; } set { orderNumber = value; } }

        public int TotalCost 
            { get { return totalCost; } set { totalCost = value; } }

        public List<string> ListOrderItems
            { get { return orderItems; } set { orderItems = new List<string>(value); } }

        public string OrderDateAndTime
            { get { return orderDateAndTime.ToString(); } set { orderDateAndTime = DateTime.Parse(value); } }


        private void displayOrder()
        {
            Console.WriteLine("@Order number: " + this.OrderNumber + "\n");
            Console.WriteLine("@Total cost: " + this.TotalCost + "\n");
            Console.WriteLine("@Order time: " + orderDateAndTime.ToString() + "\n");
            Console.WriteLine("@The list of items:\n");

            foreach(string item in orderItems)
            {
                Console.WriteLine(item + "\n");
            }
        }

        public void DisplayAllOrders(LinkedList<Order> OrderList)
        {
            Console.WriteLine("--------------------\n");
            foreach (Order order in OrderList)
            {
                order.displayOrder();
                Console.WriteLine("--------------------");
            }
        }

        static public LinkedList<Order> ReadFOrdersFromFile()
        {
            LinkedList<Order> tempListOrders = new LinkedList<Order>();
            Order OrdersNode = new Order();

            /* @ Loop through all the line in the file...................................*/
            var lines = File.ReadLines("RestaurantOrders.txt");
            foreach (var line in lines)
            {
                string[] SplitOrdersLine = line.Split('-');
                /* Store information of the food into the node of the list of the food...*/
                bool temp1;

                /* Store the order number of the order in the SplitOrdersLine[0] (int)....*/
                temp1 = int.TryParse(SplitOrdersLine[0], out int OrderNumber);
                if (temp1 == true)
                {
                    OrdersNode.OrderNumber = OrderNumber;
                }

                /* Store the total cost of the order in the SplitOrdersLine[1] (int).*/
                temp1 = int.TryParse(SplitOrdersLine[1], out int TotalCost);
                if (temp1 == true)
                {
                    OrdersNode.TotalCost = TotalCost;
                }

                /* Store the names of items of the order in the SplitOrdersLine[2] (strings)....*/
                string[] SplitItemsLine = SplitOrdersLine[2].Split('|');
                List<string> strings = new List<string>();
                foreach (string item in SplitItemsLine)
                {
                    strings.Add(item);
                }
                OrdersNode.ListOrderItems = strings;

                /* Store the date and time of the order in the SplitOrdersLine[3] (DateTime)....*/
                OrdersNode.OrderDateAndTime = SplitOrdersLine[3];

                /* | Add the node to the list of the orders..................*/
                tempListOrders.AddFirst(OrdersNode);
            }
            return tempListOrders;
        }

    }
}
