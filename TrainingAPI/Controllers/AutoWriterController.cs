using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Utility;
using Utility.Models;

namespace TrainingAPI.Controllers
{
    [ApiController]
    [Route("api/writer")]
    public class AutoWriterController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public  AutoWriterController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> SendTextToGPT(string text, [FromServices] IConfiguration configuration)
        {
            BaseInputModel baseInput = new BaseInputModel();
            int maxAttempts = 5; // Número máximo de tentativas
            int currentAttempt = 1;

            try
            {
                var token = configuration.GetValue<string>("GptKey");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var model = new GptInputModel(text);

                string requestBody = JsonConvert.SerializeObject(model);

                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                while (currentAttempt <= maxAttempts)
                {
                    var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<GptViewModel>();
                        var promptResponse = result.choices.First();
                        return Ok(promptResponse.text.Replace("\n", "").Replace("\t", ""));
                    }
                    else
                    {
                        // Caso ocorra um erro, aguarda um tempo antes de tentar novamente.
                        int retryIntervalInSeconds = 5; // Intervalo entre tentativas em segundos
                        await Task.Delay(retryIntervalInSeconds * 1000);
                        currentAttempt++;
                    }
                }

                // Caso as tentativas se esgotem e ainda haja erro, retorne um erro ou uma mensagem de falha.
                return BadRequest("Falha ao enviar a requisição após várias tentativas.");
            }
            catch (Exception ex)
            {
                // Trate a exceção de acordo com a necessidade do seu aplicativo.
                return StatusCode(500, "Erro ao enviar a requisição: " + ex.Message);
            }
        }

    }
}
