using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEvent_Leo_WeiChung.Class
{
    class CChatMember
    {
        public string name;

        public void SendMe(string message)
        {
            Console.WriteLine($"我是{name}，收到通知：{message}");
        }
    }
}
