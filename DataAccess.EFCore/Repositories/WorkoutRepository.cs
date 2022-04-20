using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccess.EFCore.Repositories
{
    public class WorkoutRepository : GenericRepository<Workout>, IWorkoutRepository
    {
        public WorkoutRepository(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<Workout> GetLongest(int count)
        {
            return _context.Workouts.OrderByDescending(d => d.WorkoutID).Take(count).ToList();
        }
    }
}
