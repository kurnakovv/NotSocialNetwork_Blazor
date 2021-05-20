using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.Entities;
using System;
using System.Linq;

namespace NotSocialNetwork.DBContexts
{
    public static class DefaultImagesInit
    {
        private static ImageEntity _imageEntity = new ImageEntity()
        {
            Title = DefaultImageConfig.DEFAULT_IMAGE_PATH,
        };

        public static void AddTestImage(AppDbContext appDbContext)
        {
            var imageCount = appDbContext.Images.Where(i => i.Title.Contains(_imageEntity.Title)).Count();

            if (imageCount == 0)
            {
                _imageEntity.Id = DefaultImageConfig.DEFAULT_IMAGE_ID;

                appDbContext.Images.Add(_imageEntity);
                appDbContext.SaveChanges();
            }
        }
    }
}
