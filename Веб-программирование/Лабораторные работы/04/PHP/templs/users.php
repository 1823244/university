<!DOCTYPE html>
<html lang="ru">
<head>
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>Пользователи</title>
	<link rel="stylesheet" href="awsm.min.css">
</head>
<body>
	<header>
		<h1>Пользователи</h1>
		<nav>
			<ul>
				<li><a href="<?=$config['index']?>">&larr;</a></li>
				<li><a href="<?=$config['index']?>new">новый пост</a></li>
				<li>пользователи</li>
				<li><a href="<?=$config['index']?>tags">теги</a></li>
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

			<table>
				<tr>
					<td>
						<label for="login">Логин:</label>
						<input type="text" name="login" id="login" value="<?=$edit['login']?>" required>
					</td>
					<td>
						<label for="password">Пароль:</label>
						<input type="text" name="password" id="password" value="<?=$edit['password']?>" required>
					</td>
				</tr>
			</table>
			<br>
			<input type="submit" name="save" value="Сохранить">
		
		<? elseif ($del): ?>

			<? if (!empty($errors)): ?>
				<? foreach ($errors as $error): ?>
				<p><mark><?=$error?></mark></p>
				<? endforeach; ?>
			<? endif ?>
			
			<label for="password">Введите пароль пользователя:</label>
			<input type="text" name="password" id="password" required>

			<input type="submit" name="del" value="Удалить">
		
		<? endif ?>
		</form>

		<? if ($users): ?>
		<table>
			<? foreach ($users as $uid => $name): ?>
			
			<tr>
				<td><?=$name?></td>
				<td><a href="<?=$config['index']?>users?edit=<?=$uid?>">редактировать</a></td>
				<td><a href="<?=$config['index']?>users?del=<?=$uid?>">удалить</a></td>
			</tr>

			<? endforeach ?>
		</table>

		<? else: ?>

			<p><em>Увы, но пользователей нет.</em></p>

		<? endif ?>

		<p><a href="<?=$config['index']?>users?add">Добавить</a></p>

	</main>
	<footer>
		<p>Игорь Адаменко</p>
	</footer>
</body>
</html>