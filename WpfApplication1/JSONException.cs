using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.json
{
    public class JSONException :ApplicationException
    {
    private Exception cause;

    /**
     * Constructs a JSONException with an explanatory message.
     *
     * @param message
     *            Detail about the reason for the exception.
     */
    public JSONException(String message) :base(message){
    }

    /**
     * Constructs a new JSONException with the specified cause.
     * @param cause The cause.
     */
    public JSONException(Exception cause) :base(cause.Message){
        this.cause = cause;
    }

    /**
     * Returns the cause of this exception or null if the cause is nonexistent
     * or unknown.
     *
     * @return the cause of this exception or null if the cause is nonexistent
     *          or unknown.
     */
    public Exception getCause() {
        return this.cause;
    }

    }
}
