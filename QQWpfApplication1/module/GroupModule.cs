using QQWpfApplication1.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class GroupModule:AbstractModule
    {


        /**
         * <p>getGroupList.</p>
         *
         * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
         * @ a {@link iqq.im.event.AbstractActionFuture} object.
         */
        public AbstractActionFuture getGroupList(QQActionListener.OnActionEvent listener)
        {
             return  pushHttpAction(new GetGroupListAction(getContext(), listener));
        }

        /**
         * <p>updateGroupMessageFilter.</p>
         *
         * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
         * @ a {@link iqq.im.event.AbstractActionFuture} object.
         */
        public AbstractActionFuture updateGroupMessageFilter(QQActionListener.OnActionEvent listener)
        {
             return  pushHttpAction(new UpdateGroupMessageFilterAction(getContext(), listener));
        }

        /**
         * <p>getGroupFace.</p>
         *
         * @param group a {@link iqq.im.bean.QQGroup} object.
         * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
         * @ a {@link iqq.im.event.AbstractActionFuture} object.
         */
        public AbstractActionFuture getGroupFace(QQGroup group, QQActionListener.OnActionEvent listener)
        {
             return  pushHttpAction(new GetGroupFaceAction(getContext(), listener, group));
        }

        /**
         * <p>getGroupInfo.</p>
         *
         * @param group a {@link iqq.im.bean.QQGroup} object.
         * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
         * @ a {@link iqq.im.event.AbstractActionFuture} object.
         */
        public AbstractActionFuture getGroupInfo(QQGroup group, QQActionListener.OnActionEvent listener)
        {
             return  pushHttpAction(new GetGroupInfoAction(getContext(), listener, group));
        }

        /**
         * <p>getGroupGid.</p>
         *
         * @param group a {@link iqq.im.bean.QQGroup} object.
         * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
         * @ a {@link iqq.im.event.AbstractActionFuture} object.
         */
        public AbstractActionFuture getGroupGid(QQGroup group, QQActionListener.OnActionEvent listener)
        {
             return  pushHttpAction(new GetGroupAccoutAction(getContext(), listener, group));
        }

        /**
         * <p>getMemberStatus.</p>
         *
         * @param group a {@link iqq.im.bean.QQGroup} object.
         * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
         * @ a {@link iqq.im.event.AbstractActionFuture} object.
         */
        public AbstractActionFuture getMemberStatus(QQGroup group, QQActionListener.OnActionEvent listener)
        {
             return  pushHttpAction(new GetGroupMemberStatusAction(getContext(), listener, group));
        }

        /**
         * <p>searchGroup.</p>
         *
         * @param resultList a {@link iqq.im.bean.QQGroupSearchList} object.
         * @param listener a {@link iqq.im.QQActionListener.OnActionEvent} object.
         * @ a {@link iqq.im.event.AbstractActionFuture} object.
         */
        //public AbstractActionFuture searchGroup(QQGroupSearchList resultList, QQActionListener.OnActionEvent listener)
        //{
        //     return  pushHttpAction(new SearchGroupInfoAction(getContext(), listener, resultList));
        //}

    }
}
