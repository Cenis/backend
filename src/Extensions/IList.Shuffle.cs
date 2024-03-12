namespace CounTrivia.Extensions;

public static class ShuffleList
{
    public static Random randomNumberGenerator = new Random();

    /// <summary>
    /// Shuffle list of generic items with Fisher-Yates algorithm.
    /// </summary>
    /// <typeparam name="T">Type of lists items.</typeparam>
    /// <param name="list">List to shuffle.</param>
    public static void Shuffle<T>(this IList<T> list)
    {
        int position = list.Count;
        while (position > 1)
        {
            position--;
            int randomPosition = randomNumberGenerator.Next(position + 1);
            T replacedValue = list[randomPosition];
            list[randomPosition] = list[position];
            list[position] = replacedValue;
        }
    }
}