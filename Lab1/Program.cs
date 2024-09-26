using Lab1.data;
using Plotly.NET.CSharp;
using GenericChart = Plotly.NET.GenericChart;

Func<float, float> function = (x) => 2 * x + 5;

var dataGenerator = new DataGenerator(123123, function);
var data = dataGenerator.CreateIncreasingSequence(20, 0, 10).ToArray();
var toPredict = data.Skip(10).Take(10).ToArray();

for (int i = 0; i < toPredict.Count(); i++)
{
	var sampleData = new Lab1.Lab1.ModelInput
	{
		X = toPredict[i].Item1
	};

	var result = Lab1.Lab1.Predict(sampleData);

	toPredict[i].Item2 = result.Score;
}



Chart.Combine(new List<GenericChart>()
	{
		Chart.Line<float, float, string>(x: data.Select(x => x.Item1), y: data.Select(x => x.Item2),
			Name: "Real"),
		Chart.Line<float, float, string>(x: toPredict.Select(x => x.Item1), y: toPredict.Select(x => x.Item2),
			Name: "Predicted")
	})
	.WithXAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("xAxis"))
	.WithYAxisStyle<double, double, string>(Title: Plotly.NET.Title.init("yAxis"))
	.Show();
