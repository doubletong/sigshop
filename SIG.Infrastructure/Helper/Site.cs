using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Principal;



namespace SIG.Infrastructure.Helper
{
    public sealed class Site
    {

        //public static readonly SIGSection Settings = ((SIGSection)WebConfigurationManager.GetSection("sigSetting"));

     
        /// <summary>
        /// 当前用户
        /// </summary>
        public static IPrincipal CurrentUser => HttpHelper.HttpContext.User;

        /// <summary>
        /// 当前用户名
        /// </summary>
        public static string CurrentUserName
        {
            get
            {
                var userName = string.Empty;
                if (HttpHelper.HttpContext != null && CurrentUser.Identity.IsAuthenticated)
                {
                    userName = CurrentUser.Identity.Name;
                }
                return userName;
            }
        }
        
        public static string CurrentUserIP => HttpHelper.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();


        public static List<int> PageSizes()
        {
            return new List<int> { 5, 10, 15, 20, 30, 50, 100 };
        }


        //public static string CurrentArea()
        //{
        //    string currentActionName = ViewContext.RouteData.GetRequiredString("action");
        //    var routeDataTokens = HttpContext.Current.Request.RequestContext.RouteData.DataTokens;
        //    if (routeDataTokens.ContainsKey("area"))
        //        return (string)routeDataTokens["area"];

        //    return string.Empty;
        //}


        //public static string CurrentController()
        //{
        //    var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
        //    if (routeValues.ContainsKey("controller"))
        //        return (string)routeValues["controller"];

        //    return string.Empty;
        //}

        //public static string CurrentAction()
        //{
        //    var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
        //    if (routeValues.ContainsKey("action"))
        //        return (string)routeValues["action"];

        //    return string.Empty;
        //}

        /// <summary>
        /// 随机返回一项从数组中
        /// </summary>
        /// <param name="ar"></param>
        /// <returns></returns>
        public static string GetRandomItemFromArray(string[] ar)
        {
            Random rnd = new Random();
            var result = ar[rnd.Next(0, ar.Length)];
            return result;
        }

        /// <summary>
        /// 随机排序List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ListT"></param>
        /// <returns></returns>
        public static List<T> RandomSortList<T>(List<T> ListT)
        {
            Random random = new Random();
            List<T> newList = new List<T>();
            foreach (T item in ListT)
            {
                newList.Insert(random.Next(newList.Count + 1), item);
            }
            return newList;
        }

        /// <summary>
        /// 中文字符错位的问题
        /// </summary>
        /// <param name="toSub"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubString(string toSub, int startIndex, int length)
        {

            byte[] subbyte = System.Text.Encoding.Default.GetBytes(toSub);
            string Sub = System.Text.Encoding.Default.GetString(subbyte, startIndex, length);
            return Sub;

        }

        public static string SubString(string toSub, int startIndex)
        {

            byte[] subbyte = System.Text.Encoding.Default.GetBytes(toSub);
            string Sub = System.Text.Encoding.Default.GetString(subbyte);
            return Sub.Substring(startIndex);

        }
    }
}
