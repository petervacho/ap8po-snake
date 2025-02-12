using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static System.Console;

namespace Snake
{
    class Program
    {
        // Game settings
        private const int WindowHeightSize = 16;
        private const int WindowWidthSize = 32;
        private const int InitialScore = 5;
        private const int PixelUpdateDelay = 500; // In milliseconds

        static void Main()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                WindowHeight = WindowHeightSize;
                WindowWidth = WindowWidthSize;
            }

            var rand = new Random();

            var score = InitialScore;

            var head = new Pixel(WindowWidth / 2, WindowHeight / 2, ConsoleColor.Red);
            var berry = GenerateBerry(rand);

            var body = new List<Pixel>();

            var currentMovement = Direction.Right;

            var gameOver = false;

            while (!gameOver)
            {
                Clear();

                // Check for game over conditions
                gameOver = IsGameOver(head);

                DrawBorder();

                // Check if head eats the berry
                if (berry.XPos == head.XPos && berry.YPos == head.YPos)
                {
                    score++;
                    berry = GenerateBerry(rand);
                }

                // Check for collision with body
                gameOver |= body.Exists(p => p.XPos == head.XPos && p.YPos == head.YPos);

                // Draw the snake and berry
                body.ForEach(DrawPixel);
                DrawPixel(head);
                DrawPixel(berry);

                // Handle movement input
                var sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds <= PixelUpdateDelay)
                {
                    currentMovement = ReadMovement(currentMovement);
                }

                // Add new head position
                body.Add(new Pixel(head.XPos, head.YPos, ConsoleColor.Green));

                // Move the snake based on the current movement direction
                MoveSnake(ref head, currentMovement);

                // Remove tail if snake length exceeds the score
                if (body.Count > score)
                {
                    body.RemoveAt(0);
                }
            }

            // Game over display
            SetCursorPosition(WindowWidth / 5, WindowHeight / 2);
            WriteLine($"Game over, Score: {score - InitialScore}");
            SetCursorPosition(WindowWidth / 5, WindowHeight / 2 + 1);
            ReadKey();
        }

        static Direction ReadMovement(Direction movement)
        {
            if (KeyAvailable)
            {
                var key = ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && movement != Direction.Down)
                {
                    movement = Direction.Up;
                }
                else if (key == ConsoleKey.DownArrow && movement != Direction.Up)
                {
                    movement = Direction.Down;
                }
                else if (key == ConsoleKey.LeftArrow && movement != Direction.Right)
                {
                    movement = Direction.Left;
                }
                else if (key == ConsoleKey.RightArrow && movement != Direction.Left)
                {
                    movement = Direction.Right;
                }
            }

            return movement;
        }

        static void DrawPixel(Pixel pixel)
        {
            SetCursorPosition(pixel.XPos, pixel.YPos);
            ForegroundColor = pixel.ScreenColor;
            Write("■");
            SetCursorPosition(0, 0); // Reset cursor position
        }

        static void DrawBorder()
        {
            for (int i = 0; i < WindowWidth; i++)
            {
                SetCursorPosition(i, 0);
                Write("■");

                SetCursorPosition(i, WindowHeight - 1);
                Write("■");
            }

            for (int i = 0; i < WindowHeight; i++)
            {
                SetCursorPosition(0, i);
                Write("■");

                SetCursorPosition(WindowWidth - 1, i);
                Write("■");
            }
        }

        static bool IsGameOver(Pixel head)
        {
            return head.XPos == WindowWidth - 1
                || head.XPos == 0
                || head.YPos == WindowHeight - 1
                || head.YPos == 0;
        }

        static Pixel GenerateBerry(Random rand)
        {
            return new Pixel(
                rand.Next(1, WindowWidth - 2),
                rand.Next(1, WindowHeight - 2),
                ConsoleColor.Cyan
            );
        }

        static void MoveSnake(ref Pixel head, Direction currentMovement)
        {
            switch (currentMovement)
            {
                case Direction.Up:
                    head.YPos--;
                    break;
                case Direction.Down:
                    head.YPos++;
                    break;
                case Direction.Left:
                    head.XPos--;
                    break;
                case Direction.Right:
                    head.XPos++;
                    break;
            }
        }

        struct Pixel
        {
            public Pixel(int xPos, int yPos, ConsoleColor color)
            {
                XPos = xPos;
                YPos = yPos;
                ScreenColor = color;
            }

            public int XPos { get; set; }
            public int YPos { get; set; }
            public ConsoleColor ScreenColor { get; set; }
        }

        enum Direction
        {
            Up,
            Down,
            Right,
            Left,
        }
    }
}
