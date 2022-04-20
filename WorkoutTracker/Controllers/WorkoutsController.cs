using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public WorkoutsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Does not get longest, but orders by largest ID value (from guide i followed).
        public IActionResult GetLongestWorkout([FromQuery]int count)
        {
            var longestWorkouts = _unitOfWork.Workouts.GetLongest(count);
            return Ok(longestWorkouts);
        }

        [HttpPost]
        public IActionResult AddWorkouts()
        {
            var workout = new Workout
            {
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now,
                Duration = 0,
                Comments = "",
            };
            _unitOfWork.Workouts.Add(workout);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var workoutList = _unitOfWork.Workouts.GetAll();
            return Ok(workoutList);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var workoutById = _unitOfWork.Workouts.GetById(id);
            return Ok(workoutById);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteWorkout(int id)
        {
            var workoutDelete = _unitOfWork.Workouts.GetById(id);
            _unitOfWork.Workouts.Remove(workoutDelete);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult UpdateWorkout(int id)
        {
            var workoutUpdate = _unitOfWork.Workouts.GetById(id);
            workoutUpdate.Comments = "updated";

            _unitOfWork.Complete();
            return Ok();
        }

    }
}
