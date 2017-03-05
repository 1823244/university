<%@ Page Title="Блог" Language="C#" MasterPageFile="~/Site.master"
    CodeBehind="Default.aspx.cs" Inherits="WebApplication._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <h1>Блог</h1>
	<nav>
		<ul>
			<li><a href="~/New.aspx" runat="server">новый пост</a></li>
            <li><a href="~/Users.aspx" runat="server">пользователи</a></li>
            <li><a href="~/Tags.aspx" runat="server">теги</a></li>
		</ul>
	</nav>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <% if (posts.Count > 0)
       { %>
           <% foreach (var post in posts)
           { %>
            <section>
                <h2><a href="<%=post["link"]%>"><%=post["title"]%></a></h2>
                <aside>
					<%=post["author"]%>
					<br>
                    <%=post["date"]%>
				</aside>
				<p><%=post["excerpt"]%></p>
            </section>
        <% } %>
    <% }
       else
       { %>
    <p><em>Увы, но постов пока нет.</em></p>
    <% } %>
</asp:Content>
