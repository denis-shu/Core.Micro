using System;
using System.Threading.Tasks;
using Micro.Base.Exceptions;
using Micro.Services.Activities.Domain.Models;
using Micro.Services.Activities.Domain.Repos;

namespace Micro.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IActivityRepo _activityRepo;
        public ActivityService(IActivityRepo activityRepo, ICategoryRepo categoryRepo)
        {
            _activityRepo = activityRepo;
            _categoryRepo = categoryRepo;

        }
        public async Task AddAsync(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            var activityCategoty = await _categoryRepo.GetAsync(name);

            if (activityCategoty == null)
                throw new MicroException("category_nf", $"Category: {category} was nf");

            var activity = new Activity(id, userId, category, name, description, createdAt);

            await _activityRepo.AddAsync(activity);

        }
    }
}