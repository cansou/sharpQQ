using QQWpfApplication1.evt;
using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.bean
{
    class OffPicItem:ContentItem
    {
        
	private Boolean success;
	private String filePath;
	private String fileName;
	private int fileSize;
	
	/**
	 * <p>Constructor for OffPicItem.</p>
	 */
	public OffPicItem() {
	}

	/**
	 * <p>Constructor for OffPicItem.</p>
	 *
	 * @param text a {@link java.lang.String} object.
	 * @throws iqq.im.QQException if any.
	 */
	public OffPicItem(String text)  {
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
	 * @param isSuccess the isSuccess to set
	 */
	public void setSuccess(Boolean isSuccess) {
		this.success = isSuccess;
	}

	/**
	 * <p>Getter for the field <code>filePath</code>.</p>
	 *
	 * @return the filePath
	 */
	public String getFilePath() {
		return filePath;
	}

	/**
	 * <p>Setter for the field <code>filePath</code>.</p>
	 *
	 * @param filePath the filePath to set
	 */
	public void setFilePath(String filePath) {
		this.filePath = filePath;
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
	 * @param fileName the fileName to set
	 */
	public void setFileName(String fileName) {
		this.fileName = fileName;
	}

	/**
	 * <p>Getter for the field <code>fileSize</code>.</p>
	 *
	 * @return the fileSize
	 */
	public int getFileSize() {
		return fileSize;
	}

	/**
	 * <p>Setter for the field <code>fileSize</code>.</p>
	 *
	 * @param fileSize the fileSize to set
	 */
	public void setFileSize(int fileSize) {
		this.fileSize = fileSize;
	}
	public Type getType() {
		return Type.OFFPIC;
	}

	/** {@inheritDoc} */
	public Object toJson()  {
		//[\"offpic\",\"/27d736df-2a59-4007-8701-7218bc70898d\",\"Beaver.bmp\",14173]
		JSONArray json = new JSONArray();
		json.put("offpic");
		json.put(filePath);
		json.put(fileName);
		json.put(fileSize);
		return json;
	}

	/** {@inheritDoc} */
	public void fromJson(String text)  {
		//["offpic",{"success":1,"file_path":"/7acccf74-0fcd-4bbd-b885-03a5cc2f7507"}]
		try {
			JSONArray json = new JSONArray(new JSONTokener(new StringReader(text)));
			JSONObject pic = json.getJSONObject(1);
			success = pic.getInt("success") == 1 ? true : false;
			filePath = pic.getString("file_path");
		} catch (JSONException e) {
			throw new QQException(QQWpfApplication1.evt.QQException.QQErrorCode.JSON_ERROR, e);
		}
		
	}


    }
}
