namespace Lab1.data
{
    public class DataGenerator
    {
        private readonly Random _random;
        private readonly Func<float, float> _dataFunction;

        public DataGenerator(int seed, Func<float, float> dataFunction)
        {
            _dataFunction = dataFunction;
            _random = new Random(seed);
        }

        private float GetRandom(double min, double max)
        {
            return (float)(_random.NextDouble() * (max - min) + min);
        }

        public IEnumerable<(float, float)> CreateIncreasingSequence(int count, float minValue, float maxValue)
        {
            float intervalDelta = (maxValue - minValue) / count;
            float min = minValue;
            for (int i = 0; i < count; i++, min += intervalDelta)
            {
                var x = GetRandom(min, min + intervalDelta);
                var y = _dataFunction(x);
                yield return (x, y);
            }
        }
    }
}
