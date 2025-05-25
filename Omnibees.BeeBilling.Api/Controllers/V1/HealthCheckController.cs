using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Omnibees.BeeBilling.Api.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController(HealthCheckService service) : ControllerBase
    {
        private readonly HealthCheckService _service = service;

        [HttpGet("api-status")]
        public IActionResult HealthCheckAPI()
        {
            return Ok(new
            {
                message = "A API está operando normalmente e pronta para receber requisições.",
                timestamp = DateTime.UtcNow
            });
        }

        [HttpGet("database-status")]
        public async Task<IActionResult> HealthCheckDB()
        {
            try
            {
                var report = await _service.CheckHealthAsync(h => h.Tags.Contains("db"));

                if (report.Status != HealthStatus.Healthy)
                {
                    return StatusCode(503, new
                    {
                        message = "Banco de dados indisponível no momento.",
                        status = report.Status.ToString(),
                        duration = report.TotalDuration,
                        timestamp = DateTime.UtcNow
                    });
                }

                return Ok(new
                {
                    message = "Conexão com o banco de dados estabelecida com sucesso.",
                    status = report.Status.ToString(),
                    duration = report.TotalDuration,
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Erro inesperado ao verificar a conexão com o banco de dados.",
                    details = ex.Message,
                    timestamp = DateTime.UtcNow
                });
            }
        }
    }
}
