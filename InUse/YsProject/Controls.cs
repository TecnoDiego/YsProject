
using System;
using System.IO;

class Controls
{
    public static string ControlsOrder = "YsControlsSaveFile up down left" +
        " right item walk inventory pause accept cancel";
    public static int Up;
    public static int Down;
    public static int Left;
    public static int Right;
    public static int UseItem;
    public static int ToggleWalk;
    public static int Inventory;
    public static int Pause;
    public static int Accept;
    public static int Cancel;

    public Controls()
    {
        Up = SdlHardware.KEY_UP;
        Down = SdlHardware.KEY_DOWN;
        Left = SdlHardware.KEY_LEFT;
        Right = SdlHardware.KEY_RIGHT;
        UseItem = SdlHardware.KEY_Z;
        ToggleWalk = SdlHardware.KEY_X;
        Inventory = SdlHardware.KEY_C;
        Pause = SdlHardware.KEY_P;
        Accept = SdlHardware.KEY_RETURN;
        Cancel = SdlHardware.KEY_ESC;
    }

    public static bool CheckKeysInUse()
    {
       if (SdlHardware.KeyPressed(Up) ||
                SdlHardware.KeyPressed(Down) ||
                SdlHardware.KeyPressed(Left) ||
                SdlHardware.KeyPressed(Right) ||
                SdlHardware.KeyPressed(UseItem) ||
                SdlHardware.KeyPressed(ToggleWalk) ||
                SdlHardware.KeyPressed(Inventory) ||
                SdlHardware.KeyPressed(Pause))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void SaveControls()
    {
        try
        {
            StreamWriter controlsFile = new StreamWriter("controls.txt");
            controlsFile.WriteLine(ControlsOrder);
            controlsFile.WriteLine(Up);
            controlsFile.WriteLine(Down);
            controlsFile.WriteLine(Left);
            controlsFile.WriteLine(Right);
            controlsFile.WriteLine(UseItem);
            controlsFile.WriteLine(ToggleWalk);
            controlsFile.WriteLine(Inventory);
            controlsFile.WriteLine(Pause);
            controlsFile.WriteLine(Accept);
            controlsFile.WriteLine(Cancel);
            controlsFile.Close();
        }
        catch (PathTooLongException)
        {
            Console.WriteLine("Path too long");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found");
        }
        catch (IOException io)
        {
            Console.WriteLine("Read/Write error" + io.Message);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static void LoadControls()
    {
        try
        {
            StreamReader controlsFile = new StreamReader("controls.txt");
            if (controlsFile.ReadLine() == ControlsOrder)
            {
                Up = Convert.ToInt32(controlsFile.ReadLine());
                Down = Convert.ToInt32(controlsFile.ReadLine());
                Left = Convert.ToInt32(controlsFile.ReadLine());
                Right = Convert.ToInt32(controlsFile.ReadLine());
                UseItem = Convert.ToInt32(controlsFile.ReadLine());
                ToggleWalk = Convert.ToInt32(controlsFile.ReadLine());
                Inventory = Convert.ToInt32(controlsFile.ReadLine());
                Pause = Convert.ToInt32(controlsFile.ReadLine());
                Accept = Convert.ToInt32(controlsFile.ReadLine());
                Cancel = Convert.ToInt32(controlsFile.ReadLine());
            }
            controlsFile.Close();
        }
        catch (PathTooLongException)
        {
            Console.WriteLine("Path too long");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found");
        }
        catch (IOException io)
        {
            Console.WriteLine("Read/Write error" + io.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static void SwapKeys(int currentKey, int newKey)
    {
        if (currentKey == Up)
        {
            Up = newKey;
        }
        else if (currentKey == Down)
        {
            Down = newKey;
        }
        else if (currentKey == Left)
        {
            Left = newKey;
        }
        else if (currentKey == Right)
        {
            Right = newKey;
        }
        else if (currentKey == UseItem)
        {
            UseItem = newKey;
        }
        else if (currentKey == Inventory)
        {
            Inventory = newKey;
        }
        else if (currentKey == Pause)
        {
            Pause = newKey;
        }
        else if (currentKey == ToggleWalk)
        {
            ToggleWalk = newKey;
        }
    }
}
