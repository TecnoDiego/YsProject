

class Game
{
    protected Player player;
    protected bool finished;
    protected Font font;
    protected Room room;
    protected SaveMenu saveMenu;


    public Game()
    {
        player = new Player();
        finished = false;
    }

    public void SaveGame()
    {

    }

    void UpdateScreen()
    {
        // TO DO
    }

    void CheckUserInput()
    {
       // TO DO
    }

    void UpdateWorld()
    {
        // TO DO
    }

    void CheckGameStatus()
    {
       // TO DO
    }

    void PauseUntilNextFrame()
    {
        // TO DO
    }

    void UpdateHighscore()
    {
        // TO DO
    }

    public void Run()
    {
        do
        {
            UpdateScreen();
            CheckUserInput();
            UpdateWorld();
            PauseUntilNextFrame();
            CheckGameStatus();
        }
        while (!finished);
    }
}
