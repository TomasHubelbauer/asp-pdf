# ASP .NET Core PDF

Generating a PDF in ASP .NET Core.

## Options

### Paid & Bad

https://dotnetcoretutorials.com/2019/07/02/creating-a-pdf-in-net-core

- Select PDF: $500 & CE version: https://selectpdf.com/community-edition 5 pages
- Iron PDF: paid
- WkHtmlToPdf: native dependency
- SpirePDF: $600
- EO PDF: $750
- Aspose PDF: $1000
- ITextSharp: AGPL

### Good

- PDF Sharp: .NET only, .NET Core port https://github.com/ststeiger/PdfSharpCore
- Chromium: CLI

## PDFSharpCore

https://www.nuget.org/packages/PdfSharpCore

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

`dotnet watch run`

See the `Index` Home controller action for a full example. Headers and footers
seem to need to be done on per-page basis.

## Chromium

https://www.nuget.org/packages/GoogleChrome

`chrome --headless --print-to-pdf="render.pdf" file://`

See the `Privacy` Home controller action for a full example. Headers and footers
seem to need to be done on per-page basis using CSS.
