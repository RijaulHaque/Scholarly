namespace Scholarly.Models
{
    public class Role
    {
        public string role_id { get; set; } = string.Empty;

        public string role_name { get; set; } = string.Empty;

        // Add this property inside the Role class
        public ICollection<Users> users_obj { get; set; } = new List<Users>();
    }
}