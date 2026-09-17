using FitEngine.Workout.Application.Workouts.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitEngine.Workout.Application.Workouts.Queries.GetWorkoutByUserId
{
    public  record GetWorkoutByUserIdQuery(
        Guid UserId
        ): IRequest<List<WorkoutDto>>;

}
