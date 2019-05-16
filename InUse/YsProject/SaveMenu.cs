
class SaveMenu : Menu
{
    protected int option;

    public SaveMenu()
    {
        option = 0;
    }

    public override void DrawMenu()
    {
        SdlHardware.ClearScreen();
        SdlHardware.WriteHiddenText("Enter name: ",
            100, 50,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.ShowHiddenScreen();
    }

    public void Run()
    {
        option = 0;
        DrawMenu();
        do
        {
            // Create file name with the given name with default information
            // Player lvl, exp, gold, items, etc
            SdlHardware.Pause(100);
        }
        while (option == 0);
    }

    public int GetChosenOption()
    {
        return option;
    }
}

