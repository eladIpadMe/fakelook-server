using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fakeLook_models.Models
{
    public class PostFilter
    {
        public ICollection<string> Publishers { get; set; }
        public DateTime? startingDate { get; set; }
        public DateTime? endingDate { get; set; }
        public ICollection<string> hashtags { get; set; }
        public ICollection<string> taggedUsers { get; set; }
        public bool checkDate(DateTime postDate, Nullable<DateTime> startingDate, Nullable<DateTime> endingDate)
        {
            bool date;
            if (!startingDate.HasValue && !endingDate.HasValue)
            {
                return true;
            }
            else if (!startingDate.HasValue && endingDate.HasValue)
            {
                date = (postDate <= endingDate);
            }
            else if (startingDate.HasValue && !endingDate.HasValue)
            {
                date = (postDate <= startingDate);
            }
            else
            {
                date = (postDate >= startingDate && postDate <= endingDate);
            }
            return date;
        }

        public bool checkPublishers(string postUserName, ICollection<string> publishers)
        {
            if (publishers == null)
            {
                return true;
            }
            return publishers.Contains(postUserName);
        }
        public bool checkHashTaggs(ICollection<Tag> postTags, ICollection<string> filtertaggs)
        {
            if (filtertaggs == null)
            {
                return true;
            }
            foreach (var filtertag in filtertaggs)
            {
                foreach (var postTag in postTags)
                {
                    if (filtertag.Equals(postTag.Content))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool checkUsersTagged(ICollection<UserTaggedPost> taggedPost, ICollection<string> taggedFilter)
        {
            if (taggedFilter.Count == 0)
            {
                return true;
            }
            if(taggedFilter.Count > 0 && taggedPost == null)
            {
                return false;
            }
            foreach (var postTag in taggedPost)
            {
                if (taggedFilter.Contains(postTag.User.UserName))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
