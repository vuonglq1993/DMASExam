using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

public class CreateAssetFunction
{
    private readonly BattleGameContext _db;
    private readonly ILogger _logger;
    public CreateAssetFunction(BattleGameContext db, ILoggerFactory loggerFactory)
    {
        _db = db;
        _logger = loggerFactory.CreateLogger<CreateAssetFunction>();
    }

    [Function("createasset")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var dto = JsonSerializer.Deserialize<Asset>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (dto == null || string.IsNullOrWhiteSpace(dto.AssetName))
        {
            var bad = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            await bad.WriteStringAsync("Invalid payload");
            return bad;
        }

        _db.Assets.Add(dto);
        await _db.SaveChangesAsync();

        var res = req.CreateResponse(System.Net.HttpStatusCode.Created);
        await res.WriteAsJsonAsync(new { message = "Asset created", assetId = dto.AssetId });
        return res;
    }
}