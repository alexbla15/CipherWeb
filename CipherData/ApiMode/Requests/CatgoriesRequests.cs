namespace CipherData.ApiMode
{
    public class CategoriesRequests : ICategoriesRequests
    {
        private static readonly string path = "/categories";

        public async Task<Tuple<List<ICategory>, ErrorResponse>> GetAll()
            => await GeneralAPIRequest.GetAll<ICategory, Category>(path);

        public async Task<Tuple<ICategory, ErrorResponse>> GetById(string? id)
        {
            if (string.IsNullOrEmpty(id)) return new(new Category(), ErrorResponse.BadRequest);
            return await GeneralAPIRequest.GetId<ICategory, Category>(path, id);
        }

        public async Task<Tuple<ICategory, ErrorResponse>> Create(ICategoryRequest cat)
        {
            var result = await GeneralAPIRequest.Post<Category>(path, cat);

            ICategory obj = result.Item1 ?? new Category();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<ICategory, ErrorResponse>> Update(string? id, ICategoryRequest cat)
        {
            var result = await GeneralAPIRequest.Put<Category>($"{path}/{id}", cat);

            ICategory obj = result.Item1 ?? new Category();
            return Tuple.Create(obj, result.Item2);
        }
    }
}
