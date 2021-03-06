﻿using System;

namespace MyApp.Web.Data.Entities
{
    public class VisitDetail
    {
        public int Id { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionTypeName { get; set; }
        public int? IdSubject { get; set; }
        public string Subject { get; set; }
        public string Note { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }
        public string ImageUrl4 { get; set; }
        public Visit Visit { get; set; }
    }
}