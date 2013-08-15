using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RESTFulNotes
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "wcf" in code, svc and config file together.
    public class wcf : Iwcf
    {
        public void DoWork()
        {
        }

        public Notes FindNotes(string tag)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.OK;
            Notes notes = new Notes();

            Note note = new Note();

            note.ID = 1965;
            note.Category = "Man";
            note.Subject = "Programming with WCF";
            note.NoteText = "Hope he can create a generic REST service";
            notes.Add(note);

            return notes;
        }
    }
}
