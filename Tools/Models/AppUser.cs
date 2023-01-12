
using Microsoft.AspNetCore.Identity;

namespace Core.Domains.Account
{
    public class AppUser : IdentityUser
    {
        public bool IsConfigured { get; set; } = true; // to appoint out if the profile is complete enough
        public int ProfileCompleateProgression { get; set; } 
        
        public int ReferenceId { get; set; }
        public string UserFullName { get; set; } = "ادخل اسمك";
    }
}
