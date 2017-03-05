<!DOCTYPE html>
<html lang="ru">
<head>
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title><?=$post['title']?></title>
	<link rel="stylesheet" href="awsm.min.css">
</head>
<body>
	<header>
		<h1><?=$post['title']?></h1>
		<nav>
			<ul>
				<li><a href="<?=$config['index']?>">←</a></li>
				<li><a href="<?=$config['index']?>users">пользователи</a></li>
				<li><a href="<?=$config['index']?>tags">теги</a></li>
			</ul>
		</nav>
	</header>
	<main>

		<article>
			<?=$post['text']?>
			<br>
			<p>
				<em>
					<?=$post['tags']?>
					<br>
					<?=$post['author']?>, <?=$post['date']?>
					(<a href="<?=$config['index']?>edit?id=<?=$_GET['id']?>">редактировать</a>,
					<a href="<?=$config['index']?>del?id=<?=$_GET['id']?>">удалить</a>)
				</em>
			</p>
		</article>

		<section>
			<h3>Комментарии <?=(count($comments)) ? '(' . count($comments) . ')' : ''?></h3>
			<br>
			
			<? foreach ($comments as $comment): ?>
			<article>
				<p><em><?=$comment['name']?> (<?=$comment['email']?>), <?=$comment['date']?></em></p>
				<?=$comment['text']?>
			</article>
			<? endforeach ?>
			
			<form method="POST" id="comment">
				<fieldset>
					<legend>Оставьте свой комментарий</legend>

					<? if (!empty($errors)): ?>
						<? foreach ($errors as $error): ?>
						<p><mark><?=$error?></mark></p>
						<? endforeach; ?>
					<? endif ?>

					<input type="text" name="name" placeholder="Имя" required>
					<input type="email" name="email" placeholder="Электронная почта" required>
					<textarea name="comment" id="comment" rows="4" placeholder="Комментарий" required></textarea>
					<input type="submit" name="submit" id="comment-submit" value="Отправить">
					<script>
						window.onkeyup = function(e) {
							if ((e.keyCode == 10 || e.keyCode == 13) && e.ctrlKey) {
								document.getElementById('comment-submit').click();
							}
						}

						<? if (!empty($errors)): ?>
						window.onload = function() {
							var comments = document.getElementById('comment');

							window.scrollTo(0, comments.offsetTop);
						}
						<? endif ?>
					</script>
				</fieldset>
			</form>
		</section>

	</main>
	<footer>
		<p>Игорь Адаменко</p>
	</footer>
</body>
</html>