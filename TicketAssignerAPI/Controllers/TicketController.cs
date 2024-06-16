using Microsoft.AspNetCore.Mvc;
using TicketAssignerAPI.Model;
using TicketAssignerAPI.TicketAssignerML;

namespace TicketAssignerAPI.Controllers
{
    public class TicketController : Controller
    {
        private readonly string modelPath = "model";
        private readonly ILogger<TicketController> _logger;

        public TicketController(ILogger<TicketController> logger)
        {
            _logger = logger;
        }
        [HttpPost("train")]
        public IActionResult TrainModel([FromQuery] string dataPath)
        {
            try
            {
                ModelTraining.TrainModel(dataPath, modelPath);
                return Ok("Model trained successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error training model");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("predict")]
        public IActionResult PredictTicket([FromBody] Ticket ticket)
        {
            try
            {
                bool isAssigned = ModelPrediction.Predict(modelPath, ticket);
                return Ok(new { IsAssigned = isAssigned });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error predicting ticket assignment");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}