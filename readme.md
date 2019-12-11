# ASP .NET Core PDF

Generating a PDF in ASP .NET Core.

1. `dotnet new mvc`
2. https://dotnetcoretutorials.com/2019/07/02/creating-a-pdf-in-net-core
  - PDF Sharp - .NET only, not .NET Core - has port https://github.com/ststeiger/PdfSharpCore
  - Select PDF - $500 & CE version: https://selectpdf.com/community-edition <5 pages
  - Iron PDF - Also paid
  - WkHtmlToPdf - Native dependency
  - SpirePDF - $600
  - EO PDF - $750
  - Aspose PDF - $1000
  - ITextSharp - AGPL

## PDFSharpCore

https://www.nuget.org/packages/PdfSharpCore/

```cs
public IActionResult Index()
{
  var document = new PdfDocument();
  var page = document.AddPage();
  var graphics = XGraphics.FromPdfPage(page);
  var font = new XFont("OpenSans", 20, XFontStyle.Bold);

  graphics.DrawString("Hello World!", font, XBrushes.Black, new XRect(20, 20, page.Width, page.Height), XStringFormats.Center);

  var memoryStream = new MemoryStream();
  document.Save(memoryStream);

  // Preview:
  return new FileStreamResult(memoryStream, new MediaTypeHeaderValue("application/pdf"));

  // Download:
  return new FileStreamResult(memoryStream, new MediaTypeHeaderValue("application/pdf")) { FileDownloadName = "download.pdf" };
}
```

`dotnet run`
