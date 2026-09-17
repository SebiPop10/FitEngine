using FitEngine.Workout.Application.Common.Interfaces;
using FitEngine.Workout.Application.Workouts.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitEngine.Workout.Application.Workouts.Queries.GetWorkoutById
{
    public class GetWorkoutByIdQueryHandler: IRequestHandler<GetWorkoutByIdQuery, WorkoutDto?>
    {
        private readonly IWorkoutRepository _workoutRepository;

        public GetWorkoutByIdQueryHandler(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        public async Task<WorkoutDto?> Handle(GetWorkoutByIdQuery req, CancellationToken cancellationToken)
        {
            var workout=await _workoutRepository.GetWorkoutByIdAsync(req.Id, cancellationToken);

            if(workout is null)
                {
                return null;
            }

            // 2. Map domain entities -> DTOs
            var exerciseDtos=workout.Exercises.Select(e=> new WorkoutExerciseDto(
               e.exerciseName,
               e.targetMuscleGroup,
               e.sets.Select(s=> new WorkoutSetDto(
                     s.SetNumber,
                     s.WeightKg,
                     s.Repetitions,
                     s.RIR
                     )).ToList()
               )).ToList();

            return new WorkoutDto(
                workout.Id,
                workout.UserId,
                workout.Title,
                workout.DateLogged,
                workout.CalculateTotalVolume(),
                exerciseDtos
                );

        }
    }
}
