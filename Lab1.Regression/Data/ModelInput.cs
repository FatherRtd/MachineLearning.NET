using Microsoft.ML.Data;

namespace Lab1.Regression.Data
{
	public class ModelInput
	{
		[LoadColumn(1)]
		public float X { get; set; }
		[LoadColumn(2)]
		public float Y { get; set; }
	}
}
