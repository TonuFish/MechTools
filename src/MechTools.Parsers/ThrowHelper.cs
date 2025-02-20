using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MechTools.Parsers;

[SkipLocalsInit]
internal static class ThrowHelper
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	[DoesNotReturn, DebuggerHidden]
	public static void ExceptionToSpecifyLater()
	{
		throw new InvalidOperationException();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[DoesNotReturn, DebuggerHidden]
	public static T ExceptionToSpecifyLater<T>()
	{
		throw new InvalidOperationException();
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	[DoesNotReturn, DebuggerHidden]
	public static void ImExcited()
	{
		throw new ImExcitedException();
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
