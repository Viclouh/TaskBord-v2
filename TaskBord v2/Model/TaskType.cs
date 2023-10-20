using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBord.Model
{
     public class TaskType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Task> Tasks { get; set;}
            
    }


}
