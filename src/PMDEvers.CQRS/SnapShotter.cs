using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

using PMDEvers.CQRS.Interfaces;

namespace PMDEvers.CQRS
{
    public class SnapShotter<T> : ISnapshotStore<T>
        where T : AggregateRoot
    {
        private readonly List<T> _requestCache = new List<T>();
        private readonly Dictionary<Guid, string> _store = new Dictionary<Guid, string>();

        public void TakeSnapshot(T aggregate)
        {
            var cached = _requestCache.FirstOrDefault(x => x.Id == aggregate.Id);
            if (cached != null)
            {
                _requestCache.Remove(cached);

            }
            _requestCache.Add(aggregate);

            _store[aggregate.Id] = JsonConvert.SerializeObject(aggregate);
        }

        public T GetSnapshot(Guid id) 
        {
            //var aggregate = _requestCache.FirstOrDefault(x => x.Id == id);
            //if (aggregate != null)
            //{
            //    return aggregate;
            //}

            var aggregate =  _store.ContainsKey(id) ? JsonConvert.DeserializeObject<T>(_store[id]) : Activator.CreateInstance<T>();
            
            return aggregate;
        }
    }
}
