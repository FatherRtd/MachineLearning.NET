using Lab1.Regression.Data;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML.Transforms;

namespace Lab1.Regression.Trainer
{
	public class FastTreeTrainer : TrainerBase
	{
		public FastTreeTrainer()
		{
			Name = "Fast tree";
			Model = MlContext.Regression.Trainers.FastTree(new FastTreeRegressionTrainer.Options
			{
				NumberOfLeaves = 20,
				NumberOfTrees = 200,
				MinimumExampleCountPerLeaf = 5,
				LearningRate = 0.1,
				LabelColumnName = "Label",
				FeatureColumnName = "Features"
			});
		}

		protected override EstimatorChain<NormalizingTransformer> BuildDataProcessingPipeline()
		{
			var dataProcessPipeline = MlContext.Transforms
				.CopyColumns("Label",
					nameof(ModelInput.Y))
				.Append(MlContext.Transforms.Concatenate("Features",
					"X"))
				.Append(MlContext.Transforms.NormalizeLogMeanVariance("Features",
					"Features"))
				.AppendCacheCheckpoint(MlContext);

			return dataProcessPipeline;
		}
	}
}
