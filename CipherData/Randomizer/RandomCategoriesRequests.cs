using CipherData.Models;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{
    public class RandomCategoriesRequests : ICategoriesRequests
    {
        public Tuple<List<Category>, ErrorResponse> GetCategories()
        {
            return new RandomGenericRequests().Request(RandomData.RandomCategories, canBadRequest: false);
        }

        public Tuple<Category, ErrorResponse> GetCategory(string id)
        {
            return new RandomGenericRequests().Request(RandomData.RandomCategory, canBadRequest: false, canBeNotFound: true);
        }

        public Tuple<Category, ErrorResponse> CreateCategory(CategoryRequest cat)
        {
            return new RandomGenericRequests().Request(RandomData.RandomCategory);
        }

        public Tuple<Category, ErrorResponse> UpdateCategory(string id, CategoryRequest cat)
        {
            return new RandomGenericRequests().Request(cat.Create(id), canBeNotFound: true);
        }
    }
}
