using System;
using Microsoft.Extensions.CommandLineUtils;

namespace ConsoleClient
{
  internal class Program
  {
    private static readonly CommandLineApplication App;


    static Program()
    {
      App = new CommandLineApplication {Name = "PC"};
      App.HelpOption("-?|-h|--help");

      App.OnExecute(() =>
      {
        Console.WriteLine(App.GetHelpText());
        return 0;
      });

      App.Command("persons", command =>
      {
        command.Description = "Show the complete list of persons and their pets.";
        command.OnExecute(() => ExecuteCommand(PersonsCommandHandler));
      });

      App.Command("cats", command =>
      {
        command.Description = "Show the list of cat names grouped by the gender of their owner.";
        command.OnExecute(() => ExecuteCommand(CatsCommandHandler));
      });
    }

    private static int ExecuteCommand(Action command)
    {
      try
      {
        command();
        return 0;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error happened\n{ex.Message}");
        return 1;
      }
    }


    private static void PersonsCommandHandler()
    {
      var client = new PersonsApiClient();
      Console.Write(client.GetPersons());
    }

    private static void CatsCommandHandler()
    {
      var client = new PersonsApiClient();
      Console.Write(client.GetCats());
    }


    private static void Main(string[] args)
    {
      App.Execute(args);
    }
  }
}