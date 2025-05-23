/// <summary>
/// Represents a combination of unique numbers.
/// </summary>
public struct Combination {
    /// <summary>
    /// List of numbers in the combination.
    /// </summary>
    public List<int> Numbers;

    /// <summary>
    /// Initializes a new instance of the <see cref="Combination"/> struct with the specified list of numbers.
    /// </summary>
    /// <param name="numbers">The list of numbers to initialize the combination with.</param>
    public Combination(List<int> numbers) {
        Numbers = numbers;
    }

    /// <summary>
    /// Generates a new <see cref="Combination"/> instance with a specified complementary number.
    /// </summary>
    /// <param name="random">An instance of <see cref="Random"/> to use for generating random numbers.</param>
    /// <param name="complementaryNumber">The number that should not be included in the generated combination.</param>
    /// <returns>A new <see cref="Combination"/> instance.</returns>
    public static Combination Generate(Random random, int complementaryNumber) {
        // Generate a list of unique numbers, excluding the complementary number, and sort it
        List<int> numbers = GenerateUniqueNumbers(random, 6, 1, 49, complementaryNumber);
        numbers.Sort();
        return new Combination(numbers);
    }

    /// <summary>
    /// Generates a list of unique numbers within a specified range, excluding a specified complementary number.
    /// </summary>
    /// <param name="random">An instance of <see cref="Random"/> to use for generating random numbers.</param>
    /// <param name="count">The number of unique numbers to generate.</param>
    /// <param name="minValue">The minimum value (inclusive) of the range from which to generate numbers.</param>
    /// <param name="maxValue">The maximum value (inclusive) of the range from which to generate numbers.</param>
    /// <param name="complementaryNumber">The number that should not be included in the generated list.</param>
    /// <returns>A list of unique numbers.</returns>
    private static List<int> GenerateUniqueNumbers(Random random, int count, int minValue, int maxValue, int complementaryNumber) {
        // Create a list of all numbers within the range, excluding the complementary number
        List<int> allNumbers = Enumerable.Range(minValue, maxValue - minValue + 1).Except(new List<int> { complementaryNumber }).ToList();
        List<int> selectedNumbers = new List<int>();

        // Randomly select 'count' unique numbers from the list
        for (int i = 0; i < count; i++) {
            int index = random.Next(allNumbers.Count);
            selectedNumbers.Add(allNumbers[index]);
            allNumbers.RemoveAt(index); // Remove the selected number to ensure uniqueness
        }

        return selectedNumbers;
    }

    /// <summary>
    /// Generates a random complementary number within a specified range.
    /// </summary>
    /// <param name="random">An instance of <see cref="Random"/> to use for generating the random number.</param>
    /// <param name="minValue">The minimum value (inclusive) of the range from which to generate the number.</param>
    /// <param name="maxValue">The maximum value (inclusive) of the range from which to generate the number.</param>
    /// <returns>A randomly generated complementary number.</returns>
    public static int GenerateComplementaryNumber(Random random, int minValue, int maxValue) {
        // Generate a random number within the specified range
        return random.Next(minValue, maxValue + 1);
    }

    /// <summary>
    /// Returns a string representation of the combination.
    /// </summary>
    /// <returns>A string representing the combination of numbers, separated by commas.</returns>
    public override string ToString() {
        return string.Join(", ", Numbers);
    }
}
