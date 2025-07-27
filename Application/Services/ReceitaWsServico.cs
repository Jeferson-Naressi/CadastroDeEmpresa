using System.Text.Json;


namespace CadastroDeEmpresa.Servicos

{
    public class ReceitaWsServico
    {
        private readonly HttpClient _httpClient;
        public ReceitaWsServico(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<JsonDocument?> ConsultarCnpjAsync(string cnpj)
        {
            var url = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var contentStream = await response.Content.ReadAsStreamAsync();
            return await JsonDocument.ParseAsync(contentStream);

        }
    }
}
