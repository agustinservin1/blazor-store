﻿using ClientLibrary.Helper.Interfaces;
using ClientLibrary.Models;
using ClientLibrary.Models.Category;
using ClientLibrary.Models.Products;
using ClientLibrary.Services.Interfaces;
using static ClientLibrary.Helper.Constant;
namespace ClientLibrary.Services.Implementations
{
    public class CategoryService(IHttpClientHelper httpClient, IApiCallHelper apiHelper) : ICategoryService
    {
        //private
        public async Task<ServiceResponse> AddAsync(CreateCategory category)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Category.Add,
                Type = ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = category
            };
            var result = await apiHelper.ApiCallTypeCall<CreateCategory>(apiCall);
            return result == null ? apiHelper.ConnectionError() :
                await apiHelper.GetServiceResponse<ServiceResponse>(result);
        }
        //private
        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Category.Delete,
                Type = ApiCallType.Delete,
                Client = client,
                Model = null!
            };
            apiCall.ToString(id);
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            return result == null ? apiHelper.ConnectionError() :
                await apiHelper.GetServiceResponse<ServiceResponse>(result);
        }
        //public
        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var client = httpClient.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Category.GetAll,
                Type = ApiCallType.Get,
                Client = client,
                Id = null!,
                Model = null!
            };
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            return result.IsSuccessStatusCode
                    ? await apiHelper.GetServiceResponse<IEnumerable<GetCategory>>(result)
                    : [];
        }
        //public

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var client = httpClient.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Category.Get,
                Type = ApiCallType.Get,
                Client = client,
                Model = null!
            };
            apiCall.ToString(id);
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            return result.IsSuccessStatusCode
                    ? await apiHelper.GetServiceResponse<GetCategory>(result)
                    : null!;
        }


        //private
        public async Task<ServiceResponse> UpdateAsync(UpdateCategory category)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Category.Update,
                Type = ApiCallType.Update,
                Client = client,
                Id = null!,
                Model = category
            };
            var result = await apiHelper.ApiCallTypeCall<UpdateCategory>(apiCall);
            return result == null ? apiHelper.ConnectionError() :
                await apiHelper.GetServiceResponse<ServiceResponse>(result);
        }
        //public
        public async Task<IEnumerable<GetProduct>> GetProductsByCategory(Guid categoryId)
        {
            var client = httpClient.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Category.GetProductsByCategory,
                Type = ApiCallType.Get,
                Client = client,
                Model = null!
            };
            apiCall.ToString(categoryId);
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            return result.IsSuccessStatusCode
                    ? await apiHelper.GetServiceResponse<IEnumerable<GetProduct>>(result)
                    : [];

        }
    }
}
