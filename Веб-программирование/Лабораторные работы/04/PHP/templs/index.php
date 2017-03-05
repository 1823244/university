<!DOCTYPE html>
<html lang="ru">
<head>
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>Блог</title>
	<link rel="stylesheet" href="awsm.min.css">
</head>
<body>
	<header>
		<h1>Блог</h1>
		<nav>
			<ul>
				<li><a href="<?=$config['index']?>new">новый пост</a></li>
				<li><a href="<?=$config['index']?>users">пользователи</a></li>
				<li><a href="<?=$config['index']?>tags">теги</a></li>
			</ul>
		</nav>
	</header>
	<main>

		<? if ($posts): ?>
			
			<? foreach ($posts as $post): ?>
			
			<section>
				<h2><a href="<?=$post['link']?>"><?=$post['title']?></a></h2>
				<aside>
					<?=$post['author']?>
					<br>
					<?=$post['date']?>
				</aside>
				<p><?=$post['excerpt']?></p>
			</section>

			<? endforeach ?>

		<? else: ?>

			<p><em>Увы, но постов пока нет.</em></p>

		<? endif ?>

	</main>
	<footer>
		<p>Игорь Адаменко</p>
	</footer>
</body>
</html>