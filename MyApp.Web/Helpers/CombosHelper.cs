﻿using Microsoft.AspNetCore.Mvc.Rendering;
using MyApp.Web.Data;
using System.Collections.Generic;
using System.Linq;


namespace MyApp.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;
        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IEnumerable<SelectListItem> GetComboCompanyTypes()
        {
            var list = _dataContext.CompanyTypes.Select(ct => new SelectListItem
            {
                Text = ct.Name,
                Value = $"{ct.Id}"
            }).OrderBy(p => p.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Elija Tipo de Empresa...)",
                Value = "0"
            });
            return list;
        }
        
        public IEnumerable<SelectListItem> GetComboQuestionsTypes()
        {
            var list = _dataContext.QuestionTypes.Select(ct => new SelectListItem
            {
                Text = ct.Name,
                Value = $"{ct.Id}"
            }).OrderBy(p => p.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Elija Tipo de Relevamiento...)",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboTechnicals()
        {
            var list = _dataContext.Technicals.Select(ct => new SelectListItem
            {
                Text = ct.User.FullName,
                Value = $"{ct.Id}"
            }).OrderBy(p => p.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Elija Técnico..)",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboRoles()
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "(Select a role...)" },
                new SelectListItem { Value = "1", Text = "Customer" },
                new SelectListItem { Value = "2", Text = "Technical" }
            };
            return list;
        }



    }
}
