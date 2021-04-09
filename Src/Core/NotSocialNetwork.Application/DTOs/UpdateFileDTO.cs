using NotSocialNetwork.Application.Entities.Abstract;

namespace NotSocialNetwork.Application.DTOs
{
    public class UpdateFileDTO
    {
        public BaseEntity OldFile { get; set; }
        public BaseEntity NewFile { get; set; }
        public string FilePath { get; set; }
    }
}
