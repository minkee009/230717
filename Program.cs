using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _230717
{
    internal class Program
    {
        static int playerX = 0;
        static int playerY = 0;

        static int enemyX = 0;
        static int enemyY = 0;

        static int[,] map = {
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // [0,x]
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, // [1,x]
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, // [2,x]
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, // [3,x]
               { 1, 0, 0, 0, 2, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 2, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 2, 2, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
        };

        static void Main(string[] args)
        {

            //처음 위치
            playerX = 5;
            playerY = 5;

            enemyX = 1;
            enemyY = 2;

            //첫 화면
            Render();

            ConsoleKeyInfo info = new ConsoleKeyInfo();
            bool isEndGame = false;

            while (!(info.Key == ConsoleKey.Escape) && !isEndGame)
            {
                ////인풋
                info = Input();

                ////프로세스
                isEndGame = Process(info);

                ////렌더
                //화면 그리기
                Console.Clear();
                Render();
            }
        }


        static ConsoleKeyInfo Input()
        {
            return Console.ReadKey(true);
        }

        static bool Process(ConsoleKeyInfo info)
        {
            int lastPlayerX = playerX;
            int lastPlayerY = playerY;

            switch (info.Key)
            {
                case ConsoleKey.UpArrow:
                    playerY--;
                    break;
                case ConsoleKey.DownArrow:
                    playerY++;
                    break;
                case ConsoleKey.LeftArrow:
                    playerX--;
                    break;
                case ConsoleKey.RightArrow:
                    playerX++;
                    break;
            }

            var lastEnemyX = enemyX;
            var lastEnemyY = enemyY;
            var random = new Random();
            var processEnemyPosX = playerX - enemyX;
            var processEnemyPosY = playerY - enemyY;

            if ((playerX == enemyX) && (playerY == enemyY))
            {
                return true;
            }
            

            if (Math.Abs(processEnemyPosX)
                > Math.Abs(processEnemyPosY))
            {
                enemyX += processEnemyPosX > 0 ? 1 : -1;
            }
            else if (Math.Abs(processEnemyPosX) == Math.Abs(processEnemyPosY))
            {
                switch (random.Next(2))
                {
                    case 0:
                        enemyY += processEnemyPosY > 0 ? 1 : -1;
                        break;
                    case 1:
                        enemyX += processEnemyPosX > 0 ? 1 : -1;
                        break;
                }
            }
            else
            {
                enemyY += processEnemyPosY > 0 ? 1 : -1;
            }

            if (map[enemyY, enemyX] == 2)
            {
                enemyX = lastEnemyX;
                enemyY = lastEnemyY;
            }

            //플레이어 위치 제한
            playerX = playerX > 0 ? Math.Min(playerX, 9) : 0;
            playerY = playerY > 0 ? Math.Min(playerY, 9) : 0;

            if (map[playerY, playerX] == 2)
            {
                playerX = lastPlayerX;
                playerY = lastPlayerY;
            }

            return (playerX == enemyX) && (playerY == enemyY);
        }

        static void Render()
        {
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if ((playerX == x && playerY == y) && (playerX == enemyX && playerY == enemyY))
                    {
                        Console.Write("X ");
                    }
                    else if (playerX == x && playerY == y)
                    {
                        Console.Write("P ");
                    }
                    else if (enemyX == x && enemyY == y)
                    {
                        Console.Write("E ");
                    }
                    else if (map[y, x] == 0)
                    {
                        Console.Write("  ");
                    }
                    else if (map[y, x] == 2)
                    {
                        Console.Write("! ");
                    }
                    else
                    {
                        Console.Write("* ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
