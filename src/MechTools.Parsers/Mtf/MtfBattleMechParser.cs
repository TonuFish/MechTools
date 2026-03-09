using MechTools.Parsers.BattleMech;
using System;
using System.IO;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;

namespace MechTools.Parsers.Mtf;

public static class MtfBattleMechParser
{
	private static readonly StreamPipeReaderOptions _pipeReaderOptions = new(leaveOpen: true);

	public static DefaultBattleMech Parse(ReadOnlySpan<char> source)
	{
		return Parse(source, new DefaultBattleMechBuilder());
	}

	public static T Parse<T>(ReadOnlySpan<char> source, IBattleMechBuilder<T> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);
		if (source.IsEmpty)
		{
			ThrowHelper.ThrowEmptySourceException();
		}

		using BattleMechParser parser = new(builder);
		try
		{
			parser.Parse(source);
		}
		catch (Exception ex)
		{
			throw WrapException(parser.LineNumber, ex);
		}

		return builder.Build();
	}

	public static Task<DefaultBattleMech> ParseAsync(Stream stream, CancellationToken ct)
	{
		return ParseAsync(stream, new DefaultBattleMechBuilder(), ct);
	}

	public static async Task<T> ParseAsync<T>(Stream stream, IBattleMechBuilder<T> builder, CancellationToken ct)
	{
		ArgumentNullException.ThrowIfNull(stream);
		ArgumentNullException.ThrowIfNull(builder);
		if (!stream.CanRead)
		{
			ThrowHelper.ThrowEmptyStreamException();
		}

		using BattleMechParser parser = new(builder);
		try
		{
			await parser.ParseAsync(PipeReader.Create(stream, _pipeReaderOptions), ct).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw WrapException(parser.LineNumber, ex);
		}

		return builder.Build();
	}

	private static MtfException WrapException(int lineNumber, Exception ex)
	{
		return new($"An error occurred on line {lineNumber}, see InnerException for more details.", ex);
	}
}
