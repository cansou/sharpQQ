using QQWpfApplication1.evt;
using QQWpfApplication1.json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.bean
{
    class QQMsg
    {
        
	public enum Type {
		BUDDY_MSG, 		//好友消息
		GROUP_MSG,		// 群消息
		DISCUZ_MSG,		//讨论组消息
		SESSION_MSG,	//临时会话消息
		EXIT_MSG
	}

	private long id; // 消息ID
	private long id2;	//暂时不知什么含义
	private Type type; // 消息类型
	private QQUser to;	// 消息发送方
	private QQUser from; // 消息发送方
	private QQGroup group; // 所在群
	private QQDiscuz discuz;	//讨论组
	private DateTime date; // 发送时间
	private List<ContentItem> contentList; // 消息列表

	/**
	 * <p>Constructor for QQMsg.</p>
	 */
	public QQMsg() {
		contentList = new List<ContentItem>();
	}

	/**
	 * <p>packContentList.</p>
	 *
	 * @throws iqq.im.QQException if any.
	 * @return a {@link java.lang.String} object.
	 */
	public String packContentList()  {
		// ["font",{"size":10,"color":"808080","style":[0,0,0],"name":"\u65B0\u5B8B\u4F53"}]
		JSONArray json = new JSONArray();
		foreach (ContentItem contentItem in contentList) {
			json.put(contentItem.toJson());
		}
		return json.ToString();
	}

	/**
	 * <p>parseContentList.</p>
	 *
	 * @param text a {@link java.lang.String} object.
	 * @throws iqq.im.QQException if any.
	 */
	public void parseContentList(String text) {
		try {
			JSONArray json = new JSONArray(new JSONTokener(new StringReader(text)));
			for (int i = 0; i < json.length(); i++) {
				Object value = json.get(i);
				if(value is JSONArray){
					JSONArray items = (JSONArray) value;
                    
					ContentItem.Type type = (ContentItem.Type)Enum.Parse(typeof(ContentItem.Type), items.getString(0), true);
					switch (type) {
						case ContentItem.Type.FACE:    addContentItem(new FaceItem(items.ToString())); break;
						case ContentItem.Type.FONT:    addContentItem(new FontItem(items.ToString())); break;
						case ContentItem.Type.CFACE:   addContentItem(new CFaceItem(items.ToString())); break;
						case ContentItem.Type.OFFPIC: addContentItem(new OffPicItem(items.ToString())); break;
                        default: break;
					}
				}else if( value is String){
					addContentItem(new TextItem((String) value));
				}else{
					throw new QQException(QQWpfApplication1.evt.QQException.QQErrorCode.UNKNOWN_ERROR, "unknown msg content type:" + value.ToString());
				}
			}
		} catch (JSONException e) {
			throw new QQException(QQWpfApplication1.evt.QQException.QQErrorCode.JSON_ERROR, e);
		}
	}

	/**
	 * <p>addContentItem.</p>
	 *
	 * @param contentItem a {@link iqq.im.bean.content.ContentItem} object.
	 */
	public void addContentItem(ContentItem contentItem) {
		contentList.Add(contentItem);
	}

	/**
	 * <p>deleteContentItem.</p>
	 *
	 * @param contentItem a {@link iqq.im.bean.content.ContentItem} object.
	 */
	public void deleteContentItem(ContentItem contentItem) {
		contentList.Remove(contentItem);
	}

	/**
	 * <p>Getter for the field <code>type</code>.</p>
	 *
	 * @return a {@link iqq.im.bean.QQMsg.Type} object.
	 */
	public Type getType() {
		return type;
	}

	/**
	 * <p>Setter for the field <code>type</code>.</p>
	 *
	 * @param type a {@link iqq.im.bean.QQMsg.Type} object.
	 */
	public void setType(Type type) {
		this.type = type;
	}

	/**
	 * <p>Getter for the field <code>from</code>.</p>
	 *
	 * @return a {@link iqq.im.bean.QQUser} object.
	 */
	public QQUser getFrom() {
		return from;
	}

	/**
	 * <p>Setter for the field <code>from</code>.</p>
	 *
	 * @param from a {@link iqq.im.bean.QQUser} object.
	 */
	public void setFrom(QQUser from) {
		this.from = from;
	}

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
	 * <p>Getter for the field <code>date</code>.</p>
	 *
	 * @return a {@link java.util.Date} object.
	 */
	public DateTime getDate() {
		return date;
	}

	/**
	 * <p>Setter for the field <code>date</code>.</p>
	 *
	 * @param date a {@link java.util.Date} object.
	 */
	public void setDate(DateTime date) {
		this.date = date;
	}

	/**
	 * <p>Getter for the field <code>contentList</code>.</p>
	 *
	 * @return the contentList
	 */
	public List<ContentItem> getContentList() {
		return contentList;
	}

	/**
	 * <p>Setter for the field <code>contentList</code>.</p>
	 *
	 * @param contentList
	 *            the contentList to set
	 */
	public void setContentList(List<ContentItem> contentList) {
		this.contentList = contentList;
	}

	/**
	 * <p>Getter for the field <code>id</code>.</p>
	 *
	 * @return the id
	 */
	public long getId() {
		return id;
	}

	/**
	 * <p>Setter for the field <code>id</code>.</p>
	 *
	 * @param id
	 *            the id to set
	 */
	public void setId(long id) {
		this.id = id;
	}

	/**
	 * <p>Getter for the field <code>to</code>.</p>
	 *
	 * @return the to
	 */
	public QQUser getTo() {
		return to;
	}

	/**
	 * <p>Setter for the field <code>to</code>.</p>
	 *
	 * @param to the to to set
	 */
	public void setTo(QQUser to) {
		this.to = to;
	}

	/**
	 * <p>Getter for the field <code>discuz</code>.</p>
	 *
	 * @return the discuz
	 */
	public QQDiscuz getDiscuz() {
		return discuz;
	}

	/**
	 * <p>Setter for the field <code>discuz</code>.</p>
	 *
	 * @param discuz the discuz to set
	 */
	public void setDiscuz(QQDiscuz discuz) {
		this.discuz = discuz;
	}

	/**
	 * <p>Getter for the field <code>id2</code>.</p>
	 *
	 * @return the id2
	 */
	public long getId2() {
		return id2;
	}

	/**
	 * <p>Setter for the field <code>id2</code>.</p>
	 *
	 * @param id2 the id2 to set
	 */
	public void setId2(long id2) {
		this.id2 = id2;
	}

	/* (non-Javadoc)
	 * @see java.lang.Object#ToString()
	 */
	/** {@inheritDoc} */
	public String ToString() {
		try {
			return packContentList();
		} catch (QQException e) {
		}
		return null;
	}
	
	/**
	 * <p>getText.</p>
	 *
	 * @return a {@link java.lang.String} object.
	 */
	public String getText(){
		StringBuilder buffer = new StringBuilder();
		foreach(ContentItem item in contentList){
			switch(item.getType()){
                case ContentItem.Type.FACE: 
				buffer.Append( "[表情 " + ((FaceItem) item).getId() + "]"); break;
                case ContentItem.Type.TEXT: 
				buffer.Append( ((TextItem) item).getContent()); break;
                case ContentItem.Type.CFACE: 
				buffer.Append("[自定义表情]"); break;
                case ContentItem.Type.OFFPIC: 
				buffer.Append("[图片]"); break;
                case ContentItem.Type.FONT:
				buffer.Append(""); break;
			default:
				buffer.Append(item.ToString());
                break;
			}
		}
		
		return buffer.ToString();
	}
	

    }
}
