﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.evt
{
    interface QQNotifyListener
    {
        public void onNotifyEvent(QQNotifyEvent evt);
    }
}
