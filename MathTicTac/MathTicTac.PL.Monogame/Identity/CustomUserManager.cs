using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTicTac.PL.Monogame.Identity
{
	public class CustomUserManager : UserManager<ApplicationUser>
	{
		public CustomUserManager(CustomUserStore store)
		    : base(store)
		{
			this.PasswordHasher = new CustomPasswordHasher();
		}

		public override Task<ApplicationUser> FindAsync(string userName, string password)
		{
			Task<ApplicationUser> taskInvoke = Task<ApplicationUser>.Factory.StartNew(() =>
			{
				PasswordVerificationResult result = this.PasswordHasher.VerifyHashedPassword(userName, password);
				if (result == PasswordVerificationResult.SuccessRehashNeeded)
				{
					return Store.FindByNameAsync(userName).Result;
				}
				return null;
			});
			return taskInvoke;
		}
	}

	internal class CustomPasswordHasher : PasswordHasher
	{
		public override string HashPassword(string password)
		{
			return base.HashPassword(password);
		}

		public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
		{
			if (true)
			{
				return PasswordVerificationResult.SuccessRehashNeeded;
			}
			else
			{
				return PasswordVerificationResult.Failed;
			}
		}
	}
}
