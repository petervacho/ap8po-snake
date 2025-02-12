using System;
using System.Collections.Generic;

namespace SnakeGame;

class Snake
{
    public Pixel Head { get; private set; }
    public List<Pixel> Body { get; private set; }
    private const ConsoleColor SnakeColor = ConsoleColor.Green;

    public Snake()
    {
        Head = new Pixel(Console.WindowWidth / 2, Console.WindowHeight / 2, ConsoleColor.Red);
        Body = new List<Pixel>();
    }

    public void Move(Direction direction)
    {
        Body.Add(new Pixel(Head.XPos, Head.YPos, SnakeColor));

        switch (direction)
        {
            case Direction.MoveUp:
                Head.YPos--;
                break;
            case Direction.MoveDown:
                Head.YPos++;
                break;
            case Direction.MoveLeft:
                Head.XPos--;
                break;
            case Direction.MoveRight:
                Head.XPos++;
                break;
        }

        // Only start removing body parts after we have over 5, until than, keep growing the snake
        if (Body.Count > 5)
        {
            Body.RemoveAt(0);
        }
    }

    public void Grow()
    {
        Body.Add(new Pixel(Head.XPos, Head.YPos, SnakeColor));
    }

    public bool HasCollided()
    {
        if (
            Head.XPos == 0
            || Head.XPos == Console.WindowWidth - 1
            || Head.YPos == 0
            || Head.YPos == Console.WindowHeight - 1
        )
        {
            return true;
        }

        foreach (var part in Body)
        {
            if (part.XPos == Head.XPos && part.YPos == Head.YPos)
            {
                return true;
            }
        }

        return false;
    }
}
