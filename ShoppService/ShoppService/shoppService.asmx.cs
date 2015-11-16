using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace ShoppService
{
    /// <summary>
    /// Summary description for shoppService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class shoppService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
