using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ERP.Categories.Dtos
{
    public class GetCatExpenseForEditOutput
    {
		public CreateOrEditCatExpenseDto CatExpense { get; set; }


    }
}