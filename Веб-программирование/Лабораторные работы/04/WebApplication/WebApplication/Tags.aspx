<%@ Page Title="Теги" Language="C#" MasterPageFile="~/Site.master"
    CodeBehind="Tags.aspx.cs" Inherits="WebApplication.Tags" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <h1>Теги</h1>
    <nav>
		<ul>
			<li><a href="~/Default.aspx" runat="server">&larr;</a></li>
            <li><a href="~/New.aspx" runat="server">новый пост</a></li>
            <li><a href="~/Users.aspx" runat="server">пользователи</a></li>
            <li>теги</li>
		</ul>
	</nav>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
       
    <% if (edit != null)
        {
            if (errors.Count > 0)
            {
                foreach (var error in errors)
                {
                %>
                    <p><mark><%=error %></mark></p>
                <%
                }
            }
            %>

    <form method="POST">
        <label for="tag">Тег:</label>
	    <input type="text" name="tag" id="tag" value="<%=(edit.ContainsKey("tag")) ? edit["tag"] : "" %>" required>

		<input type="submit" name="save" value="Сохранить">
    </form>

    <% } %>

    <% if (tags.Count > 0)
       { %>
    <table>
    <%  foreach (var tag in tags)
        {
        %>
        <tr>
				<td><%=tag["name"]%></td>
				<td><a href="/Tags.aspx?edit=<%=tag["id"] %>">редактировать</a></td>
				<td><a href="/Tags.aspx?del=<%=tag["id"] %>"">удалить</a></td>
			</tr>
        <%
        }
        %>
    </table>
    <% }
       else
       { %>
    <p><em>Увы, но тегов нет.</em></p>
    <% } %>

    <p><a href="/Tags.aspx?add">Добавить</a></p>
</asp:Content>
