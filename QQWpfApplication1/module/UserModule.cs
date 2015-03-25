using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class UserModule:AbstractModule
    {


        /**
         * <p>getUserFace.</p>
         *
         * @param user a {@link iqq.im.bean.QQUser} object.
         * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
         * @return a {@link iqq.im.event.AbstractActionFuture} object.
         */
        public AbstractActionFuture getUserFace(QQUser user, QQActionListener.OnActionEvent listener)
        {
            return pushHttpAction(new GetFriendFaceAction(getContext(), listener, user));
        }

        /**
         * <p>getUserInfo.</p>
         *
         * @param user a {@link iqq.im.bean.QQUser} object.
         * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
         * @return a {@link iqq.im.event.AbstractActionFuture} object.
         */
        public AbstractActionFuture getUserInfo(QQUser user, QQActionListener.OnActionEvent listener)
        {
            return pushHttpAction(new GetFriendInfoAction(getContext(), listener, user));
        }

        /**
         * <p>getUserAccount.</p>
         *
         * @param user a {@link iqq.im.bean.QQUser} object.
         * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
         * @return a {@link iqq.im.event.AbstractActionFuture} object.
         */
        public AbstractActionFuture getUserAccount(QQUser user, QQActionListener.OnActionEvent listener)
        {
            return pushHttpAction(new GetFriendAccoutAction(getContext(), listener, user));
        }

        /**
         * <p>getUserSign.</p>
         *
         * @param user a {@link iqq.im.bean.QQUser} object.
         * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
         * @return a {@link iqq.im.event.AbstractActionFuture} object.
         */
        public AbstractActionFuture getUserSign(QQUser user, QQActionListener.OnActionEvent listener)
        {
            return pushHttpAction(new GetFriendSignAction(getContext(), listener, user));
        }

        /**
         * <p>getUserLevel.</p>
         *
         * @param user a {@link iqq.im.bean.QQUser} object.
         * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
         * @return a {@link iqq.im.event.AbstractActionFuture} object.
         */
        public AbstractActionFuture getUserLevel(QQUser user, QQActionListener.OnActionEvent listener)
        {
            return pushHttpAction(new GetUserLevelAction(getContext(), listener, user));
        }


        /**
         * <p>changeStatus.</p>
         *
         * @param status a {@link iqq.im.bean.QQStatus} object.
         * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
         * @return a {@link iqq.im.event.AbstractActionFuture} object.
         */
        public AbstractActionFuture changeStatus(QQStatus status, QQActionListener.OnActionEvent listener)
        {
            return pushHttpAction(new ChangeStatusAction(getContext(), listener, status));
        }

        /**
         * <p>getStrangerInfo.</p>
         *
         * @param user a {@link iqq.im.bean.QQUser} object.
         * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
         * @return a {@link iqq.im.event.AbstractActionFuture} object.
         */
        public AbstractActionFuture getStrangerInfo(QQUser user, QQActionListener.OnActionEvent listener)
        {
            return pushHttpAction(new GetStrangerInfoAction(getContext(), listener, user));
        }

    }
}
