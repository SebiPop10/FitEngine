using FitEngine.Workout.Domain.Entities;
using FitEngine.Workout.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitEngine.Workout.Application.Workouts.DTOs
{
    public record WorkoutExerciseDto(
       string exerciseName,
        MuscleGroups.TargetMuscleGroup targetMuscleGroup,
        List<WorkoutSetDto> sets 
    );
    
}
