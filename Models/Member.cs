using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        public string MemberName { get; set; }

        public string MemberSurname { get; set; }

        public string MemberMail { get; set; }

        [JsonIgnore] public string MemberPassword { get; set; }

    }
}
