using System.ComponentModel.DataAnnotations;

namespace SecurityMarket.Model
{
    public class Role
    {
        //public int Id { get; set; }
        [Key]
        public UserRoleType RoleId { get; set; }
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
