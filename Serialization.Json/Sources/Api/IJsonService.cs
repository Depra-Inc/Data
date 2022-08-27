namespace Depra.Data.Serialization.Json.Api
{
    public interface IJsonService
    {
        string ToJson(object obj);

        T FromJson<T>(string json);
    }
}