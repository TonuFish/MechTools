using MechTools.Core;
using System;

namespace MechTools.Parsers;

public interface IBattleMechBuilder<TMech> : IBattleMechBuilder
{
	public TMech Build();
}
