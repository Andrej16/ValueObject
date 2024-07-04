namespace Domain.Common;

public sealed class Empty : IEquatable<Empty>, IComparable<Empty>, IComparable
{
    public static Empty Value { get; } = new();

    public static Task<Empty> Task { get; } = System.Threading.Tasks.Task.FromResult(Value);

    public bool Equals(Empty? other) => other is not null;

    public override bool Equals(object? obj) => obj is Empty;

    public int CompareTo(Empty? other) => other is not null ? 0 : throw new ArgumentNullException();

    public int CompareTo(object? obj) => obj is Empty ? 0 : throw new ArgumentException();

    public override int GetHashCode() => 0;

    public static bool operator ==(Empty left, Empty right) => left.Equals(right);

    public static bool operator !=(Empty left, Empty right) => !left.Equals(right);

    public override string ToString() => "()";
}

