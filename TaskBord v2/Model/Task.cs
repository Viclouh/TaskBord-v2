using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBord.Model
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public int TaskTypeId { get; set; }
        public User User{ get; set; }
        public TaskType TaskType { get; set; }
    }
}
