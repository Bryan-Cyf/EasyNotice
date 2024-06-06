using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// options.
    /// </summary>
    public class EasyNoticeOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:EasyNotice.Core.EasyNoticeOptions"/> class.
        /// </summary>
        public EasyNoticeOptions()
        {
            Extensions = new List<IEasyNoticeOptionsExtension>();
        }

        /// <summary>
        /// Gets the extensions.
        /// </summary>
        /// <value>The extensions.</value>
        public IList<IEasyNoticeOptionsExtension> Extensions { get; }

        /// <summary>
        /// Registers the extension.
        /// </summary>
        /// <param name="extension">Extension.</param>
        public void RegisterExtension(IEasyNoticeOptionsExtension extension)
        {
            Extensions.Add(extension);
        }
    }
}
