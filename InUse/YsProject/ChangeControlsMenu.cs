
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

   
    public int DetectKey()
    {
        if (SdlHardware.KeyPressed(SdlHardware.KEY_ESC))
        {
            return SdlHardware.KEY_ESC;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_SPC))
        {
            return SdlHardware.KEY_SPC;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_A))
        {
            return SdlHardware.KEY_A;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_B))
        {
            return SdlHardware.KEY_B;
        }
        else if (SdlHardware.KeyPressed(SdlHardware.KEY_C))
        {
            return SdlHardware.KEY_C;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_D))
        {
            return SdlHardware.KEY_D;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_E))
        {
            return SdlHardware.KEY_E;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_F))
        {
            return SdlHardware.KEY_F;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_G))
        {
            return SdlHardware.KEY_G;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_H))
        {
            return SdlHardware.KEY_H;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_I))
        {
            return SdlHardware.KEY_I;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_J))
        {
            return SdlHardware.KEY_J;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_K))
        {
            return SdlHardware.KEY_K;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_L))
        {
            return SdlHardware.KEY_L;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_M))
        {
            return SdlHardware.KEY_M;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_N))
        {
            return SdlHardware.KEY_N;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_O))
        {
            return SdlHardware.KEY_O;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_P))
        {
            return SdlHardware.KEY_P;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_Q))
        {
            return SdlHardware.KEY_Q;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_R))
        {
            return SdlHardware.KEY_R;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_S))
        {
            return SdlHardware.KEY_S;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_T))
        {
            return SdlHardware.KEY_T;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_U))
        {
            return SdlHardware.KEY_U;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_V))
        {
            return SdlHardware.KEY_V;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_W))
        {
            return SdlHardware.KEY_W;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_X))
        {
            return SdlHardware.KEY_X;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_Y))
        {
            return SdlHardware.KEY_Y;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_Z))
        {
            return SdlHardware.KEY_Z;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_1))
        {
            return SdlHardware.KEY_1;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_2))
        {
            return SdlHardware.KEY_2;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_3))
        {
            return SdlHardware.KEY_3;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_4))
        {
            return SdlHardware.KEY_4;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_5))
        {
            return SdlHardware.KEY_5;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_6))
        {
            return SdlHardware.KEY_6;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_7))
        {
            return SdlHardware.KEY_7;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_8))
        {
            return SdlHardware.KEY_8;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_9))
        {
            return SdlHardware.KEY_9;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_0))
        {
            return SdlHardware.KEY_0;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_UP))
        {
            return SdlHardware.KEY_UP;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_DOWN))
        {
            return SdlHardware.KEY_DOWN;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_RIGHT))
        {
            return SdlHardware.KEY_RIGHT;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_LEFT))
        {
            return SdlHardware.KEY_LEFT;
        }
        else if(SdlHardware.KeyPressed(SdlHardware.KEY_RETURN))
        {
            return SdlHardware.KEY_RETURN;
        }
        else
        {
            return -1;
        }
    }

    public void Run()
    {
        do
        {
            DrawMenu();
            DrawBottomMessage("Press the key you want to change");
            keyToChange = DetectKey();

            if (keyToChange < 0 || !control.CheckKeysInUse())
            {
                DrawMenu();
                DrawBottomMessage("Key not in use");
            }
            else
            {
                DrawMenu();
                DrawBottomMessage("Press the new key");
                pressedKey = DetectKey();
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
