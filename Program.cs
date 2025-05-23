/// <summary>
/// Entry point of the application.
/// </summary>
static class Program {
    /// <summary>
    /// An instance of <see cref="Random"/> used for generating random numbers.
    /// </summary>
    static Random random = new Random(DateTime.Now.Millisecond);

    /// <summary>
    /// The main method of the application.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    static void Main(string[] args) {
        do {
            const int MIN_AMOUNT_COMBINATIONS = 10;
            const int MAX_AMOUNT_COMBINATIONS = 200;
            Console.Clear();

            // Get the number of combinations from the user within the specified range
            int numberOfCombinations = Utilities.GetNumberOfCombinations(MIN_AMOUNT_COMBINATIONS, MAX_AMOUNT_COMBINATIONS);
            Console.WriteLine();

            // Generate a ticket with the specified number of combinations
            Ticket userTicket = new Ticket(numberOfCombinations, random);
            userTicket.Display();
            Console.WriteLine();

            // Generate the winning combination, excluding the user's complementary number
            Combination winningCombination = Combination.Generate(random, userTicket.ComplementaryNumber);
            Console.WriteLine("Combinaison gagnante: " + winningCombination);
            Console.WriteLine();

            // Get the list of winning combinations from the user's ticket
            List<Combination> winningUserCombinations = userTicket.GetWinningCombinations(winningCombination);
            Console.WriteLine();

            // Display the user's winning combinations
            Utilities.DisplayWinningCombinations(winningUserCombinations);
            Console.WriteLine();

            // Display statistics about the combinations and results
            Utilities.DisplayStatistics(userTicket, winningUserCombinations, numberOfCombinations, winningCombination);

        } while (Utilities.UserWantsToRetry());
    }
}
