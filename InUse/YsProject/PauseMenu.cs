
class PauseMenu : Menu
{
    protected int option;
    protected Player playerToSave;

    public PauseMenu(Player p)
    {
        playerToSave = p;
        option = 0;
    }

    public override void DrawMenu()
    {
        SdlHardware.ClearScreen();
        SdlHardware.WriteHiddenText("1. Save Game",
            100, 50,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("2. Change controls",
            100, 100,
            0xA0, 0xA0, 0xA0,
            font);
        SdlHardware.WriteHiddenText("3. To title",
            100, 150,
            0xA0, 0xA0, 0xA0,
            font);
        SdlHardware.ShowHiddenScreen();
    }

    public void Run()
    {
        option = 0;
        DrawMenu();
        do
        {
            if (SdlHardware.KeyPressed(SdlHardware.KEY_1))
            {
                SaveMenu save = new SaveMenu(playerToSave, false);
                save.Run();
            }
            if (SdlHardware.KeyPressed(SdlHardware.KEY_2))
            {
                ChangeControlsMenu changeControls = new ChangeControlsMenu();
                changeControls.Run();
            }
            if (SdlHardware.KeyPressed(SdlHardware.KEY_3))
            {
                Game.finished = true;
            }
            SdlHardware.Pause(100);
        }
        while (!SdlHardware.KeyPressed(Controls.Cancel) && !Game.finished);
        SdlHardware.Pause(200);
    }
}

