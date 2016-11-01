namespace CrossfitDiary.Model
{
    public class ExerciseMeasureType : BaseModel
    {
        public MeasureType MeasureType { get; set; }

        public string Description { get; set; }

    }
    public enum MeasureType
    {
        Distance = 0,
        Count = 1,
        Time = 2,
        Weight = 3
    }
}