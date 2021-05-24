using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEvent_Leo_WeiChung.Class
{
    class CBoss
    {
        Random random = new Random();
        public string name { get; } // NPC 名稱
        public int normalAttack { get; set; } // 普攻傷害
        public int heavyAttack { get; set; } // 重擊傷害
        public string skillName { get; } // 技能名稱
        public int skillAttack { get; } // 技能傷害
        public CBoss(string name, int normalAttack, int heavyAttack, string skillName, int skillAttack)
        {
            this.name = name;
            this.normalAttack = normalAttack;
            this.heavyAttack = heavyAttack;
            this.skillName = skillName;
            this.skillAttack = skillAttack;
        }
        public void NormalAttack(List<CPlayer> listPlayer) // 隨機挑一個進行普攻
        {
            CPlayer player;
            bool run = true;
            do
            {
                player = listPlayer[random.Next(listPlayer.Count)];                
                if (player.HP != 0)
                {                    
                    int hp = (player.HP - normalAttack >= 0) ? player.HP - normalAttack : 0;
                    Console.WriteLine($"{name} 使用 普通攻擊 隨機目標：{player.name}。");
                    Console.WriteLine($"{blank}對 {player.name}({player.HP}) 造成 {normalAttack} 點傷害! {player.name} 生命值剩 {hp} !");
                    player.HP = hp;
                    run = false;
                }
            } while (run);
        }

        public void HeavyAttack(List<CPlayer> listPlayer) // 重擊
        {
            CPlayer player;
            bool run = true;
            do
            {
                player = listPlayer[random.Next(listPlayer.Count)];
                if (player.HP != 0)
                {
                    int hp = (player.HP - heavyAttack >= 0) ? player.HP - heavyAttack : 0;
                    Console.WriteLine($"{name} 使用 重型攻擊 隨機目標：{player.name}。");
                    Console.WriteLine($"{blank}對 {player.name}({player.HP}) 造成 {heavyAttack} 點傷害! {player.name} 生命值剩 {hp} !");
                    player.HP = hp;
                    run = false;
                }
            } while (run);
        }

        public void SkillAttack(List<CPlayer> listPlayer) // 技能攻擊
        {
            Console.WriteLine($"{name} 使用 技能攻擊： {skillName}");
            foreach (var player in listPlayer)
            {
                if (player.HP != 0) // 判斷是否還有血量
                {
                    int hp = (player.HP - skillAttack >= 0) ? player.HP - skillAttack : 0;
                    
                    Console.WriteLine($"{blank}對 {player.name}({player.HP}) 造成 {skillAttack} 點傷害! {player.name} 生命值剩 {hp} !");
                    player.HP = hp;
                }                
            }
        }
        string blank = new string(' ', 31);
    }
}
