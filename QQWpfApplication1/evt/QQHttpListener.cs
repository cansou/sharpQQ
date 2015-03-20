using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWpfApplication1.evt
{
    interface QQHttpListener
    {

        /**
         * <p>onHttpFinish.</p>
         *
         * @param response a {@link iqq.im.http.QQHttpResponse} object.
         */
        public void onHttpFinish(QQHttpResponse response);
        /**
         * <p>onHttpError.</p>
         *
         * @param t a {@link java.lang.Throwable} object.
         */
        public void onHttpError(Exception t);
        /**
         * <p>onHttpHeader.</p>
         *
         * @param response a {@link iqq.im.http.QQHttpResponse} object.
         */

    }
}
