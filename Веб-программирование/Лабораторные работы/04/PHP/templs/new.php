<!DOCTYPE html>
<html lang="ru">
<head>
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title><?=($post['title']) ? 'Редактирование поста в Блоге' : 'Новый пост в Блог'?></title>
	<link rel="stylesheet" href="awsm.min.css">
</head>
<body>
	<header>
		<h1><?=($post['title']) ? 'Редактирование' : 'Новый пост'?></h1>
		<nav>
			<ul>
				<? if ($post['title']): ?>
				<li><a href="<?=$config['index']?>post?id=<?=$_GET['id']?>">←</a></li>
				<? else: ?>
				<li><a href="<?=$config['index']?>">←</a></li>
				<? endif ?>
				
				<li><a href="<?=$config['index']?>users">пользователи</a></li>
				<li><a href="<?=$config['index']?>tags">теги</a></li>
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
			<input type="text" name="title" id="title" value="<?=$post['title']?>" required>
			<label for="post">Текст:</label>
			<textarea name="post" id="post" rows="10" required><?=$post['text']?></textarea>
			<label for="tags">Теги:</label>
			<input type="text" name="tags" id="tags" value="<?=$post['tags']?>">
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