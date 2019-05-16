﻿
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
        SdlHardware.WriteHiddenText("1. New Game",
            100, 50,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("2. Load Game",
            100, 100,
            0xA0, 0xA0, 0xA0,
            font);
        SdlHardware.WriteHiddenText("3. Change Controls",
            100, 150,
            0xA0, 0xA0, 0xA0,
            font);
        SdlHardware.WriteHiddenText("Q. Quit",
            100, 200,
            0x80, 0x80, 0x80,
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
                option = 1;
            }
            if (SdlHardware.KeyPressed(SdlHardware.KEY_2))
            {
                option = 2;
            }
            if (SdlHardware.KeyPressed(SdlHardware.KEY_3))
            {
                option = 3;
            }
            if (SdlHardware.KeyPressed(SdlHardware.KEY_Q))
            {
                option = 4;
            }
            SdlHardware.Pause(100);
        }
        while (option == 0);
    }

    public int GetChosenOption()
    {
        return option;
    }
}
