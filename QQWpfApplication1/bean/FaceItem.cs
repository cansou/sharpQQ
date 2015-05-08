using QQWpfApplication1.action;
using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.bean
{
    class FaceItem:ContentItem
    {
        
	/**
	 * 表情的ID
	 */
	private int id;
	
	/**
	 * <p>Constructor for FaceItem.</p>
	 */
	public FaceItem() {
	}

	/**
	 * <p>Constructor for FaceItem.</p>
	 *
	 * @param text a {@link java.lang.String} object.
	 * @throws iqq.im.QQException if any.
	 */
	public FaceItem(String text)  {
		fromJson(text);
	}
	
	/**
	 * <p>Constructor for FaceItem.</p>
	 *
	 * @param id a int.
	 */
	public FaceItem(int id){
		this.id = id;
	}
	
	/**
	 * <p>Getter for the field <code>id</code>.</p>
	 *
	 * @return the id
	 */
	public int getId() {
		return id;
	}

	/**
	 * <p>Setter for the field <code>id</code>.</p>
	 *
	 * @param id the id to set
	 */
	public void setId(int id) {
		this.id = id;
	}

	/* (non-Javadoc)
	 * @see iqq.im.bean.content.ContentItem#getType()
	 */
	/** {@inheritDoc} */
    public override Type getType()
    {
		// TODO Auto-generated method stub
		return Type.FACE;
	}

	/** {@inheritDoc} */
    public override Object toJson()
    {
		JSONArray json = new JSONArray();
		json.put("face");
		json.put(id);
		return json;
	}

	/** {@inheritDoc} */
    public override void fromJson(String text)
    {
		try {
			JSONArray json = new JSONArray(new JSONTokener(new StringReader(text)));
			id = json.getInt(1);
		} catch (JSONException e) {
			throw new QQException(QQWpfApplication1.action.QQException.QQErrorCode.JSON_ERROR, e);
		}
	}



    }
}
