using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

public class GetAssetsByPlayerFunction
{
    private readonly BattleGameContext _db;
    private readonly ILogger _logger;
    public GetAssetsByPlayerFunction(BattleGameContext db, ILoggerFactory loggerFactory)
    {
        _db = db;
        _logger = loggerFactory.CreateLogger<GetAssetsByPlayerFunction>();
    }

    // GET /api/getassetsbyplayer/{playerId}
    [Function("getassetsbyplayer")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "getassetsbyplayer/{playerId}")] HttpRequestData req, string playerId)
    {
        if (!Guid.TryParse(playerId, out var pid))
        {
            var bad = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            await bad.WriteStringAsync("playerId is not a valid GUID");
            return bad;
        }

        var q = await _db.PlayerAssets
            .Where(pa => pa.PlayerId == pid)
            .Include(pa => pa.Player)
            .Include(pa => pa.Asset)
            .Select(pa => new {
                PlayerName = pa.Player.PlayerName,
                Level = pa.Player.CurrentLevel,
                Age = pa.Player.Age,
                AssetName = pa.Asset.AssetName,
                Quantity = pa.Quantity
            })
            .ToListAsync();

        var res = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await res.WriteAsJsonAsync(q);
        return res;
    }
}