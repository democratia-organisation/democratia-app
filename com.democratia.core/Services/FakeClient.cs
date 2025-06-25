
namespace com.democratia.Services;

/// <summary>
/// Classe utilisée pour simuler un client comportant des erreurs dans les tests unitaires.
/// </summary>
internal class FakeClient : Client, IClient
{
    private readonly string? fakeResponse;
    public FakeClient(string? fakeResponse)
    {
        this.fakeResponse = fakeResponse;
    }

    public FakeClient() : this(null) { }

    public async Task<string> CreateModelAsync(params object?[]? parameters)
    {
        return await Task.FromResult(fakeResponse!);
    }


    public async Task<string> GetModelAsync(params object?[] parameters)
    {
        return await Task.FromResult(fakeResponse!);
    }

}
