using FitEngine.Workout.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitEngine.Workout.Application.Workouts.DTOs;
using FitEngine.Workout.Domain.Entities;

namespace FitEngine.Workout.Application.Workouts.Commands.CreateWorkout
{
    public class CreateWorkoutCommandHandler : IRequestHandler<CreateWorkoutCommand, Guid>
    {
        private readonly IWorkoutRepository _workoutRepository;
        public CreateWorkoutCommandHandler(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        public async Task<Guid> Handle(CreateWorkoutCommand req, CancellationToken cancellationToken)
        {
            var workout = Domain.Entities.Workout.Create(req.UserId, req.Title);

            // 2. Map exercise DTOs -> domain entities

            foreach(var exerciseDto in req.Exercises)
            {
                var exercise=WorkoutExercise.Create(exerciseDto.exerciseName, exerciseDto.targetMuscleGroup);

                foreach(var setDto in exerciseDto.sets)
                {
                    var set=WorkoutSet.Create(setDto.SetNumber, setDto.WeightKg, setDto.Repetitions, setDto.RIR);
                    exercise.AddSet(set);
                }
                workout.AddExercise(exercise);
            }
            // 3. Persist via repository
            await _workoutRepository.AddWorkoutAsync(workout, cancellationToken);
            await _workoutRepository.SaveChangesAsync(cancellationToken);

            return workout.Id;
        }
    }
}
