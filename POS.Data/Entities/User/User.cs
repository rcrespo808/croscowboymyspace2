using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Data
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string ProfilePhoto { get; set; }
        public string Provider { get; set; }
        public string Address { get; set; }
        public bool IsSuperAdmin { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedBy { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public Guid? CustomerId { get; set; }
        
        public Guid? SupplierId { get; set; }

        public ICollection<Asistencia> Asistencia { get; set; }
        
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
