using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace Sam.ToolStock.Model.ViewModels
{
    [Validator(typeof(ToolTypeViewModel))]
    public class ToolTypeViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        public IEnumerable<ToolViewModel> Tools { get; set; }
    }
}
