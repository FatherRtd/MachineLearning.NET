using Lab1.Regression.Data;
using Lab1.Regression.Predictor;
using Lab1.Regression.Trainer;

Console.WriteLine("Hello World");

var dataGenerator = new DataGenerator(123);
var data = dataGenerator.Generate(1);

var newSample = new ModelInput()
{
	X = (float)(9 * Math.PI / 2),
};

var trainer = new FastTreeTrainer();
TrainEvaluatePredict(trainer, newSample);

static void TrainEvaluatePredict(TrainerBase trainer, ModelInput newSample)
{
	Console.WriteLine("*******************************");
	Console.WriteLine($"{trainer.Name}");
	Console.WriteLine("*******************************");

	trainer.Fit();

	var modelMetrics = trainer.Evaluate();

	Console.WriteLine($"Loss Function: {modelMetrics.LossFunction:0.##}{Environment.NewLine}" +
	                  $"Mean Absolute Error: {modelMetrics.MeanAbsoluteError:#.##}{Environment.NewLine}" +
	                  $"Mean Squared Error: {modelMetrics.MeanSquaredError:#.##}{Environment.NewLine}" +
	                  $"RSquared: {modelMetrics.RSquared:0.##}{Environment.NewLine}" +
	                  $"Root Mean Squared Error: {modelMetrics.RootMeanSquaredError:#.##}");

	trainer.Save();

	var predictor = new Predictor();
	var prediction = predictor.Predict(newSample);
	Console.WriteLine("------------------------------");
	Console.WriteLine($"For X: {newSample.X} Prediction Y: {prediction.Y}");
	Console.WriteLine("------------------------------");
}