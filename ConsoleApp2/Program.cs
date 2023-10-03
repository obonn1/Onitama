// See https://aka.ms/new-console-template for more information
public static class Diamond
{
    static void Main(string[] args)
    {
        List<string> names = new List<string>()
        {
            "AVA",
            "ALICE",
            "ANNIE",
            "LUNA",
            "OMAR",
            "KION",
            "KIARA"
        };
        foreach (var name in names)
        {
            Console.WriteLine(MakeDiamond(name));
        }
    }
    public static string MakeDiamond(string word)
    {
        List<string> diamondLines = new List<string>();
        string alphabet = word;
        if (word.Length == 0) return "";
        int sidePadding = word.Length - 1;
        int midPadding = 1;
        diamondLines.Add($"{new string(' ', sidePadding)}A{new string(' ', sidePadding)}");
        sidePadding--;
        for (int i = 1; i <= word.Length - 1; i++)
        {
            diamondLines.Add($"{new string(' ', sidePadding)}{alphabet[i]}{new string(' ', midPadding)}{alphabet[i]}{new string(' ', sidePadding)}");
            midPadding += 2;
            sidePadding--;
        }
        for (int i = diamondLines.Count - 2; i >= 0; i--)
        {
            diamondLines.Add(diamondLines[i]);
        }
        return String.Join("\n", diamondLines.ToArray());
    }
}
