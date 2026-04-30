using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskApp.Model
{
    [Table("tbl_task")]
    public class TaskModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DueDate {  get; set; }
        public TimeOnly DueTime { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime Reminders {  get; set; }
        public int user {  get; set; }
        public int category { get; set; }
    }
}
