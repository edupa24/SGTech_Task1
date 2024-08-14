using Microsoft.Extensions.Configuration;
using System.Reflection;
using Task1;

var configuration = new ConfigurationBuilder()
    .AddJsonFile(Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory!.FullName, "appsettings.json"), true, true)
    .Build();

var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

var httpClient = new HttpClient();

// sample url: https://datausa.io/api/data?drilldowns=Nation&measures=Population

var url = $"https://datausa.io/api/data?drilldowns=Nation&measures={appSettings.Measures}";

using var response = await httpClient.GetAsync(url);
var jsonResponse = await response.Content.ReadAsStringAsync();
Console.WriteLine($"{jsonResponse}");
