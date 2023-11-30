
using System.Text.Json;

public class CardHolder
{
    public string CardNum { get; set; }
    public int Pin { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Balance { get; set; }

    // Constructor with parameters
    public CardHolder(string cardNum, int pin, string firstName, string lastName, double balance)
    {
        CardNum = cardNum;
        Pin = pin;
        FirstName = firstName;
        LastName = lastName;
        Balance = balance;
    }

    // Remaining methods remain unchanged
    public string GetNum() { return CardNum; }
    public int GetPin() { return Pin; }
    public string GetFirstName() { return FirstName; }
    public string GetLastName() { return LastName; }
    public double GetBalance() { return Balance; }

    public void SetNum(string newCardNum) { CardNum = newCardNum; }
    public void SetPin(int newPin) { Pin = newPin; }
    public void SetFirstName(string newFirstName) { FirstName = newFirstName; }
    public void SetLastName(string newLastName) { LastName = newLastName; }
    public void SetBalance(double newBalance) { Balance = newBalance; }

    public static void Main(string[] args)
    {
        // Options menu and other methods remain unchanged

        // List of cardHolders
        string jsonFile = "./data/cardHolders.json";
        List<CardHolder> cardHolders = LoadFromJson(jsonFile);
        Console.WriteLine("CardHolders loaded: " + cardHolders.Count);

        // Prompt User
        Console.WriteLine("Welcome to the ATM!");

        // Card Number
        Console.WriteLine("Please enter your card number:");
        string debitCardNum = "";

        CardHolder currentUser = null;

        while (true)
        {
            try
            {
                debitCardNum = Console.ReadLine()?.Trim(); // Trim to remove leading/trailing whitespaces

                // Check against our debitCardNum (case-insensitive)
                currentUser = cardHolders.FirstOrDefault(x => x.CardNum.Equals(debitCardNum, StringComparison.OrdinalIgnoreCase));

                if (currentUser != null)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Card not recognized! Entered card number: {debitCardNum}");
                    Console.WriteLine("Available card numbers:");
                    foreach (var cardHolder in cardHolders)
                    {
                        Console.WriteLine(cardHolder.CardNum);
                    }

                    // Add a check for null currentUser
                    if (currentUser == null)
                    {
                        Console.WriteLine("Debug: currentUser is null.");
                        break;  // Exit the loop if currentUser is null
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please enter a valid card number");
            }
        }

        // Pin
        Console.WriteLine("Please enter your pin:");
        int userPin = 0;
        while (true)
        {
            try
            {
                if (!int.TryParse(Console.ReadLine(), out userPin))
                {
                    Console.WriteLine("Invalid pin! Please enter a valid pin.");
                    continue;
                }

                // Check against our db
                if (currentUser.Pin == userPin)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect pin! Please try again");
                }
            }
            catch
            {
                Console.WriteLine("Please enter a valid pin");
            }
        }

        // Rest of your program remains unchanged

        Console.WriteLine("Thank you! Goodbye");
    }

static List<CardHolder> LoadFromJson(string jsonFilePath)
{
    try
    {
        if (!File.Exists(jsonFilePath))
        {
            throw new FileNotFoundException("Json file not found.", jsonFilePath);
        }

        string jsonString = File.ReadAllText(jsonFilePath);

        if (string.IsNullOrWhiteSpace(jsonString))
        {
            throw new InvalidOperationException("Json file is empty or contains invalid data.");
        }

        List<CardHolder> cardHolders = JsonSerializer.Deserialize<List<CardHolder>>(jsonString);

        return cardHolders;
    }
    catch (FileNotFoundException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        return new List<CardHolder>();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading from JSON: {ex.Message}");
        return new List<CardHolder>();
    }
}
}