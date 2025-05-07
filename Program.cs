using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace HelloWorld;

struct PianoKey
{
    public KeyboardKey Key;
}

class Program
{
    const int SCREEN_WIDTH = 600;
    const int SCREEN_HEIGHT = 300;

    public static void Main()
    {
        PianoKey[] whiteKeys;
        InitializeWhiteKeys(out whiteKeys);
        PianoKey[] blackKeys;
        InitializeBlackKeys(out blackKeys);

        Raylib.InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, "stupidpiano");

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            DrawKeyboard(ref whiteKeys, ref blackKeys);
            
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    private static void InitializeWhiteKeys(out PianoKey[] whiteKeys)
    {
        whiteKeys = new PianoKey[7];
        whiteKeys[0] = new() { Key = KeyboardKey.A };
        whiteKeys[1] = new() { Key = KeyboardKey.S };
        whiteKeys[2] = new() { Key = KeyboardKey.D };
        whiteKeys[3] = new() { Key = KeyboardKey.F };
        whiteKeys[4] = new() { Key = KeyboardKey.G };
        whiteKeys[5] = new() { Key = KeyboardKey.H };
        whiteKeys[6] = new() { Key = KeyboardKey.J };
    }

    private static void InitializeBlackKeys(out PianoKey[] blackKeys)
    {
        blackKeys = new PianoKey[5];
        blackKeys[0] = new() { Key = KeyboardKey.W };
        blackKeys[1] = new() { Key = KeyboardKey.E };
        blackKeys[2] = new() { Key = KeyboardKey.T };
        blackKeys[3] = new() { Key = KeyboardKey.Y };
        blackKeys[4] = new() { Key = KeyboardKey.U };
    }

    private static void DrawKeyboard(ref PianoKey[] whiteKeys, ref PianoKey[] blackKeys)
    {
        // draw white keys
        int marginX = (int)(SCREEN_WIDTH * 0.05f);
        int marginY = (int)(SCREEN_WIDTH * 0.05f) + 10;

        int paddingX = 10;
        int x = marginX;
        int whiteKeyWidth = (SCREEN_WIDTH - (marginX * 2 + paddingX * 7)) / whiteKeys.Length;
        int whiteKeyHeight = SCREEN_HEIGHT - (marginY * 2);

        foreach (var whiteKey in whiteKeys)
        {
            if (Raylib.IsKeyDown(whiteKey.Key))
            {
                Raylib.DrawRectangle(
                    x,
                    marginY,
                    whiteKeyWidth, 
                    whiteKeyHeight, 
                    Color.Red);
            }

            Raylib.DrawRectangleLines(
                x,
                marginY,
                whiteKeyWidth, 
                whiteKeyHeight, 
                Color.Black);
            
            Raylib.DrawText(whiteKey.Key.ToString(), x + whiteKeyWidth / 2 - 5, marginY + whiteKeyHeight + 5, 10, Color.Black);

            x += paddingX + whiteKeyWidth;   
        }

        // draw black keys
        DrawBlackKeys(ref blackKeys, whiteKeyWidth, paddingX, marginX, whiteKeyWidth / 2 + paddingX, marginY - 10);
    }

    private static void DrawBlackKeys(ref PianoKey[] blackKeys, int whiteKeyWidth, int paddingX, int marginX, int offsetX, int startY)
    {
        int x = marginX;

        int blackKeyWidth = (int)(whiteKeyWidth * 0.8f);

        for (int i = 0; i < blackKeys.Length; i++)
        {
            var blackKey = blackKeys[i];

            if (Raylib.IsKeyDown(blackKey.Key))
            {
                Raylib.DrawRectangle(
                    x + offsetX,
                    startY,
                    blackKeyWidth, 
                    (int)(SCREEN_HEIGHT * 0.5f), 
                    Color.Red);
                
                Raylib.DrawRectangleLines(
                    x + offsetX,
                    startY,
                    blackKeyWidth, 
                    (int)(SCREEN_HEIGHT * 0.5f), 
                    Color.Black);
            }
            else
            {
                Raylib.DrawRectangle(
                    x + offsetX,
                    startY,
                    blackKeyWidth, 
                    (int)(SCREEN_HEIGHT * 0.5f), 
                    Color.Black);
            }

            Raylib.DrawText(blackKey.Key.ToString(), x + offsetX + blackKeyWidth / 2, startY - 15, 10, Color.Black);

            if (i - 1 % 3 == 0)
                x += (paddingX + whiteKeyWidth) * 2;
            else
                x += (paddingX + whiteKeyWidth);
        }
    }
}