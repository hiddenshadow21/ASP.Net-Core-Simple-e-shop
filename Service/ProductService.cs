using AspNetCore.Identity.MongoDbCore.Infrastructure;
using MongoDB.Driver;
using MVCApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCApp.Service
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _product;

        public ProductService(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _product = database.GetCollection<Product>("Products");
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _product.Find(product => true).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _product.Find(product => product.Id == id).FirstOrDefaultAsync<Product>();
        }

        public async Task<bool> CreateAsync(Product product)
        {
            if (product == null)
                return false;
            try
            {
                await _product.InsertOneAsync(product);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateAsync(string id, Product product)
        {
            if (product == null)
                return false;
            try
            {
                await _product.ReplaceOneAsync(x => x.Id == id, product);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RemoveAsync(Product product)
        {
            if (product == null)
                return false;
            try
            {
                await _product.DeleteOneAsync(x => x.Id == product.Id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
