
namespace Initialization
{
    interface IPositionable
    {
        int Line { get; }

        int Offset { get; }

        string ToString(bool withComment);
    }
}
