namespace AppView.Utilities
{
    public static class Utility
    {
        public static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            string randomStr = new string(Enumerable.Repeat(chars, length)
                                                      .Select(s => s[random.Next(s.Length)])
                                                      .ToArray());
            return randomStr;
        }
    }
}
