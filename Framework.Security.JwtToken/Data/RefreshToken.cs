//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Framework.Security.JwtToken.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class RefreshToken
    {
        public System.Guid Id { get; set; }
        public System.DateTime ExpireDate { get; set; }
        public System.DateTime CreationDate { get; set; }
        public string Text { get; set; }
        public string UserIdentity { get; set; }
    }
}