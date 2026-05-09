namespace CleanArchitectureDemo.Domain.Entities;

public class Money : IEquatable<Money>
{
    public decimal Amount { get; }
    public string Currency { get;}

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Create(decimal amount, string currency)
    {
        if(string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Para birimi boş olamaz.", nameof(currency));
        
        if(currency.Length != 3)
            throw new ArgumentException("Para birimi ISO 4217 formatında olmalıdır (3 karakter).", nameof(currency));
        return new Money(amount, currency.ToUpperInvariant());
    }

    public bool Equals(Money? other)
    {
        if (other is null) return false;
        return Amount == other.Amount && Currency == other.Currency;
    }

    public override bool Equals(object? obj) => Equals(obj as Money);
    public override int GetHashCode() => HashCode.Combine(Amount, Currency);
    public static bool operator ==(Money? left, Money? right) => Equals(left, right);
    public static bool operator !=(Money? left, Money? right) => !Equals(left, right);

    public override string ToString() => $"{Amount} {Currency}";
}