

class Game
{
    protected Player player;
    protected bool finished;
    protected Font font;
    protected Room room;
    protected SaveMenu saveMenu;


    public Game()
    {
        player = new Player("data/adol.png");
        player.MoveTo(300, 300);
        room = new Room();
        finished = false;
    }

    public void SaveGame()
    {
    }

    void UpdateScreen()
    {
        SdlHardware.ClearScreen();
        //room.DrawOnHiddenScreen();
        player.DrawOnHiddenScreen();
        SdlHardware.ShowHiddenScreen();
    }

    void CheckUserInput()
    {
        if (SdlHardware.KeyPressed(Controls.Cancel))
        {
            finished = true;
        }
        if (SdlHardware.KeyPressed(Controls.Right))
        {
            player.MoveRight();
        }
        if (SdlHardware.KeyPressed(Controls.Left))
        {
            player.MoveLeft();
        }
        if (SdlHardware.KeyPressed(Controls.Up))
        {
            player.MoveUp();
        }
        if (SdlHardware.KeyPressed(Controls.Down))
        {
            player.MoveDown();
        }
    }

    void UpdateWorld()
    {
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
