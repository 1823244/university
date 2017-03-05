	<nav>
		<ul>
			<li><a href="<?=URL?>post/add">новый пост</a></li>
			<li><a href="<?=URL?>user">пользователи</a></li>
			<li><a href="<?=URL?>tag">теги</a></li>
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