using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EP.MAINAPP.ViewModels.Artist;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace EP.MAINAPP.ViewModels.PDF
{
    public class ViewModelPDF : AbstractViewModelContainer
    {
        public ViewModelPDF(object list)
        {
            if (list is ObservableCollection<DOMAIN.Artist>)
            {
                ObservableCollection<DOMAIN.Artist> newList = (ObservableCollection<DOMAIN.Artist>)list;
                GeneratePDF(newList);
            }
            else if (list is ObservableCollection<DOMAIN.Composer>)
            {
                ObservableCollection<DOMAIN.Composer> newList = (ObservableCollection<DOMAIN.Composer>)list;
                GeneratePDF(newList);
            }
            else if (list is ObservableCollection<DOMAIN.Piece>)
            {
                ObservableCollection<DOMAIN.Piece> newList = (ObservableCollection<DOMAIN.Piece>)list;
                GeneratePDF(newList);
            }
            else if (list is ObservableCollection<DOMAIN.Type>)
            {
                ObservableCollection<DOMAIN.Type> newList = (ObservableCollection<DOMAIN.Type>)list;
                GeneratePDF(newList);
            }
        }

        private void GeneratePDF(object newList)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

            string type = newList.GetType().GetGenericArguments()[0].ToString();
            int pointPosition = type.LastIndexOf('.');
            type = type.Substring(pointPosition + 1);
            // Draw the text
            gfx.DrawString($"List from {type}", font,XBrushes.Black, new XPoint(200, 70));
            gfx.DrawLine(new XPen(XColor.FromArgb(50,30,200)), new XPoint(100,100), new XPoint(500,100));

            gfx.DrawString("Name", new XFont("Arial", 15, XFontStyle.Bold), XBrushes.Black, new XPoint(100,200));

            int currentYposition = 200;
            foreach (var item in (IEnumerable)newList)
            {
                currentYposition += 20;
                if (item is DOMAIN.Artist)
                {
                    var tempValue = (DOMAIN.Artist)item;
                    gfx.DrawString($"{tempValue.NameArtist}", new XFont("Arial", 15), XBrushes.Black, new XPoint(100, currentYposition));
                }
                else if (item is DOMAIN.Composer)
                {
                    var tempValue = (DOMAIN.Composer)item;
                    gfx.DrawString($"{tempValue.FirstName} {tempValue.LastName}", new XFont("Arial", 15), XBrushes.Black, new XPoint(100, currentYposition));
                }
                else if (item is DOMAIN.Piece)
                {
                    var tempValue = (DOMAIN.Piece)item;
                    gfx.DrawString($"{tempValue.NamePiece}", new XFont("Arial", 15), XBrushes.Black, new XPoint(100, currentYposition));
                }
                else if (item is DOMAIN.Type)
                {
                    var tempValue = (DOMAIN.Type)item;
                    gfx.DrawString($"{tempValue.NameType}", new XFont("Arial", 15), XBrushes.Black, new XPoint(100, currentYposition));
                }
            }

            // Save the document...
            string filename = $"{type}.pdf";

            document.Save(filename);

            MessageBox.Show("PDF is save in you Debug file");
        }
    }
}
