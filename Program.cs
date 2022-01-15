using System;

namespace norris
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Begin Norris jokes!");

            var jokeDisplayer = new JokeDisplayer(
                    new JokeRetriever(),
                    new JokeCache()
                );
            jokeDisplayer.Show();

            Console.WriteLine("End Norris jokes :(");
        }
    }
}