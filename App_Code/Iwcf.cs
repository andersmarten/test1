using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RESTFulNotes
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Iwcf" in both code and config file together.
    [ServiceContract]
    public interface Iwcf
    {
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "List")]
        [OperationContract]
        string List();

        [WebInvoke(Method = "GET", BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "List2")]
        [OperationContract]
        System.IO.Stream List2();
    }

    
}
