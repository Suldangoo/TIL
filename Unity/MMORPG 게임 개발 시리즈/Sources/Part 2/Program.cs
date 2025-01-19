using System;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30; // 1/30초
            const char CIRCLE = '\u25cf';

            int lastTick = 0; // 마지막 시간
            while (true)
            {
                #region 프레임 관리
                int currentTick = System.Environment.TickCount;
                if (currentTick - lastTick < WAIT_TICK)
                    continue; // 연산 프레임이 되지 않았으므로 다음 프레임으로 스킵

                lastTick = currentTick;
                #endregion

                // 입력

                // 로직

                // 렌더링
                Console.SetCursorPosition(0, 0);

                for (int i = 0; i < 25; i++)
                {
                    for (int j = 0; j < 25; j++)
                    {
                        Console.ForegroundColor = ConsoleColor.Green; // 색 결정
                        Console.Write(CIRCLE); // 쓰기
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
