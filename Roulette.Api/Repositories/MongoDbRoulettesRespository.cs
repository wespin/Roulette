using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Roulette.Api.Entities;

namespace Roulette.Api.Repositories
{
    public class MongoDbRolulettesRepository : IIRoulettesRepository
    {
        private const string databaseName = "rouletteWheel";
        private const string collectionName = "rouletteWheels";
        private readonly IMongoCollection<RouletteWheel> rouletteWheelsCollection;
        private readonly FilterDefinitionBuilder<RouletteWheel> filterBuilder = Builders<RouletteWheel>.Filter;
        public MongoDbRolulettesRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            rouletteWheelsCollection = database.GetCollection<RouletteWheel>(collectionName);
        }

        public async Task CreateRouletteWheelAsync(RouletteWheel rouletteWheel)
        {
            await rouletteWheelsCollection.InsertOneAsync(rouletteWheel);
        }

        public async Task<IEnumerable<RouletteWheel>> GetRouletteWheelsAsync()
        {
            return await rouletteWheelsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<RouletteWheel> GetRouletteWheelAsync(Guid id)
        {
            var filter = filterBuilder.Eq(roulettewheel => roulettewheel.Id, id);
            return await rouletteWheelsCollection.Find(filter).SingleOrDefaultAsync();
        }
        public async Task OpenRouletteWheelAsync(RouletteWheel rouletteWheel)
        {
            var filter = filterBuilder.Eq(existingroulettewheel => existingroulettewheel.Id, rouletteWheel.Id);
            await rouletteWheelsCollection.ReplaceOneAsync(filter, rouletteWheel);
        }
        public async Task CloseRouletteWheelAsync(RouletteWheel rouletteWheel)
        {
            var filter = filterBuilder.Eq(existingroulettewheel => existingroulettewheel.Id, rouletteWheel.Id);
            Random randObj = new Random();
            int randNumber = randObj.Next(36);
            string winColor = "black";
            if(randNumber%2 == 0){
               winColor = "red";
            }
             RouletteWheel updatedItem = rouletteWheel with {
                WinNumber = randNumber,
                WinColor = winColor
            };

            await rouletteWheelsCollection.ReplaceOneAsync(filter, updatedItem);
        }


        public async Task CreateBetAsync(Guid id, Bet bet)
        {
            await rouletteWheelsCollection.FindOneAndUpdateAsync(x => x.Id == id, Builders<RouletteWheel>.Update.AddToSet(x => x.Bets, bet));
        }

    //    public async Task<Bet> GetBetAsync(Guid id)
      //  {
            // var filter = filterBuilder.Eq(existingroulettewheel => existingroulettewheel.Id, id);
            // 

    //    }



        // public IEnumerable<Bet> GetBetsAsync(Guid rouletteWheelid)
        // {
        //     throw new NotImplementedException();
        // }





    }
}