using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace UserManager
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? Id { get; set; }

        [Required]
        [StringLength(50)] 
        public string RoleName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<RoleUser> RoleUser_RoleIds { get; set; }

        public virtual ICollection<MenuPermission> MenuPermission_RoleIds { get; set; }

    }
}
