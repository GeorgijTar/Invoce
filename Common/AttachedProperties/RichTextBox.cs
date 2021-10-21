using Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;

namespace AttachedProperties
{
    public static class RichTextBox
    {

        /// <summary>Возвращает значение присоединённого свойства FileSource для <paramref name="richTextBox "/>.</summary>
        /// <param name="richTextBox "><see cref="rtb"/> значение свойства которого будет возвращено.</param>
        /// <returns><see cref="string"/> значение свойства.</returns>
        /// <remarks>Свойство задаёт файл-источник для свойства <see cref="System.Windows.Controls.RichTextBox.Document"/>.</remarks>
        public static string GetFileSource(System.Windows.Controls.RichTextBox richTextBox)
        {
            return (string)richTextBox.GetValue(FileSourceProperty);
        }

        /// <summary>Задаёт значение присоединённого свойства FileSource для <paramref name="richTextBox "/>.</summary>
        /// <param name="richTextBox "><see cref="RichTextBox"/> значение свойства которого будет возвращено.</param>
        /// <param name="value"><see cref="string"/> значение для свойства.</param>
        /// <remarks>Свойство задаёт файл-источник для свойства <see cref="System.Windows.Controls.RichTextBox.Document"/>.</remarks>
        public static void SetFileSource(System.Windows.Controls.RichTextBox richTextBox, string value)
        {
            richTextBox.SetValue(FileSourceProperty, value);
        }

        /// <summary><see cref="DependencyProperty"/> для методов <see cref="GetFileSource(RichTextBox)"/> и <see cref="SetFileSource(RichTextBox, string)"/>.</summary>
        public static readonly DependencyProperty FileSourceProperty =
            DependencyProperty.RegisterAttached(nameof(GetFileSource).Substring(3),
                                                typeof(string),
                                                typeof(RichTextBox),
                                                new PropertyMetadata(string.Empty, FileSourceChanged));

