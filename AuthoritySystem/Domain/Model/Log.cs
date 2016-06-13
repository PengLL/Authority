using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Log
    {
        public int Log_Id { get; set; }
        public int User_Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
