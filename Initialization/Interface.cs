
namespace Initialization
{
    interface IStringable
    {
        string ToString(bool withComment);
    }

    interface IPositionable : IStringable
    {
        int Line { get; }

        int Offset { get; }
    }
}
