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

	// TODO: Override ToString.
}
