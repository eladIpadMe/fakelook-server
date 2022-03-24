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
        //date filter - only post that published in dates range will return true
        public bool checkDate(DateTime postDate, Nullable<DateTime> startingDate, Nullable<DateTime> endingDate)
        {
            bool date;
            if (!startingDate.HasValue && !endingDate.HasValue)
            {
                return true;
            }
            else if (!startingDate.HasValue && endingDate.HasValue)
            {
                date = (DateTime.Compare(postDate, (DateTime)endingDate) <= 0);
            }
            else if (startingDate.HasValue && !endingDate.HasValue)
            {
                date = (DateTime.Compare(postDate, (DateTime)startingDate) >= 0);
            }
            else
            {
                date = ((DateTime.Compare(postDate, (DateTime)startingDate) >= 0) &&
                    (DateTime.Compare(postDate, (DateTime)endingDate) <= 0));
            }
            return date;
        }
        //publisher filter - only post whose publisher is the wanted publisher will return true
        public bool checkPublishers(string postUserName, ICollection<string> publishers)
        {
            if (publishers == null || publishers.Count == 0)
            {
                return true;
            }
            return publishers.Contains(postUserName);
        }
        //hashtag filter - only post that has one or more hashtags as input will return true
        public bool checkHashTaggs(ICollection<Tag> postTags, ICollection<string> filtertaggs)
        {
            if (filtertaggs == null || filtertaggs.Count == 0)
            {
                return true;
            }
            if (postTags == null || postTags.Count == 0)
            {
                return false;
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
        //user tagged filter - only post that has one or more user tagged as input will return true
        public bool checkUsersTagged(ICollection<UserTaggedPost> taggedPost, ICollection<string> taggedFilter)
        {

            if(taggedPost == null || taggedPost.Count == 0)
            {
                return false;
            }
            if (taggedFilter == null || taggedFilter.Count == 0)
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
