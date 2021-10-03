﻿namespace ToDoWithDapr.API.Models
{
    public class Todo
    {
        public int Id {  get; set; }
        public string Description {  get; set; }
        public DateTime CreatedDate {  get; set; }
        public bool IsDone { get; set; }
    }
}
