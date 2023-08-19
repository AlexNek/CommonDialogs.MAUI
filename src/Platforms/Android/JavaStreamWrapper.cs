using Android.OS;

using Java.IO;
using Java.Nio;
using Java.Nio.Channels;

namespace CommonDialogs.Maui
{
    internal class JavaStreamWrapper : Stream
    {
        private readonly FileChannel _channel;

        private readonly ParcelFileDescriptor? _fileDescriptor;

        private readonly FileOutputStream? _outputStream;

        private readonly RandomAccessFile? _randomAccessFile;

        public JavaStreamWrapper(Java.IO.File file)
        {
            _randomAccessFile = new RandomAccessFile(file, "rw");
            _channel = _randomAccessFile.Channel!;
        }

        public JavaStreamWrapper(ParcelFileDescriptor fileDescriptor)
        {
            this._fileDescriptor = fileDescriptor;
            _outputStream = new FileOutputStream(fileDescriptor.FileDescriptor);

            _channel = _outputStream.Channel!;
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (Position + count > Length)
            {
                count = (int)(Length - Position);
            }

            if (_randomAccessFile != null)
            {
                return _randomAccessFile.Read(buffer, offset, count);
            }

            var mapBuffer = _channel.Map(FileChannel.MapMode.ReadOnly, Position, count);
            mapBuffer?.Get(buffer, offset, count);
            return count;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    _channel.Position(offset);
                    break;
                case SeekOrigin.Current:
                    _channel.Position(_channel.Position() + offset);
                    break;
                case SeekOrigin.End:
                    _channel.Position(_channel.Size() - offset);
                    break;
            }

            return Position;
        }

        public override void SetLength(long value)
        {
            _channel.Truncate(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (_randomAccessFile != null)
            {
                _randomAccessFile.Write(buffer, offset, count);
                return;
            }

            if (_outputStream != null)
            {
                _outputStream.Write(buffer, offset, count);
                return;
            }

            var javaBuffer = ByteBuffer.Wrap(buffer, offset, count);
            _channel.Write(javaBuffer);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            try
            {
                _fileDescriptor?.Close();
                _fileDescriptor?.Dispose();
                _randomAccessFile?.Dispose();
                _outputStream?.Dispose();
                _channel?.Dispose();
            }
            catch
            {
            }
        }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => true;

        public override long Length
        {
            get
            {
                return _channel.Size();
            }
        }

        public override long Position
        {
            get
            {
                return _channel.Position();
            }
            set
            {
                _channel.Position(value);
            }
        }
    }
}
