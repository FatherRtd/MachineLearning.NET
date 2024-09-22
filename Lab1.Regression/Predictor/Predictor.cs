using Lab1.Regression.Data;
using Microsoft.ML;

namespace Lab1.Regression.Predictor
{
	public class Predictor
	{
		protected static string ModelPath =>
			Path.Combine(AppContext.BaseDirectory, "regression.mdl");
		private readonly MLContext _mlContext;

		private ITransformer _model;

		public Predictor()
		{
			_mlContext = new MLContext(123);
		}

		public ModelOutput Predict(ModelInput newSample)
		{
			LoadModel();

			var predictionEngine = _mlContext.Model
				.CreatePredictionEngine<ModelInput, ModelOutput>(_model);

			return predictionEngine.Predict(newSample);
		}

		private void LoadModel()
		{
			if (!File.Exists(ModelPath))
			{
				throw new FileNotFoundException($"File {ModelPath} doesn't exist.");
			}

			using (var stream = new FileStream(
				       ModelPath,
				       FileMode.Open,
				       FileAccess.Read,
				       FileShare.Read))
			{
				_model = _mlContext.Model.Load(stream, out _);
			}

			if (_model == null)
			{
				throw new Exception($"Failed to load Model");
			}
		}
	}
}
