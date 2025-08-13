using NanoidDotNet;

namespace UniHub.Domain.Extensions;

public static class UUIDExtensions
{
    private static readonly string defaultAuphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static readonly int defaultSize = 12;

    /// <summary>
    /// Gera um novo NanoId e retorna como string.
    /// </summary>
    /// <returns>Um novo NanoId como string.</returns>
    public static string GenerateNanoId()
    {
        return Nanoid.Generate(alphabet: defaultAuphabet, size: defaultSize);
    }

    /// <summary>
    /// Gera um novo GUID e retorna como string.
    /// </summary>
    /// <returns>Um novo GUID como string.</returns>
    public static string GenerateGuid()
    {
        return Guid.NewGuid().ToString("N");
    }
}
