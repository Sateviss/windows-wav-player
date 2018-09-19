using System;
using System.Collections.Generic;
using System.IO;

namespace lab4
{
    class PlayerManager
    {
        public PlayerManager(IPlayer player)
        {
            _interfacePlayer = player;
            _previousTrackPosition = -1;
            Playlist = new List<string>();
            _unshuffeledList = new List<string>();
            AddFolder();
            _isPaused = false;
            NowPlaying = 0;
            _interfacePlayer.Open(Playlist[0]);
            _interfacePlayer.Play();
            _isShuffled = false;
        }

        private readonly IPlayer _interfacePlayer;
        public List<string> Playlist { get; private set; }
        private List<string> _unshuffeledList;
        private bool _isPaused;
        private bool _isShuffled;
        public int NowPlaying { get; private set; }

        public int CurrentSongLength
        {
            get => _currentSongLength;
            private set => _currentSongLength = value;
        }

        public int CurrentPosition
        {
            get => _currentPosition;
            private set => _currentPosition = value;
        }

        private int _previousTrackPosition;
        private int _currentPosition;
        private int _currentSongLength;

        public void CheckPos()
        {
            CurrentSongLength = _interfacePlayer.GetSongLength();
            CurrentPosition = _interfacePlayer.GetCurentMilisecond();
            if (CurrentSongLength <= CurrentPosition)
            {
                Next();
                Drawer.FullUpdate(this);
            }

            var pos = (int)Math.Floor((Console.WindowWidth-2)*1f * CurrentPosition / (CurrentSongLength))+1;
            if (pos != _previousTrackPosition)
                Drawer.UpdateBottom(this);
            _previousTrackPosition = pos;
        }
        
        public void PlayPause()
        {
            if (_isPaused)
                _interfacePlayer.Resume();
            else
                _interfacePlayer.Pause();
            _isPaused = !_isPaused;
        }

        public void ShuffleUnshuffle()
        {
            Playlist = _isShuffled ? _unshuffeledList : Playlist.Shuffle();
            NowPlaying = -1;
            Next();
            _isShuffled = !_isShuffled;
        }

        public void AddFolder()
        {
            while (true)
            {
                string folder;
                while (true)
                {
                    Console.Write("Path to folder with .wav files to add: ");
                    folder = Console.ReadLine();
                    if (Directory.Exists(folder))
                        break;
                    Console.WriteLine("This folder does not exist, try again");
                }
                var list = Directory.GetFiles(folder);
                int startL = Playlist.Count;
                foreach (var song in list)
                    if (Path.GetExtension(song) == ".wav")
                    {
                        Playlist.Add(song);
                        _unshuffeledList.Add(song);
                    }
                if (Playlist.Count == 0)
                    Console.WriteLine("The playlist is empty, please add some .wav files");
                else
                {
                    Console.WriteLine($"Added {(Playlist.Count - startL)} songs");
                    break;
                }
            }
        }

        public void Next()
        {
            if (NowPlaying >= Playlist.Count - 1) return;
            _interfacePlayer.Close();
            _interfacePlayer.Open(Playlist[++NowPlaying]);
            _interfacePlayer.Play();
        }

        public void Prev()
        {
            if (NowPlaying <= 0) return;
            _interfacePlayer.Close();
            _interfacePlayer.Open(Playlist[--NowPlaying]);
            _interfacePlayer.Play();
        }

        public void Seek(int delta)
        {
            CurrentPosition = _interfacePlayer.GetCurentMilisecond();
            _interfacePlayer.SetPosition(CurrentPosition+delta);
        }

    }
}