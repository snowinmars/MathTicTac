using MathTicTac.Enums;

namespace MathTicTac.PL.Soap.BindingLib.ServiceModels
{
	public class TypedResponce<T>
	{
		public T Value { get; set; } 

        public ResponseResult Responce { get; set; }
	}
}