using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace Automarket.DAL.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly string WWWROOT_DIR;
        private readonly string PATH_CAR_AVATARS;
        private readonly string PATH_CAR_IMAGES;

        public FileRepository(string wwwroot_dir)
        {
            WWWROOT_DIR = wwwroot_dir;
            PATH_CAR_AVATARS = "img\\CarPreviewAvatars\\";
            PATH_CAR_IMAGES = "img\\CarImages\\";
        }

        public async Task<string> SaveCarAvatarAsync(int carId, IFormFile formFile)
        {
            if (formFile.ContentType == "image/jpeg" || formFile.ContentType == "image/png")
            {
                string relativePathToSave = Path.Combine(PATH_CAR_AVATARS, carId.ToString() + "_avatar" + Path.GetExtension(formFile.FileName));
                string fullPathToSave = Path.Combine(WWWROOT_DIR, relativePathToSave);
                
                using (var fs = new FileStream(fullPathToSave, FileMode.Create)) {
                    await formFile.CopyToAsync(fs);
                }

                return "\\" + relativePathToSave;
            }
            else
            {
                throw new Exception("Wrong image format!");
            }
        }

        public async Task<List<CarImage>> SaveCarImagesAsync(int carId, IFormFileCollection formFiles)
        {
            List<CarImage> carImages = new List<CarImage>();
            var imagesDIR = Directory.CreateDirectory(Path.Combine(WWWROOT_DIR, PATH_CAR_IMAGES, carId.ToString()));
            int fileNumber = 1;
            
            foreach (IFormFile formFile in formFiles)
            {
                string pathToSave = Path.Combine(imagesDIR.FullName, fileNumber.ToString() + Path.GetExtension(formFile.FileName));
                if (formFile.ContentType == "image/jpeg" || formFile.ContentType == "image/png")
                {
                    using (var fs = new FileStream(pathToSave, FileMode.Create))
                    {
                        await formFile.CopyToAsync(fs);
                    }
                    
                    carImages.Add(new CarImage { CarId = carId, ImgPath = ("\\" + Path.Combine(PATH_CAR_IMAGES, carId.ToString(), fileNumber.ToString()) + Path.GetExtension(formFile.FileName)).Replace('\\', '/') });
                    fileNumber++;
                }
                else
                {
                    throw new Exception("Wrong image format!");
                }
            }

            return carImages;
        }
    }
}