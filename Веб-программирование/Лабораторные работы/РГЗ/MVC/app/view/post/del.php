	<nav>
		<ul>
			<li><a href="<?=URL?>post/<?=$post['id']?>">←</a></li>
			<li><a href="<?=URL?>user">пользователи</a></li>
			<li><a href="<?=URL?>tag">теги</a></li>
		</ul>
	</nav>
</header>
<main>

	<article>
		<p>Для удаления поста «<?=$post['title']?>» введите авторизационные данные:</p>
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
						<input type="text" name="login" id="login" required>
					</td>
					<td>
						<label for="password">Пароль:</label>
						<input type="password" name="password" id="password" required>
					</td>
				</tr>
			</table>
			<br>
			<input type="submit" name="del" value="Удалить">
		</form>
	</article>

</main>
