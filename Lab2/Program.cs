
using Lab2;

var path = @"C:\source\MLTest\Lab2\data\kot-eti-udivitelnye-kotiki.jpg";

var src = File.ReadAllBytes(path);
var modelInput = new MLModel.ModelInput
{
    ImageSource = src
};

var result = Lab2.MLModel.Predict(modelInput);

var scores = Lab2.MLModel.GetSortedScoresWithLabels(result);

foreach (var (key, value) in scores)
{
    Console.WriteLine($"{key}: {value:F}");
}