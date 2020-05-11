using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MultipleRanker.Definitions.Snapshots;
using MultipleRanker.Infrastructure.Repositories.Mongo;
using MultipleRanker.Infrastructure.Repositories.Mongo.Entities;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Infrastructure.Repositories
{
    public class MongoDbRatingListSnapshotRepository : IListSnapshotRepository
    {
        private readonly IMongoCollection<RatingListSnapshotEntity> _ratingCollection;

        public MongoDbRatingListSnapshotRepository()
        {
            var client = new MongoClient(
                "mongodb://briandelaney:.$RC2SpD2Zsh!eM@ds016148.mlab.com:16148/multipleranker?retryWrites=false"
            );
            
            var database = client.GetDatabase("multipleranker");

            _ratingCollection = database.GetCollection<RatingListSnapshotEntity>("ratingtables");
        }

        public async Task<RatingListSnapshot> Get(Guid ratingListId)
        {
            try
            {
                var ratingLists = await _ratingCollection
                    .FindAsync(x => x.Id == ratingListId.ToString());

                return ratingLists
                    .Single()
                    .ToRatingListSnapshot();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Set(RatingListSnapshot ratingListSnapshot)
        {
            var ratingListSnapshotEntity = ratingListSnapshot.ToRatingListEntity();

            await _ratingCollection.ReplaceOneAsync(
                x => x.Id == ratingListSnapshot.Id.ToString(),
                ratingListSnapshot.ToRatingListEntity(),
                new ReplaceOptions { IsUpsert = true });
        }
    }
}
