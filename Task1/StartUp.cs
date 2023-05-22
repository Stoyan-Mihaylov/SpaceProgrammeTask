using SpaceProgramme.Core;
using SpaceProgramme.Core.Contracts;
using SpaceProgramme.Exceptions;
using System;

public class StartUp
{
    public static void Main(string[] args)
    {
        IEngine engine = new Engine();

        try
        {
            engine.Run(args);
        }
        catch (BestDayForLaunchIsNullException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (FileIsNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

