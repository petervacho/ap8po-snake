namespace SnakeGame;

class Food : Drawable
{
    private static readonly Random RandomGenerator = new Random();
    public Pixel Position { get; private set; }

    public Food()
    {
        GenerateNewFood();
    }

    public void Draw(Renderer renderer)
    {
        renderer.RenderPixel(Position, '■');
    }

    public void GenerateNewFood()
    {
        Position = new Pixel(
            RandomGenerator.Next(1, Console.WindowWidth - 2),
            RandomGenerator.Next(1, Console.WindowHeight - 2),
            ConsoleColor.Cyan
        );
    }
}
