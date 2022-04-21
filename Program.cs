// See https://aka.ms/new-console-template for more information
using FinnHub.Net;
using Microsoft.FSharp.Core;
using Plotly.NET;
using System.Diagnostics;
using System.Text.Json;

var jsonString = await System.IO.File.ReadAllTextAsync("symbol.json");
var symbols = JsonSerializer.Deserialize<List<FinnHubModel>>(jsonString);

var count = symbols.Count;
if (count == 0)
{
    Console.WriteLine("Error: Symbol count is zero!");
    return;
}

var key = "c9c0aeqad3i8r0u4aka0";
var client = new FinnHubClient(key);

await ShowTimeOfSymbolSerial(symbols, count, client);

static async Task ShowTimeOfSymbolSerial(List<FinnHubModel>? symbols, int count, FinnHubClient client)
{
    var watch = new Stopwatch();
    watch.Start();
    foreach (var item in symbols)
    {
        var datas = await client.GetCandlesAsync(item.Symbol, FinnHub.Net.Enums.FinnHubTimeResolutions.Daily, DateTime.Now.AddDays(-2), DateTime.Now);
        if (!datas.Success)
        {
            Console.WriteLine($"Errro in get data from {item.Symbol}: {datas.Error}");
        }
    }
    watch.Stop();

    Console.WriteLine($"{count} symbols serial process run in {watch.Elapsed.TotalSeconds} sec");
}
/*
var i = 1;
Console.WriteLine("#" + ": \t" + "Low \t" + " \t" + "High" + " \t\t" + "Open" + " \t\t" + "Close" + "\t \t" + "Volume" + "\t \t" + "Time");
Console.WriteLine("--------------------------------------------------------------");
foreach (var item in datas.Data)
{

    Console.WriteLine(i++ + ": \t" + item.Low.ToString(".00") + " \t" + item.High.ToString(".00") + " \t"
     + item.Open.ToString(".00") + " \t" + item.Close.ToString(".00") + " \t"
    + item.Volume.ToString(".00") + "\t" + item.Timestamp.ToShortDateString());

}
var chart = Chart2D.Chart.Point<double, double, string>
(x: datas.Data.Select(x => Convert.ToDouble(x.High)).ToList(), y: datas.Data.Select(x => Convert.ToDouble(x.Volume)).ToList());

chart
    // .WithTraceName("Hello from C#", true)
    .WithXAxisStyle(title: Title.init("High"), ShowGrid: true, ShowLine: true)
    .WithYAxisStyle(title: Title.init("Volume"), ShowGrid: true, ShowLine: true)
    .Show();

var chart2 = Chart2D.Chart.Column<double, double, double, double, double>
(values: datas.Data.Select(x => Convert.ToDouble(x.High)).ToList()//, y: datas.Data.Select(x => Convert.ToDouble(x.Volume)).ToList()
);

chart2
    // .WithTraceName("Hello from C#", true)
    .WithXAxisStyle(title: Title.init("High"), ShowGrid: true, ShowLine: true)
    .WithYAxisStyle(title: Title.init("Volume"), ShowGrid: true, ShowLine: true)
    .Show();

var chart3 = Chart2D.Chart.StackedColumn<double, double, double, double, double>
(values: datas.Data.Select(x => Convert.ToDouble(x.High)).ToList()//, y: datas.Data.Select(x => Convert.ToDouble(x.Volume)).ToList()
);

chart3
    // .WithTraceName("Hello from C#", true)
    .WithXAxisStyle(title: Title.init("High"), ShowGrid: true, ShowLine: true)
    .WithYAxisStyle(title: Title.init("Volume"), ShowGrid: true, ShowLine: true)
    .Show();
// var chart2 = Chart3D.Chart.Surface<double, double, double, double, double>(
//     datas.Data.Select(x => Convert.ToDouble(x.High)).ToList()
//      ,new  FSharpOption<IEnumerable<double>>(datas.Data.Select(x => Convert.ToDouble(x.Close)).ToList())
//     , datas.Data.Select(x => Convert.ToDouble(x.Low)).ToList()
// );
// chart2
//     // .WithTraceName("Hello from C#", true)
//     .WithXAxisStyle(title: Title.init("Date"), ShowGrid: true, ShowLine: true)
//     .WithYAxisStyle(title: Title.init("High"), ShowGrid: true, ShowLine: true)
//     .Show();
*/