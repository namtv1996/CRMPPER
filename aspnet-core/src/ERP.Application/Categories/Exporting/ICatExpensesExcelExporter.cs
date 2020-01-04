using System.Collections.Generic;
using ERP.Categories.Dtos;
using ERP.Dto;

namespace ERP.Categories.Exporting
{
    public interface ICatExpensesExcelExporter
    {
        FileDto ExportToFile(List<GetCatExpenseForViewDto> catExpenses);
    }
}