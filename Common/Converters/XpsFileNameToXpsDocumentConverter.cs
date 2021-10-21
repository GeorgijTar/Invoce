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
    public class XpsFileNameToFixedDocumentSequenceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if ((value is string fileName || (value is FileInfo info && (fileName = info.FullName) != null)) &&
                File.Exists(fileName))
            {
                try
                {
                    XpsDocument doc = new XpsDocument(fileName, FileAccess.ReadWrite);
                    return doc.GetFixedDocumentSequence();
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

        public static XpsFileNameToFixedDocumentSequenceConverter Instance { get; } = new XpsFileNameToFixedDocumentSequenceConverter();
    }

    /// <summary>Предоставляет экземпляр <see cref="XpsFileNameToFixedDocumentSequenceConverter"/> из <see cref="XpsFileNameToFixedDocumentSequenceConverter.Instance"/>.</summary>
    [MarkupExtensionReturnType(typeof(XpsFileNameToFixedDocumentSequenceConverter))]
    public class XpsFileNameToFixedDocumentSequenceConverterExtension : MarkupExtension
    {
        /// <summary>Возвращает экземпляр конвертера из <see cref="XpsFileNameToFixedDocumentSequenceConverter.Instance"/>.</summary>
        /// <param name="serviceProvider">Вспомогательный объект поставщика служб,
        /// способный предоставлять службы для расширения разметки.<para/>
        /// Не используется.</param>
        /// <returns>Экземпляр из <see cref="BooleanNotConverter.Instance"/>.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
            => XpsFileNameToFixedDocumentSequenceConverter.Instance;

    }

}
