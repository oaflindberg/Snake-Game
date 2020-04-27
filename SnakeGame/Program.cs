using System;
using System.Threading;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            int[] xPos = new int[100]; 
            xPos[0] = 35;
            int[] yPos = new int[100];
            yPos[0] = 13;
            int appleX;
            int appleY;
            bool isEaten;
            var applesEaten = 0;
            var score = 0;
            var gameSpeed = 150m;
            var isPlaying = true;
            
            var random = new Random();
            
            PaintSnake(applesEaten, xPos, yPos, out xPos, out yPos);
            SetApplePlace(random, out appleX, out appleY);
            PlaceApple(appleX, appleY);
            GenerateWall();

            do
            {
                ConsoleKey? command = Console.ReadKey().Key;

                while (command != null)
                {
                    switch (command)
                {
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(xPos[0], yPos[0]);
                        Console.Write(" ");
                        xPos[0]--;
                        break;
                 
                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(xPos[0], yPos[0]);
                        Console.Write(" ");
                        xPos[0]++;
                        break;
                 
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(xPos[0], yPos[0]);
                        Console.Write(" ");
                        yPos[0]--;
                        break;
                 
                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(xPos[0], yPos[0]);
                        Console.Write(" ");
                        yPos[0]++;
                        break;
                }

                    PaintSnake(applesEaten, xPos, yPos, out xPos, out yPos);
                    
                    isEaten = CheckIfApplesHit(xPos[0], yPos[0], appleX, appleY);

                    if (isEaten)
                    {
                        SetApplePlace(random, out appleX, out appleY);
                        PlaceApple(appleX, appleY);
                        applesEaten++;
                        score = applesEaten * 100;
                        gameSpeed *= .925m;
                    }

                    var hasCrashed = HitWall(xPos[0], yPos[0]);

                    if (hasCrashed)
                    {
                        command = null;
                        isPlaying = false;
                        Console.SetCursorPosition(4, 3);
                        Console.WriteLine("GAME OVER!");
                        Console.SetCursorPosition(4, 4);
                        Console.WriteLine($"Score: {score}!");
                    }

                    if (Console.KeyAvailable) command = Console.ReadKey().Key;
                    Thread.Sleep(Convert.ToInt32(gameSpeed));
                    
                }
            } while (isPlaying);
        }
        
        private static void GenerateWall()
        {
            for (var i = 1; i < 41; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(1, i);
                Console.Write("#");
                Console.SetCursorPosition(70, i);
                Console.Write("#");
            }
            
            for (var i = 1; i < 71; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(i, 1);
                Console.Write("#");
                Console.SetCursorPosition(i, 40);
                Console.Write("#");
            }
        }
        
        private static bool HitWall(int xPos, int yPos)
        {
            if (xPos == 1 || xPos == 70 || yPos == 1 || yPos == 23)
            {
                return true;
            }
            return false;
        }
        
        private static void SetApplePlace(Random random, out int appleX, out int appleY)
        {
            appleX = random.Next(2, 68);
            appleY = random.Next(2, 20);
        }
        
        private static void PlaceApple(int appleX, int appleY)
        {
            Console.SetCursorPosition(appleX, appleY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("O");
        }
        
        private static bool CheckIfApplesHit(int xPos, int yPos, int appleX, int appleY)
        {
            if (xPos == appleX && yPos == appleY)
            {
                return true;
            }

            return false;
        }
        
        private static void PaintSnake(int applesEaten, int[] xPosIn, int[] yPosIn, out int[] xPosOut, out int[] yPosOut)
        {
            // HEAD
            Console.SetCursorPosition(xPosIn[0], yPosIn[0]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine('@');
            
            //BODY
            for (int i = 1; i < applesEaten +1; i++)
            {
                Console.SetCursorPosition(xPosIn[i], yPosIn[i]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine('◇'); 
            }
            
            // REMOVE LAST PART
            Console.SetCursorPosition(xPosIn[applesEaten + 1], yPosIn[applesEaten + 1]);
            Console.WriteLine(" ");

            for (int i = applesEaten+1; i > 0; i--)
            {
                xPosIn[i] = xPosIn[i - 1];
                yPosIn[i] = yPosIn[i - 1];
            }

            xPosOut = xPosIn;
            yPosOut = yPosIn;
        }
    }
}
