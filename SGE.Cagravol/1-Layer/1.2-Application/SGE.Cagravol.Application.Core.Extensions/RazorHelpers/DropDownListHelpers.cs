using SGE.Cagravol.Application.Core.Extensions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SGE.Cagravol.Application.Core.Extensions.RazorHelpers
{
    public static class DropDownListHelpers
    {
        public static MvcHtmlString DropDownListReadOnlyFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, bool isReadOnly, object htmlAttributes = null)
        {
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (isReadOnly)
            {
                AttributeMaker.AddReadOnly(attrs);
                AttributeMaker.AddDisabled(attrs);
            }

            return helper.DropDownListFor(expression, selectList, attrs);
        }

        public static MvcHtmlString InventoryDropDownList<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<CustomSelectListItem> selectList, object htmlAttributes, bool isReadonly = false, string defaultText = "")
        {
            TagBuilder tag;

            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var result = new StringBuilder();
            var selectTag = new TagBuilder("select");
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var classNames = (attrs.Any(s => s.Key == "class")) ? attrs.Single(s => s.Key == "class").Value.ToString() : string.Empty;
            classNames = string.Join(" ", classNames.Split(' ').Except(new List<string>() { "inventories" }));

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var valAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(fieldName, metadata);

            //Build Select
            selectTag.Attributes.Add("name", fieldName);
            selectTag.Attributes.Add("id", fieldName);
            selectTag.Attributes.Add("class", (string.IsNullOrEmpty(classNames) ? "inventories" : "inventories " + classNames));
            selectTag.Attributes.Add("data-url-load-inventory", urlHelper.Action("Index", "Inventories", new { area = "" }));
            selectTag.Attributes.Add("data-url-get-list-items", urlHelper.Action("GetListItems", "Inventories", new { area = "" }));

            if (isReadonly)
            {
                selectTag.Attributes.Add("readonly", "readonly");
            }

            if (selectList.First().AditionalTags != null && selectList.First().AditionalTags.Count != 0)
            {
                selectTag.Attributes.Add(selectList.First().AditionalTags.First().Name, selectList.First().AditionalTags.First().Value);
            }


            foreach (var attr in attrs)
            {
                if (!selectTag.Attributes.Any(s => s.Key == attr.Key.ToString()))
                {
                    selectTag.Attributes.Add(attr.Key.ToString(), attr.Value.ToString());
                }
            }

            foreach (var attr in valAttributes)
            {
                selectTag.Attributes.Add(attr.Key.ToString(), attr.Value.ToString());
            }

            //Build Select Options
            if (defaultText != null)
            {
                tag = new TagBuilder("option");
                tag.Attributes.Add("value", "");
                tag.InnerHtml = defaultText;
                result.Append(tag);
            }

            foreach (var item in selectList)
            {
                item.AditionalTags = item.AditionalTags != null ? item.AditionalTags : new List<Tag>();
                tag = new TagBuilder("option");

                foreach (var aditionalTag in item.AditionalTags)
                {
                    tag.Attributes.Add(aditionalTag.Name, aditionalTag.Value);
                }
                if (item.Selected)
                {
                    tag.Attributes.Add("selected", "selected");

                }
                tag.Attributes.Add("value", item.Value);
                tag.SetInnerText(item.Text);

                result.Append(tag.ToString());
            }

            if (selectList.First().AditionalTags != null && selectList.First().AditionalTags.Count != 0)
            {
                TagBuilder inventoryTag = new TagBuilder("option");
                inventoryTag.Attributes.Add("value", "0");
                inventoryTag.Attributes.Add(selectList.First().AditionalTags.First().Name, selectList.First().AditionalTags.First().Value);
                inventoryTag.SetInnerText("-Manage Inventories-");
                result.Append(inventoryTag);
            }

            selectTag.InnerHtml = result.ToString();

            return MvcHtmlString.Create(selectTag.ToString());
        }

        public static MvcHtmlString InventoryDropDownListReadOnlyFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<CustomSelectListItem> selectList, bool isReadOnly, object htmlAttributes)
        {
            return htmlHelper.InventoryDropDownList(expression, selectList, htmlAttributes, isReadOnly);
        }

       
    }
}
