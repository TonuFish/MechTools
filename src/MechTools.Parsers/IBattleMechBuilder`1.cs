namespace MechTools.Parsers;

public interface IBattleMechBuilder<out TMech> : IBattleMechBuilder
{
	public TMech Build();
}
