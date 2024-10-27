using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARCEL.Helpers;

public static class ViewBuilder<T>
{
    #region Constructors

    static ViewBuilder() { }

    #endregion

    #region Delegates
    public delegate void AdditionalSetupDelegate();

    #endregion

    #region Methods
    public static T BuildView(T view, BindingPair[]? bindings = null, AdditionalSetupDelegate? additionalSetup = null)
    {
        try
        {
            if (view is View && bindings != null)
                foreach (BindingPair binding in bindings)
                    (view as View).SetBinding(binding.Property, binding.Path);

            additionalSetup?.Invoke();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

        }
        
        return view;

    }

    #endregion

    #region Classes
    public class BindingPair
    {
        public BindingPair(BindableProperty property, string path)
        {
            Property = property;
            Path = path;


        }

        public BindableProperty Property { get; private set; }
        public string Path { get; private set; }

    }

    #endregion

}
