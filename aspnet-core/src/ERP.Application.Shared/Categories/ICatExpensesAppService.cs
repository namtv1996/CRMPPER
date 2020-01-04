using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ERP.Categories.Dtos;
using ERP.Dto;

namespace ERP.Categories
{
    public interface ICatExpensesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetCatExpenseForViewDto>> GetAll(GetAllCatExpensesInput input);

        Task<GetCatExpenseForViewDto> GetCatExpenseForView(long id);

		Task<GetCatExpenseForEditOutput> GetCatExpenseForEdit(EntityDto<long> input);

		Task CreateOrEdit(CreateOrEditCatExpenseDto input);

		Task Delete(EntityDto<long> input);

		Task<FileDto> GetCatExpensesToExcel(GetAllCatExpensesForExcelInput input);

		
    }
}