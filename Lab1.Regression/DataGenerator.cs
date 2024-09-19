namespace Lab1.Regression
{
    public class DataGenerator
    {
        public DataGenerator(int? seed)
        {
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
        }

        private readonly Random _random;

        public IEnumerable<ModelInput> GenerateRandomData(int count)
        {
            for (int i = 0; i < count; i++)
            {
                float x = (float)_random.NextDouble() * 10;
                float y = (float)(Math.Sin(x) + x / 5.0);
                yield return ModelInput.Create(x ,y);
            }
        }
    }
}
