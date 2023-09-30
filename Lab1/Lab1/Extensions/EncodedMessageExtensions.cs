using System.Text;
using Lab1.Data.Models;

namespace Lab1.Extensions;

public static class EncodedMessageExtensions
{
		private static List<int> DecodeCipher(this EncodedMessage message)
	{
		// Reverse the cipher back to the original
		var cipherCharArray = message.Key.ToCharArray();
		Array.Reverse(cipherCharArray);
 
		// convert cipher back to a string
		var cipher = new string(cipherCharArray);
 
		// Remove all characters up to and including the first '/'
		var slashIndex = cipher.IndexOf('/');
		cipher = cipher[(slashIndex + 1)..]; // This range indexer is an IDE recommended feature over the Substring method
 
		// Remove all characters before and including the first '-' + 1 position
		cipher = cipher[(cipher.IndexOf('-') + 2)..];
 
		// Split cipher into an array of numbers, split on ':'
		var splitCipher = cipher.Split(':');
 
		// Get count of letter with highest frequency from encoded message
		var letterFrequency = message.Message
			.GroupBy(c => c)
			.OrderByDescending(group => group.Count());
 
		var highestFrequency = letterFrequency.First().Count();
 
		//Convert cipher numbers to integers
		var cipherNumbers = splitCipher
			.Where(value => !string.IsNullOrWhiteSpace(value)) // Filter out empty values
			.Select(x => int.Parse(x) - highestFrequency)
			.ToList();
 
		return cipherNumbers;
	}
 
	public static string DecodeMessage(this EncodedMessage message)
	{
		var encodedMessage = message.Message;
		var cipherNumbers = message.DecodeCipher();
 
		var decodedMessage = new StringBuilder();
		var words = new List<string>();
 
		// split the encoded message into words based on the cipher numbers, each number is the length of the word to be extracted
 
		foreach (var cipherNumber in cipherNumbers)
		{
			words.Add(encodedMessage[..cipherNumber]);
			encodedMessage = encodedMessage[cipherNumber..];
		}
 
		// reverse the words and add them to the decodedMessage
		foreach (var wordArray in words.Select(word => word.ToCharArray()))
		{
			Array.Reverse(wordArray);
			var reversedWord = new string(wordArray);
 
			// Add the word to the decoded message
			decodedMessage.Append(reversedWord);
			decodedMessage.Append(' ');
		}
 
		return decodedMessage.ToString().TrimEnd();
	}
}