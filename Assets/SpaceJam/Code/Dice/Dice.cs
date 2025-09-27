using System.Text.RegularExpressions;
using UnityEngine;

public static class Dice
{
    
    public static int Roll(string diceNotation)
    {
        Regex regex = new Regex(@"(\d+)?d(\d+)([+-]\d+)?");
        Match match = regex.Match(diceNotation.ToLower());
        if (!match.Success)
        {
            Debug.LogError($"Invalid notation {diceNotation}");
            return 0;
        }
        int numberOfDice = 1;
        if (!string.IsNullOrEmpty(match.Groups[1].Value))
        {
            numberOfDice = int.Parse(match.Groups[1].Value);
        }
        int numberOfSides = int.Parse(match.Groups[2].Value);
        int modifier = 0;
        if (!string.IsNullOrEmpty(match.Groups[3].Value))
        {
            modifier = int.Parse(match.Groups[3].Value);
        }
        int total = 0;
        for (int i = 0; i < numberOfDice; i++) 
            total += Random.Range(1, numberOfSides + 1);
        return total + modifier;
    }
}
