using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.bean
{
    class TextItem:ContentItem
    {
	private String content;
	
	/**
	 * <p>Constructor for TextItem.</p>
	 */
	public TextItem() {
	}

	/**
	 * <p>Constructor for TextItem.</p>
	 *
	 * @param text a {@link java.lang.String} object.
	 */
	public TextItem(String text){
		fromJson(text);
	}
	
	/**
	 * <p>Getter for the field <code>content</code>.</p>
	 *
	 * @return the content
	 */
	public String getContent() {
		return content;
	}

	/**
	 * <p>Setter for the field <code>content</code>.</p>
	 *
	 * @param content the content to set
	 */
	public void setContent(String content) {
		this.content = content;
	}

	/* (non-Javadoc)
	 * @see iqq.im.bean.content.ContentItem#getType()
	 */
	/** {@inheritDoc} */
    public override Type getType()
    {
		// TODO Auto-generated method stub
		return Type.TEXT;
	}

	/** {@inheritDoc} */
    public override Object toJson()
    {
		return content;
	}

	/** {@inheritDoc} */
	public override void fromJson(String text) {
		content = text;
	}



    }
}
