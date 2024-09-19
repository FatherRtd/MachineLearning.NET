using Microsoft.ML;
using Microsoft.ML.Data;

namespace Lab1.Regression
{
    public class RegressionModel
    {
        public RegressionModel(int? generatorSeed = null)
        {
            _mlContext = new MLContext(generatorSeed);

            var dataGenerator = new DataGenerator(generatorSeed);
            var randomData = dataGenerator.GenerateRandomData(100);

            var data = _mlContext.Data.LoadFromEnumerable(randomData);
            _dataSplit = _mlContext.Data.TrainTestSplit(data, 0.3);
        }

        private readonly DataOperationsCatalog.TrainTestData _dataSplit;
        private readonly MLContext _mlContext;
        private ITransformer? _trainedModel;


        public void Train()
        {
            var trainData = _dataSplit.TrainSet;

            var pipeline = _mlContext.Transforms
                                     .Concatenate("Features",
                                         new[]
                                         {
                                             nameof(ModelInput.Feature)
                                         })
                                     .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: nameof(ModelInput.Label),
                                         maximumNumberOfIterations: 100));

            _trainedModel = pipeline.Fit(trainData);
        }

        public RegressionMetrics Evaluate()
        {
            if (_trainedModel == null)
            {
                throw new Exception("Модель не обучена");
            }

            var testSetTransform = _trainedModel.Transform(_dataSplit.TestSet);

            return _mlContext.Regression.Evaluate(testSetTransform);
        }

        public ModelPrediction Predict(float x)
        {
            if (_trainedModel == null)
            {
                throw new Exception("Модель не обучена");
            }

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelPrediction>(_trainedModel);

            return predictionEngine.Predict(ModelInput.Create(x));
        }
    }
}
