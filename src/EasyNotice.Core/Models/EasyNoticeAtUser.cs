
namespace EasyNotice.Core
{
    /// <summary>
    /// @用户信息
    /// </summary>
    public class EasyNoticeAtUser
    {
        /// <summary>
        /// @指定的用户ID列表，各消息平台的UserID，邮件平台为邮箱地址
        /// </summary>
        public string[] UserId { get; set; }
        /// <summary>
        /// @指定的手机号列表，请注意：飞书平台、邮箱发送不支持手机号
        /// </summary>
        public string[] Mobile { get; set; }

        /// <summary>
        /// 是否@所有人
        /// </summary>
        public bool IsAtAll { get; set; }
    }
}
