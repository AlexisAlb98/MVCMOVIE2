namespace MvcMovie.Models
{
    public class EditUserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
        public List<string> AllRoles { get; set; }
    }
}
