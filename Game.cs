using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SnakeGame;

class Game
{
    private readonly Snake _snake;
    private readonly Food _food;
    private bool _isGameOver;
    private int _score;
    private Direction _currentDirection;

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
        _food = new Food();
        _score = 5;
        _currentDirection = Direction.MoveRight;
        _isGameOver = false;
    }

    public void Run()
    {
        while (!_isGameOver)
        {
            Console.Clear();
            CheckCollision();
            Renderer.RenderGame(_snake, _food);

            // Game tick time
            var stopwatch = Stopwatch.StartNew();
            while (stopwatch.ElapsedMilliseconds <= 300) { }

            _currentDirection = GetUpdatedDirection(_currentDirection);
            _snake.Move(_currentDirection);
        }
        Renderer.DisplayGameOver(_score);
    }

    private void CheckCollision()
    {
        _isGameOver = _snake.HasCollided();

        if (_snake.HitFood(_food))
        {
            _score++;
            _food.GenerateNewFood();
            _snake.Grow();
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
}
