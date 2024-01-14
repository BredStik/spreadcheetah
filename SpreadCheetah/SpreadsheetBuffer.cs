using SpreadCheetah.CellWriters;
using SpreadCheetah.Helpers;
using System.Buffers.Text;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace SpreadCheetah;

internal readonly record struct RawString(string Value);

internal sealed class SpreadsheetBuffer(byte[] buffer)
{
    private readonly byte[] _buffer = buffer;
    private int _index;

    public Span<byte> GetSpan() => _buffer.AsSpan(_index);
    public int FreeCapacity => _buffer.Length - _index;
    public void Advance(int bytes) => _index += bytes;

    public bool WriteLongString(ReadOnlySpan<char> value, ref int valueIndex)
    {
        var bytesWritten = 0;
        var result = SpanHelper.TryWriteLongString(value, ref valueIndex, GetSpan(), ref bytesWritten);
        _index += bytesWritten;
        return result;
    }

#if NETSTANDARD2_0
    public bool WriteLongString(string? value, ref int valueIndex) => WriteLongString(value.AsSpan(), ref valueIndex);
#endif

    public ValueTask FlushToStreamAsync(Stream stream, CancellationToken token)
    {
        var index = _index;
        _index = 0;
#if NETSTANDARD2_0
        return new ValueTask(stream.WriteAsync(_buffer, 0, index, token));
#else
        return stream.WriteAsync(_buffer.AsMemory(0, index), token);
#endif
    }

    public bool TryWrite([InterpolatedStringHandlerArgument("")] ref TryWriteInterpolatedStringHandler handler)
    {
        if (handler._success)
        {
            Advance(handler._pos);
            return true;
        }

        return false;
    }

    public bool TryWriteWithXmlEncode([InterpolatedStringHandlerArgument("")] ref TryWriteInterpolatedStringHandler2 handler)
    {
        if (handler._success)
        {
            Advance(handler._pos);
            return true;
        }

        return false;
    }

    [InterpolatedStringHandler]
    public ref struct TryWriteInterpolatedStringHandler
    {
        private readonly SpreadsheetBuffer _buffer;
        internal int _pos;
        internal bool _success;

        public TryWriteInterpolatedStringHandler(int literalLength, int formattedCount, SpreadsheetBuffer buffer)
        {
            _ = literalLength;
            _ = formattedCount;
            _buffer = buffer;
            _success = true;
        }

        private readonly Span<byte> GetSpan() => _buffer._buffer.AsSpan(_buffer._index + _pos);

        [ExcludeFromCodeCoverage]
        public bool AppendLiteral(string value)
        {
            Debug.Fail("Use ReadOnlySpan<byte> instead of string literals");

            if (value is not null && Utf8Helper.TryGetBytes(value, GetSpan(), out var bytesWritten))
            {
                _pos += bytesWritten;
                return true;
            }

            return Fail();
        }

        public bool AppendFormatted(int value)
        {
            if (Utf8Formatter.TryFormat(value, GetSpan(), out var bytesWritten))
            {
                _pos += bytesWritten;
                return true;
            }

            return Fail();
        }

        public bool AppendFormatted(float value)
        {
            if (Utf8Formatter.TryFormat(value, GetSpan(), out var bytesWritten))
            {
                _pos += bytesWritten;
                return true;
            }

            return Fail();
        }

        public bool AppendFormatted(double value)
        {
            if (Utf8Formatter.TryFormat(value, GetSpan(), out var bytesWritten))
            {
                _pos += bytesWritten;
                return true;
            }

            return Fail();
        }

        [ExcludeFromCodeCoverage]
        public bool AppendFormatted<T>(T value)
        {
            Debug.Fail("Create non-generic overloads to avoid allocations when running on .NET Framework");

            string? s = value is IFormattable f
                ? f.ToString(null, CultureInfo.InvariantCulture)
                : value?.ToString();

            return AppendFormatted(s);
        }

        public bool AppendFormatted(string? value)
        {
            if (Utf8Helper.TryGetBytes(value, GetSpan(), out int bytesWritten))
            {
                _pos += bytesWritten;
                return true;
            }

            return Fail();
        }

        public bool AppendFormatted(RawString value)
        {
            if (Utf8Helper.TryXmlEncodeToUtf8(value.Value.AsSpan(), GetSpan(), out var bytesWritten))
            {
                _pos += bytesWritten;
                return true;
            }

            return Fail();
        }

        public bool AppendFormatted(scoped ReadOnlySpan<byte> utf8Value)
        {
            if (utf8Value.TryCopyTo(GetSpan()))
            {
                _pos += utf8Value.Length;
                return true;
            }

            return Fail();
        }

        public bool AppendFormatted(CellWriterState state)
        {
            var bytes = GetSpan();
            var bytesWritten = 0;

            if (!"<c r=\""u8.TryCopyTo(bytes, ref bytesWritten)) return Fail();
            if (!SpanHelper.TryWriteCellReference(state.Column + 1, state.NextRowIndex - 1, bytes, ref bytesWritten)) return Fail();

            _pos += bytesWritten;
            return true;
        }

        private bool Fail()
        {
            _success = false;
            return false;
        }
    }

