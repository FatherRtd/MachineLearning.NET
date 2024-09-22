using Lab1.Regression.Data;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML.Transforms;

namespace Lab1.Regression.Trainer
{
	public abstract class TrainerBase
	{
		public string Name { get; protected set; }

		protected static string ModelPath =>
			Path.Combine(AppContext.BaseDirectory, "regression.mdl");

		protected readonly MLContext MlContext;

		protected DataOperationsCatalog.TrainTestData DataSplit;

		//protected ITrainerEstimator<RegressionPredictionTransformer
		//	<LinearRegressionModelParameters>, LinearRegressionModelParameters> Model;
		protected FastTreeRegressionTrainer Model;

		protected ITransformer TrainedModel;

		protected DataGenerator DataGenerator;

		protected TrainerBase()
		{
			MlContext = new MLContext(123);
			DataGenerator = new DataGenerator(123);
		}

		public void Fit()
		{
			DataSplit = LoadAndPrepareData();
			var dataProcessPipeline = BuildDataProcessingPipeline();
			var trainingPipeline = dataProcessPipeline.Append(Model);

			TrainedModel = trainingPipeline.Fit(DataSplit.TrainSet);
		}

		public RegressionMetrics Evaluate()
		{
			var testSetTransform = TrainedModel.Transform(DataSplit.TestSet);

			return MlContext.Regression.Evaluate(testSetTransform);
		}

		public void Save()
		{
			MlContext.Model.Save(TrainedModel, DataSplit.TrainSet.Schema, ModelPath);
		}

		protected abstract EstimatorChain<NormalizingTransformer> BuildDataProcessingPipeline();

		private DataOperationsCatalog.TrainTestData LoadAndPrepareData()
		{
			var generatedData = DataGenerator.Generate(100000);
			var trainingDataView = MlContext.Data.LoadFromEnumerable(generatedData);
			return MlContext.Data.TrainTestSplit(trainingDataView, testFraction: 0.2);
		}
	}
}
