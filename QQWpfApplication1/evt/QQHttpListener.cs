using QQWpfApplication1.action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.action
{
    public  interface QQHttpListener
    {

        /**
         * <p>onHttpFinish.</p>
         *
         * @param response a {@link iqq.im.http.QQHttpResponse} object.
         */
         void onHttpFinish(QQHttpResponse response);
        /**
         * <p>onHttpError.</p>
         *
         * @param t a {@link java.lang.Throwable} object.
         */
         void onHttpError(Exception t);
        /**
         * <p>onHttpHeader.</p>
         *
         * @param response a {@link iqq.im.http.QQHttpResponse} object.
         */

    }
}
