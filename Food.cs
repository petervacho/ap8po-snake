namespace SnakeGame;

class Food : FoodBase
{
    public override void Draw(Renderer renderer)
    {
        renderer.RenderPixel(Position, '■');
    }

    public override void GenerateNewPosition()
    {
        Position = new Pixel(
            RandomGenerator.Next(1, Console.WindowWidth - 2),
            RandomGenerator.Next(1, Console.WindowHeight - 2),
            ConsoleColor.Cyan
        );
    }
}
