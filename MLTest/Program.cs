// See https://aka.ms/new-console-template for more information

using System.Text;
using Lab1.Regression;

Console.WriteLine("Hello, World!");

var regressionModel = new RegressionModel();

regressionModel.Train();

var evaluation = regressionModel.Evaluate();

var sb = new StringBuilder();

var evaluationString = sb.AppendLine($"Evaluation:")
                         .AppendLine($"{nameof(evaluation.LossFunction)}: {evaluation.LossFunction})")
                         .AppendLine($"{nameof(evaluation.MeanAbsoluteError)}: {evaluation.MeanAbsoluteError})")
                         .AppendLine($"{nameof(evaluation.MeanSquaredError)}: {evaluation.MeanSquaredError})")
                         .AppendLine($"{nameof(evaluation.RSquared)}: {evaluation.RSquared})")
                         .AppendLine($"{nameof(evaluation.RootMeanSquaredError)}: {evaluation.RootMeanSquaredError})");

Console.WriteLine(evaluationString);


var x = 10;
var result = regressionModel.Predict(x);
Console.WriteLine($"Prediction result: {x} : {result.Score}");

