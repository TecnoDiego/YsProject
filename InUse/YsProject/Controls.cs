
class Controls
{
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
        Pause = SdlHardware.KEY_ESC;
        Accept = UseItem;
        Cancel = ToggleWalk;
    }

    public bool CheckKeysInUse()
    {
       if (SdlHardware.KeyPressed(Up) ||
                SdlHardware.KeyPressed(Down) ||
                SdlHardware.KeyPressed(Left) ||
                SdlHardware.KeyPressed(Right) ||
                SdlHardware.KeyPressed(UseItem) ||
                SdlHardware.KeyPressed(ToggleWalk) ||
                SdlHardware.KeyPressed(Inventory) ||
                SdlHardware.KeyPressed(Pause) ||
                SdlHardware.KeyPressed(Accept) ||
                SdlHardware.KeyPressed(Cancel))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SwapKey(int currentKey, int newKey)
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
        else if (currentKey == Accept)
        {
            Accept = newKey;
        }
        else if (currentKey == Cancel)
        {
            Cancel = newKey;
        }
        else if (currentKey == ToggleWalk)
        {
            ToggleWalk = newKey;
        }
    }
}
