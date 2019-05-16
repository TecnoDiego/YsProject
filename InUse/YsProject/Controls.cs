
class Controls
{
    protected int up;
    protected int down;
    protected int left;
    protected int right;
    protected int useItem;
    protected int toggleWalk;
    protected int inventory;
    protected int pause;
    protected int accept;
    protected int cancel;

    public Controls()
    {
        up = SdlHardware.KEY_UP;
        down = SdlHardware.KEY_DOWN;
        left = SdlHardware.KEY_LEFT;
        right = SdlHardware.KEY_RIGHT;
        useItem = SdlHardware.KEY_Z;
        toggleWalk = SdlHardware.KEY_X;
        inventory = SdlHardware.KEY_C;
        pause = SdlHardware.KEY_ESC;
        accept = useItem;
        cancel = toggleWalk;
    }

    public int GetUp() { return up; }
    public int GetDown() { return down; }
    public int GetLeft() { return left; }
    public int GetRight() { return right; }
    public int GetUseItem() { return useItem; }
    public int GetToggleWalk() { return toggleWalk; }
    public int GetInventory() { return inventory; }
    public int GetPause() { return pause; }
    public int GetAccept() { return accept; }
    public int GetCancel() { return cancel; }

    public void SetUp(int up) { this.up = up; }
    public void SetDown(int down) { this.down = down; }
    public void SetLeft(int left) { this.left = left; }
    public void SetRight(int right) { this.right = right; }
    public void SetUseItem(int useItem) { this.useItem = useItem; }
    public void SetToggleWalk(int toggleWalk) { this.toggleWalk = toggleWalk; }
    public void SetInventory(int inventory) { this.inventory = inventory; }
    public void SetPause(int pause) { this.pause = pause; }
    public void SetAccept(int accept) { this.accept = accept; }
    public void SetCancel(int cancel) { this.cancel = cancel; }

    public bool CheckKeysInUse()
    {
        if (SdlHardware.KeyPressed(GetUp()) ||
                SdlHardware.KeyPressed(GetDown()) ||
                SdlHardware.KeyPressed(GetLeft()) ||
                SdlHardware.KeyPressed(GetRight()) ||
                SdlHardware.KeyPressed(GetUseItem()) ||
                SdlHardware.KeyPressed(GetUseItem()) ||
                SdlHardware.KeyPressed(GetToggleWalk()) ||
                SdlHardware.KeyPressed(GetInventory()) ||
                SdlHardware.KeyPressed(GetPause()) ||
                SdlHardware.KeyPressed(GetAccept()) ||
                SdlHardware.KeyPressed(GetCancel()))
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

    }
}
