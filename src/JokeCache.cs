using System.Collections.Generic;

namespace norris
{
    internal interface IJokeCache
    {
        (int, Joke) Add(Joke joke);
        (int, Joke) Previous();
        (int, Joke) Next();
        int Count { get; }
    }

    internal class JokeCache : IJokeCache
    {
        private List<Joke> _jokes = new();
        private int _currJoke = 0;

        public JokeCache()
        { }

        /// <summary>
        /// Add a new joke to the cache. Sets the current joke to the newly added
        /// one, at the end of the cache.
        /// </summary>
        /// <returns>Tuple of the joke's position in the cache, and the joke.</returns>
        public (int,Joke) Add(Joke joke)
        {
            _jokes.Add(joke);
            _currJoke = _jokes.Count;
            return (_currJoke, joke);
        }

        /// <summary>
        /// Move to the next cached joke.
        /// </summary>
        /// <returns>Tuple of the joke's position in the cache, and the joke.</returns>
        public (int,Joke) Next()
        {
            if (_jokes.Count == 0)
            {
                return (0, Joke.NoJoke);
            }

            if (_currJoke < _jokes.Count)
            {
                _currJoke++;
            }

            return (_currJoke, _jokes[_currJoke - 1]);
        }

        /// <summary>
        /// Move to the previous cached joke.
        /// </summary>
        /// <returns>Tuple of the joke's position in the cache, and the joke.</returns>
        public (int, Joke) Previous()
        {
            if (_jokes.Count == 0)
            {
                return (0, Joke.NoJoke);
            }

            if (_currJoke > 1)
            {
                _currJoke--;
            }

            return (_currJoke, _jokes[_currJoke - 1]);
        }

        /// <summary>
        /// The total number of cached jokes.
        /// </summary>
        public int Count => _jokes.Count;
    }
}
