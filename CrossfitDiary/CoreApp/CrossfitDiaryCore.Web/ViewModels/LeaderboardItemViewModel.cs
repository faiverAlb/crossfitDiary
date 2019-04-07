using Newtonsoft.Json;

namespace CrossfitDiaryCore.Web.ViewModels
{
    public class LeaderboardItemViewModel
    {
        [JsonProperty("level")]
        public string Level;

        [JsonProperty("userName")]
        public string UserName;

        [JsonProperty("result")]
        public string Result;

    }
}