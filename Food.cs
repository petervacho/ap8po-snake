namespace SnakeGame;

class Food : FoodBase
{
    public override void Draw(Renderer renderer)
    {
        renderer.RenderPixel(Position, '■');
    }

    public override void GenerateNewPosition()
    {
        int maxWidth = Console.WindowWidth - 2;
        int maxHeight = Console.WindowHeight - 2;

        Position = new Pixel(
            RandomGenerator.Next(1, maxWidth > 0 ? maxWidth : 1),
            RandomGenerator.Next(1, maxHeight > 0 ? maxHeight : 1),
            ConsoleColor.Cyan
        );
    }
}
