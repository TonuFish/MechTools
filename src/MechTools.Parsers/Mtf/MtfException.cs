using System;

namespace MechTools.Parsers.Mtf;

public class MtfException : Exception
{
	protected MtfException()
	{
	}

	public MtfException(string? message) : base(message)
	{
	}

	public MtfException(string? message, Exception innerException) : base(message, innerException)
	{
	}
}

#pragma warning disable CA1032, RCS1194 // Implement standard exception constructors - Manually thrown inner exception.
public sealed class MtfEnumException : MtfException
#pragma warning restore CA1032, RCS1194 // Implement standard exception constructors - Manually thrown inner exception.
{
	public Type EnumType { get; init; }

	public MtfEnumException(string message, Type enumType) : base(message)
	{
		EnumType = enumType;
	}

	public MtfEnumException(string message, Type enumType, Exception innerException) : base(message, innerException)
	{
		EnumType = enumType;
	}
}
