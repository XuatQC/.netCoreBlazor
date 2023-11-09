namespace CandidateSystem
{
    public class HttpResponseObject
    {
        public string devMessage { get; set; }
        public string userMessage { get; set; }
        public string TraceId { get; set; }

        public HttpResponseObject(string devMessage, string userMessage, string traceId)
        {
            this.devMessage = devMessage;
            this.userMessage = userMessage;
            this.TraceId = "";
        }
    }
}
