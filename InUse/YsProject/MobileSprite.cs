
abstract class MobileSprite : Sprite
{
    protected int hp;
    protected int speed;
    protected string name;

    public MobileSprite(int hp)
    {
        SetHp(hp);
    }

    public int GetHp() { return this.hp; }
    public int GetSpeed() { return this.speed; }
    public string GetName() { return this.name; }
    public void SetHp(int hp) { this.hp = hp; }
    public void SetSpeed(int speed) { this.speed = speed; }
    public void SetName(string name) { this.name = name; }
}
