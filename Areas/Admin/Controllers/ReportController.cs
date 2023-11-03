using Aspose.Pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using PROG3050_HMJJ.Areas.Admin.Models;
using System.Data;
using System.Runtime.CompilerServices;

namespace PROG3050_HMJJ.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly Reportdbo _reportdbo;
        public IActionResult Index()
        {
            return View();
        }

        public ReportController(IConfiguration configuration)
        {
            _reportdbo = new Reportdbo(configuration);
        }

        public IActionResult GetMemberDetails()
        {
            using (var connection = _reportdbo.GetConnection())
            {
                var document = new Document
                {
                    PageInfo = new PageInfo { Margin = new MarginInfo(28, 28, 28, 40) }
                };
                var pdfpage = document.Pages.Add();
                Table table = new Table
                {
                    ColumnWidths = "20% 20% 20% 20% 20%",
                    DefaultCellPadding = new MarginInfo(10, 5, 10, 5),
                    Border = new BorderInfo(BorderSide.All, .5f, Color.Black),
                    DefaultCellBorder = new BorderInfo(BorderSide.All, .2f, Color.Black)
                };
                DataTable dt = _reportdbo.GetMemberDetailsrecord();//GetMemberListsrecord
                table.ImportDataTable(dt,true,0,0);
                document.Pages[1].Paragraphs.Add(table);
                
                using (var streamout = new MemoryStream())
                {
                    document.Save(streamout);
                    return new FileContentResult(streamout.ToArray(), "application/pdf")
                    {
                        FileDownloadName = "CVGS_Member_Detail_Report.pdf",
                        
                    };
                }
            }
        }

        public IActionResult GetMemberList()
        {
            using (var connection = _reportdbo.GetConnection())
            {
                var document = new Document
                {
                    PageInfo = new PageInfo { Margin = new MarginInfo(28, 28, 28, 40) }
                };
                var pdfpage = document.Pages.Add();
                Table table = new Table
                {
                    ColumnWidths = "10% 40% 40%",
                    DefaultCellPadding = new MarginInfo(10, 5, 10, 5),
                    Border = new BorderInfo(BorderSide.All, .5f, Color.Black),
                    DefaultCellBorder = new BorderInfo(BorderSide.All, .2f, Color.Black)
                };
                DataTable dt = _reportdbo.GetMemberListsrecord();
                table.ImportDataTable(dt, true, 0, 0);
                document.Pages[1].Paragraphs.Add(table);

                using (var streamout = new MemoryStream())
                {
                    document.Save(streamout);
                    return new FileContentResult(streamout.ToArray(), "application/pdf")
                    {
                        FileDownloadName = "CVGS_Member_List_Report.pdf",

                    };
                }
            }
        }
    }
}
