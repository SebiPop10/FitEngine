using FitEngine.Workout.Application.Workouts.Commands.CreateWorkout;
using FitEngine.Workout.Application.Workouts.Queries.GetWorkoutById;
using FitEngine.Workout.Application.Workouts.Queries.GetWorkoutByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FitEngine.Workout.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutController: ControllerBase
    {
        private readonly ISender _mediator;

        public WorkoutController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{Id:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWorkoutsById([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var query = new GetWorkoutByIdQuery(Id);
            var workout =await _mediator.Send(query, cancellationToken);    

            if(workout is null)
            {
                 return NotFound(new { message = $"Workout with ID '{Id}' was not found." });
            }
            return Ok(workout);
        }

        [HttpGet("user/{UserId:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetWorkoutsByUserId([FromRoute] Guid UserId, CancellationToken cancellationToken)
        {
            var query = new GetWorkoutByUserIdQuery(UserId);
            var workouts = await _mediator.Send(query, cancellationToken);
            if (workouts == null || !workouts.Any())
            {
                return NotFound(new { message = $"No workouts found for User ID '{UserId}'." });
            }
            return Ok(workouts);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> CreateWorkout([FromBody] CreateWorkoutCommand command, CancellationToken cancellationToken)
        {
            var workoutId=await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(CreateWorkout), new { id = workoutId}, workoutId);
        }

        //[HttpPost]
        //[Route("bulk")]

        //[ProducesResponseType(typeof(List<Guid>), StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]

        // Implement save changes






    }
}
