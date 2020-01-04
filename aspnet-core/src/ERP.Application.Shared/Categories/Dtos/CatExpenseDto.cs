
using System;
using Abp.Application.Services.Dto;

namespace ERP.Categories.Dtos
{
    public class CatExpenseDto : EntityDto<long>
    {
		public string Code { get; set; }

		public string Name { get; set; }



    }
}