        private static readonly NotImplementedException InvalidCastToRichTextBox
            = new NotImplementedException($"Реализованно только для {typeof(System.Windows.Controls.RichTextBox).FullName}.");
        private static void FileSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is System.Windows.Controls.RichTextBox richTextBox))
                throw InvalidCastToRichTextBox;

            string pathFile = (string)e.NewValue;
            FlowDocument flow = new FlowDocument();
            if (!string.IsNullOrEmpty(pathFile))
            {
                TextRange doc = new TextRange(flow.ContentStart, flow.ContentEnd);
                using (FileStream fs = File.OpenRead(pathFile))
                {
                    string dataFormat = GetDataFormatFile(richTextBox);
                    if (string.IsNullOrWhiteSpace(dataFormat))
                    {
                        string ext = Path.GetExtension(pathFile);
                        if (string.IsNullOrWhiteSpace(ext))
                        {
                            dataFormat = DataFormats.UnicodeText;
                        }
                        else
                        {
                            dataFormat = FormatExtensions.FirstOrDefault(pair => pair.Value.Contains(ext)).Key;
                            if (string.IsNullOrWhiteSpace(dataFormat))
                            {
                                dataFormat = DataFormats.UnicodeText;
                            }
                        }
                    }
                    doc.Load(fs, dataFormat);

                    SetFlowData(flow, (pathFile, dataFormat, null));
                }
                flow.Unloaded += OnFlowUnloaded;
                BindingOperations.SetBinding(flow, ParentWindowProperty, ParentWindowBinding);
            }
            richTextBox.Document = flow;
        }

        private static void OnFlowUnloaded(object sender, RoutedEventArgs e)
        {
            FlowSave((FlowDocument)sender);
        }

        public static void FlowSave(FlowDocument flow)
        {
            (string pathFile, string format, _) = GetFlowData(flow);

            FlowSave(flow, pathFile, format);
        }

        public static void FlowSave(FlowDocument flow, string pathFile, string dataFormat)
        {
            TextRange doc = new TextRange(flow.ContentStart, flow.ContentEnd);
            using (FileStream file = File.Create(pathFile))
            {
                doc.Save(file, dataFormat);
            }
        }


        public static ReadOnlyDictionary<string, HashSet<string>> FormatExtensions { get; }

        static RichTextBox()
        {
            var allFormat = typeof(DataFormats)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(prop => (string)prop.GetValue(null));
            FormatExtensions = new ReadOnlyDictionary<string, HashSet<string>>(
                allFormat.ToDictionary(form => form, form => new HashSet<string>(StringComparer.InvariantCultureIgnoreCase)));

            // Здесь надо записать все расширения файлов, 
            // но я укажу только некоторые
            // Расширения надо указывать всключая точку перед ним.
            // Регистр игнорируется.
            FormatExtensions[DataFormats.Text].Add(".txt");
            FormatExtensions[DataFormats.UnicodeText].Add(".text");
            FormatExtensions[DataFormats.Rtf].Add(".rtf");
            FormatExtensions[DataFormats.Xaml].Add(".xaml");
            FormatExtensions[DataFormats.Bitmap].Add(".bmp");
            FormatExtensions[DataFormats.Tiff].AddRange(".tif .tiff".Split());
            FormatExtensions[DataFormats.Html].Add(".html");
        }



        /// <summary>Возвращает значение присоединённого свойства DataFormatFile для <paramref name="richTextBox"/>.</summary>
        /// <param name="richTextBox"><see cref="System.Windows.Controls.RichTextBox"/> значение свойства которого будет возвращено.</param>
        /// <returns><see cref="string"/> значение свойства.</returns>
        public static string GetDataFormatFile(System.Windows.Controls.RichTextBox richTextBox)
        {
            return (string)richTextBox.GetValue(DataFormatFileProperty);
        }

        /// <summary>Задаёт значение присоединённого свойства DataFormatFile для <paramref name="richTextBox"/>.</summary>
        /// <param name="richTextBox"><see cref="System.Windows.Controls.RichTextBox"/> значение свойства которого будет возвращено.</param>
        /// <param name="value"><see cref="string"/> значение для свойства.</param>
        public static void SetDataFormatFile(System.Windows.Controls.RichTextBox richTextBox, string value)
        {
            richTextBox.SetValue(DataFormatFileProperty, value);
        }

        /// <summary><see cref="DependencyProperty"/> для методов <see cref="GetDataFormatFile(System.Windows.Controls.RichTextBox)"/> и <see cref="SetDataFormatFile(System.Windows.Controls.RichTextBox, string)"/>.</summary>
        public static readonly DependencyProperty DataFormatFileProperty =
            DependencyProperty.RegisterAttached(nameof(GetDataFormatFile).Substring(3), typeof(string), typeof(RichTextBox), new PropertyMetadata(null, (s, e) => { }, CoerceDataFormatFile));

        private static object CoerceDataFormatFile(DependencyObject d, object baseValue)
            => (d is System.Windows.Controls.RichTextBox)
                ? FormatExtensions.ContainsKey((string)baseValue)
                       ? baseValue
                       : null
                : throw InvalidCastToRichTextBox;


        /// <summary>Возвращает значение присоединённого свойства FlowDataFile для <paramref name="flowDocument"/>.</summary>
        /// <param name="flowDocument"><see cref="FlowDocument"/ значение свойства которого будет возвращено.</param>
        /// <returns><see cref="(string file, string format)"/> значение свойства.</returns>
        public static (string file, string format, EventHandler onCloseWindow) GetFlowData(FlowDocument flowDocument)
        {
            return ((string, string, EventHandler))flowDocument.GetValue(FlowDataProperty);
        }

        /// <summary>Задаёт значение присоединённого свойства FlowDataFile для <paramref name="flowDocument"/>.</summary>
        /// <param name="flowDocument"><see cref="FlowDocument"/> значение свойства которого будет возвращено.</param>
        /// <param name="value"><see cref="(string file, string format)"/> значение для свойства.</param>
        private static void SetFlowData(FlowDocument flowDocument, (string file, string format, EventHandler onCloseWindow) value)
        {
            flowDocument.SetValue(FlowDataPropertyKey, value);
        }

        /// <summary><see cref="DependencyPropertyKey"/> для метода <see cref="SetFlowDataFile(FlowDocument, (string file, string format))"/>.</summary>
        public static readonly DependencyPropertyKey FlowDataPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(nameof(GetFlowData).Substring(3), typeof((string file, string format, EventHandler onCloseWindow)),
                typeof(RichTextBox), new PropertyMetadata(((string)null, (string)null, (EventHandler)null)));

        /// <summary><see cref="DependencyProperty"/> для метода <see cref="GetFlowData(FlowDocument)"/>.</summary>
        public static readonly DependencyProperty FlowDataProperty = FlowDataPropertyKey.DependencyProperty;




        /// <summary>Возвращает значение присоединённого свойства ParentWindow для <paramref name="flowDocument"/>.</summary>
        /// <param name="flowDocument"><see cref="FlowDocument"/> значение свойства которого будет возвращено.</param>
        /// <returns><see cref="Window"/> значение свойства.</returns>
        public static Window GetParentWindow(FlowDocument flowDocument)
        {
            return (Window)flowDocument.GetValue(ParentWindowProperty);
        }

        /// <summary>Задаёт значение присоединённого свойства ParentWindow для <paramref name="flowDocument"/>.</summary>
        /// <param name="flowDocument"><see cref="FlowDocument"/> значение свойства которого будет возвращено.</param>
        /// <param name="value"><see cref="Window"/> значение для свойства.</param>
        private static void SetParentWindow(FlowDocument flowDocument, Window value)
        {
            flowDocument.SetValue(ParentWindowProperty, value);
        }

        /// <summary><see cref="DependencyProperty"/> для методов <see cref="GetParentWindow(FlowDocument)"/> и <see cref="SetParentWindow(FlowDocument, Window)"/>.</summary>
        private static readonly DependencyProperty ParentWindowProperty =
            DependencyProperty.RegisterAttached("Shadow" + nameof(GetParentWindow).Substring(3), typeof(Window), typeof(RichTextBox), new PropertyMetadata(null, ParentWindowChanged));

        private static void ParentWindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FlowDocument flow = (FlowDocument)d;
            var flowData = GetFlowData(flow);

            if (e.OldValue is Window oldWindow)
                oldWindow.Closed -= flowData.onCloseWindow;

            if (e.NewValue is Window newWindow)
            {
                flowData.onCloseWindow = (s, _e) => FlowSave(flow);
                newWindow.Closed += flowData.onCloseWindow;
            }
            else
            {
                flowData.onCloseWindow = null;
            }
            SetFlowData(flow, flowData);
        }

        private static readonly Binding ParentWindowBinding = new Binding() { RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(Window), 1) };

    }


}