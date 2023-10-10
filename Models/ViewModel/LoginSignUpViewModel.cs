namespace PROG3050_HMJJ.Models.ViewModel
{
    public class LoginSignUpViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword {  get; set; }
        public bool IsActive { get; set; }
        public bool isRemember { get; set; }
    }
}
