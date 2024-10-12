namespace CipherData.ApiMode
{
    public class CategoriesRequests : ICategoriesRequests
    {
        private static readonly string path = "/categories";

        public async Task<Tuple<List<ICategory>, ErrorResponse>> GetCategories()
        {
            var result = await GeneralAPIRequest.Get<List<Category>>(path);

            List<ICategory> objs = result.Item1?.Select(x => x as ICategory).ToList() ?? new();
            return Tuple.Create(objs, result.Item2);
        }

        public async Task<Tuple<ICategory, ErrorResponse>> GetCategory(string id)
        {
            var result = await GeneralAPIRequest.Get<Category>($"{path}/{id}");

            ICategory obj = result.Item1 ?? new Category();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<ICategory, ErrorResponse>> CreateCategory(ICategoryRequest cat)
        {
            var result = await GeneralAPIRequest.Post<Category>(path, cat);

            ICategory obj = result.Item1 ?? new Category();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<ICategory, ErrorResponse>> UpdateCategory(string id, ICategoryRequest cat)
        {
            var result = await GeneralAPIRequest.Put<Category>($"{path}/{id}", cat);

            ICategory obj = result.Item1 ?? new Category();
            return Tuple.Create(obj, result.Item2);
        }
    }
}
