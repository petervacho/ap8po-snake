namespace SnakeGame;

abstract class FoodBase : Drawable
{
    protected static readonly Random RandomGenerator = new Random();
    public Pixel Position { get; protected set; }

    public FoodBase()
    {
        GenerateNewPosition();
    }

    public abstract void Draw(Renderer renderer);
    public abstract void GenerateNewPosition();
}
