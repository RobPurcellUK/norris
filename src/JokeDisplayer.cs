using System;

namespace norris
{
    /// <summary>
    /// Orchestrates the user instruction / action loop.
    /// </summary>
    internal class JokeDisplayer
    {
        private readonly IJokeRetriever _jokeRetriever;
        private readonly IJokeCache _jokeCache;

        public JokeDisplayer(
            IJokeRetriever jokeRetriever,
            IJokeCache jokeCache)
        {
            _jokeRetriever = jokeRetriever;
            _jokeCache = jokeCache;
        }

        /// <summary>
        /// Main entrypoint. Run the instruction and action loop.
        /// </summary>
        public void Show()
        {
            WriteInstructions();

            DisplayAction action;

            do
            {
                action = ReadUserInput();
                WriteAction(action);
                DoAction(action);
            } while (action != DisplayAction.Quit);
        }

        private void DoAction(DisplayAction action)
        {
            if (action == DisplayAction.Quit)
            {
                return;
            }

            var joke = Joke.NoJoke;
            var item = 0;

            switch (action)
            {
                case DisplayAction.Joke:
                    joke = _jokeRetriever.GetJoke();
                    (item, joke) = _jokeCache.Add(joke);
                    break;
                case DisplayAction.Previous:
                    (item, joke) = _jokeCache.Previous();
                    break;
                case DisplayAction.Next:
                    (item, joke) = _jokeCache.Next();
                    break;
            }

            DisplayJoke(item, _jokeCache.Count, joke);
        }

        private static void DisplayJoke(int item, int jokeCount, Joke joke)
        {
            Console.WriteLine($"Joke {item}/{jokeCount}");
            Console.WriteLine(joke.Text);
            Console.WriteLine();
        }

        private static void WriteAction(DisplayAction action)
        {
            switch (action)
            {
                case DisplayAction.Joke:
                    Console.WriteLine("Retrieve new joke");
                    break;
                case DisplayAction.Previous:
                    Console.WriteLine("Display previous joke");
                    break;
                case DisplayAction.Next:
                    Console.WriteLine("Display next joke");
                    break;
                case DisplayAction.Quit:
                    Console.WriteLine("No more! Quitting.");
                    break;
            }
        }

        private static void WriteInstructions()
            => Console.WriteLine("Press 'j' for a new joke, or 'p' and 'n' to move to previous or next jokes already retrieved. 'q' to quit.");

        /// <summary>
        /// Read user input until it matches one of the expected actions.
        /// </summary>
        private static DisplayAction ReadUserInput()
        {
            do
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.J:
                        return DisplayAction.Joke;
                    case ConsoleKey.P:
                        return DisplayAction.Previous;
                    case ConsoleKey.N:
                        return DisplayAction.Next;
                    case ConsoleKey.Q:
                        return DisplayAction.Quit;
                    default:
                        WriteInstructions();
                        break;
                }
            } while (true);
        }

        /// <summary>
        /// User-requested action.
        /// </summary>
        private enum DisplayAction
        {
            Joke,
            Previous,
            Next,
            Quit
        }
    }
}