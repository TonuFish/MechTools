using MechTools.Parsers.BattleMech;
using System;
using System.IO;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;

namespace MechTools.Parsers.Mtf;

/// <summary>
/// Provides static methods to aid in the creation of BattleMech objects.
/// </summary>
public static class MtfBattleMechParser
{
	private static readonly StreamPipeReaderOptions _pipeReaderOptions = new(leaveOpen: true);

	/// <summary>
	/// Parses a BattleMech from the provided source.
	/// </summary>
	/// <remarks>
	/// When <paramref name="strict"/> is <see langword="false"/> invalid lines are ignored, resulting in discrepancies
	/// between the <paramref name="source"/> data and returned BattleMech.
	/// </remarks>
	/// <param name="source">The source to read.</param>
	/// <param name="strict">The invalid line handling flag.</param>
	public static DefaultBattleMech Parse(ReadOnlySpan<char> source, bool strict = true)
	{
		return Parse(source, new DefaultBattleMechBuilder(), strict);
	}

	/// <summary>
	/// Parses a BattleMech from the provided source.
	/// </summary>
	/// <remarks>
	/// When <paramref name="strict"/> is <see langword="false"/> invalid lines are ignored, resulting in discrepancies
	/// between the <paramref name="source"/> data and returned BattleMech.
	/// </remarks>
	/// <param name="source">The source to read.</param>
	/// <param name="builder">The BattleMech builder.</param>
	/// <param name="strict">The invalid line handling flag.</param>
	public static T Parse<T>(ReadOnlySpan<char> source, IBattleMechBuilder<T> builder, bool strict = true)
	{
		ArgumentNullException.ThrowIfNull(builder);
		if (source.IsEmpty)
		{
			ThrowHelper.ThrowEmptySourceException();
		}

		using BattleMechParser parser = new(builder, strict);
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

	/// <summary>
	/// Parses a BattleMech from the provided source.
	/// </summary>
	/// <remarks>
	/// When <paramref name="strict"/> is <see langword="false"/> invalid lines are ignored, resulting in discrepancies
	/// between the <paramref name="stream"/> data and returned BattleMech.
	/// </remarks>
	/// <param name="stream">The stream to read.</param>
	/// <param name="ct">The token to monitor for cancellation requests.</param>
	/// <param name="strict">The invalid line handling flag.</param>
	public static Task<DefaultBattleMech> ParseAsync(Stream stream, CancellationToken ct, bool strict = true)
	{
		return ParseAsync(stream, new DefaultBattleMechBuilder(), ct, strict);
	}

	/// <summary>
	/// Parses a BattleMech from the provided source.
	/// </summary>
	/// <remarks>
	/// When <paramref name="strict"/> is <see langword="false"/> invalid lines are ignored, resulting in discrepancies
	/// between the <paramref name="stream"/> data and returned BattleMech.
	/// </remarks>
	/// <param name="stream">The stream to read.</param>
	/// <param name="builder">The BattleMech builder.</param>
	/// <param name="ct">The token to monitor for cancellation requests.</param>
	/// <param name="strict">The invalid line handling flag.</param>
	public static async Task<T> ParseAsync<T>(
		Stream stream,
		IBattleMechBuilder<T> builder,
		CancellationToken ct,
		bool strict = true)
	{
		ArgumentNullException.ThrowIfNull(stream);
		ArgumentNullException.ThrowIfNull(builder);
		if (!stream.CanRead)
		{
			ThrowHelper.ThrowEmptyStreamException();
		}

		using BattleMechParser parser = new(builder, strict);
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
