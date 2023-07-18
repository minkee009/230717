using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PositionExtension;

namespace _230717
{
    internal class Program
    {
        static Position PlayerPos = new Position(0,0);
        static Position EnemyPos = new Position(0,0);

        static int[,] map = {
               { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, // [0,x]
               { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, // [1,x]
               { 1, 0, 0, 0, 0, 0, 0, 3, 0, 1 }, // [2,x]
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
            bool isNotOverYet = true;
            while (isNotOverYet)
            {
                //처음 위치
                PlayerPos = new Position(5, 5);
                EnemyPos = new Position(1, 2);

                //첫 화면
                Render();

                ConsoleKeyInfo info = new ConsoleKeyInfo();
                int gameStateFlag = 0;

                while (!(info.Key == ConsoleKey.Escape) && gameStateFlag == 0)
                {
                    //인풋
                    info = Input();

                    //프로세스
                    gameStateFlag = Process(info);

                    //렌더
                    Console.Clear();
                    Render();
                }
                switch (gameStateFlag)
                {
                    case 1:
                        Console.WriteLine("Escape Success");
                        break;
                    case 2:
                        Console.WriteLine("Game Over");
                        break;
                }

               
                Delay(1000);
                Console.WriteLine("Restart ? Y / N");
                string restartgame = Console.ReadLine();

                if ((restartgame == "n") ||(restartgame == "N"))
                    isNotOverYet = false;
                Console.Clear();
            }

        }

        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        static ConsoleKeyInfo Input()
        {
            return Console.ReadKey(true);
        }

        /// <summary>
        /// 게임의 전반 로직을 수행하고 게임의 상태를 플래그 값으로 반환한다. // 0 : 계속, 1 : 승리, 2 : 패배
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        static int Process(ConsoleKeyInfo info)
        {
            var lastPlayerPos = new Position(PlayerPos.x, PlayerPos.y);

            switch (info.Key)
            {
                case ConsoleKey.UpArrow:
                    PlayerPos.y--;
                    break;
                case ConsoleKey.DownArrow:
                    PlayerPos.y++;
                    break;
                case ConsoleKey.LeftArrow:
                    PlayerPos.x--;
                    break;
                case ConsoleKey.RightArrow:
                    PlayerPos.x++;
                    break;
            }

            //플레이어 위치 제한
            PlayerPos.x = PlayerPos.x > 0 ? Math.Min(PlayerPos.x, 9) : 0;
            PlayerPos.y = PlayerPos.y > 0 ? Math.Min(PlayerPos.y, 9) : 0;

            if (map[PlayerPos.y, PlayerPos.x] == 2)
            {
                PlayerPos = lastPlayerPos;
            }


            var lastEnemyPos = new Position(EnemyPos.x, EnemyPos.y);
            var random = new Random();

            var processEnemyPos = PlayerPos - EnemyPos;

            if (PlayerPos.isCollideOtherPos(EnemyPos))
            {
                return 2;
            }

            if (Math.Abs(processEnemyPos.x)
                > Math.Abs(processEnemyPos.y))
            {
                EnemyPos.x += processEnemyPos.x > 0 ? 1 : -1;
            }
            else if (Math.Abs(processEnemyPos.x) == Math.Abs(processEnemyPos.y))
            {
                if(Math.Abs(processEnemyPos.x) == 1)
                {
                    processEnemyPos = lastPlayerPos - EnemyPos;

                    if (Math.Abs(processEnemyPos.x)
                > Math.Abs(processEnemyPos.y))
                    {
                        EnemyPos.x += processEnemyPos.x > 0 ? 1 : -1;
                    }
                    else
                    {
                        EnemyPos.y += processEnemyPos.y > 0 ? 1 : -1;
                    }
                }
                else
                {
                    switch (random.Next(2))
                    {
                        case 0:
                            EnemyPos.y += processEnemyPos.y > 0 ? 1 : -1;
                            break;
                        case 1:
                            EnemyPos.x += processEnemyPos.x > 0 ? 1 : -1;
                            break;
                    }
                }
            }
            else
            {
                EnemyPos.y += processEnemyPos.y > 0 ? 1 : -1;
            }

            if (map[EnemyPos.y, EnemyPos.x] == 2)
            {
                EnemyPos = lastEnemyPos;
            }

            //게임상태 갱신

            if (map[PlayerPos.y, PlayerPos.x] == 3)
            {
                return 1;
            }
            if (PlayerPos.isCollideOtherPos(EnemyPos))
            {
                return 2;
            }
            return 0;
        }

        static void Render()
        {
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if ((PlayerPos.x == x && PlayerPos.y == y) && (PlayerPos.x == EnemyPos.x && PlayerPos.y == EnemyPos.y))
                    {
                        Console.Write("X ");
                    }
                    else if (PlayerPos.x == x && PlayerPos.y == y)
                    {
                        Console.Write("P ");
                    }
                    else if (EnemyPos.x == x && EnemyPos.y == y)
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
                    else if (map[y,x] == 3)
                    {
                        Console.Write("# ");
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
