	<nav>
		<ul>
			<li><a href="<?=URL?>tag">&larr;</a></li>
			<li><a href="<?=URL?>post/add">новый пост</a></li>
			<li><a href="<?=URL?>user">пользователи</a></li>
			<li>теги</li>
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

		<label for="tag">Тег:</label>
		<input type="text" name="tag" id="tag" value="<?=$tag['name']?>" required>

		<input type="submit" name="save" value="Сохранить">
	</form>
</main>