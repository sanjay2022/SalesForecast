using System.Collections.Immutable;
using CLAP;
using SalesForeCasting;

public class Program
{
    static void Main(string[] args)
    {
        var cmds   = new SalesForeCastingCommands();
        var parser = new Parser<SalesForeCastingCommands>();

        try
        {
            parser.Run(args, cmds);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}