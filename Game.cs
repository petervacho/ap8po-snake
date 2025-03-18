using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SnakeGame;

class Game : Drawable
{
    private readonly Snake _snake;
    private readonly List<Food> _foods;
    private bool _isGameOver;
    private int _score;
    private Direction _currentDirection;
    private Renderer _renderer;
    private float _tickTime;

    public Game()
    {
        // This is the ideal game size, however, setting console height & width onlly works on windows,
        // on other platforms, we'll just work with the size what we have
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;
        }

        _snake = new Snake();
        _foods = new List<Food>();
        for (int i = 0; i < 3; i++)
        {
            Food food = new Food();
            food.GenerateNewPosition();
            _foods.Add(food);
        }
        _score = 5;
        _currentDirection = Direction.MoveRight;
        _isGameOver = false;
        _renderer = new Renderer();
        _tickTime = 150;
    }

    public void Run()
    {
        while (!_isGameOver)
        {
            CheckCollision();
            _renderer.Render(this);

            // Game tick delay
            var stopwatch = Stopwatch.StartNew();
            while (stopwatch.ElapsedMilliseconds <= _tickTime) { }

            _currentDirection = GetUpdatedDirection(_currentDirection);
            _snake.Move(_currentDirection);
        }
        _renderer.DisplayGameOver(_score);
    }

    private void CheckCollision()
    {
        _isGameOver = _snake.HasCollided();

        foreach (var food in _foods.ToList())
        {
            if (_snake.HitFood(food))
            {
                food.ApplyEffect(this);
                food.GenerateNewPosition();
            }
        }
    }

    private static Direction GetUpdatedDirection(Direction currentDirection)
    {
        if (!Console.KeyAvailable)
            return currentDirection;

        var key = Console.ReadKey(true).Key;
        return key switch
        {
            ConsoleKey.UpArrow when currentDirection != Direction.MoveDown => Direction.MoveUp,
            ConsoleKey.DownArrow when currentDirection != Direction.MoveUp => Direction.MoveDown,
            ConsoleKey.LeftArrow when currentDirection != Direction.MoveRight => Direction.MoveLeft,
            ConsoleKey.RightArrow when currentDirection != Direction.MoveLeft =>
                Direction.MoveRight,
            _ => currentDirection,
        };
    }

    public void Draw(Renderer renderer)
    {
        ConsoleColor borderColor = ConsoleColor.White;
        char borderChar = '■';

        for (int i = 0; i < Console.WindowWidth; i++)
        {
            renderer.RenderPixel(new Pixel(i, 0, borderColor), borderChar);
            renderer.RenderPixel(new Pixel(i, Console.WindowHeight - 1, borderColor), borderChar);
        }

        for (int i = 0; i < Console.WindowHeight; i++)
        {
            renderer.RenderPixel(new Pixel(0, i, borderColor), borderChar);
            renderer.RenderPixel(new Pixel(Console.WindowWidth - 1, i, borderColor), borderChar);
        }

        _snake.Draw(renderer);

        foreach (var food in _foods)
        {
            food.Draw(_renderer);
        }
    }

    public void IncreaseScore(int amount) => _score += amount;

    public void GrowSnake() => _snake.Grow();
}
