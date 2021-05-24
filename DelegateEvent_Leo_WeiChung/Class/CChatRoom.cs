using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEvent_Leo_WeiChung.Class
{
    class CChatRoom
    {
        public delegate void 通知對象(string message);
        public 通知對象 最新通知;
        public void 發送聊天室通知(string message)
        {
            //觸發事件
            最新通知.Invoke(message);
        }
    }
}
