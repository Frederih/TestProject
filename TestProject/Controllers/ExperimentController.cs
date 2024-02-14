using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestProject.Persistence;
using TestProject.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperimentController : ControllerBase
    {
        private readonly TaskContext db;
        private readonly ClientService clientService;

        public ExperimentController(TaskContext db, ClientService clientService)
        {
            this.db = db;
            this.clientService = clientService;
        }

        [HttpGet("Experiment")]
        public async Task<ActionResult<Client>> GetExperiment(string deviceToken)
        {
            var existingExperiment = await db.Clients
                .Where(e => e.DeviceToken == deviceToken)
                .FirstOrDefaultAsync();

            if (existingExperiment != null)
            {
                return Ok(new { key = existingExperiment.ExperimentKey, value = existingExperiment.ExperimentValue });
            }
            
            var newExperiment = clientService.GenerateNewExperiment(deviceToken);

            try
            {
                db.Clients.Add(newExperiment);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { key = newExperiment.ExperimentKey, value = newExperiment.ExperimentValue });
        } 
    }
}
