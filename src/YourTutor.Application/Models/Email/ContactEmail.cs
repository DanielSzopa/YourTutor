namespace YourTutor.Application.Models.Email;

public sealed class ContactEmail : EmailBase.EmailBase
{
    public ContactEmail(string body, string to, string from) : base("Offer contact", body, "Contact", to, from)
    {

    }
}


