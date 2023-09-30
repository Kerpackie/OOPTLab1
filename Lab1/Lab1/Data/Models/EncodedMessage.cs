using System.Text;

namespace Lab1.Data.Models;

public class EncodedMessage
{
	// We only use Getters for auto properties, to prevent the value from being changed.
    public string Key { get; }
    public string Message { get; }
 
    // Only allow the Key and EncodedMessage to be set in the constructor.
    public EncodedMessage(string originalMessage)
    {
        Key = GenerateKey(originalMessage);
        Message = EncodeMessage(originalMessage);
    }
 
    public EncodedMessage(string encodedMessage, string key)
    {
        Message = encodedMessage;
        Key = key;
    }
    
 
    public void Display()
    {
        Console.WriteLine(Key);
        Console.WriteLine(Message);
    }
 
    private static string GenerateKey(string message)
    {
        // Calculate the number of words
        var wordCount = message.Split(' ').Length;
 
        // Select a random symbol from the specified set
        var random = new Random();
        var symbols = new[] { '!', '*', '@', '?', '^', ':' };
        var selectedSymbol = symbols[random.Next(0, symbols.Length)];
        //var selectedSymbol = '!';
 
        // Calculate the length of each word and find the highest frequency letter
        var words = message.Split(' ');
        
        // Get count of letter with highest frequency from words
        var letterFrequency = string.Concat(words)
            .GroupBy(c => c)
            .OrderByDescending(group => group.Count());
 
        var highestFrequency = letterFrequency.First().Count();
        
        // Create the key
        var keyBuilder = new StringBuilder();
        var symbolAscii = (int) selectedSymbol;
 
        // Unit Numeric Value from Key.
        keyBuilder.Append(symbolAscii.ToString()[0]);
        keyBuilder.Append('/');
        keyBuilder.Append(wordCount);
        keyBuilder.Append('-');
        keyBuilder.Append(symbolAscii.ToString()[1]);
        foreach (var word in words)
        {
            keyBuilder.Append(word.Length + highestFrequency);
            keyBuilder.Append(':');
        }
        
        // Reverse the key
        var keyArray = keyBuilder.ToString().ToCharArray();
        Array.Reverse(keyArray);
        
        return new string(keyArray);
    }
 
    private static string EncodeMessage(string message)
    {
        var splitMessage = message.Split(" ");
        var sb = new StringBuilder();
        foreach (var word in splitMessage)
        {
            var wordCharArray = word.ToCharArray();
            Array.Reverse(wordCharArray);
            sb.Append(wordCharArray); 
        }
 
        return sb.ToString();
    }
}