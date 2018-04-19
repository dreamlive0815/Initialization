
namespace Initialization
{
    interface IPositionable : IStringable
    {
        int Line { get; }

        int Offset { get; }
    }
}
