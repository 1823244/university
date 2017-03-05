		<nav>
			<ul>
				<li><a href="<?=URL?>">←</a></li>
				<li><a href="<?=URL?>user">пользователи</a></li>
				<li><a href="<?=URL?>tag">теги</a></li>
			</ul>
		</nav>
	</header>
	<main>
		<form method="POST">

			<? if (!empty($errors)): ?>
				<? foreach ($errors as $error): ?>
				<p><mark><?=$error?></mark></p>
				<? endforeach; ?>
			<? endif ?>

			<label for="title">Заголовок:</label>
			<input type="text" name="title" id="title" required>
			<label for="post">Текст:</label>
			<textarea name="post" id="post" rows="10" required></textarea>
			<label for="tags">Теги:</label>
			<input type="text" name="tags" id="tags">
			<label for="login">Опубликовать от имени:</label>
			<table>
				<tr>
					<td>
						<label for="login">Логин:</label>
						<input type="text" name="login" id="login" required>
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
	</main>
	<footer>
		<p>Игорь Адаменко</p>
	</footer>
</body>
</html>