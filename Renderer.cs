namespace SnakeGame;

class Renderer
{
    public Renderer() { }

    public void Render(Drawable drawRoot)
    {
        Clear();
        drawRoot.Draw(this);
    }

    private void Clear()
    {
        Console.Clear();
    }

    public void RenderPixel(Pixel pixel, char character)
    {
        Console.SetCursorPosition(pixel.XPos, pixel.YPos);
        Console.ForegroundColor = pixel.ScreenColor;
        Console.Write(character);
        Console.SetCursorPosition(0, 0);
    }

    public void DisplayGameOver(int score)
    {
        Console.SetCursorPosition(Console.WindowWidth / 5, Console.WindowHeight / 2);
        Console.WriteLine($"Game Over! Score: {score - 5}");
        Console.SetCursorPosition(Console.WindowWidth / 5, Console.WindowHeight / 2 + 1);
        Console.ReadKey();
    }
}
