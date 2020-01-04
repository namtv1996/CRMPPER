

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using ERP.Categories.Exporting;
using ERP.Categories.Dtos;
using ERP.Dto;
using Abp.Application.Services.Dto;
using ERP.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ERP.Categories
{
	[AbpAuthorize(AppPermissions.Pages_CatExpenses)]
    public class CatExpensesAppService : ERPAppServiceBase, ICatExpensesAppService
    {
		 private readonly IRepository<CatExpense, long> _catExpenseRepository;
		 private readonly ICatExpensesExcelExporter _catExpensesExcelExporter;
		 

		  public CatExpensesAppService(IRepository<CatExpense, long> catExpenseRepository, ICatExpensesExcelExporter catExpensesExcelExporter ) 
		  {
			_catExpenseRepository = catExpenseRepository;
			_catExpensesExcelExporter = catExpensesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetCatExpenseForViewDto>> GetAll(GetAllCatExpensesInput input)
         {
			
			var filteredCatExpenses = _catExpenseRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter));

			var pagedAndFilteredCatExpenses = filteredCatExpenses
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var catExpenses = from o in pagedAndFilteredCatExpenses
                         select new GetCatExpenseForViewDto() {
							CatExpense = new CatExpenseDto
							{
                                Code = o.Code,
                                Name = o.Name,
                                Id = o.Id
							}
						};

            var totalCount = await filteredCatExpenses.CountAsync();

            return new PagedResultDto<GetCatExpenseForViewDto>(
                totalCount,
                await catExpenses.ToListAsync()
            );
         }
		 
		 public async Task<GetCatExpenseForViewDto> GetCatExpenseForView(long id)
         {
            var catExpense = await _catExpenseRepository.GetAsync(id);

            var output = new GetCatExpenseForViewDto { CatExpense = ObjectMapper.Map<CatExpenseDto>(catExpense) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_CatExpenses_Edit)]
		 public async Task<GetCatExpenseForEditOutput> GetCatExpenseForEdit(EntityDto<long> input)
         {
            var catExpense = await _catExpenseRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetCatExpenseForEditOutput {CatExpense = ObjectMapper.Map<CreateOrEditCatExpenseDto>(catExpense)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditCatExpenseDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_CatExpenses_Create)]
		 protected virtual async Task Create(CreateOrEditCatExpenseDto input)
         {
            var catExpense = ObjectMapper.Map<CatExpense>(input);

			
			if (AbpSession.TenantId != null)
			{
				catExpense.TenantId = (int) AbpSession.TenantId;
			}
		

            await _catExpenseRepository.InsertAsync(catExpense);
         }

		 [AbpAuthorize(AppPermissions.Pages_CatExpenses_Edit)]
		 protected virtual async Task Update(CreateOrEditCatExpenseDto input)
         {
            var catExpense = await _catExpenseRepository.FirstOrDefaultAsync((long)input.Id);
             ObjectMapper.Map(input, catExpense);
         }

		 [AbpAuthorize(AppPermissions.Pages_CatExpenses_Delete)]
         public async Task Delete(EntityDto<long> input)
         {
            await _catExpenseRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetCatExpensesToExcel(GetAllCatExpensesForExcelInput input)
         {
			
			var filteredCatExpenses = _catExpenseRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter));

			var query = (from o in filteredCatExpenses
                         select new GetCatExpenseForViewDto() { 
							CatExpense = new CatExpenseDto
							{
                                Code = o.Code,
                                Name = o.Name,
                                Id = o.Id
							}
						 });


            var catExpenseListDtos = await query.ToListAsync();

            return _catExpensesExcelExporter.ExportToFile(catExpenseListDtos);
         }


    }
}