using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace InteractiveAmslerRecordingChart.WebApp.Pages
{
    public class RecordModel : PageModel
    {
        private readonly ILogger<RecordModel> _logger;

        public RecordModel(ILogger<RecordModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int id)
        {

        }
    }
}
