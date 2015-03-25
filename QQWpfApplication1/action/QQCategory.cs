using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class QQCategory
    {
	private int index;
	private int sort;
	private String name;

	private List<QQBuddy> buddyList = new List<QQBuddy>();

	/**
	 * <p>getQQBuddyByUin.</p>
	 *
	 * @param uin a int.
	 * @return a {@link iqq.im.bean.QQBuddy} object.
	 */
	public QQBuddy getQQBuddyByUin(int uin) {
		if (buddyList.Count>0 && uin != 0) {
			foreach (QQBuddy b in  buddyList) {
				if (b.getUin() == uin) {
					return b;
				}
			}
		}
		return null;
	}

	/**
	 * <p>Getter for the field <code>buddyList</code>.</p>
	 *
	 * @return a {@link java.util.List} object.
	 */
	public List<QQBuddy> getBuddyList() {
		return buddyList;
	}

	/**
	 * <p>Setter for the field <code>buddyList</code>.</p>
	 *
	 * @param buddyList a {@link java.util.List} object.
	 */
	public void setBuddyList(List<QQBuddy> buddyList) {
		this.buddyList = buddyList;
	}

	/**
	 * <p>getOnlineCount.</p>
	 *
	 * @return a int.
	 */
	public int getOnlineCount() {
		int count = 0;
		foreach(QQBuddy buddy in buddyList){
			QQStatus stat = buddy.getStatus();
            //if(QQStatus.isGeneralOnline(stat)){
            //    count++;
            //}
		}
		return count;
	}
	
	/**
	 * <p>getBuddyCount.</p>
	 *
	 * @return a int.
	 */
	public int getBuddyCount(){
		return buddyList!=null ? buddyList.Count : 0;
	}

	/**
	 * <p>Getter for the field <code>index</code>.</p>
	 *
	 * @return a int.
	 */
	public int getIndex() {
		return index;
	}

	/**
	 * <p>Setter for the field <code>index</code>.</p>
	 *
	 * @param index a int.
	 */
	public void setIndex(int index) {
		this.index = index;
	}

	/**
	 * <p>Getter for the field <code>name</code>.</p>
	 *
	 * @return a {@link java.lang.String} object.
	 */
	public String getName() {
		return name;
	}

	/**
	 * <p>Setter for the field <code>name</code>.</p>
	 *
	 * @param name a {@link java.lang.String} object.
	 */
	public void setName(String name) {
		this.name = name;
	}

	/**
	 * <p>Getter for the field <code>sort</code>.</p>
	 *
	 * @return a int.
	 */
	public int getSort() {
		return sort;
	}

	/**
	 * <p>Setter for the field <code>sort</code>.</p>
	 *
	 * @param sort a int.
	 */
	public void setSort(int sort) {
		this.sort = sort;
	}

    }
}
