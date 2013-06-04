﻿using Usergrid.Sdk.Model;

namespace Usergrid.Sdk.Manager
{
    public interface IEntityManager
    {
        void CreateEntity<T>(string collection, T entity = null) where T : class;
        void DeleteEntity(string collection, string identifer /*name or uuid*/);
        void UpdateEntity<T>(string collection, string identifer /*name or uuid*/, T entity);
        UsergridEntity<T> GetEntity<T>(string collectionName, string identifer /*name or uuid*/);
		UsergridCollection<UsergridEntity<T>> GetEntities<T> (string collectionName, int limit);
		UsergridCollection<UsergridEntity<T>> GetNextEntities<T>(string collectionName);
		UsergridCollection<UsergridEntity<T>> GetPreviousEntities<T>(string collectionName);
    }
}