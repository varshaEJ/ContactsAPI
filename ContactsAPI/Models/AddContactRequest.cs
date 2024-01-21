namespace ContactsAPI.Models
{
    public class AddContactRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Adderess { get; set; }
    }
}
