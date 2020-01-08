# ASP .NET Core PDF

[**WEB**](https://tomashubelbauer.github.io/asp-pdf)

Generating a PDF in ASP .NET Core.

`dotnet new mvc`

https://dotnetcoretutorials.com/2019/07/02/creating-a-pdf-in-net-core

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

`dotnet watch run`

See the `Index` Home controller action for a full example.

## Chrome

https://www.nuget.org/packages/GoogleChrome

`chrome --headless --print-to-pdf="render.pdf" file://`

See the `Privacy` Home controller action for a full example.

## To-Do

### Figure out how to do repeating header and footer on every page in HTML

Doesn't seem to be supported as of today?
