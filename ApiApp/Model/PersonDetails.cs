using Internship.Model;

namespace ApiApp.Model
{
    public class PersonDetails
    {
        public int Id { get; set; } 
        public DateTime BirthDay { get; set; }
        public string PersonCity { get; set; }
        public int PersonId { get; set; } 
        public virtual Person Person { get; set; }
    }
}
