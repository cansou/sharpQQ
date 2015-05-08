using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.bean
{
    public class QQDiscuzMember:QQStranger
    {
        
	
	private QQDiscuz discuz;

	/**
	 * <p>Getter for the field <code>discuz</code>.</p>
	 *
	 * @return a {@link iqq.im.bean.QQDiscuz} object.
	 */
	public QQDiscuz getDiscuz() {
		return discuz;
	}

	/**
	 * <p>Setter for the field <code>discuz</code>.</p>
	 *
	 * @param discuz a {@link iqq.im.bean.QQDiscuz} object.
	 */
	public void setDiscuz(QQDiscuz discuz) {
		this.discuz = discuz;
	}
	

    }
}
