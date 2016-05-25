namespace MathTicTac.ServiceModels
{
	public class AccountServiceModel
	{
		public string Username { get; private set; }
		public int Id { get; set; }
		public int Lose { get; set; }
		public int Won { get; set; }
		public int Draw { get; set; }

		public AccountServiceModel(string username)
		{
			this.Username = username;
		}
	}
}