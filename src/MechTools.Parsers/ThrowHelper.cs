using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace MechTools.Parsers;

internal static class ThrowHelper
{
	[DebuggerStepThrough, DoesNotReturn]
	public static void ThrowUnspecifiedException()
	{
		throw new InvalidOperationException();
	}

	[DebuggerStepThrough, DoesNotReturn]
	[return: MaybeNull]
	public static T ThrowUnspecifiedException<T>()
	{
		throw new InvalidOperationException();
	}

	#region Internal

	[Conditional("DEBUG")]
	[DebuggerStepThrough, DoesNotReturn]
	public static void DebugThrowImpossibleException()
	{
#if DEBUG
		throw new InternalImpossibleException();
#else
		// This method is excluded from non-debug builds, but as ConditionalAttribute isn't "smart" or valid on classes
		// we have to throw a different exception in this empty body.
		throw new InvalidOperationException();
#endif
	}

#if DEBUG
#pragma warning disable CA1064, S3871 // Exceptions should be public - Debug only exception.
	private sealed class InternalImpossibleException : Exception
#pragma warning restore CA1064, S3871 // Exceptions should be public - Debug only exception.
	{
		public InternalImpossibleException()
		{
		}

		public InternalImpossibleException(string message) : base(message)
		{
		}

		public InternalImpossibleException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
#endif // DEBUG
	#endregion Internal
}
