using Microsoft.AspNetCore.Identity;

namespace IdentityFromEmpty
{
    public class ApplicationUser: IdentityUser
    {
        public virtual int ShoeSize { get; set; }
    }
}