namespace PARCEL.Helpers;

/// <summary>
/// Factory class for building Views with optional data-bindings and additional setup.
/// This allows a view to be created and configured with a single method call instead of requiring multiple lines of code to set the view's bindings.
/// </summary>
/// <typeparam name="T"></typeparam>
public static class ViewBuilder<T> where T: new()
{
    #region Constructors

    static ViewBuilder() { }

    #endregion

    #region Delegates
    public delegate void AdditionalSetupDelegate();

    #endregion

    #region Methods
    public static T BuildView(T? view, BindingPair[]? bindings = null, AdditionalSetupDelegate? additionalSetup = null) 
    {
        view ??= new T();

        try
        {
            if (view is View && bindings != null)
            {
                foreach (BindingPair binding in bindings)
                    (view as View).SetBinding(binding.Property, binding.Path);
                
            }

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
