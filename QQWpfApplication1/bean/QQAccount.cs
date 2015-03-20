using QQWpfApplication1.evt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.bean
{
    class QQAccount:QQUser
    {
        
	
	private String password;
	private String username;
	private String wbUsername;
	private String wbPassword;

	/**
	 * <p>Getter for the field <code>password</code>.</p>
	 *
	 * @return a {@link java.lang.String} object.
	 */
	public String getPassword() {
		return password;
	}

	/**
	 * <p>Setter for the field <code>password</code>.</p>
	 *
	 * @param password a {@link java.lang.String} object.
	 */
	public void setPassword(String password) {
		this.password = password;
	}

	/**
	 * <p>Getter for the field <code>username</code>.</p>
	 *
	 * @return a {@link java.lang.String} object.
	 */
	public String getUsername() {
		return username;
	}

	/**
	 * <p>Setter for the field <code>username</code>.</p>
	 *
	 * @param username a {@link java.lang.String} object.
	 */
	public void setUsername(String username) {
		this.username = username;
	}

	public String getWbUsername() {
		return wbUsername;
	}

	public void setWbUsername(String wbUsername) {
		this.wbUsername = wbUsername;
	}

	public String getWbPassword() {
		return wbPassword;
	}

	public void setWbPassword(String wbPassword) {
		this.wbPassword = wbPassword;
	}

    }
}
