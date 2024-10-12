namespace CipherData.Interfaces
{
    public interface IGenericRequests
    {
        /// <summary>
        /// General request from the API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="successResult">Result in case request was successful</param>
        /// <param name="canBadRequest">is bad request an optionional result</param>
        /// <param name="canBeNotFound">is not found an optional result</param>
        /// <param name="canFail">only for testing. in real api, of course it may fail</param>
        /// <returns></returns>
        Task<Tuple<T, ErrorResponse>> Request<T>(T successResult, bool canBadRequest = true, bool canBeNotFound = false, bool canFail = false);
    }
}
