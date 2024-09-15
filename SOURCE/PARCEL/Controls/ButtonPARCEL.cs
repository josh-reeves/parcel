namespace PARCEL.Controls;

public class ButtonPARCEL : ControlPARCEL
{
	public ButtonPARCEL()
	{
		Content = new VerticalStackLayout
		{
			Children = 
			{
				new Label 
				{ 
					HorizontalOptions = LayoutOptions.Center, 
					VerticalOptions = LayoutOptions.Center, 
					Text = "Welcome to .NET MAUI!"
				}

			}

		};

	}

}