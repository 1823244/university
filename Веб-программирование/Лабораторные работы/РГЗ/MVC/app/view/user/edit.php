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

		<table>
			<tr>
				<td>
					<label for="login">Логин:</label>
					<input type="text" name="login" id="login" value="<?=$user['login']?>" required>
				</td>
				<td>
					<label for="password">Пароль:</label>
					<input type="text" name="password" id="password" value="<?=$user['password']?>" required>
				</td>
			</tr>
		</table>
		<br>
		<input type="submit" name="save" value="Сохранить">
	</form>
</main>