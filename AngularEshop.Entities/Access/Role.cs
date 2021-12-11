using AngularEshop.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace AngularEshop.Entities.Access
{
    public class Role : IdentityRole<int>, IEntity
    {
        #region Properties

        public string Title { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        #endregion Properties
    }
}