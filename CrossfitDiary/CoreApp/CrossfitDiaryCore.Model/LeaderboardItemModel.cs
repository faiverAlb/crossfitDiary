namespace CrossfitDiaryCore.Model
{
    public class LeaderboardItemModel
    {
        public readonly string Level;

        public readonly string UserName;

        public readonly string Result;

        public LeaderboardItemModel(string level, string userName, string result)
        {
            Level = level;
            UserName = userName;
            Result = result;
        }
    }
}