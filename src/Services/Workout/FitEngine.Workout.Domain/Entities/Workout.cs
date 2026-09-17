using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitEngine.Workout.Domain.Entities
{
    public class Workout
    {
        public Guid Id { get; init; }=Guid.NewGuid();
        public Guid UserId { get; init; }
        public string Title { get; set; } = string.Empty;
        public DateTime DateLogged { get; init; } = DateTime.UtcNow;

        // Navigation property - getter only
        public readonly List<WorkoutExercise> Exercises  = new();

        public Workout() { }

        // helper method
        public decimal CalculateTotalVolume()
        {
            decimal totalVolume = 0;
            foreach (var exercise in Exercises)
            {
                foreach(var set in exercise.sets)
                {
                    totalVolume+=set.WeightKg*set.Repetitions;
                }
            }
            return totalVolume;
        }

        public static Workout Create(Guid userId, string title)
        {
            if(string.IsNullOrWhiteSpace(title))
                {
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            }

            return new Workout
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Title = title,
                DateLogged = DateTime.UtcNow
            };
        }

        public void AddExercise(WorkoutExercise exercise)
        {
            if (exercise == null)
            {
                throw new ArgumentNullException(nameof(exercise), "Exercise cannot be null.");
            }
            Exercises.Add(exercise);
        }   

    }
}
