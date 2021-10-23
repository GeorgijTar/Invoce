using InvoceModelLib.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace InvoceViewLib
{
    public class StatusIdToNameConverter : IValueConverter
    {
        private static IReadOnlyCollection<StatusDTO> statuses;

        public static IReadOnlyCollection<StatusDTO> Statuses
        {
            get => statuses; set
            {
                statuses = value;
                if (value == null)
                {
                    ids = null;
                    names = null;
                }
                else
                {
                    ids = value.ToDictionary(st => st.Id);
                    names = value.ToDictionary(st => st.Name);

                }
            }
        }
        private static Dictionary<int, StatusDTO> ids { get; set; }
        private static Dictionary<string, StatusDTO> names { get; set; }
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
    }
}
