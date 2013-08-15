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
        [OperationContract]
        void DoWork();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Notes?tag={tag}")]
        [OperationContract]
        Notes FindNotes(string tag);
    }

    [DataContract]
    public class Note
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string NoteText { get; set; }
    }

    [CollectionDataContract]
    public class Notes : List<Note>
    {
        public Notes() { }
        public Notes(List<Note> Notes) : base(Notes) { }
    }
}
