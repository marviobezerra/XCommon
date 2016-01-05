using System;
using System.IO;
using System.Text;

namespace XCommon.Web.MVC.Filters
{
    public class WhiteSpaceFilter : Stream
    {
        private Stream _shrink;
        private Func<string, string> _filter;

        public WhiteSpaceFilter(Stream shrink, Func<string, string> filter)
        {
            _shrink = shrink;
            _filter = filter;
        }

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override long Length
        {
            get
            {
                return 0;
            }
        }

        public override long Position { get; set; }

        public override void Flush()
        {
            _shrink.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _shrink.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _shrink.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _shrink.SetLength(value);
        }

        public override void Close()
        {
            _shrink.Close();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            byte[] data = new byte[count];
            Buffer.BlockCopy(buffer, offset, data, 0, count);
            string s = Encoding.UTF8.GetString(buffer);

            s = _filter(s);

            byte[] outdata = Encoding.UTF8.GetBytes(s);
            _shrink.Write(outdata, 0, outdata.GetLength(0));
        }
    }
}
