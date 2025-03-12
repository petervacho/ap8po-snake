namespace SnakeGame;

public struct Pixel
{
    public int XPos;
    public int YPos;
    public ConsoleColor ScreenColor;

    public Pixel(int xPos, int yPos, ConsoleColor color)
    {
        XPos = xPos;
        YPos = yPos;
        ScreenColor = color;
    }
}
