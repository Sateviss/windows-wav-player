using System;
using System.Runtime.InteropServices;
using System.Text;

namespace lab4
{
    class MciPlayer : IPlayer
    {
        [DllImport("winmm.dll")]
        private static extern int mciSendString(string command, StringBuilder ret, int retL, IntPtr hwndCallback);

        public MciPlayer()
        {
            _returnData = new StringBuilder();
        }
        
        private string _command;
        private readonly StringBuilder _returnData;

        public void Open(string sFileName)
        {
            _command = "open \"" + sFileName +
                      "\" alias MediaFile";
            SendCommand();
        }

        public void Close()
        {
            _command = "close MediaFile";
            SendCommand();
        }

        public void Play()
        {
            _command = "play MediaFile";
            SendCommand();
        }

        public void Pause()
        {
            _command = "pause MediaFile";
            SendCommand();
        }

        public void Resume()
        {
            _command = "resume MediaFile";
            SendCommand();
        }

        private void SendCommand()
        {
            mciSendString(_command, _returnData, _returnData.Capacity, IntPtr.Zero);
        }
        
        public int GetCurentMilisecond()
        {
            _command = "status MediaFile position";
            SendCommand();
            return int.Parse(_returnData.ToString());
        }

        public void SetPosition(int miliseconds)
        {
            _command = "seek MediaFile to " + miliseconds;
            SendCommand();
            Play();
        }

        public int GetSongLength()
        {
            _command = "status MediaFile length";
            SendCommand();
            return int.Parse(_returnData.ToString());
        }
    }
}