using System.Text.Json;


namespace Application.Services

{
    public class CnpjLookupService
    {
        private readonly HttpClient _httpClient;
        public CnpjLookupService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public interface ICnpjLookupService
        {
            Task<JsonDocument?> ConsultCnpjAsync(string cnpj);
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
