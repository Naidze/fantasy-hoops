﻿using System;
using System.Linq;

namespace fantasy_hoops.Repositories
{
    public interface IPlayerRepository
    {

        IQueryable<Object> GetActivePlayers();
        IQueryable<Object> GetPlayer(int id);

    }
}
