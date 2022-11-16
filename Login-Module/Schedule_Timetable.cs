using System;
using System.Collections.Generic;
using System.Linq;
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

        public class Time_Table_Draw_Raw
        {
            /* |@ Converts a List of string arrays to a string where each element in each line is correctly padded.
               |@ Make sure that each array contains the same amount of elements!*/
            public static void Manager(string timetable_type)
            {
                var lines = new List<string[]>();

                string[] Days = new[] { "|" + "Monday", "|" + "Tuesday", "|" + "Wednesday", "|" + "Thursday", "|" + "Friday", "|" + "Saturday", "|" + "Sunday" };
                string[] Shifts = new[] { "11a.m->3p.m", "12p.m->4p.m", "4p.m->8p.m", "5p.m->9p.m" };
                string[] Assign_staff = new[] {"x", "x", "x", "x", "x", "x", "x",
                                           "x", "x", "x", "x", "x", "x", "x",
                                           "x", "x", "x", "x", "x", "x", "x",
                                           "x", "x", "x", "x", "x", "x", "x", };

                lines.Add(new[] { timetable_type, Days[0], Days[1], Days[2], Days[3], Days[4], Days[5], Days[6] });
                lines.Add(new[] { Shifts[0], "|" + Assign_staff[0], "|" + Assign_staff[1], "|" + Assign_staff[2], "|" + Assign_staff[3], "|" + Assign_staff[4], "|" + Assign_staff[5], "|" + Assign_staff[6] });
                lines.Add(new[] { Shifts[1], "|" + Assign_staff[7], "|" + Assign_staff[8], "|" + Assign_staff[9], "|" + Assign_staff[10], "|" + Assign_staff[11], "|" + Assign_staff[12], "|" + Assign_staff[13] });
                lines.Add(new[] { Shifts[2], "|" + Assign_staff[14], "|" + Assign_staff[15], "|" + Assign_staff[16], "|" + Assign_staff[17], "|" + Assign_staff[18], "|" + Assign_staff[19], "|" + Assign_staff[20] });
                lines.Add(new[] { Shifts[3], "|" + Assign_staff[21], "|" + Assign_staff[22], "|" + Assign_staff[23], "|" + Assign_staff[24], "|" + Assign_staff[25], "|" + Assign_staff[26], "|" + Assign_staff[27] });
                var output = PadElementsInLines(lines, 5);
                Console.WriteLine(output);
            }
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

        }
        public class Server : Time_Table_Draw_Raw
        {
            public void display_Server_TimeTable()
            {
                string timetable_type = "Server TimeTable";
                Manager(timetable_type);
            }
        }
        public class Chef : Time_Table_Draw_Raw
        {
            public void display_Chef_TimeTable()
            {
                string timetable_type = "Chef TimeTable";
                Manager(timetable_type);
            }
        }

    }

}