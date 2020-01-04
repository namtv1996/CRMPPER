
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ERP.Categories.Dtos
{
    public class CreateOrEditCatExpenseDto : EntityDto<long?>
    {

		[Required]
		[StringLength(CatExpenseConsts.MaxCodeLength, MinimumLength = CatExpenseConsts.MinCodeLength)]
		public string Code { get; set; }
		
		
		[Required]
		[StringLength(CatExpenseConsts.MaxNameLength, MinimumLength = CatExpenseConsts.MinNameLength)]
		public string Name { get; set; }
		
		

    }
}