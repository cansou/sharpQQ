using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class BuddyModule:AbstractModule
    {

        /**
         * <p>getOnlineBuddy.</p>
         *
         * @param listener a {@link iqq.im.QQActionListener} object.
         * @return a {@link iqq.im.event.QQActionFuture} object.
         */
        public AbstractActionFuture getOnlineBuddy(QQActionListener.OnActionEvent listener)
        {
            return pushHttpAction(new GetOnlineFriendAction(getContext(), listener));
        }

        /**
         * <p>getRecentList.</p>
         *
         * @param listener a {@link iqq.im.QQActionListener} object.
         * @return a {@link iqq.im.event.QQActionFuture} object.
         */
        public AbstractActionFuture getRecentList(QQActionListener.OnActionEvent listener)
        {
            return pushHttpAction(new GetRecentListAction(getContext(), listener));
        }

    }
}
