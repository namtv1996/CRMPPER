using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace ERP.Categories
{
	[Table("CatExpenses")]
    public class CatExpense : FullAuditedEntity<long> , IMustHaveTenant
    {
			public int TenantId { get; set; }
			

		[Required]
		[StringLength(CatExpenseConsts.MaxCodeLength, MinimumLength = CatExpenseConsts.MinCodeLength)]
		public virtual string Code { get; set; }
		
		[Required]
		[StringLength(CatExpenseConsts.MaxNameLength, MinimumLength = CatExpenseConsts.MinNameLength)]
		public virtual string Name { get; set; }
		

    }
}