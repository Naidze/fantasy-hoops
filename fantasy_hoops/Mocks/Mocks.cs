using System;
using System.Collections.Generic;
using fantasy_hoops.Models;
using fantasy_hoops.Models.Achievements;
using fantasy_hoops.Models.Enums;

namespace fantasy_hoops
{
    public static class Mocks
    {
        public static class Tournaments
        {
            public static readonly List<DateTime> StartDates = new List<DateTime>
            {
                new DateTime(2020, 04, 20, 19, 0, 0),
                new DateTime(2020, 04, 27, 19, 0, 0),
                new DateTime(2020, 05, 04, 19, 0, 0),
                new DateTime(2020, 05, 11, 19, 0, 0),
                new DateTime(2020, 05, 18, 19, 0, 0),
                new DateTime(2020, 05, 25, 19, 0, 0),
                new DateTime(2021, 04, 02, 19, 0, 0)
            };

            public static readonly DateTime MockedStartDate = new DateTime(2021, 04, 02, 19, 0, 0);
        }

        public static class Players
        {
            public static readonly List<int> PlayerPool = new List<int>
            {
                1385, 1946, 1307, 1304, 1297, 1483, 1915, 1913, 1302, 1353, 1301, 1914, 1306, 1639, 1388, 1918, 1544,
                1390, 1485, 1810, 1378, 1377, 1386, 1948, 1308, 1249, 1452, 1449, 1453, 1870, 1505, 1282, 1871, 1455,
                1719, 1536, 1263, 1240, 1251, 1389, 1257, 1933, 1264, 1348, 1586, 1492, 1324, 1444, 1869, 1922, 1374,
                1318, 1334, 1337, 1332, 1338, 1421, 1328, 1329, 1878, 1331, 1591, 1920, 1394, 1407, 1343, 1580, 1281,
                1404, 1560, 1945, 1944, 1721, 1330, 1743, 1432, 1427, 1433, 1425, 1771, 1429, 1428, 1424, 1615, 1437,
                1847, 1285, 1447, 1290, 1293, 1287, 1565, 1265, 1361, 1563, 1705, 1436, 1258, 1571, 1506, 1821, 1271,
                1273, 1572, 1402, 1267, 1266, 1275, 1380, 1466, 1898, 1901, 1820, 1899, 1715, 1519, 1517, 1669, 1671,
                1456, 1675, 1668, 1670, 1691, 1662, 1423, 1674, 1672, 1514, 1289, 1604, 1600, 1498, 1603, 1925, 1500,
                1238, 1683, 1924, 1632, 1607, 1495, 1496, 1488, 1687, 1295, 1497, 1494, 1490, 1272, 1891, 1879, 1319,
                1605, 1387, 1905, 1684, 1692, 1794, 1516, 1602, 1502, 1597, 1535, 1904, 1906, 1957, 1228, 1234, 1553,
                1482, 1558, 1554, 1551, 1557, 1349, 1552, 1887, 1690, 1472
            };
            
            public static readonly List<Player> MockedPlayers = new List<Player>
            {
                new Player
                {
                    PlayerID = 123
                },
                new Player
                {
                    PlayerID = 456
                }
            };
        }

        public static class Users
        {
            public static readonly List<User> MockedUsers = new List<User>
            {
                new User
                {
                    Id = "xxx",
                    UserName = "xUser",
                    Email = "xxx@test.com"
                },
                new User
                {
                    Id = "yyy",
                    UserName = "yUser",
                    Email = "yyy@test.com"
                }
            };
        }
        
        public static class Achievements
        {
            public static readonly List<Achievement> MockedAchievements = new List<Achievement>
            {
                new Achievement
                {
                    Id = 123,
                    Type = AchievementType.SINGLE_LEVEL,
                    Title = "Achievement 1"
                },
                new Achievement
                {
                    Id = 456,
                    Type = AchievementType.MULTI_LEVEL,
                    Title = "Achievement 2"
                }
            };
            
            public static readonly List<UserAchievement> MockedUserAchievements = new List<UserAchievement>
            {
                new UserAchievement
                {
                    Progress = 100,
                    Level = 1,
                    LevelUpGoal = 100,
                    UserID = Users.MockedUsers[0].Id,
                    User = Users.MockedUsers[0],
                    AchievementID = MockedAchievements[0].Id,
                    Achievement = MockedAchievements[0]
                },
                new UserAchievement
                {
                    Progress = 200,
                    Level = 2,
                    LevelUpGoal = 100,
                    UserID = Users.MockedUsers[1].Id,
                    User = Users.MockedUsers[1],
                    AchievementID = MockedAchievements[1].Id,
                    Achievement = MockedAchievements[1]
                }
            };
            
        }
    }
}