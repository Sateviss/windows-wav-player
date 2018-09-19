namespace lab4
{
    public interface IPlayer
    {
        void Open(string fileName);
        void Close();
        void Play();
        void Pause();
        void Resume();
        int GetCurentMilisecond();
        void SetPosition(int miliseconds);
        int GetSongLength();
    }
}