using DevExpress.AspNetCore.Reporting.QueryBuilder;
using DevExpress.AspNetCore.Reporting.ReportDesigner;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.ReportDesigner;
using DevExpress.XtraReports.Web.ReportDesigner.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXWebApplication1.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult Error() {
            Models.ErrorModel model = new Models.ErrorModel();
            return View(model);
        }
        public async Task<IActionResult> ReportDesigner(
          [FromServices] IReportDesignerClientSideModelGenerator clientSideModelGenerator,
          [FromQuery] string reportName)
        {
            Models.ReportDesignerCustomModel model = new Models.ReportDesignerCustomModel();
            model.ReportDesignerModel = await CreateDefaultReportDesignerModel(clientSideModelGenerator, reportName, null);
            model.ReportDesignerModel.DataSourceSettings.AllowAddDataSource = true;
            model.ReportDesignerModel.DataSourceSettings.AllowEditDataSource = true;
            model.ReportDesignerModel.DataSourceSettings.AllowRemoveDataSource = true;
            return View(model);
        }

        //public IActionResult ReportDesigner(
        //    [FromServices] IReportDesignerModelBuilder reportDesignerModelBuilder, 
        //    [FromQuery] string reportName) {

        //    reportName = string.IsNullOrEmpty(reportName) ? "TestReport" : reportName;
        //    var designerModel = reportDesignerModelBuilder
        //        .Report(reportName)
        //        .BuildModel();
        //    designerModel.DataSourceSettings.AllowAddDataSource = true;
        //    designerModel.DataSourceSettings.AllowEditDataSource = true;
        //    designerModel.DataSourceSettings.AllowRemoveDataSource = true;
        //    return View(designerModel);
        //}

        public static Dictionary<string, object> GetAvailableDataSources()
        {
            var dataSources = new Dictionary<string, object>();
            SqlDataSource ds = new SqlDataSource("NWindConnectionString");
            // Create a SQL query to access the Products data table.
            SelectQuery query = SelectQueryFluentBuilder.AddTable("Products").SelectAllColumnsFromTable().Build("Products");
            ds.Queries.Add(query);
            ds.RebuildResultSchema();
            dataSources.Add("Northwind", ds);
            return dataSources;
        }

        public static async Task<ReportDesignerModel> CreateDefaultReportDesignerModel(IReportDesignerClientSideModelGenerator clientSideModelGenerator, string reportName, XtraReport report)
        {
            reportName = string.IsNullOrEmpty(reportName) ? "TestReport" : reportName;
            var dataSources = GetAvailableDataSources();
            if (report != null)
            {
                return await clientSideModelGenerator.GetModelAsync(report, dataSources, ReportDesignerController.DefaultUri, WebDocumentViewerController.DefaultUri, QueryBuilderController.DefaultUri);
            }
            return await clientSideModelGenerator.GetModelAsync(reportName, dataSources, ReportDesignerController.DefaultUri, WebDocumentViewerController.DefaultUri, QueryBuilderController.DefaultUri);
        }

    }
}