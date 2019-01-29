using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMServer
{
    public class MyWebSocketSession
    {
        private WebSocketSession session = null;
        public MyWebSocketSession(WebSocketSession websession)
        {
            this.session = websession;
        }
    
        public string userid
        {
            get;
            set;
        }

        public string sessionid
        {
            get
            {
                if (session != null)
                    return session.SessionID;

                else
                    return "";
            }
        }

        public string ipinfo
        {
            get
            {
                if (session != null)
                    return session.SocketSession.Client.RemoteEndPoint.ToString();
                else
                    return "";
            }
        }

        public void Send(IMMessage imMessage)
        {
            if (session != null)
                 session.Send(imMessage.ToString());
        }
    }
}
