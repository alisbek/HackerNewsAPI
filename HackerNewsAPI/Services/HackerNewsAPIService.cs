using HackerNewsAPI.Models;
using Microsoft.Extensions.Caching.Memory;

using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;

namespace HackerNewsAPI.Services
{
    public class HackerNewsAPIService
    {

        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly HackerNewsAPIConfig _config;

        public HackerNewsAPIService(IMemoryCache cache, IOptions<HackerNewsAPIConfig> configOptions, IHttpClientFactory httpClientFactory)
        {
            _cache = cache;
            _config = configOptions.Value;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IEnumerable<int>> GetBestStoryIds()
        {
            if (_cache.TryGetValue("bestStoryIds", out IEnumerable<int> bestStoryIds)) return bestStoryIds;

            var response = await _httpClient.GetStringAsync(_config.BestStoriesUrl);
            bestStoryIds = JArray.Parse(response).Select(id => (int)id);
            _cache.Set("bestStoryIds", bestStoryIds, TimeSpan.FromMinutes(5));

            return bestStoryIds;
        }

        public async Task<HackerNewsStory> GetStoryDetails(int storyId)
        {
            if (_cache.TryGetValue($"story_{storyId}", out HackerNewsStory story)) return story;

            var response = await _httpClient.GetStringAsync(string.Format(_config.StoryUrlTemplate ?? throw new NullReferenceException(), storyId));
            var storyData = JObject.Parse(response);

            story = StoryFormatter(storyData);

            _cache.Set($"story_{storyId}", story, TimeSpan.FromMinutes(5)); 

            return story;
        }

        private static HackerNewsStory StoryFormatter(JObject storyData)
        {
            var story = new HackerNewsStory
            {
                Title = storyData.Value<string>("title"),
                Uri = storyData.Value<string>("url"),
                PostedBy = storyData.Value<string>("by"),
                Time = DateTimeOffset.FromUnixTimeSeconds(storyData.Value<long>("time")).UtcDateTime,
                Score = storyData.Value<int>("score"),
                CommentCount = storyData.Value<int>("descendants")
            };
            return story;
        }
    }
}
