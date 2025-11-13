using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

public class RegisterPlayerFunction
{
    private readonly BattleGameContext _db;
    private readonly ILogger _logger;
    public RegisterPlayerFunction(BattleGameContext db, ILoggerFactory loggerFactory)
    {
        _db = db;
        _logger = loggerFactory.CreateLogger<RegisterPlayerFunction>();
    }

    [Function("registerplayer")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var dto = JsonSerializer.Deserialize<Player>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (dto == null || string.IsNullOrWhiteSpace(dto.PlayerName))
        {
            var bad = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            await bad.WriteStringAsync("Invalid payload");
            return bad;
        }

        _db.Players.Add(dto);
        await _db.SaveChangesAsync();

        var res = req.CreateResponse(System.Net.HttpStatusCode.Created);
        await res.WriteAsJsonAsync(new { message = "Player created", playerId = dto.PlayerId });
        return res;
    }
}