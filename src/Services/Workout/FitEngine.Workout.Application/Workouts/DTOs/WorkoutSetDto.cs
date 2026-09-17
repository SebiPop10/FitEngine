using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitEngine.Workout.Application.Workouts.DTOs
{
    public record WorkoutSetDto(
    
        int SetNumber,
        decimal WeightKg,
        int Repetitions,
        int? RIR 
    );
}
