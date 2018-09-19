using System;
using System.IO;

namespace lab4
{
    static class Drawer
    {
        private const char FillChar = '█';
        public static ColorScheme Scheme = new ColorScheme();

        public static void UpdateBottom(PlayerManager p)
        {
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
            Rect(0, Console.WindowHeight-5, Console.WindowWidth, Console.WindowHeight, Scheme.Bar);
            
            PutText("P/Space - Pause | F - Add a folder to playlist | Q - Quit | S - Toggle shuffle",
                2, Console.WindowHeight-4, Scheme.BottomText, Scheme.Bar);
            PutText("Arrows: Left/Right - Seek (5s) | Up/Down - Switch tracks | C - Change colour scheme",
                2, Console.WindowHeight-3, Scheme.BottomText, Scheme.Bar);
            
            var pos = (int)Math.Floor((Console.WindowWidth-2)*1f * p.CurrentPosition / (p.CurrentSongLength))+1;
            var left = "".PadRight(pos, FillChar);
            PutText(left, 1, Console.WindowHeight-2, Scheme.Left);
            var right = "".PadRight(Console.WindowWidth - 1 - pos, FillChar);
            PutText(right, pos, Console.WindowHeight-2, Scheme.Right);
            PutText(FillChar.ToString(), pos, Console.WindowHeight-2, Scheme.Slider);
            
            Console.SetCursorPosition(0,0);
        }       
        
        public static void FullUpdate(PlayerManager p)
        {
            for (var y = 0; y < 4; y++)
                PutText("".PadRight(Console.WindowWidth), 0, y, ConsoleColor.Black, Scheme.Background);
            
            PutText($" >{Path.GetFileName(p.Playlist[p.NowPlaying])}".PadRight(Console.WindowWidth), 0, 4, Scheme.NowPlaying, Scheme.NowPlayingBack);
            
            for (var y = 1; y < 4 && p.NowPlaying-y >= 0; y++)
                PutText($"  {Path.GetFileName(p.Playlist[p.NowPlaying-y])}".PadRight(Console.WindowWidth), 0, 4-y,Scheme.Playlist, Scheme.Background);
            
            for (var y = 1; y < Console.WindowHeight-5-4 && p.NowPlaying+y < p.Playlist.Count; y++)
                PutText($"  {Path.GetFileName(p.Playlist[p.NowPlaying+y])}".PadRight(Console.WindowWidth), 0, 4+y, Scheme.Playlist, Scheme.Background);
            
            for (var y = 4+p.Playlist.Count-p.NowPlaying; y < Console.WindowHeight-5; y++)
                PutText("".PadRight(Console.WindowWidth), 0, y, ConsoleColor.Black, Scheme.Background);
            
            UpdateBottom(p);
            Console.CursorVisible = false;
        }

        public static void PutText(string text, int left, int top, ConsoleColor color=ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            var col = Console.ForegroundColor;
            var back = Console.BackgroundColor;
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = color;
            Console.BackgroundColor = background;
            Console.Write(text);
            Console.ForegroundColor = col;
            Console.BackgroundColor = back;
        }

        public static void Rect(int x1, int y1, int x2, int y2, ConsoleColor fill)
        {

            for (var y = y1; y < y2; y++)
            {   
                PutText("\r".PadLeft(x2 - x1, '█'), x1, y, fill);
            }
        }
            
    }
}