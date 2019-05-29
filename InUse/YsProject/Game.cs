

class Game
{
    protected Player player;
    public static bool finished;
    protected Font font;
    protected Room room;
    protected PauseMenu pause;


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
        room.DrawOnHiddenScreen();
        player.DrawOnHiddenScreen();
        SdlHardware.ShowHiddenScreen();
    }

    void CheckUserInput()
    {
        if (SdlHardware.KeyPressed(Controls.Cancel))
        {
            SdlHardware.Pause(300);
            pause = new PauseMenu(player);
            pause.Run();
        }
        if (SdlHardware.KeyPressed(Controls.Right))
        {
            player.MoveRight(room);
        }
        if (SdlHardware.KeyPressed(Controls.Left))
        {
            player.MoveLeft(room);
        }
        if (SdlHardware.KeyPressed(Controls.Up))
        {
            player.MoveUp(room);
        }
        if (SdlHardware.KeyPressed(Controls.Down))
        {
            player.MoveDown(room);
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
