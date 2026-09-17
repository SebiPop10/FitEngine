using FitEngine.Workout.Domain.Entities;
using FitEngine.Workout.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FitEngine.Workout.Infrastructure.Persistence
{
    public class WorkoutDbContext: DbContext
    {
        public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : base(options)
        {
        }
        public DbSet<Domain.Entities.Workout> Workouts=> Set<Domain.Entities.Workout>();
        public DbSet<WorkoutExercise> WorkoutExercises => Set<WorkoutExercise>();
        public DbSet<WorkoutSet> WorkoutSets => Set<WorkoutSet>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Configure Workout -> WorkoutExercise (1-to-Many)
            modelBuilder.Entity<Domain.Entities.Workout>(builder =>
            {
                builder.HasKey(w => w.Id)
                .HasName("PK_Workouts");
                builder.Property(w => w.Title)
                .IsRequired()
                .HasMaxLength(100);
                builder.Property(d => d.DateLogged)
                .IsRequired();
                builder.HasMany(w => w.Exercises)
                .WithOne()
                .HasForeignKey(e => e.workoutId)
                .OnDelete(DeleteBehavior.Cascade);



            });
            // 2. Configure WorkoutExercise -> WorkoutSet (1-to-Many)
            modelBuilder.Entity<WorkoutExercise>( builder =>
            {
                builder.HasKey(e => e.Id)
                .HasName("PK_WorkoutExercises");

                builder.Property(e => e.exerciseName)
                .IsRequired()
                .HasMaxLength(100);

                builder.Property(t => t.targetMuscleGroup)
                 .HasConversion(new EnumToStringConverter<MuscleGroups.TargetMuscleGroup>())
                   .IsRequired();


                builder.HasMany(e => e.sets)
                .WithOne()
                .HasForeignKey(s => s.WorkoutExerciseId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            // 3. Configure WorkoutSet
            modelBuilder.Entity<WorkoutSet>(builder =>
            {
                builder.HasKey(s => s.Id)
                .HasName("PK_WorkoutSets");
                builder.Property(s => s.SetNumber)
                .IsRequired();
                builder.Property(s => s.WeightKg)
                .IsRequired()
                .HasPrecision(6, 2); // e.g., 9999.99 kg max
                builder.Property(s => s.Repetitions)
                .IsRequired();
                builder.Property(s => s.RIR);
                
            });
        }
    }
}
