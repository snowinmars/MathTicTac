using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.ViewModels
{
	public class TextBox : Button
	{
		public TextBox(Vector2 position, int width, int height, string buttonText = "") : base(position, width, height, buttonText)
		{
		}


	}
}
