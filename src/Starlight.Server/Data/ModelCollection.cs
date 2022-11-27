using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Starlight
{
    public class ModelCollection<T>
        where T : IModel
    {
        private readonly MongoCollectionBase<T> _collection;

        /// <summary>
        ///     Creates or finds a collection from passed <paramref name="collection"/>.
        /// </summary>
        /// <param name="collection"></param>
        /// <exception cref="ArgumentNullException">Thrown when the manager cannot succesfully create a collection for provided document name.</exception>
        public ModelCollection(string collection)
        {
            if (DatabaseProvider.IsConnected)
                _collection = DatabaseProvider.GetCollection<T>(collection);
            else throw new ArgumentNullException(nameof(collection));
        }

        /// <summary>
        ///     Inserts a document from the passed <paramref name="document"/>.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task InsertDocumentAsync(T document)
            => await _collection.InsertOneAsync(document);

        /// <summary>
        ///     Inserts a collection of documents from the passed <paramref name="documents"/>.
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        public async Task InsertDocumentsAsync(IEnumerable<T> documents)
            => await _collection.InsertManyAsync(documents);

        /// <summary>
        ///     Updates an existing entity to the new instance if found. Otherwise creates a new entity.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task InsertOrUpdateDocumentAsync(T document)
        {
            if (document.ObjectId == ObjectId.Empty)
                await _collection.InsertOneAsync(document);
            else
                await _collection.ReplaceOneAsync(x => x.ObjectId == document.ObjectId, document);
        }

        /// <summary>
        ///     Updates a document.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<bool> UpdateDocumentAsync(T document)
        {
            var entity = await (await _collection.FindAsync(x => x.ObjectId == document.ObjectId))
                .FirstOrDefaultAsync();

            if (entity is not null)
            {
                await _collection.ReplaceOneAsync(x => x.ObjectId == document.ObjectId, document);
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Modifies an existing document in atomic declaration.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<bool> ModifyDocumentAsync(T document, UpdateDefinition<T> update)
            => (await _collection.UpdateOneAsync(x => x.ObjectId == document.ObjectId, update)).IsAcknowledged;

        /// <summary>
        ///     Gets the first document from a collection.
        /// </summary>
        /// <returns></returns>
        public async Task<T> GetFirstDocumentAsync()
            => await (await _collection.FindAsync(new BsonDocument())).FirstOrDefaultAsync();

        /// <summary>
        ///     Gets all documents from a collection.
        /// </summary>
        /// <returns></returns>
        public async IAsyncEnumerable<T> GetAllDocumentsAsync()
        {
            var collection = await _collection.FindAsync(new BsonDocument());

            foreach (var entity in collection.ToEnumerable())
            {
                yield return entity;
            }
        }

        /// <summary>
        ///     Deletes a document from provided <paramref name="document"/>
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<bool> DeleteDocumentAsync(T document)
            => (await _collection.DeleteOneAsync(x => x.ObjectId == document.ObjectId)).IsAcknowledged;

        /// <summary>
        ///     Deletes a set of document matching the provided <paramref name="filter"/>.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<bool> DeleteManyDocumentsAsync(Expression<Func<T, bool>> filter)
            => (await _collection.DeleteManyAsync<T>(filter)).IsAcknowledged;

        /// <summary>
        ///     Finds the first occurence in a range of documents returned by the <paramref name="filter"/>.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<T> FindDocumentAsync(Expression<Func<T, bool>> filter)
            => await (await _collection.FindAsync(filter)).FirstOrDefaultAsync();

        /// <summary>
        ///     Returns all found documents matching <paramref name="filter"/>.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<T> FindManyDocumentsAsync(Expression<Func<T, bool>> filter)
        {
            var collection = await _collection.FindAsync(filter);

            foreach (var entity in collection.ToEnumerable())
            {
                yield return entity;
            }
        }
    }
}
