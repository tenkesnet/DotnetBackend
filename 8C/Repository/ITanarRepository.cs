﻿using Tanulok.Model;

namespace Tanulok.Repository
{
    public interface ITanarRepository
    {
        public Task<IEnumerable<Tanar>> GetTanarok();
    }
}
