using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace InteractiveAmslerRecordingChart.WebApp.Pages
{
    public class RecordsModel : PageModel
    {
        private readonly ILogger<RecordsModel> _logger;

        public RecordsModel(ILogger<RecordsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
