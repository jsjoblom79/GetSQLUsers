using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSQLUsers
{
    public class Services
    {
        public static string GetPassword()
        {
            var pwd = "";

            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    if (pwd.Length > 0)
                    {
                        pwd.Remove(pwd.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (i.KeyChar != '\u0000')
                {
                    pwd += i.KeyChar;
                    Console.Write("*");
                }
            }
            return pwd;
        }

        public static void GetSQLUserScripts(string connectionString, string outputFolder)
        {

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                ServerConnection serverConnection = new ServerConnection(connection);
                Server server = new Server(serverConnection);

                LoginCollection logins = server.Logins;
                var loginInfo = "";
                foreach (Login login in logins)
                {
                    if (!login.IsSystemObject)
                    {
                        if (!login.IsDisabled)
                        {
                            foreach (var line in login.Script())
                            {
                                loginInfo += line + "\n";
                            }
                        }
                    }
                    

                    loginInfo += "\n";
                }

                File.WriteAllText($"{outputFolder}\\UserLogins_{server.Name}.sql", loginInfo);
            }

        }
    }
}
