class Player : MobileSprite
{
    protected int gold;
    protected int exp;
    protected int lvl;
    protected int attack;
    protected int defense;
    protected bool recovering;
    protected bool walking;

    public Player(string image) : base(2, image)
    {
        gold = 0;
        exp = 0;
        lvl = 1;
        xSpeed = 1;
        ySpeed = 1;
        attack = lvl * 2; // temporary
        attack = lvl * 2;
        recovering = false;
        walking = false;
        width = 25;
        height = 32;
    }

    public int GetGold() { return this.gold; }
    public int GetExp() { return this.exp; }
    public int GetLvl() { return this.lvl; }
    public int GetAttack() { return this.attack; }
    public int GetDefense() { return this.defense; }
    public bool IsRecovering() { return this.recovering; }
    public bool IsWalking() { return this.walking; }

    public void SetGold(int gold) { this.gold = gold; }
    public void SetExp(int exp) { this.exp = exp; }
    public void SetLvl(int lvl) { this.lvl = lvl; }
    public void SetAttack(int attack) { this.attack = attack; }
    public void SetDefense(int defense) { this.defense = defense; }
    public void StartRecovering() { recovering = true; }
    public void StopRecovering() { recovering = false; }

    //When walking is true, the character moves slower, otherwise, it moves faster
    public void ChangeMovement() { this.walking = !walking; }

    
    public void MoveRight(Room room)
    {
        if(room.CanMoveTo(x, y, x + width, y + width))
        {
            x += xSpeed;
            ChangeDirection(RIGHT);
        }
    }

    public void MoveLeft(Room room)
    {
        if (room.CanMoveTo(x, y, x + width, y + width))
        {
            x -= xSpeed;
            ChangeDirection(LEFT);
        }
    }

    public void MoveUp(Room room)
    {
        if (room.CanMoveTo(x, y, x + width, y + width))
        {
            y -= ySpeed;
            ChangeDirection(UP);
        }
    }

    public void MoveDown(Room room)
    {
        if (room.CanMoveTo(x, y, x + width, y + width))
        {
            y += ySpeed;
            ChangeDirection(DOWN);
        }
    }
}