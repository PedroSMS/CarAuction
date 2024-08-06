using System.Text.Json;

namespace CarAuction.IntegrationTests.Helpers;

public static class JsonSerializerOptionsHelper
{
    public static readonly JsonSerializerOptions ReadOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };
}