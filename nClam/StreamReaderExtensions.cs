using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nClam
{
    public static class StreamReaderExtensions
    {
        /// <summary>
        /// This is a simple implementation of ReadToEndAsync(CancellationToken) that is compatible with .NET Standard 2.0.
        /// </summary>
        /// <param name="reader">The reader to extend</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        public static async Task<string> ReadToEndAsync_NetStandard20(this System.IO.StreamReader reader, CancellationToken cancellationToken)
        {
            /*
            NOTE: This implementation is specific to .NET Standard 2.1 or greater. It has been commented out
            to simplify testing. If you are using .NET Standard 2.1 or greater, the application can simply use this:
            return await reader.ReadToEndAsync(cancellationToken).ConfigureAwait(false);
            */
            
            while (!reader.EndOfStream)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (reader.Peek() > -1)
                {
                    // read the lines that are already in the buffer
                    return await reader.ReadToEndAsync().ConfigureAwait(false);
                }
                await Task.Delay(100, cancellationToken).ConfigureAwait(false);
            }
            // nothing was read
            return "";
        }

    }
}
