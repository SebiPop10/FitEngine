using FitEngine.Workout.Application.Common.Interfaces;
using FitEngine.Workout.Application.Workouts.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitEngine.Workout.Application.Workouts.Queries.GetWorkoutByUserId
{
    public class GetWorkoutByUserIdQueryHandler: IRequestHandler<GetWorkoutByUserIdQuery, List<WorkoutDto>>
    {
        private readonly IWorkoutRepository _workoutRepository;
        public GetWorkoutByUserIdQueryHandler(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }
        public async Task<List<WorkoutDto>> Handle(GetWorkoutByUserIdQuery request, CancellationToken cancellationToken)
        {
            var workouts = await _workoutRepository.GetWorkoutsByUserIdAsync(request.UserId, cancellationToken);
            return workouts.Select(workout => new WorkoutDto(
                workout.Id,
                workout.UserId,
                workout.Title,
                workout.DateLogged,
                workout.CalculateTotalVolume(),
                workout.Exercises.Select(exercise => new WorkoutExerciseDto(
                    exercise.exerciseName,
                    exercise.targetMuscleGroup,
                    exercise.sets.Select(set => new WorkoutSetDto(
                        set.SetNumber,
                        set.WeightKg,
                        set.Repetitions,
                        set.RIR
                    )).ToList()
                )).ToList()
            )).ToList();
        }
    }
}
