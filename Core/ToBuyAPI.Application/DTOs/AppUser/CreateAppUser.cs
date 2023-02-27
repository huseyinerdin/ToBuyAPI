namespace ToBuyAPI.Application.DTOs.AppUser
{
	public class CreateAppUser
	{
		public string FullName { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }
		public string Country { get; set; }
	}
}
