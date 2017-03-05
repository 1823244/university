<%@ Page Title="Новый пост в Блог" Language="C#" MasterPageFile="~/Site.master"
    CodeBehind="New.aspx.cs" Inherits="WebApplication.New" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <h1>Новый пост в Блог</h1>
    <nav>
		<ul>
			<li><a href="~/Default.aspx" runat="server">&larr;</a></li>
            <li><a href="~/Users.aspx" runat="server">пользователи</a></li>
            <li><a href="~/Tags.aspx" runat="server">теги</a></li>
		</ul>
	</nav>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <form method="POST">
		<%  if (errors.Count > 0)
            {
                foreach (var error in errors)
                {
                %>
                   <p><mark><%=error %></mark></p>
                <%
                }
            }
         %>

		<label for="title">Заголовок:</label>
		<input type="text" name="title" id="title" value="<%=post["title"] %>" required>
		<label for="post">Текст:</label>
		<textarea name="post" id="post" rows="10" required><%=post["post"]%></textarea>
		<label for="tags">Теги:</label>
		<input type="text" name="tags" id="tags" value="<%=post["tags"] %>">
		<label for="login">Опубликовать от имени:</label>
		<table>
			<tr>
				<td>
					<label for="login">Логин:</label>
					<input type="text" name="login" id="login" value="<%=post["login"] %>" required>
				</td>
				<td>
					<label for="password">Пароль:</label>
					<input type="password" name="password" id="password" required>
				</td>
			</tr>
		</table>
		<br>
		<input type="submit" name="publish" value="Опубликовать">
	</form>
</asp:Content>
