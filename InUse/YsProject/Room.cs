
class Room
{
    protected int mapHeight = 13, mapWidth = 17;
    protected int tileHeight = 32, tileWidth = 32;
    protected int leftMargin = 100, topMargin = 50;

    protected string[] levelData;

    public Room()
    {
        
    }
    public void DrawOnHiddenScreen()
    {
        for (int row = 0; row < mapHeight; row++)
        {
            for (int col = 0; col < mapWidth; col++)
            {
                int posX = col * tileWidth + leftMargin;
                int posY = row * tileHeight + topMargin;
                /*switch (levelData[row][col])
                {
                        // TO DO
                }*/
            }
        }
    }

    public bool CanMoveTo(int x1, int y1, int x2, int y2)
    {
        for (int column = 0; column < mapWidth; column++)
        {
            for (int row = 0; row < mapHeight; row++)
            {
                char tile = levelData[row][column];
                if (tile != ' ')
                {
                    int x1tile = leftMargin + column * tileWidth;
                    int y1tile = topMargin + row * tileHeight;
                    int x2tile = x1tile + tileWidth;
                    int y2tile = y1tile + tileHeight;
                    if ((x1tile < x2) &&
                        (x2tile > x1) &&
                        (y1tile < y2) &&
                        (y2tile > y1)
                        )
                        return false;
                }
            }
        }
        return true;
    }
}

