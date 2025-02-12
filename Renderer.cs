using System;

namespace SnakeGame;

class Renderer
{
    public static void RenderGame(Snake snake, Food food)
    {
        RenderGameBorder();
        RenderPixel(snake.Head);
        foreach (var part in snake.Body)
        {
            RenderPixel(part);
        }
        RenderPixel(food.Position);
    }

    private static void RenderGameBorder()
    {
        for (int i = 0; i < Console.WindowWidth; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.Write("■");

            Console.SetCursorPosition(i, Console.WindowHeight - 1);
            Console.Write("■");
        }

        for (int i = 0; i < Console.WindowHeight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("■");

            Console.SetCursorPosition(Console.WindowWidth - 1, i);
            Console.Write("■");
        }
    }

    private static void RenderPixel(Pixel pixel)
    {
        Console.SetCursorPosition(pixel.XPos, pixel.YPos);
        Console.ForegroundColor = pixel.ScreenColor;
        Console.Write("■");
        Console.SetCursorPosition(0, 0);
    }

    public static void DisplayGameOver(int score)
    {
        Console.SetCursorPosition(Console.WindowWidth / 5, Console.WindowHeight / 2);
        Console.WriteLine($"Game Over! Score: {score - 5}");
        Console.SetCursorPosition(Console.WindowWidth / 5, Console.WindowHeight / 2 + 1);
        Console.ReadKey();
    }
}
