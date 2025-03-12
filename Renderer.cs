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
        ConsoleColor borderColor = ConsoleColor.White;

        for (int i = 0; i < Console.WindowWidth; i++)
        {
            RenderPixel(new Pixel(i, 0, borderColor));
            RenderPixel(new Pixel(i, Console.WindowHeight - 1, borderColor));
        }

        for (int i = 0; i < Console.WindowHeight; i++)
        {
            RenderPixel(new Pixel(0, i, borderColor));
            RenderPixel(new Pixel(Console.WindowWidth - 1, i, borderColor));
        }
    }

    private static void RenderPixel(Pixel pixel, char character = '■')
    {
        Console.SetCursorPosition(pixel.XPos, pixel.YPos);
        Console.ForegroundColor = pixel.ScreenColor;
        Console.Write(character);
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
