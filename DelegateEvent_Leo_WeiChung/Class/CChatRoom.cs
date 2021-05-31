using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateEvent_Leo_WeiChung.Class
{
    class CChatRoom
    {
        public delegate void SendTo(string message);
        public SendTo NewInfo;
        public void 發送聊天室通知(string message)
        {
            //觸發事件
            NewInfo.Invoke(message);
        }
    }
}
