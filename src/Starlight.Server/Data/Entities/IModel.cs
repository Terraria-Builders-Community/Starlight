using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Starlight
{
    public interface IModel
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }

        public ModelState State { get; set; }

        public static async Task<T> CreateAsync<T>(Action<T> action)
            where T : IModel, new()
            => await ModelHelper<T>.CreateAsync(action);

        public static async Task<T?> GetAsync<T>(Expression<Func<T, bool>> func, bool createOnFailedFetch = false)
            where T : IModel, new()
            => await ModelHelper<T>.GetAsync(func, createOnFailedFetch);

        public static async Task<bool> ModifyAsync<T>(T model, UpdateDefinition<T> updateDefinition)
            where T : IModel, new()
            => await ModelHelper<T>.ModifyAsync(model, updateDefinition);

        public static async Task<bool> DeleteAsync<T>(T model)
            where T : IModel, new()
            => await ModelHelper<T>.DeleteAsync(model);
    }
}