    [InterpolatedStringHandler]
    public ref struct TryWriteInterpolatedStringHandler2
    {
        private readonly SpreadsheetBuffer _buffer;
        internal int _pos;
        internal bool _success;

        public TryWriteInterpolatedStringHandler2(int literalLength, int formattedCount, SpreadsheetBuffer buffer)
        {
            _ = literalLength;
            _ = formattedCount;
            _buffer = buffer;
            _success = true;
        }

        private readonly Span<byte> GetSpan() => _buffer._buffer.AsSpan(_buffer._index + _pos);

        [ExcludeFromCodeCoverage]
        public bool AppendLiteral(string value)
        {
            Debug.Fail("Use ReadOnlySpan<byte> instead of string literals");

            if (value is not null && Utf8Helper.TryGetBytes(value, GetSpan(), out var bytesWritten))
            {
                _pos += bytesWritten;
                return true;
            }

            return Fail();
        }

        public bool AppendFormatted(int value)
        {
            if (Utf8Formatter.TryFormat(value, GetSpan(), out var bytesWritten))
            {
                _pos += bytesWritten;
                return true;
            }

            return Fail();
        }

        public bool AppendFormatted(float value)
        {
            if (Utf8Formatter.TryFormat(value, GetSpan(), out var bytesWritten))
            {
                _pos += bytesWritten;
                return true;
            }

            return Fail();
        }

        public bool AppendFormatted(double value)
        {
            if (Utf8Formatter.TryFormat(value, GetSpan(), out var bytesWritten))
            {
                _pos += bytesWritten;
                return true;
            }

            return Fail();
        }

        [ExcludeFromCodeCoverage]
        public bool AppendFormatted<T>(T value)
        {
            Debug.Fail("Create non-generic overloads to avoid allocations when running on .NET Framework");

            string? s = value is IFormattable f
                ? f.ToString(null, CultureInfo.InvariantCulture)
                : value?.ToString();

            return AppendFormatted(s);
        }

        public bool AppendFormatted(string? value)
        {
            if (Utf8Helper.TryXmlEncodeToUtf8(value.AsSpan(), GetSpan(), out var bytesWritten))
            {
                _pos += bytesWritten;
                return true;
            }

            return Fail();
        }

        public bool AppendFormatted(scoped ReadOnlySpan<byte> utf8Value)
        {
            if (utf8Value.TryCopyTo(GetSpan()))
            {
                _pos += utf8Value.Length;
                return true;
            }

            return Fail();
        }

        public bool AppendFormatted(CellWriterState state)
        {
            var bytes = GetSpan();
            var bytesWritten = 0;

            if (!"<c r=\""u8.TryCopyTo(bytes, ref bytesWritten)) return Fail();
            if (!SpanHelper.TryWriteCellReference(state.Column + 1, state.NextRowIndex - 1, bytes, ref bytesWritten)) return Fail();

            _pos += bytesWritten;
            return true;
        }

        private bool Fail()
        {
            _success = false;
            return false;
        }
    }
}
