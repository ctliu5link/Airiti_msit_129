using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEvent_Leo_WeiChung.Class
{
    // public event EventHandler HPChange0;
    public class CPlayer
    {
        public event EventHandler HPChange0;

        private int hp;
        public string name { get; } // 玩家名稱
        public int HP
        {
            get { return this.hp; }
            set
            {
                this.hp = value;
                if (value == 0)
                {
                    if (HPChange0 != null)
                    {
                        HPChange0.Invoke(this, null);
                    }
                };
            }
        } // 玩家血量        
        public CPlayer(string name, int HP)
        {
            this.name = name;
            this.HP = HP;
        }
    }
}
