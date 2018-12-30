using System.Text;

namespace SimpleConsoleApp.Extensions
{
    public static class StringBuilderExtensions
    {
        public static string ToString(this StringBuilder stringBuilder, int startIndex)
        {
            if (startIndex == -1)
            {
                return string.Empty;
            }

            return stringBuilder.ToString(startIndex, stringBuilder.Length - startIndex);
        }

        public static int LastIndexOf(this StringBuilder stringBuilder, char value)
        {
            if (stringBuilder.Length == 0)
            {
                return -1;
            }

            for (int i = stringBuilder.Length - 1; i > 0; i--)
            {
                if (stringBuilder[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
