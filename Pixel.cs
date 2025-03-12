namespace SnakeGame;

// Keep pixel as a class, rather than a struct, as structs are immutable
// which is pretty annoying when working with them.
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
