using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.action
{
    public class CategoryModule:AbstractModule
    {

        /**
         * <p>getCategoryList.</p>
         *
         * @param listener a {@link iqq.im.QQActionListener} object.
         * @return a {@link iqq.im.event.QQActionFuture} object.
         */
        public AbstractActionFuture getCategoryList(QQActionListener.OnActionEvent listener)
        {
            return pushHttpAction(new GetBuddyListAction(getContext(), listener));

        }



    }
}
