using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MechTools.Parsers;

public interface IBattleMechParser<TMech> : IDisposable
{
	public TMech? Parse(ReadOnlySpan<char> chars);
	public TMech? Parse(ReadOnlyMemory<byte> memory);
	public Task<TMech?> ParseAsync(Stream stream, CancellationToken ct = default);
}
