using QQWpfApplication1.action;
using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.bean
{
    public class CFaceItem:ContentItem
    {

        
	private Boolean success;
	private long fileId;
	private String fileName;
	private String key;
	private String server;

	/**
	 * <p>Constructor for CFaceItem.</p>
	 */
	public CFaceItem() {
	}

	/**
	 * <p>Constructor for CFaceItem.</p>
	 *
	 * @param text a {@link java.lang.String} object.
	 * @throws iqq.im.QQException if any.
	 */
	public CFaceItem(String text)  {
		fromJson(text);
	}

	/**
	 * <p>isSuccess.</p>
	 *
	 * @return the isSuccess
	 */
	public Boolean isSuccess() {
		return success;
	}

	/**
	 * <p>setSuccess.</p>
	 *
	 * @param isSuccess
	 *            the isSuccess to set
	 */
	public void setSuccess(Boolean isSuccess) {
		this.success = isSuccess;
	}

	/**
	 * <p>Getter for the field <code>fileName</code>.</p>
	 *
	 * @return the fileName
	 */
	public String getFileName() {
		return fileName;
	}

	/**
	 * <p>Setter for the field <code>fileName</code>.</p>
	 *
	 * @param fileName
	 *            the fileName to set
	 */
	public void setFileName(String fileName) {
		this.fileName = fileName;
	}

	/**
	 * <p>Getter for the field <code>fileId</code>.</p>
	 *
	 * @return the fileId
	 */
	public long getFileId() {
		return fileId;
	}

	/**
	 * <p>Setter for the field <code>fileId</code>.</p>
	 *
	 * @param fileId
	 *            the fileId to set
	 */
	public void setFileId(long fileId) {
		this.fileId = fileId;
	}

	/**
	 * <p>Getter for the field <code>key</code>.</p>
	 *
	 * @return the key
	 */
	public String getKey() {
		return key;
	}

	/**
	 * <p>Setter for the field <code>key</code>.</p>
	 *
	 * @param key
	 *            the key to set
	 */
	public void setKey(String key) {
		this.key = key;
	}

	/**
	 * <p>Getter for the field <code>server</code>.</p>
	 *
	 * @return the server
	 */
	public String getServer() {
		return server;
	}

	/**
	 * <p>Setter for the field <code>server</code>.</p>
	 *
	 * @param server
	 *            the server to set
	 */
	public void setServer(String server) {
		this.server = server;
	}

	/*
	 * (non-Javadoc)
	 * 
	 * @see iqq.im.bean.content.ContentItem#getType()
	 */
	/** {@inheritDoc} */
    public override Type getType()
    {
		return Type.CFACE;
	}

	/** {@inheritDoc} */
    public override Object toJson()
    {
		// [\"cface\",\"group\",\"5F7E31F0001EF4310865F1FF4549B12B.jPg\"]
		JSONArray json = new JSONArray();
		json.put("cface");
		json.put("group");
		json.put(fileName);
		return json;
	}

	/** {@inheritDoc} */
    public override void fromJson(String text)
    {
		// ["cface",{"name":"{94E8E5BA-9304-043E-F028-93986EEF0450}.jpg","file_id":445318646,"key":"PN576E5TyB53muY9","server":"124.115.11.111:8000"}]
		//["cface","4D72EF8CF64D53DECB31ABC2B601AB23.jpg",""],
		try {
			JSONArray json = new JSONArray(new JSONTokener(new StringReader(text)));
			if(json.get(1) is String){
				this.setFileName((String)json.get(1));
			}else{
				JSONObject pic = json.getJSONObject(1);
				this.setFileName(pic.getString("name"));
				this.setFileId(pic.getLong("file_id"));
				this.setKey(pic.getString("key"));
				this.setServer(pic.getString("server"));
			}
		} catch (JSONException e) {
			throw new QQException(QQWpfApplication1.action.QQException.QQErrorCode.JSON_ERROR, e);
		}

	}


    }
}
