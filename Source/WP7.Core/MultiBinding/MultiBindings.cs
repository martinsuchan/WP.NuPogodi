using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Win8.Core.MultiBinding
{
    /// <summary>
    /// Manages the construction of multiple MultiBinding instances
    /// </summary>
    [ContentProperty("Bindings")]
    public class MultiBindings : FrameworkElement
    {
        private FrameworkElement _targetElement;

        /// <summary>
        /// Gets / sets the collection of MultiBindings
        /// </summary>
        public ObservableCollection<MultiBinding> Bindings { get; set; }

        public MultiBindings()
        {
            Bindings = new ObservableCollection<MultiBinding>();
        }

        /// <summary>
        /// Sets the DataContext of each of the MultiBinding instances
        /// </summary>
        public void SetDataContext(object dataContext)
        {
            foreach (MultiBinding relay in Bindings)
            {
                relay.DataContext = dataContext;
            }
        }

        /// <summary>
        /// Initialises each of the MultiBindings, and binds their ConvertedValue
        /// to the given target property.
        /// </summary>
        public void Initialize(FrameworkElement targetElement)
        {
            _targetElement = targetElement;
            const BindingFlags DpFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;

            foreach (MultiBinding relay in Bindings)
            {
                relay.Initialise(targetElement);

                // find the target dependency property
                Type targetType = null;
                string targetProperty = null;

                // assume it is an attached property if the dot syntax is used.
                if (relay.TargetProperty.Contains("."))
                {
                    // split to find the type and property name
                    string[] parts = relay.TargetProperty.Split('.');
                    targetType = Type.GetType("System.Windows.Controls." + parts[0] +
                        ", System.Windows, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e");
                    targetProperty = parts[1];
                }
                else
                {
                    targetType = targetElement.GetType();
                    targetProperty = relay.TargetProperty;
                }

                FieldInfo[] sourceFields = targetType.GetFields(DpFlags);
                FieldInfo targetDependencyPropertyField =
                    sourceFields.First(i => i.Name == targetProperty + "Property");
                DependencyProperty targetDependencyProperty =
                    targetDependencyPropertyField.GetValue(null) as DependencyProperty;

                // bind the ConvertedValue of our MultiBinding instance to the target property
                // of our targetElement
                Binding binding = new Binding("ConvertedValue")
                {
                    Source = relay,
                    Mode = relay.Mode
                };
                targetElement.SetBinding(targetDependencyProperty, binding);
            }
        }
    }
}
