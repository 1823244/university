	<nav>
		<ul>
			<li><a href="<?=URL?>user">&larr;</a></li>
			<li><a href="<?=URL?>post/add">новый пост</a></li>
			<li>пользователи</li>
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
		
		<label for="password">Введите пароль пользователя:</label>
		<input type="text" name="password" id="password" required>

		<input type="submit" name="del" value="Удалить">
	</form>
</main>
