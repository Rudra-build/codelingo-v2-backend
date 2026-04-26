using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CodeLingo.Backend.Services
{
    public class OpenAIService
    {
        private readonly string _apiKey;

        public OpenAIService()
        {
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? "";
        }

        public string GenerateQuiz(string content, int level)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var prompt = $@"
            Generate exactly 5 multiple choice questions from the text below.

            User level: {level}

            Difficulty rules:
            - Level 1 = basic recall questions
            - Higher levels = harder reasoning questions
            - As level increases, make distractors more similar and questions less obvious
            - Always keep questions answerable from the provided text

            STRICT RULES:
            - Return ONLY valid JSON
            - No markdown
            - No explanation
            - No ```json
            - Format EXACTLY like this:

            [
            {{
                ""question"": ""..."",
                ""options"": [""A"", ""B"", ""C"", ""D""],
                ""correctIndex"": 0
            }}
            ]

            Text:
            {content}
            ";


            var requestBody = new
            {
                model = "gpt-4.1-mini",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);

            var response = httpClient.PostAsync(
                "https://api.openai.com/v1/chat/completions",
                new StringContent(json, Encoding.UTF8, "application/json")
            ).Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;

            var jsonResponse = JsonNode.Parse(responseContent);

            var contentOnly = jsonResponse?["choices"]?[0]?["message"]?["content"]?.ToString();

            return contentOnly ?? "";
        }
    }
}