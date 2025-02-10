namespace Otanimes.Domain.Interfaces;

public interface IAggregateRoot
{
    bool IsValid();
    void Validate();
    string ErrorsToString();
}