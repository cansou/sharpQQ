using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
  public   interface QQNotifyListener
    {
        void onNotifyEvent(QQNotifyEvent evt);
    }
}
