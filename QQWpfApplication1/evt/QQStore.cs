using QQWpfApplication1.action;
using QQWpfApplication1.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.action
{
    public class QQStore
    {
	private Dictionary<long, QQBuddy> buddyDictionary; // uin => QQBudy, 快速通过uin查找QQ好友
	private Dictionary<long, QQStranger> strangerDictionary; // uin => QQStranger, 快速通过uin查找临时会话用户
	private Dictionary<long, QQCategory> categoryDictionary; // index => QQCategory
	private Dictionary<long, QQDiscuz> discuzDictionary;		// did = > QQDiscuz
	private Dictionary<long, QQGroup> groupDictionary; // code => QQGroup, 快速通过群ID查找群
	private List<ContentItem> pictureItemList; // filename -> PicItem 上传图片列表

	/**
	 * <p>Constructor for QQStore.</p>
	 */
	public QQStore() {
		this.buddyDictionary = new Dictionary<long, QQBuddy>();
		this.strangerDictionary = new Dictionary<long, QQStranger>();
		this.categoryDictionary = new Dictionary<long, QQCategory>();
		this.groupDictionary = new Dictionary<long, QQGroup>();
		this.discuzDictionary = new Dictionary<long, QQDiscuz>();
		this.pictureItemList = new List<ContentItem>();
	}

	/** {@inheritDoc} */
	
	public void init(QQContext context)  {
	}

	/** {@inheritDoc} */
	
	public void destroy()  {
	}

	// add
	/**
	 * <p>addBuddy.</p>
	 *
	 * @param buddy a {@link iqq.im.bean.QQBuddy} object.
	 */
	public void addBuddy(QQBuddy buddy) {
		buddyDictionary.Add(buddy.getUin(), buddy);
	}
	
	/**
	 * <p>addStranger.</p>
	 *
	 * @param stranger a {@link iqq.im.bean.QQStranger} object.
	 */
	public void addStranger(QQStranger stranger) {
		strangerDictionary.Add(stranger.getUin(), stranger);
	}

	/**
	 * <p>addCategory.</p>
	 *
	 * @param category a {@link iqq.im.bean.QQCategory} object.
	 */
	public void addCategory(QQCategory category) {
		categoryDictionary.Add(category.getIndex(), category);
	}

	/**
	 * <p>addGroup.</p>
	 *
	 * @param group a {@link iqq.im.bean.QQGroup} object.
	 */
	public void addGroup(QQGroup group) {
		groupDictionary.Add(group.getCode(), group);
	}

	/**
	 * <p>addPicItem.</p>
	 *
	 * @param pictureItem a {@link iqq.im.bean.content.ContentItem} object.
	 */
	public void addPicItem(ContentItem pictureItem) {
		pictureItemList.Add(pictureItem);
	}
	
	/**
	 * <p>addDiscuz.</p>
	 *
	 * @param discuz a {@link iqq.im.bean.QQDiscuz} object.
	 */
	public void addDiscuz(QQDiscuz discuz){
		discuzDictionary.Add(discuz.getDid(), discuz);
	}

	// delete
	/**
	 * <p>deleteBuddy.</p>
	 *
	 * @param buddy a {@link iqq.im.bean.QQBuddy} object.
	 */
	public void deleteBuddy(QQBuddy buddy) {
		buddyDictionary.Remove(buddy.getUin());
	}
	
	/**
	 * <p>deleteStranger.</p>
	 *
	 * @param stranger a {@link iqq.im.bean.QQStranger} object.
	 */
	public void deleteStranger(QQStranger stranger) {
		strangerDictionary.Remove(stranger.getUin());
	}

	/**
	 * <p>deleteCategory.</p>
	 *
	 * @param category a {@link iqq.im.bean.QQCategory} object.
	 */
	public void deleteCategory(QQCategory category) {
		categoryDictionary.Remove(category.getIndex());
	}

	/**
	 * <p>deleteGroup.</p>
	 *
	 * @param group a {@link iqq.im.bean.QQGroup} object.
	 */
	public void deleteGroup(QQGroup group) {
		groupDictionary.Remove(group.getGin());
	}

	/**
	 * <p>deletePicItem.</p>
	 *
	 * @param picItem a {@link iqq.im.bean.content.ContentItem} object.
	 */
	public void deletePicItem(ContentItem picItem) {
		pictureItemList.Remove(picItem);
	}
	
	/**
	 * <p>deleteDiscuz.</p>
	 *
	 * @param discuz a {@link iqq.im.bean.QQDiscuz} object.
	 */
	public void deleteDiscuz(QQDiscuz discuz){
		discuzDictionary.Remove(discuz.getDid());
	}

	// get
	/**
	 * <p>getBuddyByUin.</p>
	 *
	 * @param uin a long.
	 * @return a {@link iqq.im.bean.QQBuddy} object.
	 */
	public QQBuddy getBuddyByUin(long uin) {
		return buddyDictionary[uin];
	}
	
	/**
	 * <p>getStrangerByUin.</p>
	 *
	 * @param uin a long.
	 * @return a {@link iqq.im.bean.QQStranger} object.
	 */
	public QQStranger getStrangerByUin(long uin) {
		return strangerDictionary[uin];
	}

	/**
	 * <p>getCategoryByIndex.</p>
	 *
	 * @param index a long.
	 * @return a {@link iqq.im.bean.QQCategory} object.
	 */
	public QQCategory getCategoryByIndex(long index) {
		return categoryDictionary[index];
	}

	/**
	 * <p>getGroupByCode.</p>
	 *
	 * @param code a long.
	 * @return a {@link iqq.im.bean.QQGroup} object.
	 */
	public QQGroup getGroupByCode(long code) {
		return groupDictionary[code];
	}
	
	/**
	 * <p>getGroupById.</p>
	 *
	 * @param id a long.
	 * @return a {@link iqq.im.bean.QQGroup} object.
	 */
	public QQGroup getGroupById(long id) {
		foreach(QQGroup g in getGroupList()) {
			if(g.getGid() == id) {
				return g;
			}
		}
		return null;
	}
	
	/**
	 * <p>getGroupByGin.</p>
	 *
	 * @param gin a long.
	 * @return a {@link iqq.im.bean.QQGroup} object.
	 */
	public QQGroup getGroupByGin(long gin) {
		foreach(QQGroup g in  getGroupList()) {
			if(g.getGin() == gin) {
				return g;
			}
		}
		return null;
	}
	
	/**
	 * <p>getDiscuzByDid.</p>
	 *
	 * @param did a long.
	 * @return a {@link iqq.im.bean.QQDiscuz} object.
	 */
	public QQDiscuz getDiscuzByDid(long did){
		return discuzDictionary[did];
	}

	// get list
	/**
	 * <p>getBuddyList.</p>
	 *
	 * @return a {@link java.util.List} object.
	 */
	public List<QQBuddy> getBuddyList() {
		return new List<QQBuddy>(buddyDictionary.Values);
	}
	
	/**
	 * <p>getStrangerList.</p>
	 *
	 * @return a {@link java.util.List} object.
	 */
	public List<QQStranger> getStrangerList() {
		return new List<QQStranger>(strangerDictionary.Values);
	}

	/**
	 * <p>getCategoryList.</p>
	 *
	 * @return a {@link java.util.List} object.
	 */
	public List<QQCategory> getCategoryList() {
		return new List<QQCategory>(categoryDictionary.Values);
	}

	/**
	 * <p>getGroupList.</p>
	 *
	 * @return a {@link java.util.List} object.
	 */
	public List<QQGroup> getGroupList() {
		return new List<QQGroup>(groupDictionary.Values);
	}
	
	/**
	 * <p>getDiscuzList.</p>
	 *
	 * @return a {@link java.util.List} object.
	 */
	public List<QQDiscuz> getDiscuzList() {
		return new List<QQDiscuz>(discuzDictionary.Values);
	}

	/**
	 * <p>getOnlineBuddyList.</p>
	 *
	 * @return a {@link java.util.List} object.
	 */
	public List<QQBuddy> getOnlineBuddyList() {
		List<QQBuddy> onlines = new List<QQBuddy>();
		foreach(QQBuddy buddy in  buddyDictionary.Values){
            //if(QQStatus.isGeneralOnline(buddy.getStatus())){
            //    onlines.add(buddy);
            //}
		}
		return onlines;
	}

	/**
	 * <p>getPicItemList.</p>
	 *
	 * @return a {@link java.util.List} object.
	 */
	public List<ContentItem> getPicItemList() {
		return pictureItemList;
	}

	// get size
	/**
	 * <p>getPicItemListSize.</p>
	 *
	 * @return a int.
	 */
	public int getPicItemListSize() {
        return pictureItemList.Count == 0 ? 0 : pictureItemList.Count;
	}
	
	// 查找临时会话用户 QQGroup/QQDiscuz/QQStranger
	/**
	 * <p>searchUserByUin.</p>
	 *
	 * @param uin a long.
	 * @return a {@link iqq.im.bean.QQUser} object.
	 */
	public QQUser searchUserByUin(long uin) {
		QQUser user = getBuddyByUin(uin);
		if(user == null) {
			user = getStrangerByUin(uin);
		}
		if(user == null) {
			foreach(QQGroup group in getGroupList()) {
				foreach(QQGroupMember u in  group.getMembers()) {
					if(u .getUin() == uin) {
						return u;
					}
				}
			}
			foreach(QQDiscuz discuz in getDiscuzList()) {
				foreach(QQUser u in  discuz.getMembers()) {
					if(u .getUin() == uin) {
						return u;
					}
				}
			}
		}
		return user;
	}

    }
}
