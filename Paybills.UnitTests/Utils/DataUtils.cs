namespace Paybills.UnitTests.Utils;

static class DataUtils
{
    private static Random random = new Random();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static int RandomInt(int min, int max)
    {
        return random.Next(min, max);
    }
}