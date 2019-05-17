using System;

class SaveMenu : Menu
{
    public static int SLOTS = 3;
    protected string name;
    public SaveMenu()
    {
        name = "";
    }

    public override void DrawMenu()
    {
        //DateTime pressTic, drawTic;
        //TimeSpan diff;
        SdlHardware.ClearScreen();
        SdlHardware.WriteHiddenText("Enter name for the save file",
            100, 50,
            0xC0, 0xC0, 0xC0,
            font);
        //pressTic = DateTime.Now;
        int key = SdlHardware.DetectKey();
        //drawTic = DateTime.Now;
        //diff = drawTic - pressTic;
        if (key != SdlHardware.KEY_SPC && key != SdlHardware.KEY_ESC &&
            key != SdlHardware.KEY_UP && key != SdlHardware.KEY_DOWN &&
            key != SdlHardware.KEY_LEFT && key != SdlHardware.KEY_RIGHT &&
            key != SdlHardware.KEY_RETURN /*&& ((int) diff.TotalMilliseconds > 500)*/)
        {
            name += SdlHardware.KeyToString(key);
        }
        else if(key == SdlHardware.KEY_SPC && name.Length > 0)
        {
            name = name.Remove(name.Length - 1);
        }

        SdlHardware.WriteHiddenText("Name: " + name,
            100, 150,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.ShowHiddenScreen();
    }

    public void Run()
    {
        while (!SdlHardware.KeyPressed(SdlHardware.KEY_ESC))
        {
            DrawMenu();
            SdlHardware.Pause(100); // Works but I'm not sure why
        }
    }

    public int GetSlots()
    {
        return SLOTS;
    }
}

