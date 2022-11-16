﻿using K9.Base.WebApplication.Constants.Html;
using K9.Base.WebApplication.Enums;
using K9.Base.WebApplication.Helpers;
using K9.DataAccessLayer.Models;
using K9.Globalisation;
using K9.WebApplication.Controllers;
using K9.WebApplication.Models;
using K9.WebApplication.Options;
using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using WebMatrix.WebData;

namespace K9.WebApplication.Helpers
{
    public static partial class HtmlHelpers
    {
        public static string ActionWithBookmark(this UrlHelper helper, string actionName, string controllerName, string bookmark)
        {
            return helper.RequestContext.RouteData.Values["action"].ToString().ToLower() == actionName.ToLower() &&
                   helper.RequestContext.RouteData.Values["controller"].ToString().ToLower() == controllerName.ToLower()
                ? $"#{bookmark}"
                : $"{helper.Action(actionName, controllerName)}#{bookmark}";
        }

        public static MvcHtmlString CollapsiblePanel(this HtmlHelper html, CollapsiblePanelOptions options)
        {
            return html.Partial("Controls/_CollapsiblePanel", options);
        }

        public static MvcHtmlString CollapsiblePanel(this HtmlHelper html, string title, string body, bool expanded = false, string footer = "", string id = "", string imageSrc = "", EPanelImageSize imageSize = EPanelImageSize.Default, EPanelImageLayout imageLayout = EPanelImageLayout.Cover)
        {
            return html.Partial("Controls/_CollapsiblePanel", new CollapsiblePanelOptions
            {
                Id = id,
                Title = title,
                Body = body,
                Expaded = expanded,
                Footer = footer,
                ImageSrc = string.IsNullOrEmpty(imageSrc) ? string.Empty : new UrlHelper(html.ViewContext.RequestContext).Content(imageSrc),
                ImageLayout = imageLayout,
                ImageSize = imageSize
            });
        }

        public static MvcHtmlString Panel(this HtmlHelper html, PanelOptions options)
        {
            return html.Partial("Controls/_Panel", options);
        }

        public static MvcHtmlString Panel(this HtmlHelper html, string title, string body, string id = "", string imageSrc = "", EPanelImageSize imageSize = EPanelImageSize.Default, EPanelImageLayout imageLayout = EPanelImageLayout.Cover)
        {
            return html.Partial("Controls/_Panel", new PanelOptions
            {
                Id = id,
                Title = title,
                Body = body,
                ImageSrc = string.IsNullOrEmpty(imageSrc) ? string.Empty : new UrlHelper(html.ViewContext.RequestContext).Content(imageSrc),
                ImageSize = imageSize,
                ImageLayout = imageLayout
            });
        }

        public static MvcHtmlString ImagePanel(this HtmlHelper html, PanelOptions options)
        {
            return html.Partial("Controls/_ImagePanel", options);
        }

        public static MvcHtmlString ImagePanel(this HtmlHelper html, string title, string imageSrc = "", EPanelImageSize imageSize = EPanelImageSize.Default, EPanelImageLayout imageLayout = EPanelImageLayout.Cover)
        {
            return html.Partial("Controls/_ImagePanel", new PanelOptions
            {
                Title = title,
                ImageSrc = string.IsNullOrEmpty(imageSrc) ? string.Empty : new UrlHelper(html.ViewContext.RequestContext).Content(imageSrc),
                ImageSize = imageSize,
                ImageLayout = imageLayout
            });
        }
        
        public static IDisposable PaidContent(this HtmlHelper html, MembershipOption.ESubscriptionType subscriptionType = MembershipOption.ESubscriptionType.MonthlyStandard,  bool silent = false)
        {
            var baseController = html.ViewContext.Controller as BasePureController;
            var activeUserMembership = baseController?.GetActiveUserMembership();
            return html.PaidContent<MembershipModel>(null,
                () => activeUserMembership?.MembershipOption?.SubscriptionType >= subscriptionType, silent);
        }

        public static IDisposable PaidContent<T>(this HtmlHelper html, T model, Func<bool> condition = null, bool silent = false)
        {
            var baseController = html.ViewContext.Controller as BasePureController;
            var activeUserMembership = baseController?.GetActiveUserMembership();
            var showContent = activeUserMembership != null && (condition?.Invoke() ?? true);
            var isMembership = typeof(T) == typeof(MembershipModel);
            var retrieveLast = isMembership ? "m" : "none";

            var div = new TagBuilder(Tags.Div);
            if (!(WebSecurity.IsAuthenticated && showContent))
            {
                div.MergeAttribute(Attributes.Style, "display: none !important;");

                if (model != null)
                {
                    if (isMembership)
                    {
                        // SessionHelper.SetLastSomething(model as MembershipModel);
                    }
                }
            }

            if (!silent)
            {
                var centerDiv = new TagBuilder(Tags.Div);
                centerDiv.MergeAttribute(Attributes.Class,
                    "upgrade-container center-block");
                html.ViewContext.Writer.WriteLine(centerDiv.ToString(TagRenderMode.StartTag));
                if (WebSecurity.IsAuthenticated)
                {
                    if (!showContent)
                    {
                        html.ViewContext.Writer.WriteLine(
                            $"<h4><strong>{Dictionary.UpgradeMembershipFullText}</strong></h4>");
                        html.ViewContext.Writer.WriteLine(html.BootstrapActionLinkButton(
                            Dictionary.UpgradeMembershipText,
                            "Index", "Membership", null, "fa-level-up-alt", EButtonClass.Large));
                        html.ViewContext.Writer.WriteLine(
                            $"<div class=\"inline\" data-toggle=\"tooltip\" title=\"{Dictionary.CreditsDescriptionUI}\">{html.BootstrapActionLinkButton(Dictionary.PurchaseCredits, "PurchaseCreditsStart", "Membership", null, "fa-money-bill-alt", EButtonClass.Large, EButtonClass.Info)}</div>");
                    }
                }
                else
                {
                    html.ViewContext.Writer.WriteLine(html.BootstrapActionLinkButton(
                        Dictionary.LogIntoYourAccountToView, "Login", "Account", new { retrieveLast }, "fa-sign-in",
                        EButtonClass.Large));
                }

                html.ViewContext.Writer.WriteLine(centerDiv.ToString(TagRenderMode.EndTag));
            }

            html.ViewContext.Writer.WriteLine(div.ToString(TagRenderMode.StartTag));
            return new TagCloser(html, Tags.Div);
        }
    }
}