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
			ThrowMissingValueException();
		}
	}

	[DebuggerStepThrough, DoesNotReturn]
	public static void ThrowInvalidValueException(ReadOnlySpan<char> chars)
	{
		throw new MtfException($"Value could not be parsed from '{chars}'.");
	}

	[DebuggerStepThrough, DoesNotReturn]
	[return: MaybeNull]
	public static T ThrowInvalidValueException<T>(ReadOnlySpan<char> chars)
	{
		throw new MtfException($"Value could not be parsed from '{chars}'.");
	}

	[DebuggerStepThrough, DoesNotReturn]
	public static void ThrowMissingSectionTagException(ReadOnlySpan<char> line)
	{
		throw new MtfException($"Section tag could not be parsed from line '{line}'.");
	}

	[DebuggerStepThrough, DoesNotReturn]
	public static void ThrowMissingValueException()
	{
		throw new MtfException("Value may not be empty or whitespace.");
	}

	[DebuggerStepThrough, DoesNotReturn]
	public static T ThrowUnknownEnumException<T>(int number) where T : struct, Enum
	{
		throw new MtfEnumException(
			$"{nameof(T)} could not be converted from '{number}'.",
			typeof(T));
	}

	[DebuggerStepThrough, DoesNotReturn]
	public static T ThrowUnknownEnumException<T>(ReadOnlySpan<char> chars) where T : struct, Enum
	{
		throw new MtfEnumException(
			$"{nameof(T)} could not be parsed from '{chars}'.",
			typeof(T));
	}

	[DebuggerStepThrough, DoesNotReturn]
	public static void ThrowUnknownSectionTagException(ReadOnlySpan<char> section)
	{
		throw new MtfException($"Section tag '{section}' is unknown.");
	}
}
