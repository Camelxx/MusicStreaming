using Newtonsoft.Json;
using StreamingWebApi.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;


namespace StreamingWebApi.Controllers
{
    
    public class VideoController : ApiController
    {

        Dictionary<string, Video> dicVideos = new Dictionary<string, Video>();
        string videoDirectory = @"C:\Users\Admin\Videos\Any\Video";


        // GET api/hello
        public string[] Get()
        {
            Console.WriteLine("Get Runnig");
            return new string[] { "Hello", "World" };
        }

        public IHttpActionResult GetAllVideosFilename ()
        {
            List<Video> returnVideos = new List<Video>();
            Console.WriteLine("Get Videos Filenames Running");

            var videos = Directory.GetFiles(videoDirectory);

            foreach (var vd in videos)
            {
                Video video = new Video();
                video.VideoFullName = Path.GetFileName(vd).Split('.')[0];
                video.VideoFullPath = vd;

                if (!dicVideos.ContainsKey(video.VideoFullName))
                {
                    dicVideos.Add(video.VideoFullName, video);
                }
                returnVideos.Add(video);
            }
            return Json(JsonConvert.SerializeObject(returnVideos));            
        }

        [HttpGet]
        [Route("playvideo")]
        public HttpResponseMessage playVideo()
        {
            try
            {
                loadVids();
                var filePath = dicVideos.Values.Last().VideoFullPath;




                if (!File.Exists(filePath))
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                var response = Request.CreateResponse();
                response.Headers.AcceptRanges.Add("bytes");

                var streamer = new FileStreamer();
                streamer.FileInfo = new FileInfo(filePath);
                response.Content = new PushStreamContent(streamer.WriteToStream, new MediaTypeHeaderValue("video/mp4"));

                RangeHeaderValue rangeHeader = Request.Headers.Range;
                if (rangeHeader != null)
                {
                    long totalLength = streamer.FileInfo.Length;
                    var range = rangeHeader.Ranges.First();
                    streamer.Start = range.From ?? 0;
                    streamer.End = range.To ?? totalLength - 1;

                    response.Content.Headers.ContentLength = streamer.End - streamer.Start + 1;
                    response.Content.Headers.ContentRange = new ContentRangeHeaderValue(streamer.Start, streamer.End,
                        totalLength);
                    response.StatusCode = HttpStatusCode.PartialContent;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.OK;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }



        private void loadVids()
        {

            var videos = Directory.GetFiles(videoDirectory);

            foreach (var vd in videos)
            {
                Video video = new Video();
                video.VideoFullName = Path.GetFileName(vd);
                video.VideoFullPath = vd;

                if (!dicVideos.ContainsKey(video.VideoFullName))
                {
                    dicVideos.Add(video.VideoFullName, video);
                }
            }
        }


        //public IEnumerable<Product> GetAllProducts()
        //{
        //    return products;
        //}
    }
}
