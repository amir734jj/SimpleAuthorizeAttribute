using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Tests
{
    public class MockSession: ISession
    {
        private readonly Dictionary<string, string> _dictionary;
        
        public bool IsAvailable => throw new NotImplementedException();

        public string Id => throw new NotImplementedException();
        
        public IEnumerable<string> Keys => throw new NotImplementedException();
        
        public MockSession(Dictionary<string, string> dictionary) => _dictionary = dictionary;

        public Task LoadAsync(CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

        public Task CommitAsync(CancellationToken cancellationToken = new CancellationToken()) => throw new NotImplementedException();

        public bool TryGetValue(string key, out byte[] value)
        {
            value = Encoding.Default.GetBytes(_dictionary[key]);
            
            return true;
        }

        public void Set(string key, byte[] value) => throw new NotImplementedException();

        public void Remove(string key) => throw new NotImplementedException();

        public void Clear() => throw new NotImplementedException();
    }
}