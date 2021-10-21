using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Common
{
    public static partial class ExtensionMethods
    {
        /// <summary>Создаёт клон экземпляра класса <typeparamref name="T"/>.</summary>
        /// <typeparam name="T">Тип экземплярв.</typeparam>
        /// <param name="obj">Исходный экземпляр.</param>
        /// <returns>Возвращает новый экземпляр типа <typeparamref name="T"/>
        /// являющийся копией исходного экземляра.</returns>
        /// <remarks>Если класс <typeparamref name="T"/> реализует интерфейс <see cref="ICloneable"/>,
        /// то копия создаётся методом <see cref="ICloneable.Clone"/>.
        /// Иначе - методом 
        /// <a href="https://docs.microsoft.com/ru-ru/dotnet/api/system.object.memberwiseclone?view=net-5.0">
        /// Object.MemberwiseClone()</a>.</remarks>
        public static T Clone<T>(this T obj)
        {
            if (obj is ICloneable cln && cln.Clone() is T t)
                return t;

            return (T)memberwiseClone(obj);
        }

        private static readonly Func<object, object> memberwiseClone
            = (Func<object, object>)(typeof(object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance))
            .CreateDelegate(typeof(Func<object, object>));

        /// <summary>Создаёт клон экземпляра класса <typeparamref name="T"/>.
        /// <typeparamref name="T"/> - <see cref="DependencyObject"/> или производный от него.</summary>
        /// <typeparam name="T">Тип экземплярв.</typeparam>
        /// <param name="dObj">Исходный экземпляр.</param>
        /// <param name="newObject">Делегат функции создающей новый экземпляр объекта из заданного,
        /// без копирования значений <see cref="DependencyProperty"/>.</param>
        /// <returns>Возвращает новый экземпляр типа <typeparamref name="T"/>
        /// являющийся копией исходного экземляра.</returns>
        /// <remarks>Создаётся копия методом <see cref="Clone{T}(T)"/>.<br/>
        /// После этого в клон копируются привязки или значения из всех
        /// <see cref="DependencyProperty"/> заданных в исходном экземпляре.</remarks>
        public static T CloneDO<T>(this T dObj, Func<T, T> newObject)
            where T : DependencyObject
        {
            T clone = newObject(dObj);
            if (clone == null)
            {
                throw new Exception("Не удаётся создать новый экземпляр этого типа.");
            }

            dObj.CopyDpTo(clone);

            return clone;
        }

        /// <summary>Копирует значения и привязки всех <see cref="DependencyProperty"/>
        /// из объекта источника в целевой объект.</summary>
        /// <typeparam name="T">Тип объектов.</typeparam>
        /// <param name="source">Объект источник.</param>
        /// <param name="target">Целевой объект.</param>
        public static void CopyDpTo<T>(this T source, T target)
            where T : DependencyObject
        {

            List<DependencyProperty> properties = new List<DependencyProperty>();


            {
                LocalValueEnumerator propEnumerator = source.GetLocalValueEnumerator();
                while (propEnumerator.MoveNext())
                {
                    if (!propEnumerator.Current.Property.ReadOnly)
                    {
                        properties.Add(propEnumerator.Current.Property);
                    }
                }
            }
            {
                LocalValueEnumerator propEnumerator = target.GetLocalValueEnumerator();
                while (propEnumerator.MoveNext())
                {
                    if (!propEnumerator.Current.Property.ReadOnly && !properties.Contains(propEnumerator.Current.Property))
                    {
                        target.ClearValue(propEnumerator.Current.Property);
                    }
                }
            }
            foreach (var property in properties)
            {

                var binding = BindingOperations.GetBindingBase(source, property);
                if (binding == null)
                {
                    target.SetValue(property, source.GetValue(property));
                }
                else
                {
                    BindingOperations.SetBinding(target, property, binding);
                }
            }
        }

        ///<inheritdoc cref="CloneDO{T}(T, Func{T, T})"/>
        public static T CloneDO<T>(this T dObj)
            where T : DependencyObject
        {
            Func<T> constructor = GetConstructor<T>(dObj.GetType());
            T clone = constructor();
            if (clone == null)
            {
                throw new Exception("Не удаётся создать новый экземпляр этого типа.");
            }

            dObj.CopyDpTo(clone);

            return clone;
        }


        private static readonly Dictionary<Type, Func<object>> constructors = new Dictionary<Type, Func<object>>();
        private static readonly Type[] emptyTypes = new Type[0];
        private static Func<T> GetConstructor<T>(Type type)
            where T : DependencyObject
        {
            if (!constructors.TryGetValue(type, out var func))
            {
                var constructor = type.GetConstructor
                    (
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly,
                        null, emptyTypes, null
                    );
                if (constructor == null)
                {
                    func = new Func<T>(() => null);
                }
                else
                {
                    DynamicMethod dynamic = new DynamicMethod(string.Empty,
                                type,
                                Type.EmptyTypes,
                                type);
                    ILGenerator il = dynamic.GetILGenerator();

                    il.DeclareLocal(type);
                    il.Emit(OpCodes.Newobj, constructor);
                    il.Emit(OpCodes.Stloc_0);
                    il.Emit(OpCodes.Ldloc_0);
                    il.Emit(OpCodes.Ret);

                    func = (Func<T>)dynamic.CreateDelegate(typeof(Func<T>));
                }
                constructors.Add(type, func);
            }
            return (Func<T>)func;
        }

        /// <summary>Обход логического дерева "в глубину".</summary>
        /// <param name="main">Элемент являющийся вершиной дерева.</param>
        /// <returns>Последовательность всех элементов дерева, включая вершину.</returns>
        public static IEnumerable<DependencyObject> GetAllLogicalChildrenDeep(this DependencyObject main)
        {
            Stack<DependencyObject> stack = new Stack<DependencyObject>();
            stack.Push(main);
            while (stack.Count > 0)
            {
                DependencyObject current = stack.Pop();
                foreach (DependencyObject item
                    in LogicalTreeHelper.GetChildren(current).OfType<DependencyObject>())
                {
                    stack.Push(item);
                }
                yield return current;
            }
        }

        /// <summary>Обход визуального дерева "в глубину".</summary>
        /// <param name="main">Элемент являющийся вершиной дерева.</param>
        /// <returns>Последовательность всех элементов дерева, включая вершину.</returns>
        public static IEnumerable<DependencyObject> GetAllVisualChildrenDeep(this DependencyObject main)
        {
            Stack<DependencyObject> stack = new Stack<DependencyObject>();
            stack.Push(main);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                var count = VisualTreeHelper.GetChildrenCount(current);
                for (int i = 0; i < count; i++)
                {
                    stack.Push(VisualTreeHelper.GetChild(current, i));
                }
                yield return current;
            }
        }
        /// <summary>Обход логического дерева "в ширину".</summary>
        /// <param name="main">Элемент являющийся вершиной дерева.</param>
        /// <returns>Последовательность всех элементов дерева, включая вершину.</returns>
        public static IEnumerable<DependencyObject> GetAllLogicalChildrenBfs(this DependencyObject main)
        {
            Queue<DependencyObject> queue = new Queue<DependencyObject>();
            queue.Enqueue(main);
            while (queue.Count > 0)
            {
                DependencyObject current = queue.Dequeue();
                foreach (DependencyObject item
                    in LogicalTreeHelper.GetChildren(current).OfType<DependencyObject>())
                {
                    queue.Enqueue(item);
                }
                yield return current;
            }
        }

        /// <summary>Обход визуального дерева "в ширину".</summary>
        /// <param name="main">Элемент являющийся вершиной дерева.</param>
        /// <returns>Последовательность всех элементов дерева, включая вершину.</returns>
        public static IEnumerable<DependencyObject> GetAllVisualChildrenBfs(this DependencyObject main)
        {
            Queue<DependencyObject> queue = new Queue<DependencyObject>();
            queue.Enqueue(main);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var count = VisualTreeHelper.GetChildrenCount(current);
                for (int i = 0; i < count; i++)
                {
                    queue.Enqueue(VisualTreeHelper.GetChild(current, i));
                }
                yield return current;
            }
        }
    }
}
