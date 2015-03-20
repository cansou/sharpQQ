using QQWpfApplication1.evt;
using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.bean
{
    class FontItem:ContentItem
    {
        
	private String name = "宋体";
	private int size = 12;
	private Boolean bold;
	private Boolean underline;
	private Boolean italic;
	private int color = 0;

	/**
	 * <p>Constructor for FontItem.</p>
	 */
	public FontItem() {
	}

	/**
	 * <p>Constructor for FontItem.</p>
	 *
	 * @param text a {@link java.lang.String} object.
	 * @throws iqq.im.QQException if any.
	 */
	public FontItem(String text)  {
		fromJson(text);
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
	 * <p>Getter for the field <code>size</code>.</p>
	 *
	 * @return the size
	 */
	public int getSize() {
		return size;
	}

	/**
	 * <p>Setter for the field <code>size</code>.</p>
	 *
	 * @param size
	 *            the size to set
	 */
	public void setSize(int size) {
		this.size = size;
	}

	/**
	 * <p>isBold.</p>
	 *
	 * @return a Boolean.
	 */
	public Boolean isBold() {
		return bold;
	}

	/**
	 * <p>Setter for the field <code>bold</code>.</p>
	 *
	 * @param bold a Boolean.
	 */
	public void setBold(Boolean bold) {
		this.bold = bold;
	}

	/**
	 * <p>isUnderline.</p>
	 *
	 * @return a Boolean.
	 */
	public Boolean isUnderline() {
		return underline;
	}

	/**
	 * <p>Setter for the field <code>underline</code>.</p>
	 *
	 * @param underline a Boolean.
	 */
	public void setUnderline(Boolean underline) {
		this.underline = underline;
	}

	/**
	 * <p>isItalic.</p>
	 *
	 * @return a Boolean.
	 */
	public Boolean isItalic() {
		return italic;
	}

	/**
	 * <p>Setter for the field <code>italic</code>.</p>
	 *
	 * @param italic a Boolean.
	 */
	public void setItalic(Boolean italic) {
		this.italic = italic;
	}

	/**
	 * <p>Getter for the field <code>color</code>.</p>
	 *
	 * @return the color
	 */
	public int getColor() {
		return color;
	}

	/**
	 * <p>Setter for the field <code>color</code>.</p>
	 *
	 * @param color
	 *            the color to set
	 */
	public void setColor(int color) {
		this.color = color;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see iqq.im.bean.content.ContentItem#getType()
	 */
	/** {@inheritDoc} */
	public Type getType() {
		// TODO Auto-generated method stub
		return Type.FONT;
	}

	/** {@inheritDoc} */
	public Object toJson()  {
		// ["font",{"size":10,"color":"808080","style":[0,0,0],"name":"\u65B0\u5B8B\u4F53"}]
		try {
			JSONArray json = new JSONArray();
			json.put("font");
			JSONObject font = new JSONObject();
			font.put("size", size);
			font.put("color", String.Format("%06x", color));
			JSONArray style = new JSONArray();
			style.put(bold ? 1 : 0);
			style.put(italic ? 1 : 0);
			style.put(underline ? 1 : 0);
			font.put("style", style);
			font.put("name", name);
			json.put(font);
			return json;
		} catch (JSONException e) {
			throw new QQException(QQWpfApplication1.evt.QQException.QQErrorCode.JSON_ERROR, e);
		}
	}

	/** {@inheritDoc} */
	public void fromJson(String text)  {
		try {
			JSONArray json = new JSONArray(new JSONTokener(new StringReader(text)));
			JSONObject font = json.getJSONObject(1);
			size = font.getInt("size");
			color = int.Parse(font.getString("color"), NumberStyles.HexNumber);
			JSONArray style = font.getJSONArray("style");
			bold = style.getInt(0) == 1 ? true : false;
			italic = style.getInt(1) == 1 ? true : false;
			underline = style.getInt(2) == 1 ? true : false;
			name = font.getString("name");
		}  catch (JSONException e) {
			throw new QQException(QQWpfApplication1.evt.QQException.QQErrorCode.JSON_ERROR, e);
		}

	}


    }
}
