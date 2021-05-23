using DelegateEvent_Leo_WeiChung.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEvent_Leo_WeiChung
{
    class Program
    {
        public delegate void Delegate(CPlayer CPlayer);
        static void Main(string[] args)
        {
            bool newGame = true;
            var random = new Random();
            do
            {
                Console.WriteLine("遊戲開始");
                
                Console.WriteLine();
                // 模擬遊戲
                int round = 1; // 回合數
                CPlayer Tom = new CPlayer("Tom", 150);
                Tom.HPChange0 += HPChange0;
                CPlayer John = new CPlayer("John", 200);
                John.HPChange0 += HPChange0;
                CBoss 巴哈姆特 = new CBoss("巴哈姆特", 20, 30, "龍之怒", 30);

                // Boss attack 模式 round % 3
                // 1：隨機普攻一名玩家。
                // 2：隨機普攻且重擊一名玩家。
                // 3：隨機普攻一名玩家，施放技能。

                Delegate delegate1 = new Delegate(巴哈姆特.NormalAttack);

                Delegate delegate2 = new Delegate(巴哈姆特.NormalAttack);
                delegate2 += 巴哈姆特.HeavyAttack;

                //Delegate delegate2 = delegate1 + 巴哈姆特.HeavyAttack;

                Delegate delegate3 = new Delegate(巴哈姆特.NormalAttack);                
                delegate3 += 巴哈姆特.SkillAttack;

                List<CPlayer> listPlayer = new List<CPlayer> { Tom, John };
                List<CBoss> listBoss = new List<CBoss> { 巴哈姆特 };

                Console.WriteLine("建立玩家");
                foreach (var item in listPlayer)
                    Console.WriteLine("名稱：" + item.name + ", HP：" + item.HP);
                Console.WriteLine();

                Console.WriteLine("建立Boss");
                foreach (var item in listBoss)
                    Console.WriteLine("Boss：" + item.name + 
                        ", 普攻傷害：" + item.normalAttack +
                        ", 重擊傷害：" + item.heavyAttack +
                        ", 技能名稱：" + item.skillName +
                        ", 技能傷害：" + item.skillAttack);

                do
                {
                    var list = new List<CPlayer> { Tom, John };
                    Console.WriteLine($"================================= 回合{String.Format("{0,2}",round)} =================================");
                    Console.WriteLine();
                    if (round % 3 == 1) // 隨機挑一個進行普攻
                    {
                        bool flag = true;
                        while (flag)
                        {
                            var player = list[random.Next(list.Count)];
                            if (player.HP != 0) // 判斷隨機的player是否還有HP
                            {
                                delegate1.Invoke(player);
                                flag = false;
                            }
                        }
                    }
                    else if (round % 3 == 2) // 隨機挑一個進行普攻，隨機挑一個進行重擊。
                    {
                        bool flag = true;
                        while (flag)
                        {
                            var player = list[random.Next(list.Count)];
                            if (player.HP != 0) // 判斷隨機的player是否還有HP
                            {
                                delegate2.Invoke(player);
                                flag = false;
                            }
                        }
                    }
                    else // 使用增益技能、隨機挑一個進行普攻、使用技能
                    {
                        bool flag = true;
                        while (flag)
                        {
                            var player = list[random.Next(list.Count)];
                            if (player.HP != 0) // 判斷隨機的player是否還有HP
                            {
                                delegate3.Invoke(player);
                                flag = false;
                            }
                        }
                    }
                    round++;

                    foreach (var item in list)
                    {
                        Console.WriteLine(item.name + "：" + item.HP + " HP");
                    }
                    Console.WriteLine();
                    Console.WriteLine("============================= 輸入任意鍵繼續 =============================");
                    Console.ReadLine();
                } while (Tom.HP != 0 || John.HP != 0); // 玩家血量都歸零時結束迴圈

                Console.WriteLine("Game Over!!");
                Console.WriteLine("任意鍵：重新遊戲，0：離開：");                
                bool success = Int32.TryParse(Console.ReadLine(), out int number); // 轉換選項
                if (success)
                    if (number == 0)
                        newGame = false;
            } while (newGame);
        }
        private static void HPChange0(object sender, EventArgs e)
        {
            Console.WriteLine($"{((CPlayer)sender).name}已經死了");
        }
    }
    public delegate void DelegateAOE(string str);

}
