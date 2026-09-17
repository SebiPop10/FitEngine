using FitEngine.Workout.Application.Workouts.DTOs;
using FitEngine.Workout.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitEngine.Workout.Application.Common.Interfaces
{
    public interface  IWorkoutRepository
    {
        // get workout by id
        public Task<FitEngine.Workout.Domain.Entities.Workout?> GetWorkoutByIdAsync( Guid Id, CancellationToken cancellationToken = default);

        // get workout by user id
        public Task<List<FitEngine.Workout.Domain.Entities.Workout>> GetWorkoutsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        // add workout

        public Task AddWorkoutAsync(FitEngine.Workout.Domain.Entities.Workout workout, CancellationToken cancellationToken = default);

        // save workout

        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
