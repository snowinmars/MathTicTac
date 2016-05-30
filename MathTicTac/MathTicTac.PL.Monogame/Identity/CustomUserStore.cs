using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.PL.Monogame.Identity
{
	/// GET	   - READ
	/// POST   - ADD
	/// PUT    - UPDATE
	/// DELETE - DELETE
	public class CustomUserStore : IUserStore<ApplicationUser>
	{
		public void Dispose()
		{
		}

		public Task CreateAsync(ApplicationUser user)
		{
			return MyHttpClient.PostAsync<ApplicationUser>(user, "Account");
		}

		public Task UpdateAsync(ApplicationUser user)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(ApplicationUser user)
		{
			return MyHttpClient.DeleteAsync<ApplicationUser>($"Account?{user.Token}");
		}

		public Task<ApplicationUser> FindByIdAsync(string userId)
		{
			throw new NotImplementedException();
		}

		public Task<ApplicationUser> FindByNameAsync(string userName)
		{
			throw new NotImplementedException();
		}
	}
}
