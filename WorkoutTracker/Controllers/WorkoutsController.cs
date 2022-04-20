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

        public IActionResult GetById()
        {
            var workoutById = _unitOfWork.Workouts.GetById(1);
            return Ok(workoutById);
        }
        // Implement Update method in generic repository
        //[HttpPut]
        //public IActionResult UpdateWorkout()
        //{
        //    return Ok();
        //}

    }
}
