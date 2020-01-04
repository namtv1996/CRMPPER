using Abp.Application.Services.Dto;
using System;

namespace ERP.Categories.Dtos
{
    public class GetAllCatExpensesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}