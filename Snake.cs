using System;
using System.Collections.Generic;

namespace SnakeGame;

class Snake : Drawable
{
    public Pixel Head { get; private set; }
    public List<Pixel> Body { get; private set; }
    private const ConsoleColor SnakeColor = ConsoleColor.Green;

    public Snake()
    {
        Head = new Pixel(Console.WindowWidth / 2, Console.WindowHeight / 2, ConsoleColor.Red);
        Body = new List<Pixel>();
    }

    public void Draw(Renderer renderer)
    {
        renderer.RenderPixel(Head, '■');
        foreach (var part in Body)
        {
            renderer.RenderPixel(part, '■');
        }
    }

    public void Move(Direction direction)
    {
        Body.Add(new Pixel(Head.XPos, Head.YPos, SnakeColor));

        // Since Pixel is a struct, we need to reset it to a new one when moving
        // (mutating structs isn't possible)
        switch (direction)
        {
            case Direction.MoveUp:
                Head = new Pixel(Head.XPos, Head.YPos - 1, Head.ScreenColor);
                break;
            case Direction.MoveDown:
                Head = new Pixel(Head.XPos, Head.YPos + 1, Head.ScreenColor);
                break;
            case Direction.MoveLeft:
                Head = new Pixel(Head.XPos - 1, Head.YPos, Head.ScreenColor);
                break;
            case Direction.MoveRight:
                Head = new Pixel(Head.XPos + 1, Head.YPos, Head.ScreenColor);
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

    /**
     * Check if the snake has colided with a game border or with itself, triggering
     * a game-over condition.
     */
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

    /**
     * Check if the snake hit a piece of food.
     */
    public bool HitFood(Food food)
    {
        return Head.XPos == food.Position.XPos && Head.YPos == food.Position.YPos;
    }
}
