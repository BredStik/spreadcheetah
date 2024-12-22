using System.IO.Compression;

namespace SpreadCheetah.MetadataXml;

internal static class XmlWriterExtensions
{
    public static async ValueTask WriteToBufferAsync<TXmlWriter>(
        this TXmlWriter writer,
        ZipArchiveEntry entry,
        SpreadsheetBuffer buffer,
        CancellationToken token)
        where TXmlWriter : IBufferXmlWriter
    {
        var stream = entry.Open();
#if NETSTANDARD2_0
        using (stream)
#else
        await using (stream.ConfigureAwait(false))
#endif
        {
            await writer.WriteToBufferAsync(stream, buffer, token).ConfigureAwait(false);
        }
    }

    public static async ValueTask WriteToBufferAsync<TXmlWriter>(
        this TXmlWriter writer,
        Stream stream,
        SpreadsheetBuffer buffer,
        CancellationToken token)
        where TXmlWriter : IBufferXmlWriter
    {
        bool done;

        do
        {
            done = writer.TryWrite(buffer);
            await buffer.FlushToStreamAsync(stream, token).ConfigureAwait(false);
        } while (!done);
    }
}
