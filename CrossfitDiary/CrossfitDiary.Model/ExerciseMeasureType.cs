namespace CrossfitDiary.Model
{
    public class ExerciseMeasureType : BaseModel
    {
        public MeasureType MeasureType { get; set; }

        public string Description { get; set; }

        public string ShortMeasureDescription { get; set; }

    }
    public enum MeasureType
    {
        Distance = 0,
        Count = 1,
        Weight = 2,
        Calories = 3
    }
}