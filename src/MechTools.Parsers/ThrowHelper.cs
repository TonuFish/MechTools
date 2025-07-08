using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MechTools.Parsers;

internal static class ThrowHelper
{
	[DoesNotReturn, DebuggerHidden, StackTraceHidden]
	public static void ExceptionToSpecifyLater()
	{
		throw new InvalidOperationException();
	}

	[DoesNotReturn, DebuggerHidden, StackTraceHidden]
	public static T ExceptionToSpecifyLater<T>()
	{
		throw new InvalidOperationException();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[DebuggerHidden, StackTraceHidden]
	public static void ThrowIfEmptyOrWhiteSpace(ReadOnlySpan<char> chars)
	{
		if (chars.IsEmpty || chars.IsWhiteSpace())
		{
			ExceptionToSpecifyLater();
		}
	}

	[Conditional("DEBUG")]
	[DoesNotReturn, DebuggerHidden, StackTraceHidden]
	public static void ThrowImpossibleException()
	{
		throw new InvalidOperationException();
	}
}
