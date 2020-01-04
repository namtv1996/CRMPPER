using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ERP.DataExporting.Excel.EpPlus;
using ERP.Categories.Dtos;
using ERP.Dto;
using ERP.Storage;

namespace ERP.Categories.Exporting
{
    public class CatExpensesExcelExporter : EpPlusExcelExporterBase, ICatExpensesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public CatExpensesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetCatExpenseForViewDto> catExpenses)
        {
            return CreateExcelPackage(
                "CatExpenses.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("CatExpenses"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Code"),
                        L("Name")
                        );

                    AddObjects(
                        sheet, 2, catExpenses,
                        _ => _.CatExpense.Code,
                        _ => _.CatExpense.Name
                        );

					

                });
        }
    }
}
