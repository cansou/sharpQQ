using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQWpfApplication1.bean
{
    public class QQDiscuz
    {
	private long did;	//讨论组ID，每次登陆都固定，视为没有变换
	private String name;	//讨论组的名字
	private long owner;		//创建者的UIN
	private List<QQDiscuzMember> members = new List<QQDiscuzMember>();	//讨论组成员
	
	/**
	 * <p>Getter for the field <code>did</code>.</p>
	 *
	 * @return the did
	 */
	public long getDid() {
		return did;
	}
	/**
	 * <p>Setter for the field <code>did</code>.</p>
	 *
	 * @param did the did to set
	 */
	public void setDid(long did) {
		this.did = did;
	}
	/**
	 * <p>Getter for the field <code>name</code>.</p>
	 *
	 * @return the name
	 */
	public String getName() {
		return name;
	}
	/**
	 * <p>Setter for the field <code>name</code>.</p>
	 *
	 * @param name the name to set
	 */
	public void setName(String name) {
		this.name = name;
	}
	/**
	 * <p>Getter for the field <code>owner</code>.</p>
	 *
	 * @return the owner
	 */
	public long getOwner() {
		return owner;
	}
	/**
	 * <p>Setter for the field <code>owner</code>.</p>
	 *
	 * @param owner the owner to set
	 */
	public void setOwner(long owner) {
		this.owner = owner;
	}
	/**
	 * <p>Getter for the field <code>members</code>.</p>
	 *
	 * @return the memebers
	 */
	public List<QQDiscuzMember> getMembers() {
		return members;
	}
	/**
	 * <p>Setter for the field <code>members</code>.</p>
	 *
	 * @param members a {@link java.util.List} object.
	 */
	public void setMembers(List<QQDiscuzMember> members) {
		this.members = members;
	}
	
	/**
	 * <p>getMemberByUin.</p>
	 *
	 * @param uin a long.
	 * @return a {@link iqq.im.bean.QQDiscuzMember} object.
	 */
	public QQDiscuzMember getMemberByUin(long uin){
		foreach(QQDiscuzMember mem in members){
			if(mem.getUin() == uin){
				return mem;
			}
		}
		return null;
	}
	
	/**
	 * <p>clearStatus.</p>
	 */
	public void clearStatus(){
		foreach(QQDiscuzMember mem in  members){
			mem.setStatus(QQStatus.OFFLINE);
		}
	}
	
	/**
	 * <p>addMemeber.</p>
	 *
	 * @param user a {@link iqq.im.bean.QQDiscuzMember} object.
	 */
	public void addMemeber(QQDiscuzMember user){
		this.members.Add(user);
	}

	/* (non-Javadoc)
	 * @see java.lang.Object#hashCode()
	 */
	/** {@inheritDoc} */
	public int hashCode() {
		int prime = 31;
		int result = 1;
		result = prime * result + (int) (did ^ (did >> 32));
		return result;
	}
	/* (non-Javadoc)
	 * @see java.lang.Object#equals(java.lang.Object)
	 */
	/** {@inheritDoc} */
	public Boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (GetType() != obj.GetType())
			return false;
		QQDiscuz other = (QQDiscuz) obj;
		if (did != other.did)
			return false;
		return true;
	}
	/** {@inheritDoc} */
	public String toString() {
		return "QQDiscuz [did=" + did + ", name=" + name + ", owner=" + owner + "]";
	}

    }
}
