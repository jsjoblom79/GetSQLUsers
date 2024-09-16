// See https://aka.ms/new-console-template for more information

//Gather info to connect to the database
using GetSQLUsers;

Console.ForegroundColor = ConsoleColor.Green;
Console.Write("Enter the server you wish to connect to: ");
var host = Console.ReadLine();
Console.Write("Do you need to enter SQL Credentials? [Y] or [N]: ");
var cred = Console.ReadLine();
var userId = "";
var pwd = "";
var connectionString = "";
//Get User Credentials if necessary
if (cred.ToLower().Equals("y"))
{
    Console.Write("Enter User ID: ");
    userId = Console.ReadLine();
    pwd = Services.GetPassword();

    connectionString = $"Server={host};Database=master;User ID={userId};Password={pwd};Trust Server Certificate=True";
}
else
{
    connectionString = $"Server={host};Database=master;Integrated Security=True;Trust Server Certificate=True";
}

Console.Write("Where do you want to place the output files: ");
var outputFolder = Console.ReadLine();

if (!string.IsNullOrEmpty(outputFolder) && !Directory.Exists(outputFolder))
{
    Directory.CreateDirectory(outputFolder);
}
Console.ForegroundColor = ConsoleColor.DarkCyan;
Console.WriteLine($"Getting Logins for server: {host} - {DateTime.Now:MM/dd/yyyy hh:mm:ss}");
Services.GetSQLUserScripts(connectionString, outputFolder);
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"Login Script Complete. {DateTime.Now:MM/dd/yyyy hh:mm:ss}");
