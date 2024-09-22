using Microsoft.ML.Data;

namespace Lab1.Regression.Data
{
	public class ModelOutput
	{
		[ColumnName("Score")]
		public float Y { get; set; }
	}
}
