using NotSocialNetwork.Application.Configs;
using NotSocialNetwork.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotSocialNetwork.DBContexts
{
    public static class TestDataInit
    {
        private static readonly ImageEntity _imageEntity1 = new ImageEntity() { Title = "https://images.pexels.com/photos/1193743/pexels-photo-1193743.jpeg" };
        private static readonly ImageEntity _imageEntity2 = new ImageEntity() { Title = "https://img.freepik.com/free-vector/multicolored-abstract-background_23-2148463672.jpg" };
        private static readonly ImageEntity _imageEntity3 = new ImageEntity() { Title = "https://images.ctfassets.net/hrltx12pl8hq/37lqQySBsACiSCc4i4oCue/4149c19142eee853780c0d650566227f/shutterstock_574261726_thumb.jpg" };

        private static readonly UserEntity _userEntity1 = new UserEntity() { Name = "Maksim", Email = "maksim@gmail.com", DateOfBirth = DateTime.Now, Image = _imageEntity1, Role = RoleConfig.DEFAULT_USER };
        private static readonly UserEntity _userEntity2 = new UserEntity() { Name = "Ivan", Email = "ivan@gmail.com", DateOfBirth = DateTime.Now, Image = _imageEntity1, Role = RoleConfig.DEFAULT_USER };

        public static void AddTestData(AppDbContext appDbContext)
        {
            if (appDbContext.Images.Any() == false)
            {
                appDbContext.Images.AddRange(GetImages());
                appDbContext.SaveChanges();
            }
            if (appDbContext.Users.Any() == false)
            {
                appDbContext.Users.AddRange(GetUsers());
                appDbContext.SaveChanges();
            }
            if (appDbContext.Publications.Any() == false)
            {
                appDbContext.Publications.AddRange(GetPublications());
                appDbContext.SaveChanges();
            }
        }

        private static ICollection<ImageEntity> GetImages()
        {
            return new List<ImageEntity>()
            {
                _imageEntity1,
                _imageEntity2,
                _imageEntity3,
            };
        }

        private static IEnumerable<UserEntity> GetUsers()
        {
            return new List<UserEntity>()
            {
                _userEntity1,
                _userEntity2,
            };
        }

        private static IEnumerable<PublicationEntity> GetPublications()
        {
            var publications = new List<PublicationEntity>()
            {
                new PublicationEntity() { Author = _userEntity1, Images = new List<ImageEntity>() { _imageEntity1, _imageEntity2 }, Text = "Some text 1", },
                new PublicationEntity() { Author = _userEntity2, Images = new List<ImageEntity>() { _imageEntity3 }, Text = "Some text 2" }
            };
            return publications;
        }
    }
}
