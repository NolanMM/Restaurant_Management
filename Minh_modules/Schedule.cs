using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Login_Module
{
    public class Schedule_Timetable
    {
        static public void display_Timetable()
        {
            Console.WriteLine("Checking TimeTable\n");
            Console.WriteLine("1. Checking Timetable for the Server\n");
            Console.WriteLine("2. Checking Timetable for the Chef\n");
            Console.WriteLine("3. Return\n");
            bool isNumerical = false;
            int choice = 3;
            string? input = Console.ReadLine();
            while (isNumerical == false)
            {
                isNumerical = int.TryParse(input, out choice);
                if (isNumerical == false) { Console.WriteLine("Wrong Input. Please enter integer\n"); }
            }
            switch (choice)
            {
                case 1:
                    Server server = new Server();
                    server.display_Server_TimeTable();
                    break;
                case 2:
                    Chef chef = new Chef();
                    chef.display_Chef_TimeTable();
                    break;
            }
        }
        public class Server : Time_Table_Draw_Raw
        {
            public void display_Server_TimeTable()
            {
                var lines = new LinkedList<(int index, string name)>();
                string timetable_type = "Server TimeTable";
                Manager(timetable_type, lines);
            }
        }
        public class Chef : Time_Table_Draw_Raw
        {
            public void display_Chef_TimeTable()
            {
                var lines = new LinkedList<(int index, string name)>();
                lines = Modify_Timetable();
                string timetable_type = "Chef TimeTable";
                Manager(timetable_type, lines);
            }
        }

        public class Time_Table_Draw_Raw
        {
            public static void Manager(string timetable_type, LinkedList<(int index, string name)> temp)
            {
                var lines = new List<string[]>();

                string[] Days = new[] { "|" + "Monday", "|" + "Tuesday", "|" + "Wednesday", "|" + "Thursday", "|" + "Friday", "|" + "Saturday", "|" + "Sunday" };
                string[] Shifts = new[] { "11a.m->3p.m", "12p.m->4p.m", "4p.m->8p.m", "5p.m->9p.m" };
                string[] Assign_staff = new[] {"1.x", "2.x", "3.x", "4.x", "5.x", "6.x", "7.x",
                                           "8.x", "9.x", "10.x", "11.x", "12.x", "13.x", "14.x",
                                           "15.x", "16.x", "17.x", "18.x", "19.x", "20.x", "21.x",
                                           "22.x", "23.x", "24.x", "25.x", "26.x", "27.x", "28.x", };
                for (int i = 0; i < Assign_staff.Count(); i++)
                {
                    foreach ((int index, string name) s in temp)
                    {
                        if (i == s.index)
                        {
                            Assign_staff[i] = s.name;
                        }
                    }
                }

                lines.Add(new[] { timetable_type, Days[0], Days[1], Days[2], Days[3], Days[4], Days[5], Days[6] });
                lines.Add(new[] { Shifts[0], "|" + Assign_staff[0], "|" + Assign_staff[1], "|" + Assign_staff[2], "|" + Assign_staff[3], "|" + Assign_staff[4], "|" + Assign_staff[5], "|" + Assign_staff[6] });
                lines.Add(new[] { Shifts[1], "|" + Assign_staff[7], "|" + Assign_staff[8], "|" + Assign_staff[9], "|" + Assign_staff[10], "|" + Assign_staff[11], "|" + Assign_staff[12], "|" + Assign_staff[13] });
                lines.Add(new[] { Shifts[2], "|" + Assign_staff[14], "|" + Assign_staff[15], "|" + Assign_staff[16], "|" + Assign_staff[17], "|" + Assign_staff[18], "|" + Assign_staff[19], "|" + Assign_staff[20] });
                lines.Add(new[] { Shifts[3], "|" + Assign_staff[21], "|" + Assign_staff[22], "|" + Assign_staff[23], "|" + Assign_staff[24], "|" + Assign_staff[25], "|" + Assign_staff[26], "|" + Assign_staff[27] });
                var output = PadElementsInLines(lines, 5);
                Console.WriteLine(output);
            }
            /* |@ Converts a List of string arrays to a string where each element in each line is correctly padded.
               |@ Make sure that each array contains the same amount of elements!*/
            public static string PadElementsInLines(List<string[]> lines, int padding) // = 1
            {
                // Calculate maximum numbers for each element accross all lines
                var numElements = lines[0].Length;
                var maxValues = new int[numElements];
                for (int i = 0; i < numElements; i++)
                {
                    maxValues[i] = lines.Max(x => x[i].Length) + padding;
                }
                var sb = new StringBuilder();
                // Build the output
                bool isFirst = true;
                foreach (var line in lines)
                {
                    if (!isFirst)
                    {
                        sb.AppendLine();
                    }
                    isFirst = false;
                    for (int i = 0; i < line.Length; i++)
                    {
                        var value = line[i];
                        // Append the value with padding of the maximum length of any value for this element
                        sb.Append(value.PadRight(maxValues[i]));
                    }
                }
                return sb.ToString();
            }
            public static LinkedList<(int index, string name)> Modify_Timetable()
            {
                var lines = new LinkedList<(int index, string name)>();

                string[] Days = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
                bool isNumerical = false;
                int portions = 0;
                int temp_value = 0;
                int hour = -1;
                string name;
                int position_In_Table = -1;
                while (isNumerical == false)
                {
                    Console.WriteLine("This is the timetable in the restaurant\n");
                    for (var i = 0; i < 7; i++)
                    {
                        Console.WriteLine($"{i + 1}. {Days[i]}\n");
                    }
                    Console.WriteLine("Please enter the Day you want to work (by words) or (by number)\n");
                    string day = Console.ReadLine();
                    isNumerical = int.TryParse(day, out portions);

                    /* @ Check if the user enter the number */
                    if (isNumerical == true)
                    {
                        if (portions < 8 && portions > 0)
                        {
                            temp_value = portions - 1;
                            isNumerical = true;
                        }
                        else
                        {
                            Console.WriteLine("Please enter the correct input\n");
                            isNumerical = false;

                        }
                        continue;
                    }
                    else
                    {
                        switch (day)
                        {
                            case "Monday":
                                hour = Assign_The_Index_In_TimeTable();
                                switch (hour)
                                {
                                    case 0:
                                        position_In_Table = 0;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 1:
                                        position_In_Table = 7;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 2:
                                        position_In_Table = 14;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 3:
                                        position_In_Table = 21;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;

                                }
                                isNumerical = true;
                                break;
                            case "Tuesday":
                                hour = Assign_The_Index_In_TimeTable();
                                switch (hour)
                                {
                                    case 0:
                                        position_In_Table = 1;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 1:
                                        position_In_Table = 7;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 2:
                                        position_In_Table = 15;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 3:
                                        position_In_Table = 22;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;

                                }
                                isNumerical = true;
                                break;
                            case "Wednesday":
                                hour = Assign_The_Index_In_TimeTable();
                                switch (hour)
                                {
                                    case 0:
                                        position_In_Table = 2;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 1:
                                        position_In_Table = 9;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 2:
                                        position_In_Table = 16;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 3:
                                        position_In_Table = 23;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;

                                }
                                isNumerical = true;
                                break;
                            case "Thursday":
                                hour = Assign_The_Index_In_TimeTable();
                                switch (hour)
                                {
                                    case 0:
                                        position_In_Table = 3;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 1:
                                        position_In_Table = 10;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 2:
                                        position_In_Table = 17;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 3:
                                        position_In_Table = 24;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;

                                }
                                isNumerical = true;
                                break;
                            case "Friday":
                                hour = Assign_The_Index_In_TimeTable();
                                switch (hour)
                                {
                                    case 0:
                                        position_In_Table = 4;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 1:
                                        position_In_Table = 11;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 2:
                                        position_In_Table = 18;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 3:
                                        position_In_Table = 25;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;

                                }
                                isNumerical = true;
                                break;
                            case "Saturday":
                                hour = Assign_The_Index_In_TimeTable();
                                switch (hour)
                                {
                                    case 0:
                                        position_In_Table = 5;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 1:
                                        position_In_Table = 12;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 2:
                                        position_In_Table = 19;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 3:
                                        position_In_Table = 26;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;

                                }
                                isNumerical = true;
                                break;
                            case "Sunday":
                                hour = Assign_The_Index_In_TimeTable();
                                switch (hour)
                                {
                                    case 0:
                                        position_In_Table = 6;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 1:
                                        position_In_Table = 13;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 2:
                                        position_In_Table = 20;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 3:
                                        position_In_Table = 27;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;

                                }
                                isNumerical = true;
                                break;
                            case "monday":
                                hour = Assign_The_Index_In_TimeTable();
                                switch (hour)
                                {
                                    case 0:
                                        position_In_Table = 0;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 1:
                                        position_In_Table = 7;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 2:
                                        position_In_Table = 14;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 3:
                                        position_In_Table = 21;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;

                                }
                                isNumerical = true;
                                break;
                            case "tuesday":
                                hour = Assign_The_Index_In_TimeTable();
                                switch (hour)
                                {
                                    case 0:
                                        position_In_Table = 1;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 1:
                                        position_In_Table = 7;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 2:
                                        position_In_Table = 15;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 3:
                                        position_In_Table = 22;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;

                                }
                                isNumerical = true;
                                break;
                            case "wednesday":
                                hour = Assign_The_Index_In_TimeTable();
                                switch (hour)
                                {
                                    case 0:
                                        position_In_Table = 2;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 1:
                                        position_In_Table = 9;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 2:
                                        position_In_Table = 16;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 3:
                                        position_In_Table = 23;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;

                                }
                                isNumerical = true;
                                break;
                            case "thursday":
                                hour = Assign_The_Index_In_TimeTable();
                                switch (hour)
                                {
                                    case 0:
                                        position_In_Table = 3;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 1:
                                        position_In_Table = 10;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 2:
                                        position_In_Table = 17;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 3:
                                        position_In_Table = 24;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;

                                }
                                isNumerical = true;
                                break;
                            case "friday":
                                hour = Assign_The_Index_In_TimeTable();
                                switch (hour)
                                {
                                    case 0:
                                        position_In_Table = 4;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 1:
                                        position_In_Table = 11;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 2:
                                        position_In_Table = 18;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 3:
                                        position_In_Table = 25;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;

                                }
                                isNumerical = true;
                                break;
                            case "saturday":
                                hour = Assign_The_Index_In_TimeTable();
                                switch (hour)
                                {
                                    case 0:
                                        position_In_Table = 5;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 1:
                                        position_In_Table = 12;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 2:
                                        position_In_Table = 19;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 3:
                                        position_In_Table = 26;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;

                                }
                                isNumerical = true;
                                break;
                            case "sunday":
                                hour = Assign_The_Index_In_TimeTable();
                                switch (hour)
                                {
                                    case 0:
                                        position_In_Table = 6;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 1:
                                        position_In_Table = 13;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 2:
                                        position_In_Table = 20;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;
                                    case 3:
                                        position_In_Table = 27;
                                        Console.WriteLine("Please enter your Name\n");
                                        name = Console.ReadLine();
                                        lines.AddFirst((position_In_Table, name));
                                        break;

                                }
                                isNumerical = true;
                                break;
                        }
                    }

                }
                return lines;


            }

            static public int Assign_The_Index_In_TimeTable()
            {
                string[] Shifts = new[] { "11a.m->3p.m", "12p.m->4p.m", "4p.m->8p.m", "5p.m->9p.m" };
                bool isNumerical = false;
                int portions = 0;
                int return_Value = 0;
                while (isNumerical == false)
                {
                    Console.WriteLine("This is the timetable in the restaurant\n");
                    for (var i = 0; i < 4; i++)
                    {
                        Console.WriteLine($"{i + 1}. {Shifts[i]}\n");
                    }
                    Console.WriteLine("Please enter the Hour you want to work (by words) or (by number)\n");
                    string hour = Console.ReadLine();
                    isNumerical = int.TryParse(hour, out portions);

                    /* @ Check if the user enter the number */
                    if (isNumerical == true)
                    {
                        if (portions < 5 && portions > 0)
                        {
                            return_Value = portions - 1;
                            isNumerical = true;
                            return return_Value;
                        }
                        else
                        {
                            Console.WriteLine("Please enter the correct input\n");
                            isNumerical = false;

                        }
                        continue;
                    }
                    else
                    {
                        switch (hour)
                        {
                            case "11a.m->3p.m":
                                return_Value = 0;
                                isNumerical = true;
                                break;
                            case "12p.m->4p.m":
                                return_Value = 1;
                                isNumerical = true;
                                break;
                            case "4p.m->8p.m":
                                return_Value = 2;
                                isNumerical = true;
                                break;
                            case "5p.m->9p.m":
                                return_Value = 3;
                                isNumerical = true;
                                break;
                            default:
                                isNumerical = false;
                                break;
                        }
                    }

                }
                return return_Value;
            }


        }


    }

}