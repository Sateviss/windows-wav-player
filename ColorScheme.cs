using System;

namespace lab4
{
    public struct ColorScheme
    {
        public readonly ConsoleColor Background;
        public readonly ConsoleColor Playlist;
        public readonly ConsoleColor NowPlaying;
        public readonly ConsoleColor NowPlayingBack;
        public readonly ConsoleColor Left;
        public readonly ConsoleColor Right;
        public readonly ConsoleColor Slider;
        public readonly ConsoleColor Bar;
        public readonly ConsoleColor BottomText;


        public ColorScheme(
            ConsoleColor background = ConsoleColor.Black,
            ConsoleColor playlist = ConsoleColor.White,
            ConsoleColor nowPlaying = ConsoleColor.White,
            ConsoleColor nowPlayingBack = ConsoleColor.DarkGray, 
            ConsoleColor left = ConsoleColor.DarkGray,
            ConsoleColor right = ConsoleColor.Gray,
            ConsoleColor slider = ConsoleColor.White,
            ConsoleColor bar = ConsoleColor.Black,
            ConsoleColor bottomText = ConsoleColor.White
            )
        {
            Background = background;
            Playlist = playlist;
            NowPlaying = nowPlaying;
            NowPlayingBack = nowPlayingBack;
            Left = left;
            Right = right;
            Slider = slider;
            Bar = bar;
            BottomText = bottomText;
        }
    }
}