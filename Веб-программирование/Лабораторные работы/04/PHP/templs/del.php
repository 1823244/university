<!DOCTYPE html>
<html lang="ru">
<head>
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>Удаление «<?=$post['title']?>»</title>
	<link rel="stylesheet" href="awsm.min.css">
</head>
<body>
	<header>
		<h1>Удаление поста</h1>
		<nav>
			<ul>
				<li><a href="<?=$config['index']?>post?id=<?=$_GET['id']?>">←</a></li>
				<li><a href="<?=$config['index']?>users">пользователи</a></li>
				<li><a href="<?=$config['index']?>tags">теги</a></li>
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
	<footer>
		<p>Игорь Адаменко</p>
	</footer>
</body>
</html>