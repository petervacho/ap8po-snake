namespace SnakeGame;

public class Pixel
{
    public int XPos { get; set; }
    public int YPos { get; set; }
    public ConsoleColor ScreenColor { get; set; }

    public Pixel(int xPos, int yPos, ConsoleColor color)
    {
        XPos = xPos;
        YPos = yPos;
        ScreenColor = color;
    }
}
