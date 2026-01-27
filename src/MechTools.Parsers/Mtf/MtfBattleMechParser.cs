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
			ThrowHelper.ThrowUnspecifiedException();
		}

		DefaultBattleMechBuilder builder = new();
		using BattleMechParser parser = new(builder);
		try
		{
			parser.Parse(source);
		}
		catch (Exception ex)
		{
			throw WrapException(ex);
		}

		return builder.Build();
	}

	public static T Parse<T>(ReadOnlySpan<char> source, IBattleMechBuilder<T> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);
		if (source.IsEmpty)
		{
			ThrowHelper.ThrowUnspecifiedException();
		}

		using BattleMechParser parser = new(builder);
		try
		{
			parser.Parse(source);
		}
		catch (Exception ex)
		{
			throw WrapException(ex);
		}

		return builder.Build();
	}

	public static async Task<DefaultBattleMech> ParseAsync(Stream stream, CancellationToken ct)
	{
		ArgumentNullException.ThrowIfNull(stream);
		if (!stream.CanRead)
		{
			ThrowHelper.ThrowUnspecifiedException();
		}

		DefaultBattleMechBuilder builder = new();
		using BattleMechParser parser = new(builder);
		try
		{
			await parser.ParseAsync(PipeReader.Create(stream, _pipeReaderOptions), ct).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw WrapException(ex);
		}

		return builder.Build();
	}

	public static async Task<T> ParseAsync<T>(Stream stream, IBattleMechBuilder<T> builder, CancellationToken ct)
	{
		ArgumentNullException.ThrowIfNull(stream);
		ArgumentNullException.ThrowIfNull(builder);
		if (!stream.CanRead)
		{
			ThrowHelper.ThrowUnspecifiedException();
		}

		using BattleMechParser parser = new(builder);
		try
		{
			await parser.ParseAsync(PipeReader.Create(stream, _pipeReaderOptions), ct).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			throw WrapException(ex);
		}

		return builder.Build();
	}

	private static Exception WrapException(Exception ex)
	{
		// TODO: This.
		return ex;
	}
}
