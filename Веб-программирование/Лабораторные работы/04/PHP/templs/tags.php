<!DOCTYPE html>
<html lang="ru">
<head>
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>Теги</title>
	<link rel="stylesheet" href="awsm.min.css">
</head>
<body>
	<header>
		<h1>Теги</h1>
		<nav>
			<ul>
				<li><a href="<?=$config['index']?>">&larr;</a></li>
				<li><a href="<?=$config['index']?>new">новый пост</a></li>
				<li><a href="<?=$config['index']?>users">пользователи</a></li>
				<li>теги</li>
			</ul>
		</nav>
	</header>
	<main>
		<form method="POST">
		<? if ($edit): ?>

			<? if (!empty($errors)): ?>
				<? foreach ($errors as $error): ?>
				<p><mark><?=$error?></mark></p>
				<? endforeach; ?>
			<? endif ?>

			<label for="tag">Тег:</label>
			<input type="text" name="tag" id="tag" value="<?=$edit['tag']?>" required>

			<input type="submit" name="save" value="Сохранить">
		
		<? endif ?>
		</form>

		<? if ($tags): ?>
		<table>
			<? foreach ($tags as $tid => $name): ?>
			
			<tr>
				<td><?=$name?></td>
				<td><a href="<?=$config['index']?>tags?edit=<?=$tid?>">редактировать</a></td>
				<td><a href="<?=$config['index']?>tags?del=<?=$tid?>">удалить</a></td>
			</tr>

			<? endforeach ?>
		</table>

		<? else: ?>

			<p><em>Увы, но тегов нет.</em></p>

		<? endif ?>

		<p><a href="<?=$config['index']?>tags?add">Добавить</a></p>

	</main>
	<footer>
		<p>Игорь Адаменко</p>
	</footer>
</body>
</html>