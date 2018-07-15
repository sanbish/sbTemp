using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MyInventory.Models;
using System.Security.Principal;
using System.Web;

namespace MyInventory.Helper
{
    public static class IIdentityExtensions
    {
        public static string GetUserFriendlyName(this IIdentity identity)
        {
            ApplicationUser user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (user != null)
            {
                return user.FriendlyName;
            }
            return string.Empty;
        }
        public static string GetUserJobTitle(this IIdentity identity)
        {
            ApplicationUser user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (user != null)
            {
                return user.JobTitle;
            }
            return string.Empty;
        }
        public static string GetUserPhoto(this IIdentity identity)
        {
            ApplicationUser user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (user != null)
            {
                return user.Photo;
            }
            return string.Empty;
        }
    }
}