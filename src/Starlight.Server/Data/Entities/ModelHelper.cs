using MongoDB.Driver;
using System.Linq.Expressions;

namespace Starlight
{
    internal static class ModelHelper<T>
        where T : IModel, new()
    {
        public static readonly ModelCollection<T> Collection = new(typeof(T).Name + "s");

        public static async Task<bool> ModifyAsync(T model, UpdateDefinition<T> updateDefinition)
        {
            if (model.State is ModelState.Stateless or ModelState.Deleted or ModelState.Deserializing)
                return false;

            return await Collection.ModifyDocumentAsync(model, updateDefinition);
        }

        public static async Task<bool> DeleteAsync(T model)
        {
            if (model.State is ModelState.Stateless or ModelState.Deleted)
                return false;

            model.State = ModelState.Deleted;

            return await Collection.DeleteDocumentAsync(model);
        }

        public static async Task<T?> GetAsync(Expression<Func<T, bool>> func, bool createOnFailedFetch)
        {
            var value = await Collection.FindDocumentAsync(func);

            if (value is null)
            {
                if (createOnFailedFetch)
                    value = await CreateAsync((x) => { });

                else
                    return default;
            }

            value.State = ModelState.Ready;

            return value;
        }

        public static async Task<T> CreateAsync(Action<T> action)
        {
            var value = new T();

            action(value);

            await Collection.InsertOrUpdateDocumentAsync(value);

            value.State = ModelState.Ready;

            return value;
        }
    }
}
