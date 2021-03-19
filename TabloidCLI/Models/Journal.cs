using System;
using System.Collections.Generic;

namespace TabloidCLI.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDateTime { get; set; }
        public List<Journal> Journals { get; set; } = new List<Journal>();

        public override string ToString()
        {
            return $" {PublishDateTime} {Title} ({Content})";
        }
    }
}