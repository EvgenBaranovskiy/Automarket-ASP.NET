using Automarket.Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace Automarket.DAL.Interfaces
{
    public interface IFileRepository
    {
        public Task<string> SaveCarAvatarAsync(int carId, IFormFile formFile);
        public Task<List<CarImage>> SaveCarImagesAsync(int carId, IFormFileCollection formFiles);
    }
}