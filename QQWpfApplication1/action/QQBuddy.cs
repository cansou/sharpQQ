using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class QQBuddy:QQUser
    {
        
	
	private String markname; // 备注
	private QQCategory category;

	/**
	 * <p>Getter for the field <code>category</code>.</p>
	 *
	 * @return a {@link iqq.im.bean.QQCategory} object.
	 */
	public QQCategory getCategory() {
		return category;
	}

	/**
	 * <p>Setter for the field <code>category</code>.</p>
	 *
	 * @param category a {@link iqq.im.bean.QQCategory} object.
	 */
	public void setCategory(QQCategory category) {
		this.category = category;
	}
	
	/**
	 * <p>Getter for the field <code>markname</code>.</p>
	 *
	 * @return a {@link java.lang.String} object.
	 */
	public String getMarkname() {
		return markname;
	}

	/**
	 * <p>Setter for the field <code>markname</code>.</p>
	 *
	 * @param markname a {@link java.lang.String} object.
	 */
	public void setMarkname(String markname) {
		this.markname = markname;
	}

    }
}
