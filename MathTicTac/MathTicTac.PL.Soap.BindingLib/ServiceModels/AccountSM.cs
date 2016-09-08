namespace MathTicTac.PL.Soap.BindingLib.ServiceModels
{
	public class AccountSM
	{
		public string Username { get; set; }
		public int Id { get; set; }
		public int Lose { get; set; }
		public int Won { get; set; }
		public int Draw { get; set; }
	}
}