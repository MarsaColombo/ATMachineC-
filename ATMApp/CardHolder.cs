
public class CardHolder
{
    public string CardNum { get; set; }
    public int Pin { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Balance { get; set; }

    // Constructor with parameters
    public CardHolder(string cardNum, int pin, string firstName, string lastName, decimal balance)
    {
        CardNum = cardNum;
        Pin = pin;
        FirstName = firstName;
        LastName = lastName;
        Balance = balance;
    }

    public string GetNum() { return CardNum; }
    public int GetPin() { return Pin; }
    public string GetFirstName() { return FirstName; }
    public string GetLastName() { return LastName; }
    public decimal GetBalance() { return Balance; }

    public void SetNum(string newCardNum) { CardNum = newCardNum; }
    public void SetPin(int newPin) { Pin = newPin; }
    public void SetFirstName(string newFirstName) { FirstName = newFirstName; }
    public void SetLastName(string newLastName) { LastName = newLastName; }
    public void SetBalance(decimal newBalance) { Balance = newBalance; }
}
