namespace SoftTouchSearch.Pages
{
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.Search;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using SoftTouchSearch.Data;
    using SoftTouchSearch.Data.Models;
    using SoftTouchSearch.Index.Services;

    public class TestModel(IIndexService indexService, StoryDbContext context) : PageModel
    {
        private readonly IIndexService indexService = indexService;

        private readonly StoryDbContext context = context;

        [BindProperty]
        public string? QueryText { get; set; } = "Ghost Office";

        public IEnumerable<Document> Results { get; set; } = new List<Document>();

        public IEnumerable<Episode> ResultsContents { get; set; } = new List<Episode>();

        public IActionResult OnGet()
        {
            if (!this.indexService.IsIndexBuilt)
            {
                return this.StatusCode(StatusCodes.Status503ServiceUnavailable);
            }

            BooleanQuery query = new()
            {
                { new TermQuery(new Term("content", "ghost")), Occur.SHOULD },
                { new TermQuery(new Term("content", "office")), Occur.SHOULD },
            };

            this.Results = this.indexService.Search(query);
            ICollection<Guid> episodeIds = this.Results
                .Select(doc => doc.Get("id"))
                .Select(id => Guid.Parse(id))
                .ToList();

            this.ResultsContents = this.context.Episodes
                .Where(episode => episodeIds.Contains(episode.Id))
                .ToList();

            return this.Page();
        }
    }
}
