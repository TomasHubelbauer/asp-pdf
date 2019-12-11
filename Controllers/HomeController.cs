using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using asp_pdf.Models;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using System.IO;
using Microsoft.Net.Http.Headers;

namespace asp_pdf.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      var document = new PdfDocument();
      var page = document.AddPage();
      var graphics = XGraphics.FromPdfPage(page);

      DrawHeader(graphics); // 80
      DrawFooter(graphics); // 60

      var height = graphics.PageSize.Height - 80 - 60;
      var leftListHeight = DrawList(graphics, 0, 80, new[] { "1st item", "2nd item", "3rd item" });
      var rightListHeight = DrawList(graphics, graphics.PageSize.Width / 2, 80, new[] { "1st item", "2nd item", "3rd item", "4th item", "5th item" });
      var tallerListHeight = Math.Max(leftListHeight, rightListHeight);

      var pen = new XPen(new XColor());
      graphics.DrawLine(pen, graphics.PageSize.Width / 2, 80, graphics.PageSize.Width / 2, 80 + tallerListHeight);

      var image = XImage.FromFile(@"32-file_extension_pdf.png");
      var font = new XFont("OpenSans", 20);
      var offset = 0;
      for (var index = 0; index < 100; index++)
      {
        if (90 + tallerListHeight + ((index - offset) + 1) * 40 > graphics.PageSize.Height - 60)
        {
          page = document.AddPage();
          graphics = XGraphics.FromPdfPage(page);
          offset = index;
          tallerListHeight = 0;
          DrawHeader(graphics);
          DrawFooter(graphics);
        }

        graphics.DrawImage(image, 10, 90 + tallerListHeight + (index - offset) * 40);
        graphics.DrawString("File #" + index, font, XBrushes.Black, 40, 110 + tallerListHeight + (index - offset) * 40);
      }

      var memoryStream = new MemoryStream();
      document.Save(memoryStream);
      return new FileStreamResult(memoryStream, new MediaTypeHeaderValue("application/pdf"));
    }

    void DrawHeader(XGraphics graphics)
    {
      var font = new XFont("OpenSans", 20, XFontStyle.Bold);

      graphics.DrawString("First Name:", font, XBrushes.Black, 10, 20);
      graphics.DrawString("Tomas", font, XBrushes.Black, 150, 20);

      graphics.DrawString("Last Name:", font, XBrushes.Black, 10, 40);
      graphics.DrawString("Hubelbauer", font, XBrushes.Black, 150, 40);

      graphics.DrawString("Occupation:", font, XBrushes.Black, 10, 60);
      graphics.DrawString("Programmer", font, XBrushes.Black, 150, 60);

      var pen = new XPen(new XColor());
      graphics.DrawLine(pen, 0, 80, graphics.PageSize.Width, 80);
    }

    void DrawFooter(XGraphics graphics)
    {
      var pen = new XPen(new XColor());
      graphics.DrawLine(pen, 0, graphics.PageSize.Height - 60, graphics.PageSize.Width, graphics.PageSize.Height - 60);

      var font = new XFont("OpenSans", 20);
      graphics.DrawString("This is the footer :-)", font, XBrushes.Black, 10, graphics.PageSize.Height - 30);
    }

    double DrawList(XGraphics graphics, double x, double y, string[] items)
    {
      var font = new XFont("OpenSans", 20);
      var index = 0.0;
      foreach (var item in items)
      {
        graphics.DrawString("1st item", font, XBrushes.Black, x + 10, y + index++ * 20 + 20);
      }

      index++;
      var pen = new XPen(new XColor());
      graphics.DrawLine(pen, x, y + index * 20, x + graphics.PageSize.Width / 2, y + index * 20);

      return index * 20;
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
