using System;
using System.Linq;

namespace NRpc.Utils
{
    /// <summary>
    /// 类名：WebUtil.cs
    /// 类功能描述：
    /// 创建标识：yjq 2018/5/13 19:59:27
    /// </summary>
    internal static class WebUtil
    {
        #region 获取当前网络IP

        /// <summary>
        /// 获取当前网络IP
        /// </summary>
        /// <returns>当前网络IP</returns>
        internal static string GetRealIP()
        {
            if (!IsHaveHttpContext()) return string.Empty;
            string result = string.Empty;
            if (System.Web.HttpContext.Current.Request.Headers != null)
            {
                var forwardedHttpHeader = "X-FORWARDED-FOR";
                string xff = System.Web.HttpContext.Current.Request.Headers.AllKeys
                    .Where(x => forwardedHttpHeader.Equals(x, StringComparison.OrdinalIgnoreCase))
                    .Select(k => System.Web.HttpContext.Current.Request.Headers[k])
                    .FirstOrDefault();
                if (!string.IsNullOrEmpty(xff))
                {
                    string lastIp = xff.Split(new char[] { ',' }).FirstOrDefault();
                    result = lastIp;
                }
            }
            if (string.IsNullOrEmpty(result) && System.Web.HttpContext.Current.Request.UserHostAddress != null)
            {
                result = System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            if (result == "::1")
                result = "127.0.0.1";
            if (!string.IsNullOrEmpty(result))
            {
                int index = result.IndexOf(":", StringComparison.InvariantCultureIgnoreCase);
                if (index > 0)
                    result = result.Substring(0, index);
            }
            else result = "0.0.0.0";
            return result;
        }

        #endregion 获取当前网络IP

        #region 获取客户端浏览器的原始用户代理信息

        /// <summary>
        /// 获取客户端浏览器的原始用户代理信息
        /// </summary>
        /// <returns></returns>
        public static string GetUserAgent()
        {
            if (IsHaveHttpContext())
                return System.Web.HttpContext.Current.Request.UserAgent;
            return string.Empty;
        }

        #endregion 获取客户端浏览器的原始用户代理信息

        #region 判断是否有网络请求上下文

        /// <summary>
        /// 判断是否有网络请求上下文
        /// </summary>
        /// <returns></returns>
        public static bool IsHaveHttpContext()
        {
            if (System.Web.HttpContext.Current != null)
            {
                try
                {
                    return System.Web.HttpContext.Current.Request != null;
                }
                catch
                {
                }
            }
            return false;
        }

        #endregion 判断是否有网络请求上下文

        #region 获取请求地址

        /// <summary>
        /// 获取请求地址
        /// </summary>
        /// <returns>请求地址</returns>
        public static string GetHttpRequestUrl()
        {
            if (IsHaveHttpContext())
                return System.Web.HttpContext.Current.Request.Url.ToString();
            return string.Empty;
        }

        /// <summary>
        /// 获取绝对Uri
        /// </summary>
        /// <returns>请求的绝对Uri</returns>
        public static string GetAbsoluteUrl()
        {
            if (IsHaveHttpContext())
                return System.Web.HttpContext.Current.Request.Url.Authority + System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            return string.Empty;
        }

        /// <summary>
        /// 获取请求源的URL
        /// </summary>
        /// <returns>请求的绝对Uri</returns>
        public static string GetReferrerUrl()
        {
            if (IsHaveHttpContext())
                return System.Web.HttpContext.Current.Request.UrlReferrer.AbsolutePath;
            return string.Empty;
        }

        #endregion 获取请求地址

        #region 获取请求类型

        /// <summary>
        /// 获取请求类型
        /// </summary>
        /// <returns>请求类型</returns>
        public static string GetHttpMethod()
        {
            if (IsHaveHttpContext())
                return System.Web.HttpContext.Current.Request.RequestType;
            return string.Empty;
        }

        #endregion 获取请求类型

        #region 获取请求内容

        /// <summary>
        /// 获取请求内容
        /// </summary>
        /// <returns>请求内容</returns>
        public static string GetRequestData()
        {
            if (IsHaveHttpContext() && System.Web.HttpContext.Current.Request.Form != null)
                return System.Web.HttpContext.Current.Request.Form.ToString();
            return string.Empty;
        }

        #endregion 获取请求内容
    }
}