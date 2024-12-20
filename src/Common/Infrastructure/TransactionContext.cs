using System.Data;

public static class TransactionContext
{
    private static readonly AsyncLocal<IDbTransaction?> _transaction = new();

    /// <summary>
    /// Gets the current database transaction.
    /// </summary>
    public static IDbTransaction? CurrentTransaction => _transaction.Value;

    /// <summary>
    /// Sets the current database transaction.
    /// </summary>
    public static void SetTransaction(IDbTransaction transaction)
    {
        _transaction.Value = transaction;
    }

    /// <summary>
    /// Clears the current database transaction.
    /// </summary>
    public static void ClearTransaction()
    {
        _transaction.Value = null;
    }

    public static IDbTransaction GetCurrentTransaction()
    {
        var transaction = TransactionContext.CurrentTransaction;
        if (transaction == null)
        {
            throw new InvalidOperationException("No transaction is available.");
        }
        return transaction;
    }
}