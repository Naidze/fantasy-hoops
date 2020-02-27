using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using fantasy_hoops.Database;
using fantasy_hoops.Dtos;
using fantasy_hoops.Repositories.Interfaces;

namespace fantasy_hoops.Repositories
{
    public class AchievementsRepository : IAchievementsRepository
    {
        private readonly GameContext _context;

        public AchievementsRepository()
        {
            _context = new GameContext();
        }
        
        public List<AchievementDto> GetExistingAchievements()
        {
            return _context.Achievements
                .Select(achievement => new AchievementDto
                {
                    Id = achievement.Id,
                    Type = achievement.Type,
                    Title = achievement.Title,
                    Description = achievement.Description
                        .Replace("{}", achievement.GoalBase.ToString()),
                    CompletedMessage = achievement.CompletedMessage,
                    Icon = achievement.Icon,
                    GoalBase = achievement.GoalBase
                })
                .ToList();
        }

        public Dictionary<String, List<UserAchievementDto>> GetAllUserAchievements()
        {
            return _context.UserAchievements
                .Include(achievement => achievement.Achievement)
                .Include(achievement => achievement.User)
                .Select(achievement => new UserAchievementDto
                {
                    UserId = achievement.UserID,
                    UserName = achievement.User.UserName,
                    Progress = achievement.Progress,
                    Level = achievement.Level,
                    LevelUpGoal = achievement.LevelUpGoal,
                    IsAchieved = achievement.IsAchieved,
                    Achievement = new AchievementDto
                    {
                        Id = achievement.AchievementID,
                        Type = achievement.Achievement.Type,
                        Title = achievement.Achievement.Title,
                        Description = achievement.Achievement.Description
                            .Replace("{}", achievement.LevelUpGoal.ToString()),
                        CompletedMessage = achievement.Achievement.CompletedMessage,
                        Icon = achievement.Achievement.Icon,
                        GoalBase = achievement.Achievement.GoalBase
                    }
                })
                .ToList()
                .GroupBy(achievement => achievement.UserName)
                .ToDictionary(group => group.Key, group => group.ToList());
        }

        public List<UserAchievementDto> GetUserAchievements(string userId)
        {
            return _context.UserAchievements
                .Include(achievement => achievement.Achievement)
                .Include(achievement => achievement.User)
                .Where(achievement => achievement.UserID.Equals(userId))
                .Select(achievement => new UserAchievementDto
                {
                    Progress = achievement.Progress,
                    Level = achievement.Level,
                    LevelUpGoal = achievement.LevelUpGoal,
                    IsAchieved = achievement.IsAchieved,
                    Achievement = new AchievementDto
                    {
                        Id = achievement.AchievementID,
                        Type = achievement.Achievement.Type,
                        Title = achievement.Achievement.Title,
                        Description = achievement.Achievement.Description
                            .Replace("{}", achievement.LevelUpGoal.ToString()),
                        CompletedMessage = achievement.Achievement.CompletedMessage,
                        Icon = achievement.Achievement.Icon,
                        GoalBase = achievement.Achievement.GoalBase
                    }
                })
                .ToList();
        }
    }
}