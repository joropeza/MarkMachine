<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<img height="480" width="500" src="<%= Url.Action("Image", new { MarketId=Model}) %>" alt="image" />

