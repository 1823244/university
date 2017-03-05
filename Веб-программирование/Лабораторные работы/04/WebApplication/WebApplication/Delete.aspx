<%@ Page Title="Удаление поста" Language="C#" MasterPageFile="~/Site.master"
    CodeBehind="Delete.aspx.cs" Inherits="WebApplication.Delete" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <h1>Удаление поста</h1>
    <nav>
		<ul>
			<li><a href="<%=post["back-link"] %>">&larr;</a></li>
            <li><a href="~/Users.aspx" runat="server">пользователи</a></li>
            <li><a href="~/Tags.aspx" runat="server">теги</a></li>
		</ul>
	</nav>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <article>
        <p>Для удаления поста «<%=post["title"] %>» введите авторизационные данные:</p>
        <form method="POST" runat="server">

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

			<table>
				<tr>
					<td>
						<label for="login">Логин:</label>
						<input type="text" name="login" required>
					</td>
					<td>
						<label for="password">Пароль:</label>
						<input type="password" name="password" required>
					</td>
				</tr>
			</table>
			<br>
			<input type="submit" name="del" value="Удалить">
		</form>
    </article>
</asp:Content>
