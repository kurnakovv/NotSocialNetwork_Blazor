using NotSocialNetwork.Application.DTOs;
using NotSocialNetwork.Application.DTOs.Favorite;
using NotSocialNetwork.Application.Entities;
using NotSocialNetwork.Application.Exceptions;
using NotSocialNetwork.Application.Interfaces.Repositories;
using NotSocialNetwork.Application.Interfaces.UseCases.Favorite;
using NotSocialNetwork.Application.Interfaces.UseCases.Publication;
using NotSocialNetwork.Application.Interfaces.UseCases.User;
using System.Linq;

namespace NotSocialNetwork.Application.UseCases.Favorite
{
    public class LikeFavorite : ILikeableFavorite
    {
        public LikeFavorite(
            IGetableUser getableUser,
            IGetablePublication getablePublication,
            IGetableFavorite getableFavorite,
            IRepositoryAsync<PublicationEntity> repository)
        {
            _getableUser = getableUser;
            _getablePublication = getablePublication;
            _getableFavorite = getableFavorite;
            _repository = repository;
        }

        private readonly IGetableUser _getableUser;
        private readonly IGetablePublication _getablePublication;
        private readonly IGetableFavorite _getableFavorite;
        private readonly IRepositoryAsync<PublicationEntity> _repository;


        public FavoriteResultDTO LikeOrUnlike(FavoriteDTO favoriteDTO)
        {
            var author = _getableUser.GetById(favoriteDTO.UserId);
            var publication = _getablePublication.GetById(favoriteDTO.PublicationId);
            var result = InitFavorite(publication, author);
            result.IsLike = LikeOrUnlike(author, publication);
            return result;
        }

        public bool LikeOrUnlike(UserEntity author, PublicationEntity publication) 
        {
            try
            {
                CheckFavorites(author, publication);

                return Unlike(author, publication);
            }
            catch (FavoritesNotFoundException)
            {
                return Like(author, publication);
            }
        }

        private bool Like(UserEntity author, PublicationEntity publication)
        {
            author.Favorites.Add(publication);
            publication.Favorites.Add(author);
            _repository.Commit();

            return true;
        }

        private bool Unlike(UserEntity author, PublicationEntity publication)
        {
            author.Favorites.Remove(publication);
            publication.Favorites.Remove(author);
            _repository.Commit();

            return false;
        }

        private void CheckFavorites(UserEntity author, PublicationEntity publication)
        {
            _getableFavorite.GetPublicationsWithFavorites(author.Id);
            _getableFavorite.GetAuthors(publication.Id);
        }

        private FavoriteResultDTO InitFavorite(PublicationEntity publication, UserEntity author)
        {
            return new FavoriteResultDTO()
            {
                Publication = new PublicationDTO()
                {
                    Id = publication.Id,
                    Author = new UserDTO()
                    {
                        Id = author.Id,
                        Email = author.Email,
                        Image = author.Image,
                        Name = author.Name
                    },
                    Text = publication.Text,
                    ImagePaths = publication.Images.Select(i => i.Title).ToList(),
                },
                User = new UserDTO()
                {
                    Id = author.Id,
                    Email = author.Email,
                    Image = author.Image,
                    Name = author.Name
                },
            };
        }
    }
}
