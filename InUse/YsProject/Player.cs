class Player : MobileSprite
{
    protected int gold;
    protected int exp;
    protected int lvl;
    protected bool recovering;
    protected bool walking;

    public Player() : base(10)
    {
        gold = 0;
        exp = 0;
        lvl = 1;
        recovering = false;
        walking = false;
    }

    public int GetGold() { return this.gold; }
    public int GetExp() { return this.exp; }
    public int GetLvl() { return this.lvl; }
    public bool IsRecovering() { return this.recovering; }
    public bool IsWalking() { return this.walking; }

    public void SetGold(int gold) { this.gold = gold; }
    public void SetExp(int exp) { this.exp = exp; }
    public void SetLvl(int lvl) { this.lvl = lvl; }
    public void StartRecovering() { recovering = true; }
    public void StopRecovering() { recovering = false; }

    //When walking is true, the character moves slower, otherwise, it moves faster
    public void ChangeMovement() { this.walking = !walking; }

    /*
    public void MoveRight(Room room)
    {
        
    }

    public void MoveLeft(Room room)
    {
        
    }

    public void MoveUp(Room room)
    {

    }

    public void MoveDown(Room room)
    {

    }
    */
}