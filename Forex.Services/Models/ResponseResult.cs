namespace Forex.Services.Models
{
    public class ResponseResult
    {
        public string Result { get; set; }

        public ResponseResult() { }
        public ResponseResult(string result)
        {
            this.Result = result;
        }
    }
}
