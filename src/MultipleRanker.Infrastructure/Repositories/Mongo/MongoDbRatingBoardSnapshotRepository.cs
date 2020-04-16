using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MultipleRanker.Definitions.Snapshots;
using MultipleRanker.Infrastructure.Repositories.Mongo;
using MultipleRanker.Infrastructure.Repositories.Mongo.Entities;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Infrastructure.Repositories
{
    public class MongoDbRatingBoardSnapshotRepository : IRatingBoardSnapshotRepository
    {
        private readonly IMongoCollection<RatingBoardSnapshotEntity> _ratingCollection;

        public MongoDbRatingBoardSnapshotRepository()
        {
            var client = new MongoClient(
                "mongodb://briandelaney:.$RC2SpD2Zsh!eM@ds016148.mlab.com:16148/multipleranker?retryWrites=false"
            );
            
            var database = client.GetDatabase("multipleranker");

            _ratingCollection = database.GetCollection<RatingBoardSnapshotEntity>("ratingtables");
        }

        public async Task<RatingBoardSnapshot> Get(Guid ratingBoardId)
        {
            try
            {
                var ratingBoards = await _ratingCollection
                    .FindAsync(x => x.Id == ratingBoardId);

                return ratingBoards
                    .Single()
                    .ToRatingBoardSnapshot();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Set(RatingBoardSnapshot ratingBoardSnapshot)
        {
            await _ratingCollection.ReplaceOneAsync(
                x => x.Id == ratingBoardSnapshot.Id,
                ratingBoardSnapshot.ToRatingBoardEntity(), 
                new ReplaceOptions {IsUpsert = true});
        }
    }
}
