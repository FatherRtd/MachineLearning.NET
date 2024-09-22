using Lab1.Regression.Data;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;

namespace Lab1.Regression.Trainer
{
	public class OGDTrainer: TrainerBase
	{
		public OGDTrainer() : base()
		{
			Name = "Online Gradient Descent";
			//Model = MlContext.Regression.Trainers
			//	.OnlineGradientDescent(labelColumnName: "Label", featureColumnName: "Features");
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
