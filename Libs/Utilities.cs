/// <summary>
/// Provides utility methods for handling combinations and displaying statistics.
/// </summary>
public static class Utilities {
    /// <summary>
    /// Prompts the user to enter the number of combinations desired within a specified range.
    /// </summary>#
    /// <param name="min">The minimum number of combinations.</param>
    /// <param name="max">The maximum number of combinations.</param>
    /// <returns>The number of combinations entered by the user.</returns>
    public static int GetNumberOfCombinations(int min, int max) {
        int number;
        do {
            Console.Write($"Entrez le nombre de combinaisons souhaitées (entre {min} et {max}): ");
        } while (!int.TryParse(Console.ReadLine(), out number) || number < min || number > max);

        return number;
    }

    /// <summary>
    /// Displays the winning combinations provided by the user.
    /// </summary>
    /// <param name="winningUserCombinations">A list of the user's winning combinations.</param>
    public static void DisplayWinningCombinations(List<Combination> winningUserCombinations) {
        Console.WriteLine("Combinaisons gagnantes de l'utilisateur:");
        if (winningUserCombinations.Count > 0) {
            for (int i = 0; i < winningUserCombinations.Count; i++) {
                Console.WriteLine($"Combination {i + 1}: " + winningUserCombinations[i]);
            }
        } else {
            Console.WriteLine("Aucune combinaison gagnante.");
        }
    }

    /// <summary>
    /// Calculates the occurrences of each number in the winning combinations.
    /// </summary>
    /// <param name="winningUserCombinations">A list of the user's winning combinations.</param>
    /// <returns>A dictionary where the key is the number and the value is the occurrence count.</returns>
    private static Dictionary<int, int> CalculateNumberOccurrences(List<Combination> winningUserCombinations) {
        Dictionary<int, int> numberOccurrences = new Dictionary<int, int>();
        foreach (var combination in winningUserCombinations) {
            foreach (var number in combination.Numbers) {
                if (numberOccurrences.ContainsKey(number)) {
                    numberOccurrences[number]++;
                } else {
                    numberOccurrences[number] = 1;
                }
            }
        }
        return numberOccurrences;
    }

    /// <summary>
    /// Displays the occurrences of each number in the user's winning combinations.
    /// </summary>
    /// <param name="numberOccurrences">A dictionary where the key is the number and the value is the occurrence count.</param>
    private static void DisplayNumberOccurrences(Dictionary<int, int> numberOccurrences) {
        Console.WriteLine("Statistiques: ");
        foreach (var kvp in numberOccurrences.OrderBy(k => k.Key)) {
            Console.WriteLine($"Nombre {kvp.Key}: {kvp.Value} fois");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Displays general statistics about the combinations.
    /// </summary>
    /// <param name="numberOfCombinations">The total number of combinations generated.</param>
    /// <param name="winningCombinationCount">The number of winning combinations.</param>
    private static void DisplayGeneralStatistics(int numberOfCombinations, int winningCombinationCount) {
        Console.WriteLine($"Quantité de combinaisons: {numberOfCombinations}");
        Console.WriteLine();
        Console.WriteLine($"Quantité de combinaisons gagnantes: {winningCombinationCount}");
        Console.WriteLine();
    }

    /// <summary>
    /// Calculates the result categories based on the user's ticket and the winning combination.
    /// </summary>
    /// <param name="userTicket">The user's ticket containing their combinations and complementary number.</param>
    /// <param name="winningCombination">The winning combination of numbers.</param>
    /// <returns>A dictionary where the key is the result category and the value is the count of combinations in that category.</returns>
    private static Dictionary<string, int> CalculateResultCategories(Ticket userTicket, Combination winningCombination) {
        Dictionary<string, int> resultCategories = new Dictionary<string, int>
        {
            { "0 de 6", 0 },
            { "1 de 6", 0 },
            { "2 de 6", 0 },
            { "2 de 6 + complémentaire", 0 },
            { "3 de 6", 0 },
            { "4 de 6", 0 },
            { "5 de 6", 0 },
            { "5 de 6 + complémentaire", 0 },
            { "6 de 6", 0 }
        };

        foreach (var combination in userTicket.Combinations) {
            int matchCount = 0;
            bool hasComplementary = false;

            foreach (var number in combination.Numbers) {
                if (winningCombination.Numbers.Contains(number)) {
                    matchCount++;
                }
                if (number == userTicket.ComplementaryNumber) {
                    hasComplementary = true;
                }
            }

            string category = DetermineCategory(matchCount, hasComplementary);
            resultCategories[category]++;
        }
        return resultCategories;
    }

    /// <summary>
    /// Determines the result category based on the match count and whether the complementary number is present.
    /// </summary>
    /// <param name="matchCount">The number of matches in the combination.</param>
    /// <param name="hasComplementary">Indicates whether the complementary number is present in the combination.</param>
    /// <returns>The result category as a string.</returns>
    private static string DetermineCategory(int matchCount, bool hasComplementary) {
        if (matchCount == 0)
            return "0 de 6";
        else if (matchCount == 1)
            return "1 de 6";
        else if (matchCount == 2 && !hasComplementary)
            return "2 de 6";
        else if (matchCount == 2 && hasComplementary)
            return "2 de 6 + complémentaire";
        else if (matchCount == 3)
            return "3 de 6";
        else if (matchCount == 4)
            return "4 de 6";
        else if (matchCount == 5 && !hasComplementary)
            return "5 de 6";
        else if (matchCount == 5 && hasComplementary)
            return "5 de 6 + complémentaire";
        else // matchCount == 6
            return "6 de 6";
    }

    /// <summary>
    /// Displays the result categories with their counts and percentages.
    /// </summary>
    /// <param name="resultCategories">A dictionary where the key is the result category and the value is the count of combinations in that category.</param>
    /// <param name="numberOfCombinations">The total number of combinations generated.</param>
    private static void DisplayResultCategories(Dictionary<string, int> resultCategories, int numberOfCombinations) {
        foreach (var kvp in resultCategories.OrderBy(k => k.Key)) {
            double percentage = (double)kvp.Value / numberOfCombinations * 100;
            Console.WriteLine($"{kvp.Key}: {kvp.Value} combinaisons ({percentage:F2}%)");
        }
    }

    /// <summary>
    /// Displays comprehensive statistics about the user's ticket and winning combinations.
    /// </summary>
    /// <param name="userTicket">The user's ticket containing their combinations and complementary number.</param>
    /// <param name="winningUserCombinations">A list of the user's winning combinations.</param>
    /// <param name="numberOfCombinations">The total number of combinations generated.</param>
    /// <param name="winningCombination">The winning combination of numbers.</param>
    public static void DisplayStatistics(Ticket userTicket, List<Combination> winningUserCombinations, int numberOfCombinations, Combination winningCombination) {
        var numberOccurrences = CalculateNumberOccurrences(winningUserCombinations);
        DisplayNumberOccurrences(numberOccurrences);

        DisplayGeneralStatistics(numberOfCombinations, winningUserCombinations.Count);

        var resultCategories = CalculateResultCategories(userTicket, winningCombination);
        DisplayResultCategories(resultCategories, numberOfCombinations);
    }

    /// <summary>
    /// Prompts the user to decide whether they want to retry the process.
    /// </summary>
    /// <returns><c>true</c> if the user wants to retry; otherwise, <c>false</c>.</returns>
    public static bool UserWantsToRetry() {
        Console.Write("Voulez-vous recommencer ? (o/n): ");
        string response = Console.ReadLine()?.Trim().ToLower();
        return response == "o" || response == "oui";
    }
}
