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
		if (source.IsEmpty)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		DefaultBattleMechBuilder builder = new();
		using BattleMechParser parser = new(builder);
		parser.Parse(source);
		return builder.Build();
	}

	public static T Parse<T>(ReadOnlySpan<char> source, IBattleMechBuilder<T> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);
		if (source.IsEmpty)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		using BattleMechParser parser = new(builder);
		parser.Parse(source);
		return builder.Build();
	}

	public static async Task<DefaultBattleMech> ParseAsync(Stream stream, CancellationToken ct)
	{
		// TODO: Docomment about consuming the stream

		ArgumentNullException.ThrowIfNull(stream);
		if (!stream.CanRead)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		DefaultBattleMechBuilder builder = new();
		using BattleMechParser parser = new(builder);
		await parser.ParseAsync(PipeReader.Create(stream, _pipeReaderOptions), ct).ConfigureAwait(false);
		return builder.Build();
	}

	public static async Task<T> ParseAsync<T>(Stream stream, IBattleMechBuilder<T> builder, CancellationToken ct)
	{
		ArgumentNullException.ThrowIfNull(stream);
		ArgumentNullException.ThrowIfNull(builder);
		if (!stream.CanRead)
		{
			ThrowHelper.ExceptionToSpecifyLater();
		}

		using BattleMechParser parser = new(builder);
		await parser.ParseAsync(PipeReader.Create(stream, _pipeReaderOptions), ct).ConfigureAwait(false);
		return builder.Build();
	}
}
