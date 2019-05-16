
using System;

class ChangeControlsMenu : Menu
{
    protected Controls control;
    protected int pressedKey;
    protected int keyToChange;
    public ChangeControlsMenu(Controls control)
    {
        font = new Font("data/Joystix.ttf", 12);
        this.control = control;
    }

    public override void DrawMenu()
    {
        SdlHardware.ClearScreen();
        SdlHardware.WriteHiddenText("Current controls: ",
            100, 30,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Up: " + control.GetUp(),
            100, 40,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Down: " + control.GetDown(),
            100, 50,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Left: " + control.GetLeft(),
            100, 60,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Right: " + control.GetRight(),
            100, 70,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Use item: " + control.GetUseItem(),
            100, 80,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Toggle walk: " + control.GetToggleWalk(),
            100, 90,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Open inventory: " + control.GetInventory(),
            100, 100,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Pause: " + control.GetPause(),
            100, 110,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Accept: " + control.GetAccept(),
            100, 120,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Cancel: " + control.GetCancel(),
            100, 130,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.ShowHiddenScreen();
    }

    public void DrawBottomMessage(string message)
    {
        SdlHardware.WriteHiddenText(message,
           100, 230,
           0xC0, 0xC0, 0xC0,
           font);
        SdlHardware.Pause(200);
    }

   
    

    public void Run()
    {
        do
        {
            DrawMenu();
            DrawBottomMessage("Press the key you want to change");
            keyToChange = SdlHardware.DetectKey();

            if (keyToChange < 0 || !control.CheckKeysInUse())
            {
                DrawMenu();
                DrawBottomMessage("Key not in use");
            }
            else
            {
                DrawMenu();
                DrawBottomMessage("Press the new key");
                pressedKey = SdlHardware.DetectKey();
                if (pressedKey < 0)
                {
                    DrawMenu();
                    DrawBottomMessage("Invalid Key");
                }
                else
                {
                    DrawMenu();
                    DrawBottomMessage("Key changed");
                    // SwapKeys(keyToChange, pressedKey);
                }
            }
            SdlHardware.Pause(100);
        }
        while (!SdlHardware.KeyPressed(control.GetCancel()));
    }

    public Controls GetNewControls() { return this.control; }
}
