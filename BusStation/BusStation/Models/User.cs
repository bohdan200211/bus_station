using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStation.DataAccess;

namespace BusStation.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreateAt { get; set; }

        private Profile profile = null;
        public Document Document { get; set; }
        public User(int id, string username, string password, string firstName, string lastName, DateTime createAt)
        {
            this.Id = id;
            this.Username = username.Trim();
            this.Password = password.Trim();
            this.profile = createProfile(firstName == null? "":firstName.Trim(), lastName == null ? "" : lastName.Trim());
            this.CreateAt = createAt;
            this.Document = getDocument(id);
        }
        public User(int id, string username, string password, DateTime createAt)
        {
            this.Id = id;
            this.Username = username.Trim();
            this.Password = password.Trim();
            this.profile = null;
            this.CreateAt = createAt;
            this.Document = getDocument(id);
        }
        private Profile createProfile(string firstname, string lastname)
        {
            ProfileAccess db = new ProfileAccess();
            Profile profile = new Profile { Id = this.Id, LastName = lastname, FirstName = firstname };

            return profile;
        }
        private Document getDocument(int id)
        {
            try
            {
                DocumentAccess db = new DocumentAccess();
                Document document = db.GetOne(id);
                return document;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Profile getProfile()
        {
            return this.profile;
        }
        public void setProfile(Profile profile)
        {
            this.profile = profile;

            ProfileAccess db = new ProfileAccess();
            db.Update(profile);
        }

        public override string ToString()
        {
            return this.Id + " " + 
                this.Username + " " + 
                (this.profile != null ? this.profile.ToString() + " " : "") 
                + this.CreateAt.ToString();
        }
    }
}
 