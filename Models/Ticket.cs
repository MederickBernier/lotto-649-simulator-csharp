/// <summary>
/// Represents a lottery ticket containing multiple combinations and a complementary number.
/// </summary>
public struct Ticket {
    /// <summary>
    /// A list of combinations in the ticket.
    /// </summary>
    public List<Combination> Combinations;

    /// <summary>
    /// The complementary number associated with the ticket.
    /// </summary>
    public int ComplementaryNumber;

    /// <summary>
    /// Initializes a new instance of the <see cref="Ticket"/> struct with a specified number of combinations.
    /// </summary>
    /// <param name="numberOfCombinations">The number of combinations to generate for the ticket.</param>
    /// <param name="random">The random number generator used for generating combinations and the complementary number.</param>
    public Ticket(int numberOfCombinations, Random random) {
        // Initialize the list of combinations.
        Combinations = new List<Combination>();

        // Generate the complementary number within the range of 1 to 49.
        ComplementaryNumber = Combination.GenerateComplementaryNumber(random, 1, 49);

        // Generate the specified number of combinations and add them to the list.
        for (int i = 0; i < numberOfCombinations; i++) {
            Combinations.Add(Combination.Generate(random, ComplementaryNumber));
        }
    }

    /// <summary>
    /// Displays the combinations and the complementary number of the ticket.
    /// </summary>
    public void Display() {
        Console.WriteLine("Vos combinaisons:");
        for (int i = 0; i < Combinations.Count; i++) {
            // Display each combination.
            Console.WriteLine($"Combinaison {i + 1}: " + Combinations[i]);
        }
        // Display the complementary number.
        Console.WriteLine($"Numéro complémentaire: {ComplementaryNumber}");
    }

    /// <summary>
    /// Retrieves the combinations that match the winning combination.
    /// </summary>
    /// <param name="winningCombination">The winning combination to compare against.</param>
    /// <returns>A list of combinations that have at least 2 matching numbers or 1 matching number and the complementary number.</returns>
    public List<Combination> GetWinningCombinations(Combination winningCombination) {
        // Initialize the list to hold winning user combinations.
        List<Combination> winningUserCombinations = new List<Combination>();

        // Iterate through each combination in the ticket.
        foreach (var combination in Combinations) {
            int matchCount = 0;
            bool hasComplementary = false;

            // Check each number in the combination for matches with the winning combination.
            foreach (var number in combination.Numbers) {
                if (winningCombination.Numbers.Contains(number)) {
                    matchCount++;
                }
                if (number == ComplementaryNumber) {
                    hasComplementary = true;
                }
            }

            // Determine if the combination qualifies as a winning combination.
            if (matchCount >= 2 || (matchCount == 1 && hasComplementary)) {
                winningUserCombinations.Add(combination);
            }
        }

        // Return the list of winning combinations.
        return winningUserCombinations;
    }
}
