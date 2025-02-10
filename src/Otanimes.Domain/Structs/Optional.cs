namespace Otanimes.Domain.Structs;

public readonly struct Optional<T>
{
    public bool IsEmpty => _value == null;

    private bool HasValue => _value != null;

    private readonly T _value;

    public T Value => IsEmpty ? default : _value;

    private Optional(T value)
    {
        _value = value;
    }

    public static explicit operator T(Optional<T> optional)
        => optional.Value;

    public static implicit operator Optional<T>(T value)
        => new(value);

    public override bool Equals(object obj)
        => obj is Optional<T> optional && Equals(optional);

    private bool Equals(Optional<T> other)
    {
        if (HasValue && other.HasValue)
            return Equals(_value, other._value);
        
        return HasValue == other.HasValue;
    }

    public static bool operator ==(Optional<T> left, Optional<T> right)
        => left.Equals(right);

    public static bool operator !=(Optional<T> left, Optional<T> right)
        => (left == right) is false;
}