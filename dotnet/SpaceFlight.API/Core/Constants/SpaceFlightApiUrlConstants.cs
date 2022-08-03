namespace SpaceFlight.API.Core.Constants
{
    public static class SpaceFlightApiUrlConstants
    {
        const string BaseUrl = "https://api.spaceflightnewsapi.net/v3";
        public static string GetArticles => $"{BaseUrl}/articles";
        public static string GetTotalArticles => $"{BaseUrl}/articles/count";
    }
}
