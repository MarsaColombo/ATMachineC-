using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string jsonFile = "./data/cardHolders.json";
        CardHolderManager cardHolderManager = new CardHolderManager();
        List<CardHolder> cardHolders = cardHolderManager.LoadFromJson(jsonFile);

        Console.WriteLine("CardHolders loaded: " + cardHolders.Count);

        Console.WriteLine("Welcome to the ATM!");

        // Example of exporting the cardHolders list
        cardHolderManager.ExportToJson(cardHolders, "./data/exportedCardHolders.json");

        Console.WriteLine("Please enter your card number:");
        string? debitCardNum = "";

        CardHolder? currentUser = null;

        while (currentUser == null)
        {
            debitCardNum = Console.ReadLine()?.Trim();

            currentUser = cardHolders.Find(cardHolder => cardHolder.CardNum == debitCardNum);

            if (currentUser == null)
            {
                Console.WriteLine($"Card not recognized! Entered card number: {debitCardNum}");
                Console.WriteLine("Debug: currentUser is null.");
            }
        }

        Console.WriteLine("Please enter your pin:");
        int userPin = 0;
        while (true)
        {
            if (!int.TryParse(Console.ReadLine(), out userPin))
            {
                Console.WriteLine("Invalid pin! Please enter a valid pin.");
                continue;
            }

            if (currentUser.Pin == userPin)
            {
                break;
            }
            else
            {
                Console.WriteLine("Incorrect pin! Please try again");
            }
        }

        Console.WriteLine($"Welcome, {currentUser.FirstName} {currentUser.LastName}!");

        // ATM system
        while (true)
        {
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Withdraw Money");
            Console.WriteLine("3. Add Money");
            Console.WriteLine("4. Exit");
            Console.Write("Please select an option: ");

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Invalid option. Please try again.");
                continue;
            }

            switch (option)
            {
                case 1:
                    Console.WriteLine($"Your current balance is: {currentUser.Balance:C}");
                    break;

                case 2:
                    Console.Write("Enter the amount to withdraw: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal withdrawalAmount))
                    {
                        if (withdrawalAmount > 0 && withdrawalAmount <= currentUser.Balance)
                        {
                            currentUser.Balance -= withdrawalAmount;
                            Console.WriteLine($"Successfully withdrew {withdrawalAmount:C}. Remaining balance: {currentUser.Balance:C}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount or insufficient funds. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount. Please enter a valid number.");
                    }
                    break;

                case 3:
                    Console.Write("Enter the amount to add: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                    {
                        if (depositAmount > 0)
                        {
                            currentUser.Balance += depositAmount;
                            Console.WriteLine($"Successfully added {depositAmount:C}. New balance: {currentUser.Balance:C}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount. Please enter a positive number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount. Please enter a valid number.");
                    }
                    break;

                case 4:
                    Console.WriteLine("Thank you! Goodbye");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
