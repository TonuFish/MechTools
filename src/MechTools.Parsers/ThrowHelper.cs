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

	[DoesNotReturn, DebuggerHidden, StackTraceHidden]
	public static void ImExcited()
	{
		throw new ImExcitedException();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[DebuggerHidden, StackTraceHidden]
	public static void ThrowIfEmptyOrWhiteSpace(ReadOnlySpan<char> chars)
	{
		if (chars.IsWhiteSpace())
		{
			ExceptionToSpecifyLater();
		}
	}
}

/// <summary>
/// A "This exception is annoying me" exception.
/// </summary>
public sealed class ImExcitedException : Exception
{
	public ImExcitedException() : base()
	{
	}

	public ImExcitedException(string? message) : base(message)
	{
	}

	public ImExcitedException(string? message, Exception? innerException) : base(message, innerException)
	{
	}
}
