<%@ Page Title="Пользователи" Language="C#" MasterPageFile="~/Site.master"
    CodeBehind="Users.aspx.cs" Inherits="WebApplication.Users" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <h1>Пользователи</h1>
    <nav>
		<ul>
			<li><a href="~/Default.aspx" runat="server">&larr;</a></li>
            <li><a href="~/New.aspx" runat="server">новый пост</a></li>
            <li>пользователи</li>
            <li><a href="~/Tags.aspx" runat="server">теги</a></li>
		</ul>
	</nav>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <form method="POST">
   
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
        
        <table>
			<tr>
				<td>
					<label for="login">Логин:</label>
					<input type="text" name="login" id="Text1" value="<%=(edit.ContainsKey("login")) ? edit["login"] : "" %>" required>
				</td>
				<td>
					<label for="password">Пароль:</label>
					<input type="text" name="password" id="Text2" value="<%=(edit.ContainsKey("password")) ? edit["password"] : "" %>" required>
				</td>
			</tr>
		</table>
        <br>
		<input type="submit" name="save" value="Сохранить">

    <% }
       else if (del)
       {
           if (errors.Count > 0)
           {
                foreach (var error in errors)
                {
                %>
                    <p><mark><%=error%></mark></p>
                <%
                }
           } %>

        <label for="password">Введите пароль пользователя:</label>
		<input type="text" name="password" required>

		<input type="submit" name="del" value="Удалить">
    <% } %>

	</form>

    <% if (users.Count > 0)
       { %>
    <table>
    <%  foreach (var user in users)
        {
        %>
        <tr>
				<td><%=user["name"]%></td>
				<td><a href="/Users.aspx?edit=<%=user["id"] %>">редактировать</a></td>
				<td><a href="/Users.aspx?del=<%=user["id"] %>"">удалить</a></td>
			</tr>
        <%
        }
        %>
    </table>
    <% }
       else
       { %>
    <p><em>Увы, но пользователей нет.</em></p>
    <% } %>

    <p><a href="/Users.aspx?add">Добавить</a></p>
</asp:Content>
