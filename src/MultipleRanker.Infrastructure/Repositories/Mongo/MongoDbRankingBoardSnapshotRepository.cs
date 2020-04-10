using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MultipleRanker.Definitions.Snapshots;
using MultipleRanker.Infrastructure.Repositories.Mongo;
using MultipleRanker.Infrastructure.Repositories.Mongo.Entities;
using MultipleRanker.Interfaces;

namespace MultipleRanker.Infrastructure.Repositories
{
    public class MongoDbRankingBoardSnapshotRepository : IRankingBoardSnapshotRepository
    {
        private readonly IMongoCollection<RankingBoardSnapshotEntity> _rankingCollection;

        public MongoDbRankingBoardSnapshotRepository()
        {
            var client = new MongoClient(
                "mongodb://briandelaney:.$RC2SpD2Zsh!eM@ds016148.mlab.com:16148/multipleranker?retryWrites=false"
            );
            
            var database = client.GetDatabase("multipleranker");

            _rankingCollection = database.GetCollection<RankingBoardSnapshotEntity>("rankingtables");
        }

        public async Task<RankingBoardSnapshot> Get(Guid rankingBoardId)
        {
            try
            {
                var rankingBoards = await _rankingCollection
                    .FindAsync(x => x.Id == rankingBoardId);

                return rankingBoards
                    .Single()
                    .ToRankingBoardSnapshot();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Set(RankingBoardSnapshot rankingBoardSnapshot)
        {
            await _rankingCollection.ReplaceOneAsync(
                x => x.Id == rankingBoardSnapshot.Id,
                rankingBoardSnapshot.ToRankingBoardEntity(), 
                new ReplaceOptions {IsUpsert = true});
        }
    }
}
