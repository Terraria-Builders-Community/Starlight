using MongoDB.Driver;
using System.Linq.Expressions;

namespace Starlight
{
    public static class ModelExtensions
    {
        public static async Task<bool> ModifyAsync<T, TField>(this T? entity, Expression<Func<T, TField>> expression, TField value)
            where T : IModel, new()
            => entity is not null && await IModel.ModifyAsync(entity, Builders<T>.Update.Set(expression, value));

        public static async Task<bool> DeleteAsync<T>(this T? entity)
            where T : IModel, new()
            => entity is not null && await IModel.DeleteAsync(entity);
    }
}
