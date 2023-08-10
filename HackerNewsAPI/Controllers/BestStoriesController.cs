using HackerNewsAPI.Models;
using HackerNewsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsAPI.Controllers
{
    public class BestStoriesController : Controller
    {
        private readonly HackerNewsAPIService _hackerNewsService;

        public BestStoriesController(HackerNewsAPIService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        [HttpGet("beststories/{n}")]
        public async Task<IActionResult> GetBestStories(int n)
        {
            try
            {
                if (n <= 0)
                {
                    return BadRequest("Invalid value of n.");
                }

                var bestStoryIds = await _hackerNewsService.GetBestStoryIds();
                var bestStories = await GetBestStoriesDetails(bestStoryIds.Take(n));

                return Ok(bestStories);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<IEnumerable<HackerNewsStory>> GetBestStoriesDetails(IEnumerable<int> storyIds)
        {
            var tasks = storyIds.Select(async id =>
            {
                var story = await _hackerNewsService.GetStoryDetails(id);
                return story;
            }).OrderByDescending(s => s.Result.Score);

            return await Task.WhenAll(tasks);
        }
    }
}
