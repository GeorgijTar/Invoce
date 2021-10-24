using InvoceModelLib.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace InvoceViewLib
{
    public class StatusIdToNameConverter : Freezable, IValueConverter
    {

        /// <summary>Коллекция <see cref="StatusDTO"/>.
        /// В коллекции все <see cref="StatusDTO.ID"/> и <see cref="StatusDTO.Name"/> должны быть ункальными.</summary>
        /// <remarks>Не поддерживается отслеживание изменения коллекции.
        /// Если в коллекцию были внесенны изменения, то источник должен быть заменён на новый.</remarks>
        public IEnumerable<StatusDTO> Statuses
        {
            get => (IEnumerable<StatusDTO>)GetValue(StatusesProperty);
            set => SetValue(StatusesProperty, value);
        }

        /// <summary><see cref="DependencyProperty"/> для свойства <see cref="Statuses"/>.</summary>
        public static readonly DependencyProperty StatusesProperty =
            DependencyProperty.Register(nameof(Statuses), typeof(IEnumerable<StatusDTO>), typeof(StatusIdToNameConverter), new PropertyMetadata(null, StatusesChanged));

        private static void StatusesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StatusIdToNameConverter converter = (StatusIdToNameConverter)d;
            if (e.NewValue is IEnumerable<StatusDTO> statuses)
            {
                converter.ids = statuses.ToDictionary(st => st.Id);
                converter.names = statuses.ToDictionary(st => st.Name);

            }
            else
            {
                converter.ids = null;
                converter.names = null;
            }
        }

        private Dictionary<int, StatusDTO> ids;
        private Dictionary<string, StatusDTO> names;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int id &&
                ids != null &&
                ids.TryGetValue(id, out StatusDTO status))
            {
                return status.Name;
            }
            return DependencyProperty.UnsetValue;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string name &&
                names != null &&
                names.TryGetValue(name, out StatusDTO status))
            {
                return status.Id;
            }

            return DependencyProperty.UnsetValue;
        }

        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }
    }
}
