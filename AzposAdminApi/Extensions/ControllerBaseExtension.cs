using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace AzposAdminApi.Extensions
{
    public static class ControllerBaseExtension
    {
        public static string GetActionName(this ControllerBase controllerBase)
        {
            ControllerContext controllerContext = controllerBase.ControllerContext;

            return $"{controllerContext.ActionDescriptor.ActionName}_{controllerContext.ActionDescriptor.ControllerName}";
        }

        /// <summary>
        /// eg. GetById_MetodePembayaran to Metode Pembayaran
        /// </summary>
        /// <param name="controllerBase"></param>
        /// <returns></returns>
        public static string GetFormattedControllerName(this ControllerBase controllerBase)
        {
            ControllerContext controllerContext = controllerBase.ControllerContext;

            string controllerName = controllerContext.ActionDescriptor.ControllerName;

            var charName = controllerName.ToCharArray();
            string formattedName = "";

            formattedName += charName[0];

            foreach (char a in charName.Skip(1))
            {
                if (char.IsUpper(a))
                {
                    formattedName += " ";
                }

                formattedName += a;
            }


            return formattedName;
        }

        public static object GetActionParameter(this ControllerBase controllerBase)
        {
            ControllerContext controllerContext = controllerBase.ControllerContext;

            return controllerContext.ActionDescriptor.Parameters;
        }


        public static string GetUserId(this ControllerBase controllerBase)
        {
            ControllerContext controllerContext = controllerBase.ControllerContext;
            string userId = "";

            if (controllerBase != null && controllerBase.Request.HttpContext != null)
            {
                string? userIdFromRequest = controllerBase.Request.HttpContext.User.Claims.SingleOrDefault(a => a.Type == ClaimTypes.Name)?.Value;

                userId = userIdFromRequest ?? "NoLoginUser";
            }

            return userId;
        }

        public static string GetTenantId(this ControllerBase controllerBase)
        {
            ControllerContext controllerContext = controllerBase.ControllerContext;
            string tenantId = "";

            if (controllerBase != null && controllerBase.Request.HttpContext != null)
            {
                string? tenantIdFromRequest = controllerBase.Request.HttpContext.User.Claims.SingleOrDefault(a => a.Type == ClaimTypes.Sid)?.Value;

                tenantId = tenantIdFromRequest ?? "NoTenantLoginUser";
            }

            return tenantId;
        }

        public static string GetTenantUniqNumber(this ControllerBase controllerBase)
        {
            ControllerContext controllerContext = controllerBase.ControllerContext;
            string tenantId = "";

            if (controllerBase != null && controllerBase.Request.HttpContext != null)
            {
                string? tenantIdFromRequest = controllerBase.Request.HttpContext.User.Claims
                .SingleOrDefault(a => a.Type == ClaimTypes.GroupSid)?.Value;

                tenantId = tenantIdFromRequest ?? "NoTenantLoginUser";
            }

            return tenantId;
        }
    }
}