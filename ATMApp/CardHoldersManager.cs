using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class CardHolderManager
{
    public List<CardHolder> LoadFromJson(string jsonFilePath)
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

            List<CardHolder> cardHolders = JsonConvert.DeserializeObject<List<CardHolder>>(jsonString);

            return cardHolders;
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return new List<CardHolder>();
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error loading from JSON: {ex.Message}");
            return new List<CardHolder>();
        }
    }

    public void ExportToJson(List<CardHolder> cardHolders, string jsonFilePath)
    {
        try
        {
            string jsonString = JsonConvert.SerializeObject(cardHolders, Formatting.Indented);
            File.WriteAllText(jsonFilePath, jsonString);
            Console.WriteLine("CardHolders exported to JSON successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error exporting to JSON: {ex.Message}");
        }
    }
}
