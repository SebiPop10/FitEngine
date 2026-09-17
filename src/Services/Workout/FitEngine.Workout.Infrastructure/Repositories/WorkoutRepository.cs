using FitEngine.Workout.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitEngine.Workout.Infrastructure.Persistence;
using FitEngine.Workout.Domain.Entities;
using FitEngine.Workout.Domain;

namespace FitEngine.Workout.Infrastructure.Repositories
{
    public class WorkoutRepository: IWorkoutRepository
    {
        private readonly WorkoutDbContext _context;

        public WorkoutRepository(WorkoutDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Workout?> GetWorkoutByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            return await _context.Workouts.Include(w=>w.Exercises).ThenInclude(e => e.sets).FirstOrDefaultAsync(w => w.Id == Id, cancellationToken);


        }

        public async Task<List<Domain.Entities.Workout>> GetWorkoutsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Workouts.Include(w=>w.Exercises).ThenInclude(e=>e.sets).Where(u=> u.UserId==userId).ToListAsync(cancellationToken);
        }

        public async Task AddWorkoutAsync(Domain.Entities.Workout workout, CancellationToken cancellationToken=default)
        {
            await _context.Workouts.AddAsync(workout, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
