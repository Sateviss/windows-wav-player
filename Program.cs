using System;
using System.Collections.Generic;

namespace lab4
{
    internal static class Program
    {
        private static void Main()
        {

            ColorScheme[] schemes =
            {
                new ColorScheme(
                    // Default
                ),
                new ColorScheme(
                    // Inverted
                    background: ConsoleColor.White,
                    playlist: ConsoleColor.Black,
                    nowPlaying: ConsoleColor.White,
                    nowPlayingBack: ConsoleColor.Black,
                    left: ConsoleColor.Gray,
                    right: ConsoleColor.DarkGray,
                    slider: ConsoleColor.Black,
                    bar: ConsoleColor.White,
                    bottomText: ConsoleColor.Black
                ),
                new ColorScheme(
                    // Yellow
                    background: ConsoleColor.Black,
                    playlist: ConsoleColor.Yellow,
                    nowPlaying: ConsoleColor.Black,
                    nowPlayingBack: ConsoleColor.Yellow,
                    left: ConsoleColor.DarkGray,
                    right: ConsoleColor.DarkYellow
                ),
                new ColorScheme(
                    // Red
                    background: ConsoleColor.Black,
                    playlist: ConsoleColor.Red,
                    nowPlaying: ConsoleColor.Black,
                    nowPlayingBack: ConsoleColor.Red,
                    left: ConsoleColor.DarkGray,
                    right: ConsoleColor.DarkRed
                ),
                new ColorScheme(
                    // Blue
                    background: ConsoleColor.Black,
                    playlist: ConsoleColor.Cyan,
                    nowPlaying: ConsoleColor.Black,
                    nowPlayingBack: ConsoleColor.Cyan,
                    left: ConsoleColor.DarkGray,
                    right: ConsoleColor.DarkBlue
                ),
                new ColorScheme(
                    // Green
                    background: ConsoleColor.Black,
                    playlist: ConsoleColor.Green,
                    nowPlaying: ConsoleColor.Black,
                    nowPlayingBack: ConsoleColor.Green,
                    left: ConsoleColor.DarkGray,
                    right: ConsoleColor.DarkGreen
                ),
                new ColorScheme(
                    // Neon
                    background: ConsoleColor.DarkMagenta,
                    playlist: ConsoleColor.Cyan,
                    nowPlaying: ConsoleColor.DarkMagenta,
                    nowPlayingBack: ConsoleColor.Cyan,
                    left: ConsoleColor.DarkCyan,
                    right: ConsoleColor.Magenta,
                    slider: ConsoleColor.Yellow
                )
            };
            var currentScheme = 0;
            Console.Title = "WAV Player";
            PlayerManager p = new PlayerManager(new MciPlayer());
            p.CheckPos();
            Drawer.FullUpdate(p);
            while (true)
            {
                while (!Console.KeyAvailable)
                    p.CheckPos();
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.P:
                    case ConsoleKey.Spacebar:
                        p.PlayPause();
                        break;
                    case ConsoleKey.F:
                        p.PlayPause();
                        Console.Clear();
                        p.AddFolder();
                        Drawer.FullUpdate(p);
                        break;
                    case ConsoleKey.Q:
                        return;
                    case ConsoleKey.S:
                        p.ShuffleUnshuffle();
                        Drawer.FullUpdate(p);
                        break;
                    case ConsoleKey.UpArrow:
                        p.Prev();
                        Drawer.FullUpdate(p);
                        break;
                    case ConsoleKey.DownArrow:
                        p.Next();
                        Drawer.FullUpdate(p);
                        break;
                    case ConsoleKey.LeftArrow:
                        p.Seek(-5000);
                        break;
                    case ConsoleKey.RightArrow:
                        p.Seek(+5000);
                        break;
                    case ConsoleKey.C:
                        currentScheme = (currentScheme + 1) % schemes.Length;
                        Drawer.Scheme = schemes[currentScheme];
                        Drawer.FullUpdate(p);
                        break;
                }
            }
        }

        public static List<T> Shuffle<T>(this List<T> inputList)
        {
            var random = new Random();
            var outputList = new List<T>();
            inputList.ForEach(x => outputList.Add(x));
            for (var i = inputList.Count-1; i > 0; i--)
            {
                var j = random.Next(i);
                var temp = outputList[i];
                outputList[i] = outputList[j];
                outputList[j] = temp;
            }
            return outputList;
        }
    }
    
}
