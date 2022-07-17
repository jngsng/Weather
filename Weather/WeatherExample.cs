
public class WeatherExample
{
    public async Task RunAsync()
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync("https://www.weather.go.kr/w/index.do");

        // 오류검사
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"서버에서 오류를 반환했습니다 반환코드 = {response.StatusCode}");
            return;
        }

        // 오류가 없다면
        string Contents = await response.Content.ReadAsStringAsync();
        Console.WriteLine(Contents);
    }
}