using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework4
{
    public class CommanderC
    {
        public void Operation(string command)
        {
            JsonManager manger = new JsonManager();
            switch (command.ToLower())
            {
                case "add": User u = new User() {
                    _firstName = "Rostyk",
                    _id = "17",
                   _secondName = "Diakiv"
                };
                    manger.SereilizeJson<User>("UserSerealization.json", u);
                    break;
                //case "delete": return "Executing command: " + command;
                //case "update": return "Executing command: " + command;
                //case "get": return "Executing command: " + command;
                default: Console.WriteLine("sdgsdg");
                    break;
            }
        }
    }
}
