
using System;

class ChangeControlsMenu : Menu
{
    protected int pressedKey;
    protected int keyToChange;
    protected bool currentKeyChoosen;
    protected bool newKeyChoosen;
    public ChangeControlsMenu()
    {
        font = new Font("data/Joystix.ttf", 12);
        currentKeyChoosen = false;
        newKeyChoosen = false;
        keyToChange = -1;
    }

    public override void DrawMenu()
    {
        SdlHardware.ClearScreen();
        SdlHardware.WriteHiddenText("Current controls: ",
            100, 30,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Up: " + SdlHardware.KeyToString(Controls.Up),
            100, 40,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Down: " + SdlHardware.KeyToString(Controls.Down),
            100, 50,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Left: " + SdlHardware.KeyToString(Controls.Left),
            100, 60,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Right: " + SdlHardware.KeyToString(Controls.Right),
            100, 70,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Use item: " + SdlHardware.KeyToString(Controls.UseItem),
            100, 80,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Toggle walk: " + SdlHardware.KeyToString(Controls.ToggleWalk),
            100, 90,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Open inventory: " + SdlHardware.KeyToString(Controls.Inventory),
            100, 100,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Pause: " + SdlHardware.KeyToString(Controls.Pause),
            100, 110,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Accept: " + SdlHardware.KeyToString(Controls.Accept),
            100, 120,
            0xC0, 0xC0, 0xC0,
            font);
        SdlHardware.WriteHiddenText("Cancel: " + SdlHardware.KeyToString(Controls.Cancel),
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
            do
            {
                DrawMenu();
                DrawBottomMessage("Press the key you want to change");
                keyToChange = SdlHardware.DetectKey();
                if (keyToChange > 0)
                    currentKeyChoosen = true;
            }
            while (!currentKeyChoosen);
            
            if (!Controls.CheckKeysInUse())
            {
                DrawMenu();
                DrawBottomMessage("Key not in use. Choose again");
                keyToChange = -1;
                currentKeyChoosen = false;
            }
            else
            {
                do
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
                        Controls.SwapKeys(keyToChange, pressedKey);
                        newKeyChoosen = true;
                    }
                }
                while (!newKeyChoosen);
                
            }
            SdlHardware.Pause(50);
        }
        while (!SdlHardware.KeyPressed(Controls.Cancel));
    }
}
