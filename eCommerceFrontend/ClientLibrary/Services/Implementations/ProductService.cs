using ClientLibrary.Helper.Interfaces;
using ClientLibrary.Models;
using ClientLibrary.Models.Category;
using ClientLibrary.Models.Products;
using ClientLibrary.Services.Interfaces;
using static ClientLibrary.Helper.Constant;

namespace ClientLibrary.Services.Implementations
{
    public class ProductService(IHttpClientHelper httpClient, IApiCallHelper apiHelper) : IProductService
    {
        public async Task<ServiceResponse> AddAsync(CreateProduct product)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Product.Add,
                Type = ApiCallType.Post,
                Client = client,
                Id = null!,
                Model = product
            };
            var result = await apiHelper.ApiCallTypeCall<CreateProduct>(apiCall);
            return result == null ? apiHelper.ConnectionError() :
                await apiHelper.GetServiceResponse<ServiceResponse>(result);
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Product.Delete,
                Type = ApiCallType.Delete,
                Client = client,
                Model = null!
            };
            apiCall.ToString(id);
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            return result == null ? apiHelper.ConnectionError() :
                await apiHelper.GetServiceResponse<ServiceResponse>(result);
        }

        public async Task<IEnumerable<GetProduct>> GetAllAsync()
        {
            var client = httpClient.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Product.GetAll,
                Type = ApiCallType.Get,
                Client = client,
                Id = null!,
                Model = null!
            };
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            return result.IsSuccessStatusCode
                     ? await apiHelper.GetServiceResponse<IEnumerable<GetProduct>>(result)
                     : [];
        }

        public async Task<GetProduct> GetByIdAsync(Guid id)
        {
            var client = httpClient.GetPublicClient();
            var apiCall = new ApiCall
            {
                Route = Product.Get,
                Type = ApiCallType.Get,
                Client = client,
                Model = null!
            };
            apiCall.ToString(id);
            var result = await apiHelper.ApiCallTypeCall<Dummy>(apiCall);
            return result.IsSuccessStatusCode
                     ? await apiHelper.GetServiceResponse<GetProduct>(result)
                     : null!;
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProduct product)
        {
            var client = await httpClient.GetPrivateClientAsync();
            var apiCall = new ApiCall
            {
                Route = Product.Update,
                Type = ApiCallType.Update,
                Client = client,
                Id = null!,
                Model = product
            };
            var result = await apiHelper.ApiCallTypeCall<UpdateProduct>(apiCall);
            return result == null ? apiHelper.ConnectionError() :
                await apiHelper.GetServiceResponse<ServiceResponse>(result);
        }
    }
}
