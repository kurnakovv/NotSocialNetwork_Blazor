namespace NotSocialNetwork.Application.DTOs.Favorite
{
    public class FavoriteResultDTO
    {
        public UserDTO User { get; set; }
        public PublicationDTO Publication { get; set; }
        public bool IsLike { get; set; }
    }
}
