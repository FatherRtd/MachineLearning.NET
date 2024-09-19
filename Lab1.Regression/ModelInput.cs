namespace Lab1.Regression
{
    public class ModelInput
    {
        private ModelInput(float feature, float label)
        {
            Feature = feature;
            Label = label;
        }
        public float Feature { get; }
        public float Label { get; }

        public static ModelInput Create(float feature, float label = 0)
        {
            return new ModelInput(feature, label);
        }
    }
}
