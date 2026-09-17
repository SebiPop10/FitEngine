using FitEngine.Workout.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitEngine.Workout.Domain.Entities
{
    public class WorkoutExercise
    {
        public Guid Id { get; init; }=Guid.NewGuid();
        public string exerciseName { get; set; }= string.Empty;
        public MuscleGroups.TargetMuscleGroup targetMuscleGroup { get;  set; }
        public List<WorkoutSet> sets { get; } = new();
        public Guid workoutId { get; set; }

        public static WorkoutExercise Create(string exerciseName, MuscleGroups.TargetMuscleGroup targetMuscleGroup)
        {
            return new WorkoutExercise
            {
                Id = Guid.NewGuid(),
                exerciseName = exerciseName,
                targetMuscleGroup = targetMuscleGroup
            };
        }

        public void AddSet(WorkoutSet set)
        {
            sets.Add(set);
        }
    }
}
