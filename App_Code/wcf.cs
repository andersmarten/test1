using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Newtonsoft.Json.Linq;

namespace RESTFulNotes
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "wcf" in code, svc and config file together.
    public class wcf : Iwcf
    {
        /*
         * {"items":[{"id":2490779948001,"name":"kf20130321_1 f4v direkt","shortDescription":"Laddade upp f4v:n direkt... ljudsynk Ã¥t fanders"},{"id":2601261876001,"name":"kf20130321_1 namnbytt till mov","shortDescription":"kf20130321_1 namnbytt till mov"},{"id":2601261892001,"name":"kf20130321_1 namnbytt till mov ffmpeg copy till mp4","shortDescription":"kf20130321_1 namnbytt till mov ffmpeg copy till mp4"},{"id":2490920344001,"name":"KF20140101","shortDescription":"KF20140101"},{"id":2348367516001,"name":"Sample Video 1","shortDescription":"The short description can be used to give more information about your video and will appear in many standard Brightcove player templates."}],"page_number":0,"page_size":5,"total_count":7}
         *
         */

        public string List()
        {
            // Connect to BC OVP
            // Using Brigthcove Media API Request generator at:
            // http://docs.brightcove.com/en/video-cloud/media/samples/search_videos.html

            // Removed &callback=BCL.onSearchResponse from the URL
            string url = "http://api.brightcove.com/services/library?command=search_videos&page_size=5&video_fields=id%2Cname%2CshortDescription&media_delivery=default&sort_by=DISPLAY_NAME%3AASC&page_number=0&get_item_count=true&token=7kUUuCExWP1WwHpR_kHJ3dBR6NEDdLbSL0-ncHqUw3-eUQLsK76inQ..";
            string json = "";
            string strRes = "";

            // 1) Using WebClient to read data from service

            WebClient webClient = new WebClient();
            json = webClient.DownloadString(url);

            // 2) Parsing the Json with Linq

            JObject jo = JObject.Parse(json);

            var items = from p in jo["items"] select (string)p["name"];

            foreach (var item in items) {
                strRes += item;
            }

            // TODO: Map data to our own (generic) object

            return strRes;
        }

        public System.IO.Stream List2()
        {
            // Connect to Brightcove OVP
            // Using Brigthcove Media API Request generator at:
            // http://docs.brightcove.com/en/video-cloud/media/samples/search_videos.html

            // Get READ TOKEN from app settings
            // http://www.codeproject.com/Articles/602146/Keeping-Sensitive-Config-Settings-Secret-with-Azur
            // http://social.msdn.microsoft.com/Forums/vstudio/en-US/e13194df-6308-4cbe-973c-f6a462f43eae/how-can-wcf-library-dll-access-application-settings
            
            string bc_read_token = ConfigurationManager.AppSettings["BC_READ_TOKEN"];

            // Removed &callback=BCL.onSearchResponse from the URL
            string url = "http://api.brightcove.com/services/library?command=search_videos&page_size=5&video_fields=id%2Cname%2CshortDescription&media_delivery=default&sort_by=DISPLAY_NAME%3AASC&page_number=0&get_item_count=true&token={0}";
            // Insert BC READ TOKEN to the URL
            url = String.Format(url, bc_read_token);
            string json = "";

            // Use WebClient to read data from service. Synchronous call.
            WebClient webClient = new WebClient();
            json = webClient.DownloadString(url);

            // Pass through Json from Brightcove
            // http://stackoverflow.com/questions/992533/wcf-responseformat-for-webget

            // Set MIME type for returned data
            OutgoingWebResponseContext context = WebOperationContext.Current.OutgoingResponse;
            context.ContentType = "application/json"; // "text/plain";

            return new System.IO.MemoryStream(ASCIIEncoding.Default.GetBytes(json));
        }
    }
}
