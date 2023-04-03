using System;
using System.Collections.Generic;
using System.Text;

namespace EasyNotice.Dingtalk
{
    public class ActionCardMessage : MessageBase
    {
        public ActionCardMessage() : base("actionCard")
        {
        }

        /// <summary>
        /// 整体跳转
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="btnOrientation"></param>
        /// <param name="singleTitle"></param>
        /// <param name="singleURL"></param>
        public ActionCardMessage(string title, string text, string singleTitle, string singleURL, string btnOrientation = "0") : base("actionCard")
        {
            this.title = title;
            actionCard = new Actioncard
            {
                title = title,
                text = text,
                btnOrientation = btnOrientation,
                singleTitle = singleTitle,
                singleURL = singleURL,
            };
        }

        /// <summary>
        /// 独立跳转
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <param name="btnOrientation"></param>
        /// <param name="singleTitle"></param>
        /// <param name="singleURL"></param>
        public ActionCardMessage(string title, string text, List<Btn> btns, string btnOrientation = "0") : base("actionCard")
        {
            this.title = title;
            actionCard = new Actioncard
            {
                title = title,
                text = text,
                btnOrientation = btnOrientation,
                btns = btns,
            };
        }

        public Actioncard actionCard { get; set; }
        public class Actioncard
        {
            public string title { get; set; }
            public string text { get; set; }
            public string hideAvatar { get; set; }
            public string btnOrientation { get; set; }
            public string singleTitle { get; set; }
            public string singleURL { get; set; }
            public IEnumerable<Btn> btns { get; set; }
        }

        public class Btn
        {
            public string title { get; set; }
            public string actionURL { get; set; }

        }
    }
}
