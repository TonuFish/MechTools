using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MechTools.Parsers.Mtf;

internal static class MtfThrowHelper
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ThrowIfEmptyOrWhiteSpace(ReadOnlySpan<char> chars)
	{
		if (chars.IsWhiteSpace())
		{
			ThrowMissingDataException();
		}
	}

	[DebuggerStepThrough, DoesNotReturn]
	public static void ThrowMissingDataException()
	{
		// TODO
		throw new MtfException("TODO");
	}

	[DebuggerStepThrough, DoesNotReturn]
	public static void ThrowMissingSectionTagException(ReadOnlySpan<char> line)
	{
		// TODO
		throw new MtfException("TODO");
	}

	[DebuggerStepThrough, DoesNotReturn]
	public static T ThrowUnknownEnumException<T>(int number) where T : struct, Enum
	{
		throw new MtfEnumException(
			$"Unknown {nameof(T)}: {number}",
			typeof(T));
	}

	[DebuggerStepThrough, DoesNotReturn]
	public static T ThrowUnknownEnumException<T>(ReadOnlySpan<char> chars) where T : struct, Enum
	{
		throw new MtfEnumException(
			$"Unknown {nameof(T)}: {chars}",
			typeof(T));
	}

	[DebuggerStepThrough, DoesNotReturn]
	public static void ThrowUnknownSectionTagException(ReadOnlySpan<char> section)
	{
		// TODO
		throw new MtfException($"Unknown section: {section}");
	}
}
