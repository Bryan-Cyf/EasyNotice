using System;
using System.Collections.Generic;
using System.Text;

namespace EasyNotice.Weixin
{
    /// <summary>
    /// 图片类型消息
    /// </summary>
    public class ImageMessage : MessageBase
    {
        public ImageMessage(string base64,string md5) : base("image")
        {
            image = new Image
            {
                base64 = base64,
                md5 = md5
            };
        }

        public Image image { get; set; }

        public class Image
        {
            /// <summary>
            /// 图片内容的base64编码 图片大小不能大于2M
            /// </summary>
            public string base64 { get; set; }
            /// <summary>
            /// 图片内容（base64编码前）的md5值
            /// </summary>
            public string md5 { get; set; }
        }
    }
}
