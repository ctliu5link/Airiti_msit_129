using DelegateEvent_Leo_WeiChung.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEvent_Leo_WeiChung
{

    class Program
    {
        //delegate 支援out and ref
        public static string abbb(int a )
        {

            return "(a + b)";
        }
        public static void abc(Func<int,string> abc)
        {
            Console.WriteLine(abc(5));
        }



        public delegate void Delegate(List<CPlayer> listPlayer);        
        static CChatMember 鄉民A = new CChatMember() { name = "鄉民A" };
        static CChatMember 鄉民B = new CChatMember() { name = "鄉民B" };
        static CChatMember 鄉民C = new CChatMember() { name = "鄉民C" };
        static CChatRoom 鄉民看戲區 = new CChatRoom();
        
        static void Main(string[] args)
        {
            abc(abbb);

            bool newGame = true;
            var random = new Random();
            do
            {
                Console.Clear();
                Console.WriteLine("遊戲開始");                
                Console.WriteLine();

                // 模擬遊戲
                int round = 1; // 回合數
                CPlayer Tom = new CPlayer("Tom", 150);
                Tom.HPChange0 += HPChange0;                
                CPlayer John = new CPlayer("John", 200);
                John.HPChange0 += HPChange0;
                CBoss 巴哈姆特 = new CBoss("巴哈姆特", 20, 30, "龍之怒", 30);

                鄉民看戲區.NewInfo += 鄉民A.SendMe;
                鄉民看戲區.NewInfo += 鄉民B.SendMe;
                鄉民看戲區.NewInfo += 鄉民C.SendMe;

                Console.WriteLine("鄉民A 已加入聊天室");
                Console.WriteLine("鄉民B 已加入聊天室");
                Console.WriteLine("鄉民C 已加入聊天室");
                Console.WriteLine();

                // Boss attack 模式 round % 3
                // 1：隨機普攻一名玩家。
                // 2：隨機普攻一名玩家，隨機重擊一名玩家。
                // 3：隨機普攻一名玩家，施放技能。
                // 有玩家死亡時觸發 event 對聊天室內的成員發送訊息並發送信件通知。


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
                
                Console.WriteLine();
                Console.WriteLine("============================= 輸入任意鍵繼續 =============================");
                Console.ReadLine();

                do
                {
                    var list = new List<CPlayer> { Tom, John };
                    //Console.WriteLine($"================================= 回合{String.Format("{0,2}", round)} =================================");
                    Console.WriteLine($"                                 [回合{String.Format("{0,2}", round)}]                                 ");
                    Console.WriteLine();
                    if (round % 3 == 1) // 隨機挑一個進行普攻
                    {
                        delegate1.Invoke(listPlayer);
                        Console.WriteLine();
                    }
                    else if (round % 3 == 2) // 隨機挑一個進行普攻，隨機挑一個進行重擊。
                    {
                        delegate2.Invoke(listPlayer);
                        Console.WriteLine();
                    }
                    else // 隨機挑一個進行普攻、使用範圍技能.
                    {
                        delegate3.Invoke(listPlayer);
                        Console.WriteLine();
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
            string message = $"{((CPlayer)sender).name}已經死了";
            Console.WriteLine();
            Console.WriteLine("死亡 Event 觸發，發送聊天室通知中...");            
            Console.WriteLine();
            ChatRoom(message, 鄉民看戲區);
            foreach(var item in 鄉民看戲區.NewInfo.GetInvocationList())
            {
                if (((CChatMember)鄉民看戲區.NewInfo.GetInvocationList()[0].Target).name == "鄉民A")
                {
                    鄉民看戲區.NewInfo -= 鄉民A.SendMe;
                    Console.WriteLine("鄉民A 已退出聊天室");
                }
            }
             
            Console.WriteLine();
            Console.WriteLine("死亡 Event 觸發，發送信件通知中...");
            string title = "對戰結果";
            List<string> toList = new List<string>(ConfigurationManager.AppSettings["toList"].Split(new char[] { ';' }));            
            
            CMail cMail = new CMail();
            cMail.SendMail("伺服器公告<rovingwind93@gmail.com>", toList, title, message);
            cMail.SendMail2("伺服器公告2<rovingwind93@gmail.com>", toList, title, message);
            
            Console.WriteLine(message + " 通知信已發送");
            Console.WriteLine();
        }
        
        private static void ChatRoom(string message, CChatRoom cChatRoom)
        {
            cChatRoom.發送聊天室通知(message);
        }
    }    
}
