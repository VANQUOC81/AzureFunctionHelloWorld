namespace Common
{
    public class LibraryFunctions
    {
        public static int GetRandomNumberWithinRange(int range)
        {
            // Initiate object with new()
            Random random = new();

            return random.Next(range);
        }

        public async static Task<string> ReverseStringAsync(string input)
        {
            // Suspend task and return to the caller
            await Task.Delay(10000);

            // Convert the string to a character array
            char[] charArray = input.ToCharArray();

            // Reverse the character array
            Array.Reverse(charArray);

            // Convert the character array back to a string
            string reversedString = new string(charArray);

            return reversedString;
        }
    }
}