using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Omnibees.BeeBilling.Api.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController(HealthCheckService service) : ControllerBase
    {
        private readonly HealthCheckService _service = service;

        /// <summary>
        /// Endpoint para verificar se a API está operando normalmente e pronta para receber requisições.
        /// </summary>
        /// <returns>Retorna status 200 com mensagem de operação normal e timestamp da verificação.</returns>
        [HttpGet("api-status")]
        public IActionResult HealthCheckAPI()
        {
            return Ok(new
            {
                message = "A API está operando normalmente e pronta para receber requisições.",
                timestamp = DateTime.UtcNow
            });
        }

        /// <summary>
        /// Endpoint para verificar a saúde da conexão com o banco de dados, utilizando os health checks configurados.
        /// </summary>
        /// <returns>
        /// Retorna status 200 caso o banco esteja saudável, 503 se indisponível, ou 500 em caso de erro inesperado.
        /// Inclui detalhes do status, duração da verificação e timestamp.
        /// </returns>
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
