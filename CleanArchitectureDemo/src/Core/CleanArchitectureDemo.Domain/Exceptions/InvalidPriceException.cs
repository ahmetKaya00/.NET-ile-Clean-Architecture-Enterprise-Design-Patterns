namespace CleanArchitectureDemo.Domain.Exeptions;

public class InvalidPriceException : DomainException
{

    public InvalidPriceException(decimal price)       
     : base($"Geçersiz fiyat değeri: {price}. Fiyat sıfır veya negatif olamaz.")
    {
    }
}