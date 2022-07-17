
using System.Xml;

public class WeatherExample
{
    public async Task RunAsync()
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync("http://www.kma.go.kr/weather/forecast/mid-term-rss3.jsp?stnId=156");

        // Error
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error Return Code = {response.StatusCode}");
            return;
        }

        // RSS data
        string Contents = await response.Content.ReadAsStringAsync();

        // XML 파싱
        XmlDocument document  = new XmlDocument();
        document.LoadXml(Contents);

        // Location
        var nodes = document.DocumentElement.SelectNodes("descendant :: location");

        foreach(XmlNode node in nodes)
        {
            var provinceNode = node.SelectSingleNode("province");
            var cityNode = node.SelectSingleNode("city");

            if (provinceNode == null || cityNode == null) continue;

            if (provinceNode.InnerText != "광주ㆍ전라남도" || cityNode.InnerText != "나주") continue;

            PrintNode(node);

            break;
        }

    }

    private static void PrintNode(XmlNode sourceNode)
    {
        var nodes = sourceNode.SelectNodes("descendant :: data");
        foreach(XmlNode node in nodes)
        {
            var dataNode = node.SelectSingleNode("tmEf");
            var weatherNode = node.SelectSingleNode("wf");

            if(dataNode == null || weatherNode == null) continue;

            Console.WriteLine($"날짜 : {dataNode.InnerText}, 날씨 : {weatherNode.InnerText}");
        }
    }
}