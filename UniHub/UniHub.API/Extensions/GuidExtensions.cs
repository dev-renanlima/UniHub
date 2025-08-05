using MongoDB.Bson;

namespace UniHub.API.Extensions;

public static class GuidExtensions
{
    /// <summary>
    /// Gera um novo ObjectId e retorna como string.
    /// </summary>
    /// <returns>Um novo ObjectId como string.</returns>
    public static string GenerateObjectId()
    {
        return ObjectId.GenerateNewId().ToString();
    }
}
