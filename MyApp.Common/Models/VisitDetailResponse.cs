using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Common.Models
{
    public class VisitDetailResponse
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
        public string ImageFullPath1 => string.IsNullOrEmpty(ImageUrl1)
            ? "noimageavailable"//null
            : $"{ImageUrl1.Substring(0)}";
        public string ImageFullPath2 => string.IsNullOrEmpty(ImageUrl2)
            ? "noimageavailable"//null
            : $"{ImageUrl2.Substring(0)}";
        public string ImageFullPath3 => string.IsNullOrEmpty(ImageUrl3)
            ? "noimageavailable"//null
            : $"{ImageUrl3.Substring(0)}";
        public string ImageFullPath4 => string.IsNullOrEmpty(ImageUrl4)
            ? "noimageavailable"//null
            : $"{ImageUrl4.Substring(0)}";
    }
}
