using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEvent_Leo_WeiChung.Class
{
    class CBoss
    {
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
        public void NormalAttack(CPlayer cplayer) // 普攻
        {
            int hp = (cplayer.HP - normalAttack >= 0) ? cplayer.HP - normalAttack : 0;
            Console.WriteLine($"{name} 使用 普通攻擊 對 {cplayer.name}({cplayer.HP}) 造成 {normalAttack} 點傷害! {cplayer.name} 生命值剩 {hp} !");
            cplayer.HP = hp;
        }

        public void HeavyAttack(CPlayer cplayer) // 重擊
        {
            int hp = (cplayer.HP - heavyAttack >= 0) ? cplayer.HP - heavyAttack : 0;
            Console.WriteLine($"{name} 使用 重型攻擊 對 {cplayer.name}({cplayer.HP}) 造成 {heavyAttack} 點傷害! {cplayer.name} 生命值剩 {hp} !");
            cplayer.HP = hp;
        }

        public void SkillAttack(CPlayer cplayer) // 技能攻擊
        {
            int hp = (cplayer.HP - skillAttack >= 0) ? cplayer.HP - skillAttack : 0;
            Console.WriteLine($"{name} 使用 {skillName} 對 {cplayer.name}({cplayer.HP}) 造成 {skillAttack} 點傷害! {cplayer.name} 生命值剩 {hp} !");
            cplayer.HP = hp;
        }
    }
}
