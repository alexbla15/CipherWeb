﻿namespace CipherData.RandomMode
{
    public class RandomCategoriesRequests : ICategoriesRequests
    {
        public async Task<Tuple<List<ICategory>, ErrorResponse>> GetAll() => 
            await new RandomGenericRequests().Request(RandomData.Categories, canBadRequest: false);

        public async Task<Tuple<ICategory, ErrorResponse>> GetById(string? id) => 
            await new RandomGenericRequests().Request(RandomData.Category, canBadRequest: false, canBeNotFound: true);

        public async Task<Tuple<ICategory, ErrorResponse>> Create(ICategoryRequest cat) => 
            await new RandomGenericRequests().Request(RandomData.Category);

        public async Task<Tuple<ICategory, ErrorResponse>> Update(string? id, ICategoryRequest cat) => 
            await new RandomGenericRequests().Request((ICategory)RandomFuncs.Request(cat, id), canBeNotFound: true);
    }
}
