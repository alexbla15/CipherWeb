using CipherData.Models;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{
    public class RandomCategoriesRequests : ICategoriesRequests
    {
        public Tuple<List<ICategory>, ErrorResponse> GetCategories() => new RandomGenericRequests().Request(RandomData.Categories, canBadRequest: false);

        public Tuple<ICategory, ErrorResponse> GetCategory(string id) => new RandomGenericRequests().Request(RandomData.Category, canBadRequest: false, canBeNotFound: true);

        public Tuple<ICategory, ErrorResponse> CreateCategory(ICategoryRequest cat) => new RandomGenericRequests().Request(RandomData.Category);

        public Tuple<ICategory, ErrorResponse> UpdateCategory(string id, ICategoryRequest cat) => new RandomGenericRequests().Request((ICategory)RandomFuncs.Request(cat,id), canBeNotFound: true);
    }
}
