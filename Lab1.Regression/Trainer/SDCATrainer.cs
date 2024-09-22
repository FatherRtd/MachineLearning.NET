using Lab1.Regression.Data;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;

namespace Lab1.Regression.Trainer
{
	public class SDCATrainer : TrainerBase
	{
		public SDCATrainer() : base()
		{
			Name = "SDCA trainer";
			//Model = MlContext.Regression.Trainers
			//	.Sdca(labelColumnName: "Label", featureColumnName: "Features");
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
