using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitEngine.Workout.Domain.Entities
{
    public class WorkoutSet
    {
        // Properties
        public Guid Id { get; init; }=Guid.NewGuid();
        public int SetNumber { get; set; }
        public decimal WeightKg { get; set; }
        public int Repetitions { get; set; }
        public int? RIR { get; set; } // reps in reserve
        public Guid WorkoutExerciseId { get; set; }

        public static WorkoutSet Create(int setNumber, decimal weightKg, int reps, int? rir)
        {
            return new WorkoutSet
            {
                Id = Guid.NewGuid(),
                SetNumber = setNumber,
                WeightKg = weightKg,
                Repetitions = reps,
                RIR = rir
            };
        }
    }
}
