using GemBox.Pdf;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Xps.Packaging;

namespace WpfMvvm.Converters
{
    [ValueConversion(typeof(string), typeof(FixedDocumentSequence))]
    [ValueConversion(typeof(FileInfo), typeof(FixedDocumentSequence))]
    public class PdfFileNameToFixedDocumentSequenceConverter : IValueConverter
    {
        static PdfFileNameToFixedDocumentSequenceConverter()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if ((value is string fileName || (value is FileInfo info && (fileName = info.FullName) != null)) &&
                File.Exists(fileName))
            {
                try
                {
                    using (var document = PdfDocument.Load(fileName))
                    {
                        // XpsDocument needs to stay referenced so that DocumentViewer can access additional required resources.
                        // Otherwise, GC will collect/dispose XpsDocument and DocumentViewer will not work.
                        XpsDocument doc = document.ConvertToXpsDocument(SaveOptions.Xps);

                        return doc.GetFixedDocumentSequence();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static PdfFileNameToFixedDocumentSequenceConverter Instance { get; } = new PdfFileNameToFixedDocumentSequenceConverter();
    }

    /// <summary>Предоставляет экземпляр <see cref="PdfFileNameToFixedDocumentSequenceConverter"/> из <see cref="PdfFileNameToFixedDocumentSequenceConverter.Instance"/>.</summary>
    [MarkupExtensionReturnType(typeof(PdfFileNameToFixedDocumentSequenceConverter))]
    public class PdfFileNameToFixedDocumentSequenceConverterExtension : MarkupExtension
    {
        /// <summary>Возвращает экземпляр конвертера из <see cref="PdfFileNameToFixedDocumentSequenceConverter.Instance"/>.</summary>
        /// <param name="serviceProvider">Вспомогательный объект поставщика служб,
        /// способный предоставлять службы для расширения разметки.<para/>
        /// Не используется.</param>
        /// <returns>Экземпляр из <see cref="BooleanNotConverter.Instance"/>.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
            => PdfFileNameToFixedDocumentSequenceConverter.Instance;

    }

}
