using System;

namespace Lab1.Regression.Data
{
	public class DataGenerator
	{
		private Random _random;

		public DataGenerator(int seed)
		{
			_random = new Random(seed);
		}

		public IEnumerable<ModelInput> Generate(int count = 1000, float start = (float)(-2 * Math.PI), float end = (float)(2 * Math.PI))
		{
			float segmentSize = (end - start) / count;

			for (int i = 0; i < count; i++)
			{
				float segmentStart = start + segmentSize * i;
				float segmentEnd = segmentStart + segmentSize;
				float x = (float)(_random.NextDouble() * (segmentEnd - segmentStart) + segmentStart);
				float y = (float)Math.Sin(x);

				yield return new ModelInput { X = x, Y = y };
			}
		}
	}
}
