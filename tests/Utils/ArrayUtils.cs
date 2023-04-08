namespace Panbyte.Tests.Utils;

public class ArrayUtils
{
    public static bool Compare(string[] a, string[] b)
    {
        if (a.Length != b.Length)
        {
            Console.WriteLine(a.Length.ToString(), b.Length.ToString());
            return false;
        }

        for (int i = 0; i < a.Length; i++)
        {

            if (!a[i].Equals(b[i]))
            {
                return false;
            }
        }

        return true;
    }
}