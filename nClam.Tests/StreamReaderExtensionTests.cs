using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace nClam.Tests
{
    public class StreamReaderExtensionTests
    {
        [Fact]
        public void StreamReaderReadToEndAsyncOnCancelledTokenShouldThrowTaskCancelled()
        {
            using MemoryStream ms = new MemoryStream();
            WriteHelloWorldToStream(ms);
            StreamReader sr = new StreamReader(ms);
            using var cts = new System.Threading.CancellationTokenSource();
            cts.Cancel();
            Assert.ThrowsAsync<TaskCanceledException>(() => sr.ReadToEndAsync_NetStandard20(cts.Token));
        }

        [Fact]
        public void StreamReaderReadToEndAsyncOnNonCancelledTokenShouldReturnHelloWorld()
        {
            using MemoryStream ms = new MemoryStream();
            WriteHelloWorldToStream(ms);
            StreamReader sr = new StreamReader(ms);
            using var cts = new System.Threading.CancellationTokenSource();
            Assert.Equal("HelloWorld", sr.ReadToEndAsync_NetStandard20(cts.Token).Result);
        }
        private void WriteHelloWorldToStream(Stream streamToWrite)
        {
            StreamWriter sw = new StreamWriter(streamToWrite);
            sw.Write("HelloWorld");
            sw.Flush();
            streamToWrite.Position = 0;
        }
    }
}
