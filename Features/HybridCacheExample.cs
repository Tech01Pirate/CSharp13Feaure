using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;
using System.Text.Json;

namespace Csharp13Net.Features
{
    internal class HybridCacheExample : IDoWork
    {
        public void DoWork()
        {
            // Create dummy dependencies (replace with actual DI in real app)
            var httpClientFactory = new HttpClientFactoryStub();
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var distributedCache = new DistributedCacheStub();
            //HybridCache hybridCache = new HybridCache(memoryCache, distributedCache);
            HybridCache hybridCache = new DefaultHybridCache(memoryCache, distributedCache);

            var postsService = new PostsService(httpClientFactory, memoryCache, distributedCache, hybridCache);
        }
    }
    public class DefaultHybridCache : HybridCache
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;

        internal DefaultHybridCache(MemoryCache memoryCache, IDistributedCache distributedCache)
        {
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        public override async ValueTask<T> GetOrCreateAsync<TState, T>(string key, TState state, Func<TState, CancellationToken, ValueTask<T>> factory, HybridCacheEntryOptions? options = null, IEnumerable<string>? tags = null, CancellationToken cancellationToken = default)
        {
            // Example implementation: Use distributed cache as fallback
            var cachedValue = await _distributedCache.GetAsync(key, cancellationToken);
            if (cachedValue != null)
            {
                return JsonSerializer.Deserialize<T>(cachedValue)!;
            }

            var value = await factory(state, cancellationToken);
            var serializedValue = JsonSerializer.SerializeToUtf8Bytes(value);
            await _distributedCache.SetAsync(key, serializedValue, new DistributedCacheEntryOptions(), cancellationToken);
            return value;
        }

        public override async ValueTask SetAsync<T>(string key, T value, HybridCacheEntryOptions? options = null, IEnumerable<string>? tags = null, CancellationToken cancellationToken = default)
        {
            var serializedValue = JsonSerializer.SerializeToUtf8Bytes(value);
            await _distributedCache.SetAsync(key, serializedValue, new DistributedCacheEntryOptions(), cancellationToken);
        }

        public override async ValueTask RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);
        }

        public override async ValueTask RemoveByTagAsync(string tag, CancellationToken cancellationToken = default)
        {
            // Example implementation: No-op as tags are not supported in this stub
            await Task.CompletedTask;
        }
    }
    public class HttpClientFactoryStub : IHttpClientFactory
    {
        public HttpClient CreateClient(string name = null) => new HttpClient();
    }
    public record Post(int UserId, int Id, string Title, string Body);
    public class PostsService(
    IHttpClientFactory httpClientFactory,
    IMemoryCache memoryCache,
    IDistributedCache distributedCache,
    HybridCache hybridCache)
    {
        public async Task<List<Post>?> GetUserPostsAsync(string userId)
        {
            var cacheKey = $"posts_{userId}";

            // Before (Memory Chache)
            var posts = await memoryCache.GetOrCreateAsync(cacheKey,
                async _ => await GetPostsAsync(userId));

            // Before (Distributed Chache)
            var postsJson = await distributedCache.GetStringAsync(cacheKey);
            if (postsJson is null)
            {
                posts = await GetPostsAsync(userId);
                await distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(posts));
            }
            else
            {
                posts = JsonSerializer.Deserialize<List<Post>>(postsJson);
            }

            // .NET 9 Hybrid Cache
            posts = await hybridCache.GetOrCreateAsync(cacheKey,
                async _ => await GetPostsAsync(userId), new HybridCacheEntryOptions()
                {
                    Flags = HybridCacheEntryFlags.DisableLocalCache | // Act as distributed cache
                            HybridCacheEntryFlags.DisableDistributedCache // Act as local cache
                });

            return posts;
        }

        private async Task<List<Post>?> GetPostsAsync(string userId)
        {
            Console.WriteLine("===========Fetching posts from API");
            var url = $"https://jsonplaceholder.typicode.com/posts?userId={userId}";
            var client = httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Post>>();
        }
    }
    public class DistributedCacheStub : IDistributedCache
    {
        public byte[]? Get(string key) => null;
        public Task<byte[]?> GetAsync(string key, CancellationToken token = default) => Task.FromResult<byte[]?>(null);
        public void Refresh(string key) { }
        public Task RefreshAsync(string key, CancellationToken token = default) => Task.CompletedTask;
        public void Remove(string key) { }
        public Task RemoveAsync(string key, CancellationToken token = default) => Task.CompletedTask;
        public void Set(string key, byte[] value, DistributedCacheEntryOptions options) { }
        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default) => Task.CompletedTask;
    }

}
