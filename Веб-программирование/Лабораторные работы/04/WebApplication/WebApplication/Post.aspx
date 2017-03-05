<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master"
    CodeBehind="Post.aspx.cs" Inherits="WebApplication.Post" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeaderContent">
    <h1><%=post["title"] %></h1>
    <nav>
		<ul>
			<li><a href="~/Default.aspx" runat="server">&larr;</a></li>
            <li><a href="~/Users.aspx" runat="server">пользователи</a></li>
            <li><a href="~/Tags.aspx" runat="server">теги</a></li>
		</ul>
	</nav>
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <article>
        <%=post["text"] %>
		<br>
		<p><em><%=post["tags"] %></em>
		<br>
		<em><%=post["author"] %>, <%=post["date"] %> (<a href="<%=post["edit-link"] %>">редактировать</a>, <a href="<%=post["del-link"] %>">удалить</a>)</em></p>
	</article>

	<section>
		<h3>Комментарии <%=(comments.Count > 0) ? '(' + comments.Count.ToString() + ')' : "" %></h3>
		<br>
		
       <% foreach (var comment in comments)
           { %>
            <article>
                <p><em><%=comment["name"] %> (<%=comment["email"] %>), <%=comment["date"] %></em></p>
                <%=comment["text"] %>
            </article>
        <% } %>
			
		<form method="POST" id="comment" runat="server">
			<fieldset>
				<legend>Оставьте свой комментарий</legend>

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

				<input type="text" name="name" placeholder="Имя" value="<%=Request.Form["name"] %>" required>
				<input type="email" name="email" placeholder="Электронная почта" value="<%=Request.Form["email"] %>" required>
				<textarea name="comment" id="comment" rows="4" placeholder="Комментарий" required><%=Request.Form["text"] %></textarea>
				<input type="submit" name="submit" id="comment-submit" value="Отправить">
				<script>
					window.onkeyup = function (e) {
					    if ((e.keyCode == 10 || e.keyCode == 13) && e.ctrlKey) {
					        document.getElementById('comment-submit').click();
					    }
					}
                    <% if (errors.Count > 0) { %>
                    window.onload = function() {
			            var comments = document.getElementById('comment');

			            window.scrollTo(0, comments.offsetTop);
		            }
                    <% } %>
				</script>
			</fieldset>
		</form>
	</section>
</asp:Content>
