using InteractiveAmslerRecordingChart.Domain.Interactors;
using InteractiveAmslerRecordingChart.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static InteractiveAmslerRecordingChart.Domain.Utils;

namespace InteractiveAmslerRecordingChart.WebApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractiveAmslerRecordingChartController : Controller
    {
        private readonly FetchCoordinates _fetchSpots;
        private readonly SessionInteractor _sessionInteractor;

        public InteractiveAmslerRecordingChartController(FetchCoordinates fetchSpots, SessionInteractor sessionInteractor)
        {
            _fetchSpots = fetchSpots;
            _sessionInteractor = sessionInteractor;
        }

        [HttpPost("addSession")]
        public IActionResult AddSession(SessionInputModel session)
        {
            _sessionInteractor.AddSession(session);

            return Ok();
        }

        [HttpGet("oldCoordinates")]
        public List<CoordinateModel> GetOldCoordinates(string name)
        {
            return _fetchSpots.FetchOldCoordinates(name);
        }

        [HttpGet("sessions")]
        public SessionsPage GetSessions(int pageIndex, int size)
        {
            return _sessionInteractor.FetchSessions(pageIndex, size);
        }

        [HttpGet("session")]
        public SessionOutputModel GetSession(int id)
        {
            return _sessionInteractor.FetchSession(id);
        }

        [HttpGet("comparisonId")]
        public int? GetComparisonId(int currentId, TimeTravel timeTravel)
        {
            return _sessionInteractor.FetchComparisonId(currentId, timeTravel);
        }
    }
}
