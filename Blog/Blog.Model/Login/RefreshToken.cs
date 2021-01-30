using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Model.Login
{
    public partial class RefreshToken
    {
        public string Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime Createddate { get; set; }
        public string CreatedByIp { get; set; }


        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedId { get; set; }
        public string UpdatedId { get; set; }
        public int Totalrows { get; set; }
        public int PageNumber { get; set; }

    }
}
