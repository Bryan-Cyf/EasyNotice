namespace EasyNotice.Core
{
    public class EasyNoticeSendResponse
    {
        public string ErrMsg { get; set; }

        public int ErrCode { get; set; }

        public bool IsSuccess => string.IsNullOrEmpty(ErrMsg);
    }
}
