using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.bean
{
    public class QQGroupMember:QQStranger
    {
	private QQGroup group;
	private String card;

	/**
	 * <p>Getter for the field <code>group</code>.</p>
	 *
	 * @return a {@link iqq.im.bean.QQGroup} object.
	 */
	public QQGroup getGroup() {
		return group;
	}

	/**
	 * <p>Setter for the field <code>group</code>.</p>
	 *
	 * @param group a {@link iqq.im.bean.QQGroup} object.
	 */
	public void setGroup(QQGroup group) {
		this.group = group;
	}

	/**
	 * <p>Getter for the field <code>card</code>.</p>
	 *
	 * @return the card
	 */
	public String getCard() {
		return card;
	}

	/**
	 * <p>Setter for the field <code>card</code>.</p>
	 *
	 * @param card
	 *            the card to set
	 */
	public void setCard(String card) {
		this.card = card;
	}


    }
}
